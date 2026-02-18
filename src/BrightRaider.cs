using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using System.Security.Principal;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Threading;
using Microsoft.Win32;

class BrightRaider : Form
{
    // === GDI / Display API ===
    [DllImport("gdi32.dll")]
    static extern bool SetDeviceGammaRamp(IntPtr hDC, ref RAMP lpRamp);

    [DllImport("user32.dll")]
    static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    [DllImport("user32.dll")]
    static extern bool SetForegroundWindow(IntPtr hWnd);

    [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
    static extern IntPtr CreateDC(string lpszDriver, string lpszDevice, string lpszOutput, IntPtr lpInitData);

    [DllImport("gdi32.dll")]
    static extern bool DeleteDC(IntPtr hdc);

    [DllImport("gdi32.dll")]
    static extern int BitBlt(IntPtr hdcDest, int xDest, int yDest, int wDest, int hDest,
        IntPtr hdcSrc, int xSrc, int ySrc, int rop);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    static extern bool EnumDisplayDevices(string lpDevice, uint iDevNum, ref DISPLAY_DEVICE lpDisplayDevice, uint dwFlags);

    [DllImport("user32.dll")]
    static extern bool DestroyIcon(IntPtr handle);

    [DllImport("user32.dll")]
    static extern IntPtr GetDC(IntPtr hWnd);

    [DllImport("user32.dll")]
    static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

    [DllImport("user32.dll")]
    static extern int GetSystemMetrics(int nIndex);

    // === Low-level keyboard hook ===
    [DllImport("user32.dll")]
    static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

    [DllImport("user32.dll")]
    static extern bool UnhookWindowsHookEx(IntPtr hhk);

    [DllImport("user32.dll")]
    static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

    [DllImport("kernel32.dll")]
    static extern IntPtr GetModuleHandle(string lpModuleName);

    [DllImport("kernel32.dll")]
    static extern IntPtr LoadLibrary(string lpFileName);

    [DllImport("kernel32.dll")]
    static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

    delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

    // === Audio: Per-Process Mute via Core Audio COM (managed interfaces) ===
    [DllImport("user32.dll")]
    static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll")]
    static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);

    // COM Interfaces for Windows Core Audio API
    [ComImport, Guid("BCDE0395-E52F-467C-8E3D-C4579291692E")]
    class MMDeviceEnumeratorCOM { }

    [Guid("A95664D2-9614-4F35-A746-DE8DB63617E6"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    interface IMMDeviceEnumerator
    {
        int EnumAudioEndpoints(int dataFlow, int stateMask, out IntPtr ppDevices);
        int GetDefaultAudioEndpoint(int dataFlow, int role, out IMMDevice ppEndpoint);
    }

    [Guid("D666063F-1587-4E43-81F1-B948E807363F"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    interface IMMDevice
    {
        int Activate([MarshalAs(UnmanagedType.LPStruct)] Guid iid, int dwClsCtx, IntPtr pActivationParams, [MarshalAs(UnmanagedType.IUnknown)] out object ppInterface);
    }

    [Guid("77AA99A0-1BD6-484F-8BC7-2C654C9A9B6F"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    interface IAudioSessionManager2
    {
        int GetAudioSessionControl(IntPtr AudioSessionGuid, int StreamFlags, out IntPtr SessionControl);
        int GetSimpleAudioVolume(IntPtr AudioSessionGuid, int StreamFlags, out IntPtr AudioVolume);
        int GetSessionEnumerator(out IAudioSessionEnumerator SessionEnum);
    }

    [Guid("E2F5BB11-0570-40CA-ACDD-3AA01277DEE8"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    interface IAudioSessionEnumerator
    {
        int GetCount(out int SessionCount);
        int GetSession(int SessionCount, [MarshalAs(UnmanagedType.IUnknown)] out object Session);
    }

    [Guid("bfb7ff88-7239-4fc9-8fa2-07c950be9c6d"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    interface IAudioSessionControl2
    {
        // IAudioSessionControl methods (9 total)
        int GetState(out int pRetVal);
        int GetDisplayName([MarshalAs(UnmanagedType.LPWStr)] out string pRetVal);
        int SetDisplayName([MarshalAs(UnmanagedType.LPWStr)] string Value, [MarshalAs(UnmanagedType.LPStruct)] Guid EventContext);
        int GetIconPath([MarshalAs(UnmanagedType.LPWStr)] out string pRetVal);
        int SetIconPath([MarshalAs(UnmanagedType.LPWStr)] string Value, [MarshalAs(UnmanagedType.LPStruct)] Guid EventContext);
        int GetGroupingParam(out Guid pRetVal);
        int SetGroupingParam([MarshalAs(UnmanagedType.LPStruct)] Guid Override, [MarshalAs(UnmanagedType.LPStruct)] Guid EventContext);
        int RegisterAudioSessionNotification(IntPtr NewNotifications);
        int UnregisterAudioSessionNotification(IntPtr NewNotifications);
        // IAudioSessionControl2 methods
        int GetSessionIdentifier([MarshalAs(UnmanagedType.LPWStr)] out string pRetVal);
        int GetSessionInstanceIdentifier([MarshalAs(UnmanagedType.LPWStr)] out string pRetVal);
        int GetProcessId(out uint pRetVal);
        int IsSystemSoundsSession();
        int SetDuckingPreference(bool optOut);
    }

    [Guid("87CE5498-68D6-44E5-9215-6DA47EF883D8"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    interface ISimpleAudioVolume
    {
        int SetMasterVolume(float fLevel, [MarshalAs(UnmanagedType.LPStruct)] Guid EventContext);
        int GetMasterVolume(out float pfLevel);
        int SetMute([MarshalAs(UnmanagedType.Bool)] bool bMute, [MarshalAs(UnmanagedType.LPStruct)] Guid EventContext);
        int GetMute([MarshalAs(UnmanagedType.Bool)] out bool pbMute);
    }

    static void SetProcessMute(int pid, bool mute)
    {
        try
        {
            IMMDeviceEnumerator deviceEnumerator = (IMMDeviceEnumerator)(new MMDeviceEnumeratorCOM());
            IMMDevice device;
            deviceEnumerator.GetDefaultAudioEndpoint(0 /*eRender*/, 1 /*eMultimedia*/, out device);
            if (device == null) return;

            object objMgr;
            device.Activate(new Guid("77AA99A0-1BD6-484F-8BC7-2C654C9A9B6F"), 23, IntPtr.Zero, out objMgr);
            IAudioSessionManager2 mgr = (IAudioSessionManager2)objMgr;
            if (mgr == null) return;

            IAudioSessionEnumerator sessions;
            mgr.GetSessionEnumerator(out sessions);
            if (sessions == null) return;

            int count;
            sessions.GetCount(out count);

            for (int i = 0; i < count; i++)
            {
                object objSession;
                sessions.GetSession(i, out objSession);
                if (objSession == null) continue;

                IAudioSessionControl2 ctrl = objSession as IAudioSessionControl2;
                if (ctrl == null) continue;

                uint sessionPid;
                ctrl.GetProcessId(out sessionPid);

                if (sessionPid == (uint)pid)
                {
                    ISimpleAudioVolume vol = objSession as ISimpleAudioVolume;
                    if (vol != null)
                    {
                        vol.SetMute(mute, Guid.Empty);
                    }
                    break;
                }
            }
        }
        catch { }
    }

    // === NvAPI ===
    [DllImport("nvapi64.dll", EntryPoint = "nvapi_QueryInterface", CallingConvention = CallingConvention.Cdecl)]
    static extern IntPtr NvAPI64_QueryInterface(uint interfaceId);

    [DllImport("nvapi.dll", EntryPoint = "nvapi_QueryInterface", CallingConvention = CallingConvention.Cdecl)]
    static extern IntPtr NvAPI32_QueryInterface(uint interfaceId);

    const uint NVAPI_ID_INITIALIZE = 0x0150E828;
    const uint NVAPI_ID_ENUM_DISPLAY = 0x9ABDD40D;
    const uint NVAPI_ID_GET_DVC_INFO = 0x4085DE45;
    const uint NVAPI_ID_SET_DVC_LEVEL = 0x172409B4;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate int NvAPI_Initialize_t();
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate int NvAPI_EnumDisplay_t(int index, ref IntPtr handle);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate int NvAPI_SetDVCLevel_t(IntPtr handle, int outputId, int level);

    static NvAPI_Initialize_t nvInit;
    static NvAPI_EnumDisplay_t nvEnumDisplay;
    static NvAPI_SetDVCLevel_t nvSetDVC;
    static bool nvApiReady = false;

    // === AMD ADL ===
    const int ADL_OK = 0;
    const int ADL_DISPLAY_COLOR_SATURATION = 4;

    delegate IntPtr ADL_Main_Memory_Alloc_Delegate(int size);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate int ADL_Main_Control_Create_t(ADL_Main_Memory_Alloc_Delegate callback, int enumConnectedAdapters);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate int ADL_Main_Control_Destroy_t();
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate int ADL_Adapter_NumberOfAdapters_Get_t(ref int numAdapters);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate int ADL_Adapter_AdapterInfo_Get_t(IntPtr info, int inputSize);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate int ADL_Adapter_Active_Get_t(int adapterIndex, ref int status);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate int ADL_Display_DisplayInfo_Get_t(int adapterIndex, ref int numDisplays, out IntPtr displayInfoPtr, int forceDetect);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate int ADL_Display_Color_Set_t(int adapterIndex, int displayIndex, int colorType, int current);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate int ADL_Display_Color_Get_t(int adapterIndex, int displayIndex, int colorType,
        ref int lpCurrent, ref int lpDefault, ref int lpMin, ref int lpMax, ref int lpStep);

    static ADL_Main_Control_Create_t adlMainControlCreate;
    static ADL_Main_Control_Destroy_t adlMainControlDestroy;
    static ADL_Adapter_NumberOfAdapters_Get_t adlAdapterNumberGet;
    static ADL_Adapter_AdapterInfo_Get_t adlAdapterInfoGet;
    static ADL_Adapter_Active_Get_t adlAdapterActiveGet;
    static ADL_Display_DisplayInfo_Get_t adlDisplayInfoGet;
    static ADL_Display_Color_Set_t adlDisplayColorSet;
    static ADL_Display_Color_Get_t adlDisplayColorGet;
    static bool adlReady = false;

    struct ADLDisplayTarget { public int AdapterIndex; public int DisplayIndex; }
    static List<ADLDisplayTarget> adlDisplayTargets = new List<ADLDisplayTarget>();

    [StructLayout(LayoutKind.Sequential)]
    struct ADLAdapterInfo
    {
        public int Size; public int AdapterIndex;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)] public string UDID;
        public int BusNumber; public int DeviceNumber; public int FunctionNumber; public int VendorID;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)] public string AdapterName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)] public string DisplayName;
        public int Present; public int Exist;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)] public string DriverPath;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)] public string DriverPathExt;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)] public string PNPString;
        public int OSDisplayIndex;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct ADLDisplayID { public int DisplayLogicalIndex; public int DisplayPhysicalIndex; public int DisplayLogicalAdapterIndex; public int DisplayPhysicalAdapterIndex; }

    [StructLayout(LayoutKind.Sequential)]
    struct ADLDisplayInfo
    {
        public ADLDisplayID DisplayID; public int DisplayControllerIndex;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)] public string DisplayName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)] public string DisplayManufacturerName;
        public int DisplayType; public int DisplayOutputType; public int DisplayConnector; public int DisplayInfoMask; public int DisplayInfoValue;
    }

    // === Constants ===
    const int WH_KEYBOARD_LL = 13;
    const int WM_KEYDOWN = 0x0100;
    const int WM_SYSKEYDOWN = 0x0104;
    const int SRCCOPY = 0x00CC0020;
    const int SM_CXSCREEN = 0;
    const int SM_CYSCREEN = 1;

    const int VK_NUMPAD0 = 0x60;
    const int VK_NUMPAD1 = 0x61; const int VK_NUMPAD2 = 0x62; const int VK_NUMPAD3 = 0x63;
    const int VK_NUMPAD4 = 0x64; const int VK_NUMPAD5 = 0x65; const int VK_NUMPAD6 = 0x66;
    const int VK_NUMPAD7 = 0x67; const int VK_NUMPAD8 = 0x68; const int VK_NUMPAD9 = 0x69;
    const int VK_INSERT = 0x2D; const int VK_END = 0x23; const int VK_DOWN = 0x28;
    const int VK_NEXT = 0x22; const int VK_LEFT = 0x25; const int VK_CLEAR = 0x0C;
    const int VK_RIGHT = 0x27; const int VK_HOME = 0x24; const int VK_UP = 0x26;
    const int VK_PRIOR = 0x21;
    const uint LLKHF_EXTENDED = 0x01;

    [StructLayout(LayoutKind.Sequential)]
    struct KBDLLHOOKSTRUCT { public uint vkCode; public uint scanCode; public uint flags; public uint time; public IntPtr dwExtraInfo; }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    struct DISPLAY_DEVICE
    {
        public int cb;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)] public string DeviceName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)] public string DeviceString;
        public int StateFlags;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)] public string DeviceID;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)] public string DeviceKey;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct RAMP
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)] public ushort[] Red;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)] public ushort[] Green;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)] public ushort[] Blue;
    }

    struct DisplayInfo { public string DeviceName; public string FriendlyName; }

    // === Profile Data ===
    class ProfileData
    {
        public double Gamma = 1.0;
        public double Contrast = 1.0;
        public int Vibrance = 50;
        public string Name = "Normal";
        public double BrightnessMin = -1; // -1 = not set (auto-brightness won't use this profile)
        public double BrightnessMax = -1; // -1 = not set
        public ProfileData() { }
        public ProfileData(double g, double c, int v, string n, double bMin, double bMax)
        { Gamma = g; Contrast = c; Vibrance = v; Name = n; BrightnessMin = bMin; BrightnessMax = bMax; }
    }

    // === License ===
    const string RSA_PUBLIC_KEY = "PFJTQUtleVZhbHVlPjxNb2R1bHVzPnRyLzBjTnBwS3g3VkdZMTQzMCtnbHdVRm85d3B3bGZRbmxaN245dm9vY1NIaE1CSWt4bTVWUlRrUnlJSUpRYzY5K2YxQ3FqTmk4OVFNQTBvQVZRelUzRUMzNzVLZmVBTk5kVHFnMm13aTVrZkhQMkR2VkkvcytzZVRYOVRmd3Y4MngybUNYaHpqNWhsV292bnFxcTBjOUozK3JQN3pabjZVVGg2bk5qcEQ4NjRxbW9aYnhZUFVMSkJFQTh4ZktTMVVuckhnc0dHSnIxckdEN2VZQjNtZFB0aXJoYkhpWWhIemcvUjA0TXcvRE9jOVVjRVhleFZlNE1FUStRTjZlSmx3NjlPTk5jLzlsRE9JV00xWVdjYk1xbTViRi9WK2dXVlJrNlpFMjJaVEs1WW1mS0tNbTY5QVdWVndHWmFpS0pXQVBhNWlzK2FEOW5aa2hucW5Tc0ViUT09PC9Nb2R1bHVzPjxFeHBvbmVudD5BUUFCPC9FeHBvbmVudD48L1JTQUtleVZhbHVlPg==";

    // === State ===
    NotifyIcon trayIcon;
    int currentProfile = 1;
    bool exiting = false;
    bool isProLicensed = false;
    string licenseKey = "";
    string licenseEmail = "";

    string selectedDisplay = null;
    bool showNotifications = true;
    string language = "en";
    bool autoStart = false;
    bool autoBrightness = false;
    int autoBrightnessInterval = 1000; // ms, configurable (500-5000)
    bool gameMuted = false;
    List<DisplayInfo> activeDisplays;
    string configPath;
    ProfileData[] profiles;
    int profileCount = 3;

    static Bitmap baseIconBmp = null;
    IntPtr hookId = IntPtr.Zero;
    LowLevelKeyboardProc hookProc;
    System.Windows.Forms.Timer autoBrightnessTimer;
    Form brightnessOverlay;
    Label overlayLabel;
    Form measureFrameOverlay; // red rectangle showing the measurement area
    Form toastForm;
    Label toastLabel;
    System.Windows.Forms.Timer toastTimer;

    string L(string en, string de) { return language == "de" ? de : en; }

    void ShowToast(string text)
    {
        if (!showNotifications || exiting) return;

        if (toastForm == null || toastForm.IsDisposed)
        {
            toastForm = new Form();
            toastForm.FormBorderStyle = FormBorderStyle.None;
            toastForm.BackColor = Color.FromArgb(30, 30, 30);
            toastForm.Opacity = 0.9;
            toastForm.TopMost = true;
            toastForm.ShowInTaskbar = false;
            toastForm.StartPosition = FormStartPosition.Manual;

            toastLabel = new Label();
            toastLabel.ForeColor = Color.White;
            toastLabel.BackColor = Color.Transparent;
            toastLabel.Font = new Font("Segoe UI", 11f, FontStyle.Bold);
            toastLabel.TextAlign = ContentAlignment.MiddleCenter;
            toastLabel.Dock = DockStyle.Fill;
            toastLabel.Padding = new Padding(10, 5, 10, 5);
            toastForm.Controls.Add(toastLabel);

            // Make click-through
            toastForm.Shown += delegate {
                int exStyle = GetWindowLong(toastForm.Handle, GWL_EXSTYLE);
                SetWindowLong(toastForm.Handle, GWL_EXSTYLE, exStyle | WS_EX_LAYERED | WS_EX_TRANSPARENT);
            };
        }

        toastLabel.Text = text;

        // Size to text
        using (Graphics g = toastForm.CreateGraphics())
        {
            SizeF sz = g.MeasureString(text, toastLabel.Font);
            toastForm.Size = new Size((int)sz.Width + 30, (int)sz.Height + 16);
        }

        // Position: bottom-right above taskbar
        Rectangle workArea = Screen.PrimaryScreen.WorkingArea;
        toastForm.Location = new Point(workArea.Right - toastForm.Width - 10, workArea.Bottom - toastForm.Height - 10);

        toastForm.Show();

        // Auto-hide after 1.5 seconds
        if (toastTimer != null) { toastTimer.Stop(); toastTimer.Dispose(); }
        toastTimer = new System.Windows.Forms.Timer();
        toastTimer.Interval = 1500;
        toastTimer.Tick += delegate { toastTimer.Stop(); toastTimer.Dispose(); toastTimer = null; if (toastForm != null && !toastForm.IsDisposed) toastForm.Hide(); };
        toastTimer.Start();
    }

    // === License Validation ===
    static bool ValidateLicense(string email, string key)
    {
        // Key format: NNNNN-<base64signature>
        // NNNNN is the key number, signature signs "BRIGHTRAIDER-PRO:NNNNN"
        // Email is displayed but NOT validated (psychological lock)
        try
        {
            int dashPos = key.IndexOf('-');
            if (dashPos < 1) return false;
            string keyNumber = key.Substring(0, dashPos);
            string sigBase64 = key.Substring(dashPos + 1);

            string payload = "BRIGHTRAIDER-PRO:" + keyNumber;
            string xmlKey = Encoding.UTF8.GetString(Convert.FromBase64String(RSA_PUBLIC_KEY));
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.PersistKeyInCsp = false;
                rsa.FromXmlString(xmlKey);
                byte[] data = Encoding.UTF8.GetBytes(payload);
                byte[] sig = Convert.FromBase64String(sigBase64);
                return rsa.VerifyData(data, new SHA256CryptoServiceProvider(), sig);
            }
        }
        catch { return false; }
    }

    // AES encryption for files (.lic and .cfg)
    static string licFilePath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "BrightRaider.lic");

    static byte[] DeriveAesKey(string seed)
    {
        using (SHA256Managed sha = new SHA256Managed())
            return sha.ComputeHash(Encoding.UTF8.GetBytes(seed));
    }

    static byte[] AesEncrypt(byte[] data, string seed)
    {
        byte[] key = DeriveAesKey(seed);
        byte[] iv = new byte[16]; Array.Copy(key, 4, iv, 0, 16);
        using (System.Security.Cryptography.Aes aes = System.Security.Cryptography.Aes.Create())
        {
            aes.Key = key; aes.IV = iv; aes.Mode = CipherMode.CBC; aes.Padding = PaddingMode.PKCS7;
            using (ICryptoTransform enc = aes.CreateEncryptor())
                return enc.TransformFinalBlock(data, 0, data.Length);
        }
    }

    static byte[] AesDecrypt(byte[] cipher, string seed)
    {
        byte[] key = DeriveAesKey(seed);
        byte[] iv = new byte[16]; Array.Copy(key, 4, iv, 0, 16);
        using (System.Security.Cryptography.Aes aes = System.Security.Cryptography.Aes.Create())
        {
            aes.Key = key; aes.IV = iv; aes.Mode = CipherMode.CBC; aes.Padding = PaddingMode.PKCS7;
            using (ICryptoTransform dec = aes.CreateDecryptor())
                return dec.TransformFinalBlock(cipher, 0, cipher.Length);
        }
    }

    void SaveLicenseFile()
    {
        try
        {
            if (licenseEmail.Length == 0 || licenseKey.Length == 0)
            {
                if (File.Exists(licFilePath)) File.Delete(licFilePath);
                return;
            }
            string payload = licenseEmail + "\n" + licenseKey;
            File.WriteAllBytes(licFilePath, AesEncrypt(Encoding.UTF8.GetBytes(payload), "BrightRaider-v5-Pro-2025-LicFile"));
        }
        catch { }
    }

    void LoadLicenseFile()
    {
        try
        {
            if (!File.Exists(licFilePath)) return;
            byte[] cipher = File.ReadAllBytes(licFilePath);
            string payload = Encoding.UTF8.GetString(AesDecrypt(cipher, "BrightRaider-v5-Pro-2025-LicFile"));
            int nl = payload.IndexOf('\n');
            if (nl > 0)
            {
                licenseEmail = payload.Substring(0, nl);
                licenseKey = payload.Substring(nl + 1);
                if (licenseEmail.Length > 0 && licenseKey.Length > 0)
                    isProLicensed = ValidateLicense(licenseEmail, licenseKey);
            }
        }
        catch { }
    }

    void ShowProRequired()
    {
        MessageBox.Show(
            L("This feature requires BrightRaider Pro.\n\nGet your license at:\nhttps://LEMONSQUEEZY_LINK_HERE",
              "Diese Funktion erfordert BrightRaider Pro.\n\nLizenz erhalten:\nhttps://LEMONSQUEEZY_LINK_HERE"),
            "BrightRaider Pro",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    void PromptLicenseKey()
    {
        Form dialog = new Form();
        dialog.Text = "BrightRaider - " + L("Enter License", "Lizenz eingeben");
        dialog.Size = new Size(420, 220);
        dialog.FormBorderStyle = FormBorderStyle.FixedDialog;
        dialog.StartPosition = FormStartPosition.CenterScreen;
        dialog.MaximizeBox = false;
        dialog.MinimizeBox = false;

        Label lblEmail = new Label() { Text = "Email:", Location = new Point(15, 20), AutoSize = true };
        TextBox txtEmail = new TextBox() { Location = new Point(15, 40), Size = new Size(370, 22) };
        txtEmail.Text = licenseEmail;

        Label lblKey = new Label() { Text = L("License Key:", "Lizenzschlüssel:"), Location = new Point(15, 70), AutoSize = true };
        TextBox txtKey = new TextBox() { Location = new Point(15, 90), Size = new Size(370, 22) };
        txtKey.Text = licenseKey;

        Button btnOk = new Button() { Text = "OK", Location = new Point(220, 130), Size = new Size(80, 30), DialogResult = DialogResult.OK };
        Button btnCancel = new Button() { Text = L("Cancel", "Abbrechen"), Location = new Point(305, 130), Size = new Size(80, 30), DialogResult = DialogResult.Cancel };

        dialog.Controls.AddRange(new Control[] { lblEmail, txtEmail, lblKey, txtKey, btnOk, btnCancel });
        dialog.AcceptButton = btnOk;
        dialog.CancelButton = btnCancel;

        if (dialog.ShowDialog() == DialogResult.OK)
        {
            string email = txtEmail.Text.Trim();
            string key = txtKey.Text.Trim();
            if (ValidateLicense(email, key))
            {
                isProLicensed = true;
                licenseEmail = email;
                licenseKey = key;
                SaveLicenseFile();
                BuildMenu();
                MessageBox.Show(
                    L("License activated!\nRegistered to: ",
                      "Lizenz aktiviert!\nRegistriert auf: ") + email + "\n\n" +
                    L("All Pro features are now unlocked.",
                      "Alle Pro-Funktionen sind freigeschaltet."),
                    "BrightRaider Pro", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(
                    L("Invalid license key. Please check your email and key.",
                      "Ungültiger Lizenzschlüssel. Bitte Email und Key prüfen."),
                    "BrightRaider", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        dialog.Dispose();
    }

    // === NvAPI Init ===
    static IntPtr QueryInterface(uint id)
    {
        try { return Environment.Is64BitProcess ? NvAPI64_QueryInterface(id) : NvAPI32_QueryInterface(id); }
        catch { return IntPtr.Zero; }
    }

    static bool InitNvAPI()
    {
        try
        {
            IntPtr pInit = QueryInterface(NVAPI_ID_INITIALIZE);
            if (pInit == IntPtr.Zero) return false;
            nvInit = (NvAPI_Initialize_t)Marshal.GetDelegateForFunctionPointer(pInit, typeof(NvAPI_Initialize_t));
            if (nvInit() != 0) return false;
            IntPtr pEnum = QueryInterface(NVAPI_ID_ENUM_DISPLAY);
            IntPtr pSetDVC = QueryInterface(NVAPI_ID_SET_DVC_LEVEL);
            if (pEnum == IntPtr.Zero || pSetDVC == IntPtr.Zero) return false;
            nvEnumDisplay = (NvAPI_EnumDisplay_t)Marshal.GetDelegateForFunctionPointer(pEnum, typeof(NvAPI_EnumDisplay_t));
            nvSetDVC = (NvAPI_SetDVCLevel_t)Marshal.GetDelegateForFunctionPointer(pSetDVC, typeof(NvAPI_SetDVCLevel_t));
            nvApiReady = true;
            return true;
        }
        catch { return false; }
    }

    // === AMD ADL Init ===
    static IntPtr ADL_Main_Memory_Alloc(int size) { return Marshal.AllocCoTaskMem(size); }

    static T GetADLDelegate<T>(IntPtr hModule, string procName) where T : class
    {
        IntPtr ptr = GetProcAddress(hModule, procName);
        if (ptr == IntPtr.Zero) return null;
        return Marshal.GetDelegateForFunctionPointer(ptr, typeof(T)) as T;
    }

    static bool InitADL()
    {
        try
        {
            string dllName = Environment.Is64BitProcess ? "atiadlxx.dll" : "atiadlxy.dll";
            IntPtr hModule = LoadLibrary(dllName);
            if (hModule == IntPtr.Zero) return false;

            adlMainControlCreate = GetADLDelegate<ADL_Main_Control_Create_t>(hModule, "ADL_Main_Control_Create");
            adlMainControlDestroy = GetADLDelegate<ADL_Main_Control_Destroy_t>(hModule, "ADL_Main_Control_Destroy");
            adlAdapterNumberGet = GetADLDelegate<ADL_Adapter_NumberOfAdapters_Get_t>(hModule, "ADL_Adapter_NumberOfAdapters_Get");
            adlAdapterInfoGet = GetADLDelegate<ADL_Adapter_AdapterInfo_Get_t>(hModule, "ADL_Adapter_AdapterInfo_Get");
            adlAdapterActiveGet = GetADLDelegate<ADL_Adapter_Active_Get_t>(hModule, "ADL_Adapter_Active_Get");
            adlDisplayInfoGet = GetADLDelegate<ADL_Display_DisplayInfo_Get_t>(hModule, "ADL_Display_DisplayInfo_Get");
            adlDisplayColorSet = GetADLDelegate<ADL_Display_Color_Set_t>(hModule, "ADL_Display_Color_Set");
            adlDisplayColorGet = GetADLDelegate<ADL_Display_Color_Get_t>(hModule, "ADL_Display_Color_Get");

            if (adlMainControlCreate == null || adlMainControlDestroy == null ||
                adlAdapterNumberGet == null || adlDisplayColorSet == null) return false;

            ADL_Main_Memory_Alloc_Delegate memAlloc = new ADL_Main_Memory_Alloc_Delegate(ADL_Main_Memory_Alloc);
            if (adlMainControlCreate(memAlloc, 1) != ADL_OK) return false;

            int adapterCount = 0;
            if (adlAdapterNumberGet(ref adapterCount) != ADL_OK || adapterCount <= 0) return false;

            int adapterInfoSize = Marshal.SizeOf(typeof(ADLAdapterInfo));
            IntPtr adapterInfoPtr = Marshal.AllocCoTaskMem(adapterInfoSize * adapterCount);
            if (adlAdapterInfoGet(adapterInfoPtr, adapterInfoSize * adapterCount) != ADL_OK)
            { Marshal.FreeCoTaskMem(adapterInfoPtr); return false; }

            HashSet<int> processed = new HashSet<int>();
            for (int i = 0; i < adapterCount; i++)
            {
                ADLAdapterInfo ai = (ADLAdapterInfo)Marshal.PtrToStructure(
                    new IntPtr(adapterInfoPtr.ToInt64() + i * adapterInfoSize), typeof(ADLAdapterInfo));
                if (processed.Contains(ai.AdapterIndex)) continue;
                processed.Add(ai.AdapterIndex);
                if (adlAdapterActiveGet != null)
                { int active = 0; if (adlAdapterActiveGet(ai.AdapterIndex, ref active) != ADL_OK || active == 0) continue; }
                if (adlDisplayInfoGet != null)
                {
                    int numDisplays = 0; IntPtr displayInfoPtr;
                    if (adlDisplayInfoGet(ai.AdapterIndex, ref numDisplays, out displayInfoPtr, 0) == ADL_OK && numDisplays > 0)
                    {
                        int diSize = Marshal.SizeOf(typeof(ADLDisplayInfo));
                        for (int j = 0; j < numDisplays; j++)
                        {
                            ADLDisplayInfo di = (ADLDisplayInfo)Marshal.PtrToStructure(
                                new IntPtr(displayInfoPtr.ToInt64() + j * diSize), typeof(ADLDisplayInfo));
                            if ((di.DisplayInfoValue & 0x03) == 0x03)
                            { ADLDisplayTarget t; t.AdapterIndex = ai.AdapterIndex; t.DisplayIndex = di.DisplayID.DisplayLogicalIndex; adlDisplayTargets.Add(t); }
                        }
                        if (displayInfoPtr != IntPtr.Zero) Marshal.FreeCoTaskMem(displayInfoPtr);
                    }
                }
            }
            Marshal.FreeCoTaskMem(adapterInfoPtr);
            if (adlDisplayTargets.Count > 0) { adlReady = true; return true; }
            return false;
        }
        catch { return false; }
    }

    // === Unified Saturation ===
    void SetSaturation(int panelLevel)
    {
        int targetIdx = -1;
        if (selectedDisplay != null)
            for (int i = 0; i < activeDisplays.Count; i++)
                if (activeDisplays[i].DeviceName == selectedDisplay) { targetIdx = i; break; }

        if (nvApiReady)
        {
            try
            {
                int lv = (int)Math.Round((panelLevel - 50) * 63.0 / 50.0);
                if (lv < 0) lv = 0; if (lv > 63) lv = 63;
                int idx = 0; IntPtr handle = IntPtr.Zero;
                while (nvEnumDisplay(idx, ref handle) == 0)
                { if (targetIdx < 0 || targetIdx == idx) nvSetDVC(handle, 0, lv); idx++; }
            }
            catch { }
        }
        else if (adlReady)
        {
            try
            {
                int lv = (panelLevel - 50) * 2 + 100;
                if (lv < 0) lv = 0; if (lv > 200) lv = 200;
                for (int i = 0; i < adlDisplayTargets.Count; i++)
                    if (targetIdx < 0 || targetIdx == i)
                    { ADLDisplayTarget t = adlDisplayTargets[i]; adlDisplayColorSet(t.AdapterIndex, t.DisplayIndex, ADL_DISPLAY_COLOR_SATURATION, lv); }
            }
            catch { }
        }
    }

    // === Registry ===
    static bool IsAdmin()
    { WindowsIdentity id = WindowsIdentity.GetCurrent(); return new WindowsPrincipal(id).IsInRole(WindowsBuiltInRole.Administrator); }

    static bool EnsureGammaRegistryKey()
    {
        try
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\ICM", false);
            if (key != null) { object val = key.GetValue("GdiIcmGammaRange"); key.Close(); if (val != null && (int)val == 256) return true; }
            if (!IsAdmin())
            {
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = Application.ExecutablePath; psi.Arguments = "--setregistry"; psi.Verb = "runas"; psi.UseShellExecute = true;
                try { Process p = Process.Start(psi); p.WaitForExit(); return p.ExitCode == 0; }
                catch { return false; }
            }
            else return SetGammaRegistryValue();
        }
        catch { return false; }
    }

    static bool SetGammaRegistryValue()
    {
        try
        { RegistryKey k = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\ICM"); k.SetValue("GdiIcmGammaRange", 256, RegistryValueKind.DWord); k.Close(); return true; }
        catch { return false; }
    }

    // === Auto-Start ===
    void SetAutoStart(bool enable)
    {
        try
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            if (enable)
                key.SetValue("BrightRaider", "\"" + Application.ExecutablePath + "\"");
            else
                key.DeleteValue("BrightRaider", false);
            key.Close();
            autoStart = enable;
            SaveConfig();
        }
        catch { }
    }

    bool GetAutoStart()
    {
        try
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", false);
            object val = key.GetValue("BrightRaider");
            key.Close();
            return val != null;
        }
        catch { return false; }
    }

    // === Display detection ===
    static List<DisplayInfo> DetectDisplays()
    {
        List<DisplayInfo> displays = new List<DisplayInfo>();
        uint devNum = 0; DISPLAY_DEVICE dev = new DISPLAY_DEVICE(); dev.cb = Marshal.SizeOf(dev);
        while (EnumDisplayDevices(null, devNum, ref dev, 0))
        {
            if ((dev.StateFlags & 1) != 0)
            { DisplayInfo info; info.DeviceName = dev.DeviceName; info.FriendlyName = "Display " + (displays.Count + 1) + " (" + dev.DeviceString + ")"; displays.Add(info); }
            dev.cb = Marshal.SizeOf(dev); devNum++;
        }
        return displays;
    }

    // === Default Profiles ===
    void InitDefaultProfiles()
    {
        profiles = new ProfileData[9];
        profiles[0] = new ProfileData(1.0, 1.0, 50, "Normal", 10.0, 255.0);
        profiles[1] = new ProfileData(1.5, 1.1, 60, "Bright", 4.0, 9.9);
        profiles[2] = new ProfileData(2.0, 1.1, 70, "Brighter", 0.0, 3.9);
        for (int i = 3; i < 9; i++)
            profiles[i] = new ProfileData(1.0, 1.0, 50, "Profile " + (i + 1), -1.0, -1.0);
    }

    // === Config ===
    void LoadConfig()
    {
        try
        {
            if (!File.Exists(configPath)) return;
            string[] lines;
            // Try AES-encrypted first, fall back to plain text (migration from old version)
            try
            {
                byte[] cipher = File.ReadAllBytes(configPath);
                string decrypted = Encoding.UTF8.GetString(AesDecrypt(cipher, "BrightRaider-v5-Config-2025"));
                lines = decrypted.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            }
            catch
            {
                lines = File.ReadAllLines(configPath); // plain text fallback
            }
            foreach (string line in lines)
            {
                int eq = line.IndexOf('=');
                if (eq < 0) continue;
                string k = line.Substring(0, eq).Trim();
                string v = line.Substring(eq + 1).Trim();
                if (k == "SelectedDisplay") selectedDisplay = v.Length > 0 ? v : null;
                else if (k == "ShowNotifications") showNotifications = v != "0";
                else if (k == "Language") language = (v == "de") ? "de" : "en";
                else if (k == "ProfileCount") { int pc; if (int.TryParse(v, out pc) && pc >= 3 && pc <= 9) profileCount = pc; }
                else if (k == "AutoBrightness") autoBrightness = v == "1";
                else if (k == "AutoBrightnessInterval") { int iv; if (int.TryParse(v, out iv) && iv >= 200 && iv <= 10000) autoBrightnessInterval = iv; }
                else if (k.StartsWith("Profile") && k.Contains("_"))
                {
                    // e.g. Profile1_Gamma=1.5
                    string[] parts = k.Split('_');
                    int pIdx;
                    if (parts.Length == 2 && parts[0].Length > 7 && int.TryParse(parts[0].Substring(7), out pIdx) && pIdx >= 1 && pIdx <= 9)
                    {
                        pIdx--;
                        if (parts[1] == "Gamma") { double g; if (double.TryParse(v, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out g)) profiles[pIdx].Gamma = g; }
                        else if (parts[1] == "Contrast") { double c; if (double.TryParse(v, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out c)) profiles[pIdx].Contrast = c; }
                        else if (parts[1] == "Vibrance") { int vb; if (int.TryParse(v, out vb)) profiles[pIdx].Vibrance = vb; }
                        else if (parts[1] == "Name") profiles[pIdx].Name = v;
                        else if (parts[1] == "BrightnessMin") { double bm; if (double.TryParse(v, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out bm)) profiles[pIdx].BrightnessMin = bm; }
                        else if (parts[1] == "BrightnessMax") { double bm; if (double.TryParse(v, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out bm)) profiles[pIdx].BrightnessMax = bm; }
                    }
                }
            }
        }
        catch { }
    }

    void SaveConfig()
    {
        try
        {
            List<string> lines = new List<string>();
            lines.Add("SelectedDisplay=" + (selectedDisplay ?? ""));
            lines.Add("ShowNotifications=" + (showNotifications ? "1" : "0"));
            lines.Add("Language=" + language);
            lines.Add("ProfileCount=" + profileCount);
            lines.Add("AutoBrightness=" + (autoBrightness ? "1" : "0"));
            lines.Add("AutoBrightnessInterval=" + autoBrightnessInterval);
            for (int i = 0; i < 9; i++)
            {
                string p = "Profile" + (i + 1);
                lines.Add(p + "_Gamma=" + profiles[i].Gamma.ToString("F2", System.Globalization.CultureInfo.InvariantCulture));
                lines.Add(p + "_Contrast=" + profiles[i].Contrast.ToString("F2", System.Globalization.CultureInfo.InvariantCulture));
                lines.Add(p + "_Vibrance=" + profiles[i].Vibrance);
                lines.Add(p + "_Name=" + profiles[i].Name);
                lines.Add(p + "_BrightnessMin=" + profiles[i].BrightnessMin.ToString("F1", System.Globalization.CultureInfo.InvariantCulture));
                lines.Add(p + "_BrightnessMax=" + profiles[i].BrightnessMax.ToString("F1", System.Globalization.CultureInfo.InvariantCulture));
            }
            string content = string.Join("\n", lines.ToArray());
            File.WriteAllBytes(configPath, AesEncrypt(Encoding.UTF8.GetBytes(content), "BrightRaider-v5-Config-2025"));
        }
        catch { }
    }

    // === Keyboard Hook ===
    IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
    {
        if (nCode >= 0 && (wParam == (IntPtr)WM_KEYDOWN || wParam == (IntPtr)WM_SYSKEYDOWN))
        {
            KBDLLHOOKSTRUCT kb = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));
            int profile = 0;
            bool isMute = false;

            // Numpad with NumLock ON
            if ((int)kb.vkCode >= VK_NUMPAD0 && (int)kb.vkCode <= VK_NUMPAD9)
            {
                int num = (int)kb.vkCode - VK_NUMPAD0;
                if (num == 0) isMute = true;
                else profile = num;
            }
            // Numpad with NumLock OFF (non-extended = numpad)
            // Also catches Shift+Numpad which sends VK_INSERT/VK_END etc.
            else if ((kb.flags & LLKHF_EXTENDED) == 0)
            {
                switch ((int)kb.vkCode)
                {
                    case VK_INSERT: isMute = true; break; // Numpad 0 (NumLock OFF or Shift+Numpad0)
                    case VK_END: profile = 1; break;
                    case VK_DOWN: profile = 2; break;
                    case VK_NEXT: profile = 3; break;
                    case VK_LEFT: profile = 4; break;
                    case VK_CLEAR: profile = 5; break;
                    case VK_RIGHT: profile = 6; break;
                    case VK_HOME: profile = 7; break;
                    case VK_UP: profile = 8; break;
                    case VK_PRIOR: profile = 9; break;
                }
            }

            // Limit to 3 profiles if not Pro
            if (profile > 3 && !isProLicensed) profile = 0;
            if (profile > profileCount) profile = 0;

            if (profile > 0)
            {
                this.BeginInvoke(new Action(delegate { ApplyProfile(profile); }));
                return (IntPtr)1;
            }

            if (isMute && isProLicensed)
            {
                this.BeginInvoke(new Action(delegate { ToggleGameMute(); }));
                return (IntPtr)1;
            }
        }
        return CallNextHookEx(hookId, nCode, wParam, lParam);
    }

    // === Game Mute (per-process via direct Core Audio COM vtable calls) ===
    void ToggleGameMute()
    {
        try
        {
            // Find Arc Raiders or foreground game process
            string[] gameNames = { "ARC-Win64-Shipping", "ArcRaiders", "arc-Win64-Shipping" };
            int targetPid = 0;
            string targetName = "";
            foreach (string name in gameNames)
            {
                Process[] procs = Process.GetProcessesByName(name);
                if (procs.Length > 0) { targetPid = procs[0].Id; targetName = procs[0].ProcessName; break; }
            }

            if (targetPid == 0)
            {
                // Fallback: use foreground window process
                IntPtr fg = GetForegroundWindow();
                uint pid; GetWindowThreadProcessId(fg, out pid);
                if (pid != 0)
                {
                    try { Process p = Process.GetProcessById((int)pid); targetPid = p.Id; targetName = p.ProcessName; } catch { }
                }
            }

            if (targetPid == 0)
            {
                ShowToast(L("No game found", "Kein Spiel gefunden"));
                return;
            }

            gameMuted = !gameMuted;
            SetProcessMute(targetPid, gameMuted);

            ShowToast(gameMuted
                ? L("Game muted", "Spiel stumm") + " (" + targetName + ")"
                : L("Game unmuted", "Spiel Ton an") + " (" + targetName + ")");
        }
        catch { }
    }

    // === Auto-Brightness ===
    void StartAutoBrightness()
    {
        if (autoBrightnessTimer != null) return;
        autoBrightnessTimer = new System.Windows.Forms.Timer();
        autoBrightnessTimer.Interval = autoBrightnessInterval;
        autoBrightnessTimer.Tick += delegate { CheckScreenBrightness(); };
        autoBrightnessTimer.Start();

        // Show "A" (Auto) icon
        if (!exiting)
        {
            try
            {
                Icon oldIcon = trayIcon.Icon;
                trayIcon.Icon = MakeIcon("A", Color.Cyan);
                if (oldIcon != null) oldIcon.Dispose();
            }
            catch { }
        }
    }

    void StopAutoBrightness()
    {
        if (autoBrightnessTimer != null)
        {
            autoBrightnessTimer.Stop();
            autoBrightnessTimer.Dispose();
            autoBrightnessTimer = null;
        }
    }

    [DllImport("user32.dll")]
    static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
    [DllImport("user32.dll")]
    static extern int GetWindowLong(IntPtr hWnd, int nIndex);
    const int GWL_EXSTYLE = -20;
    const int WS_EX_LAYERED = 0x80000;
    const int WS_EX_TRANSPARENT = 0x20;

    void ToggleBrightnessOverlay()
    {
        if (brightnessOverlay != null && brightnessOverlay.Visible)
        {
            brightnessOverlay.Hide();
            if (measureFrameOverlay != null) measureFrameOverlay.Hide();
            return;
        }

        int screenW = GetSystemMetrics(SM_CXSCREEN);
        int screenH = GetSystemMetrics(SM_CYSCREEN);

        // Create measurement zone overlay (5 zones, click-through)
        if (measureFrameOverlay == null || measureFrameOverlay.IsDisposed)
        {
            measureFrameOverlay = new Form();
            measureFrameOverlay.FormBorderStyle = FormBorderStyle.None;
            measureFrameOverlay.TopMost = true;
            measureFrameOverlay.ShowInTaskbar = false;
            measureFrameOverlay.TransparencyKey = Color.Magenta;
            measureFrameOverlay.BackColor = Color.Magenta;
            // Full screen overlay so we can draw all 5 zones
            measureFrameOverlay.Location = new Point(0, 0);
            measureFrameOverlay.Size = new Size(screenW, screenH);
            measureFrameOverlay.StartPosition = FormStartPosition.Manual;

            // Draw 5 measurement zone rectangles
            measureFrameOverlay.Paint += delegate(object s, PaintEventArgs pe) {
                int[][] zones = GetMeasurementZones(screenW, screenH);
                string[] zoneNames = new string[] { "C", "TL", "TR", "BL", "BR" };
                Color[] zoneColors = new Color[] { Color.Red, Color.Orange, Color.Orange, Color.Orange, Color.Orange };
                using (Font f = new Font("Consolas", 9f, FontStyle.Bold))
                {
                    for (int i = 0; i < zones.Length; i++)
                    {
                        using (Pen pen = new Pen(zoneColors[i], 2))
                            pe.Graphics.DrawRectangle(pen, zones[i][0], zones[i][1], zones[i][2], zones[i][3]);
                        using (SolidBrush bg = new SolidBrush(Color.FromArgb(180, 0, 0, 0)))
                        using (SolidBrush fg = new SolidBrush(zoneColors[i]))
                        {
                            pe.Graphics.FillRectangle(bg, zones[i][0] + 2, zones[i][1] + 2, 18, 16);
                            pe.Graphics.DrawString(zoneNames[i], f, fg, zones[i][0] + 3, zones[i][1] + 2);
                        }
                    }
                }
            };

            // Make click-through after showing
            measureFrameOverlay.Shown += delegate {
                int exStyle = GetWindowLong(measureFrameOverlay.Handle, GWL_EXSTYLE);
                SetWindowLong(measureFrameOverlay.Handle, GWL_EXSTYLE, exStyle | WS_EX_LAYERED | WS_EX_TRANSPARENT);
            };
        }

        // Info overlay (top-right)
        if (brightnessOverlay == null || brightnessOverlay.IsDisposed)
        {
            brightnessOverlay = new Form();
            brightnessOverlay.FormBorderStyle = FormBorderStyle.None;
            brightnessOverlay.BackColor = Color.Black;
            brightnessOverlay.Opacity = 0.85;
            brightnessOverlay.TopMost = true;
            brightnessOverlay.ShowInTaskbar = false;
            brightnessOverlay.Size = new Size(320, 145);
            brightnessOverlay.StartPosition = FormStartPosition.Manual;
            brightnessOverlay.Location = new Point(screenW - 330, 10);

            overlayLabel = new Label();
            overlayLabel.ForeColor = Color.FromArgb(0, 255, 100);
            overlayLabel.BackColor = Color.Transparent;
            overlayLabel.Font = new Font("Consolas", 11f, FontStyle.Bold);
            overlayLabel.Dock = DockStyle.Fill;
            overlayLabel.TextAlign = ContentAlignment.MiddleLeft;
            overlayLabel.Padding = new Padding(8);
            overlayLabel.Text = "BrightRaider Overlay\n" + L("Waiting for data...", "Warte auf Daten...");
            brightnessOverlay.Controls.Add(overlayLabel);

            // Click to close both
            Action closeOverlays = delegate {
                brightnessOverlay.Hide();
                if (measureFrameOverlay != null) measureFrameOverlay.Hide();
            };
            overlayLabel.Click += delegate { closeOverlays(); };
            brightnessOverlay.Click += delegate { closeOverlays(); };
        }

        measureFrameOverlay.Show();
        brightnessOverlay.Show();

        // If auto-brightness is not running, start timer so overlay gets updates
        if (!autoBrightness)
        {
            StartAutoBrightness();
        }
    }

    void UpdateBrightnessOverlay(double brightness, double gamma, double contrast, int vibrance, string interpInfo, List<double> zoneValues)
    {
        if (overlayLabel == null || overlayLabel.IsDisposed) return;
        string line1 = L("Median: ", "Median: ") + brightness.ToString("F1") + " / 255";
        string line2 = "G:" + gamma.ToString("F2") + " C:" + contrast.ToString("F2") + " V:" + vibrance;
        string line3 = interpInfo;
        // Show individual zone values
        string line4 = "C:" + (zoneValues.Count > 0 ? zoneValues[0].ToString("F1") : "?")
            + " TL:" + (zoneValues.Count > 1 ? zoneValues[1].ToString("F1") : "?")
            + " TR:" + (zoneValues.Count > 2 ? zoneValues[2].ToString("F1") : "?")
            + " BL:" + (zoneValues.Count > 3 ? zoneValues[3].ToString("F1") : "?")
            + " BR:" + (zoneValues.Count > 4 ? zoneValues[4].ToString("F1") : "?");
        string line5 = L("Interval: ", "Intervall: ") + autoBrightnessInterval + "ms";
        overlayLabel.Text = line1 + "\n" + line2 + "\n" + line3 + "\n" + line4 + "\n" + line5;

        if (brightness < 4.0)
            overlayLabel.ForeColor = Color.Red;
        else if (brightness < 10.0)
            overlayLabel.ForeColor = Color.Orange;
        else
            overlayLabel.ForeColor = Color.FromArgb(0, 255, 100);
    }

    // Apply interpolated gamma/contrast/vibrance directly (no profile switch, smooth)
    void ApplyInterpolated(double gamma, double contrast, int vibrance, string overlayInfo)
    {
        SetGammaRamp(gamma, contrast);
        SetSaturation(vibrance);

        // Update tray tooltip with interpolated info
        if (!exiting)
        {
            try
            {
                string txt = "BrightRaider - Auto: G" + gamma.ToString("F2") + " C" + contrast.ToString("F2") + " V" + vibrance;
                if (txt.Length > 63) txt = txt.Substring(0, 63);
                trayIcon.Text = txt;
            }
            catch { }
        }
    }

    // Multi-zone measurement: returns brightness for a single zone (StretchBlt to 16x16)
    double MeasureZone(IntPtr hdcScreen, int zoneX, int zoneY, int zoneW, int zoneH)
    {
        using (Bitmap bmp = new Bitmap(16, 16))
        {
            using (Graphics g = Graphics.FromImage(bmp))
            {
                IntPtr hdcBmp = g.GetHdc();
                StretchBlt(hdcBmp, 0, 0, 16, 16, hdcScreen, zoneX, zoneY, zoneW, zoneH, SRCCOPY);
                g.ReleaseHdc(hdcBmp);
            }
            double total = 0;
            for (int y = 0; y < 16; y++)
                for (int x = 0; x < 16; x++)
                {
                    Color c = bmp.GetPixel(x, y);
                    total += c.R * 0.299 + c.G * 0.587 + c.B * 0.114;
                }
            return total / 256.0;
        }
    }

    // Returns 5 measurement zone rectangles: [x, y, width, height]
    // Tiny 3% zones in X-pattern: center + 4 corners
    static int[][] GetMeasurementZones(int screenW, int screenH)
    {
        int zW = (int)(screenW * 0.03);
        int zH = (int)(screenH * 0.03);
        int marginX = (int)(screenW * 0.15); // 15% from edges
        int marginY = (int)(screenH * 0.15);

        return new int[][] {
            new int[] { screenW / 2 - zW / 2, screenH / 2 - zH / 2, zW, zH },                   // 0: Center
            new int[] { marginX, marginY, zW, zH },                                                // 1: Top-Left
            new int[] { screenW - marginX - zW, marginY, zW, zH },                                 // 2: Top-Right
            new int[] { marginX, screenH - marginY - zH, zW, zH },                                 // 3: Bottom-Left
            new int[] { screenW - marginX - zW, screenH - marginY - zH, zW, zH }                   // 4: Bottom-Right
        };
    }

    void CheckScreenBrightness()
    {
        try
        {
            int screenW = GetSystemMetrics(SM_CXSCREEN);
            int screenH = GetSystemMetrics(SM_CYSCREEN);
            int[][] zones = GetMeasurementZones(screenW, screenH);

            // Measure all 5 zones
            IntPtr hdcScreen = GetDC(IntPtr.Zero);
            List<double> zoneBrightness = new List<double>();
            for (int i = 0; i < zones.Length; i++)
                zoneBrightness.Add(MeasureZone(hdcScreen, zones[i][0], zones[i][1], zones[i][2], zones[i][3]));
            ReleaseDC(IntPtr.Zero, hdcScreen);

            // Weighted average: center counts 2x, corners 1x each (total weight = 6)
            double avgBrightness = (zoneBrightness[0] * 2 + zoneBrightness[1] + zoneBrightness[2] + zoneBrightness[3] + zoneBrightness[4]) / 6.0;

            // Collect profiles that have brightness ranges, sorted by BrightnessMin ascending
            List<int> rangeProfiles = new List<int>();
            for (int i = 0; i < profileCount; i++)
            {
                if (profiles[i].BrightnessMin >= 0 && profiles[i].BrightnessMax >= 0)
                    rangeProfiles.Add(i);
            }

            // Sort by BrightnessMin (brightest profile first = highest min)
            rangeProfiles.Sort(delegate(int a, int b) { return profiles[b].BrightnessMin.CompareTo(profiles[a].BrightnessMin); });

            double interpGamma = profiles[0].Gamma;
            double interpContrast = profiles[0].Contrast;
            int interpVibrance = profiles[0].Vibrance;
            string interpInfo = "";

            if (rangeProfiles.Count >= 2)
            {
                // Find the two profiles to interpolate between
                // rangeProfiles sorted bright→dark (highest BrightnessMin first)
                int upperIdx = rangeProfiles[0]; // brightest
                int lowerIdx = rangeProfiles[rangeProfiles.Count - 1]; // darkest

                // Find the two closest profiles bracketing the current brightness
                for (int i = 0; i < rangeProfiles.Count - 1; i++)
                {
                    int hiIdx = rangeProfiles[i];
                    int loIdx = rangeProfiles[i + 1];
                    if (avgBrightness <= profiles[hiIdx].BrightnessMax && avgBrightness >= profiles[loIdx].BrightnessMin)
                    {
                        upperIdx = hiIdx;
                        lowerIdx = loIdx;
                        break;
                    }
                }

                // If above all ranges, use brightest profile
                if (avgBrightness > profiles[rangeProfiles[0]].BrightnessMax)
                {
                    upperIdx = rangeProfiles[0];
                    lowerIdx = rangeProfiles[0];
                }
                // If below all ranges, use darkest profile
                else if (avgBrightness < profiles[rangeProfiles[rangeProfiles.Count - 1]].BrightnessMin)
                {
                    upperIdx = rangeProfiles[rangeProfiles.Count - 1];
                    lowerIdx = rangeProfiles[rangeProfiles.Count - 1];
                }

                ProfileData pHi = profiles[upperIdx];
                ProfileData pLo = profiles[lowerIdx];

                if (upperIdx == lowerIdx)
                {
                    // Same profile, no interpolation
                    interpGamma = pHi.Gamma;
                    interpContrast = pHi.Contrast;
                    interpVibrance = pHi.Vibrance;
                    interpInfo = "P" + (upperIdx + 1) + " " + pHi.Name;
                }
                else
                {
                    // Interpolate: t=0 means at upper profile (bright), t=1 means at lower profile (dark)
                    double rangeTop = pHi.BrightnessMin; // where upper starts
                    double rangeBot = pLo.BrightnessMax;  // where lower ends
                    double t = 0;
                    if (Math.Abs(rangeTop - rangeBot) > 0.1)
                        t = (rangeTop - avgBrightness) / (rangeTop - rangeBot);
                    t = Math.Max(0, Math.Min(1, t));

                    interpGamma = pHi.Gamma + (pLo.Gamma - pHi.Gamma) * t;
                    interpContrast = pHi.Contrast + (pLo.Contrast - pHi.Contrast) * t;
                    interpVibrance = (int)(pHi.Vibrance + (pLo.Vibrance - pHi.Vibrance) * t);
                    interpInfo = "P" + (upperIdx + 1) + "\u2194P" + (lowerIdx + 1) + " t=" + t.ToString("F2");
                }
            }
            else if (rangeProfiles.Count == 1)
            {
                ProfileData p = profiles[rangeProfiles[0]];
                interpGamma = p.Gamma;
                interpContrast = p.Contrast;
                interpVibrance = p.Vibrance;
                interpInfo = "P" + (rangeProfiles[0] + 1) + " " + p.Name;
            }

            // Update overlay if visible
            if (brightnessOverlay != null && brightnessOverlay.Visible)
            {
                double fG = interpGamma; double fC = interpContrast; int fV = interpVibrance; string fI = interpInfo;
                List<double> zb = new List<double>(zoneBrightness);
                try
                {
                    this.BeginInvoke(new Action(delegate {
                        UpdateBrightnessOverlay(avgBrightness, fG, fC, fV, fI, zb);
                    }));
                }
                catch { }
            }

            // Apply interpolated values
            ApplyInterpolated(interpGamma, interpContrast, interpVibrance, interpInfo);
        }
        catch { }
    }

    [DllImport("gdi32.dll")]
    static extern int StretchBlt(IntPtr hdcDest, int xDest, int yDest, int wDest, int hDest,
        IntPtr hdcSrc, int xSrc, int ySrc, int wSrc, int hSrc, int rop);

    // === Constructor ===
    public BrightRaider()
    {
        configPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "BrightRaider.cfg");

        activeDisplays = DetectDisplays();
        InitDefaultProfiles();
        LoadConfig();
        LoadLicenseFile();
        autoStart = GetAutoStart();

        this.ShowInTaskbar = false;
        this.WindowState = FormWindowState.Minimized;
        this.FormBorderStyle = FormBorderStyle.None;
        this.Opacity = 0;

        trayIcon = new NotifyIcon();
        trayIcon.Text = "BrightRaider - Profile 1 (" + profiles[0].Name + ")";
        trayIcon.Icon = MakeIcon("1", Color.White);
        trayIcon.Visible = true;

        BuildMenu();

        hookProc = new LowLevelKeyboardProc(HookCallback);
        using (Process curProcess = Process.GetCurrentProcess())
        using (ProcessModule curModule = curProcess.MainModule)
            hookId = SetWindowsHookEx(WH_KEYBOARD_LL, hookProc, GetModuleHandle(curModule.ModuleName), 0);

        if (autoBrightness && isProLicensed)
            StartAutoBrightness();
    }

    // === Calibration Wizard (Pro) ===
    double MeasureBrightnessNow()
    {
        try
        {
            int screenW = GetSystemMetrics(SM_CXSCREEN);
            int screenH = GetSystemMetrics(SM_CYSCREEN);
            int[][] zones = GetMeasurementZones(screenW, screenH);
            IntPtr hdcScreen = GetDC(IntPtr.Zero);
            List<double> values = new List<double>();
            for (int i = 0; i < zones.Length; i++)
                values.Add(MeasureZone(hdcScreen, zones[i][0], zones[i][1], zones[i][2], zones[i][3]));
            ReleaseDC(IntPtr.Zero, hdcScreen);
            // Weighted: center 2x, corners 1x
            return (values[0] * 2 + values[1] + values[2] + values[3] + values[4]) / 6.0;
        }
        catch { return -1; }
    }

    void ShowCalibrationWizard()
    {
        // Topmost overlay for calibration steps
        Form wizard = new Form();
        wizard.Text = "BrightRaider - " + L("Calibration", "Kalibrierung");
        wizard.FormBorderStyle = FormBorderStyle.None;
        wizard.BackColor = Color.FromArgb(20, 20, 20);
        wizard.Size = new Size(420, 200);
        wizard.StartPosition = FormStartPosition.CenterScreen;
        wizard.TopMost = true;
        wizard.Opacity = 0.92;

        Label lblStep = new Label();
        lblStep.ForeColor = Color.White;
        lblStep.Font = new Font("Segoe UI", 13f, FontStyle.Bold);
        lblStep.Location = new Point(20, 15);
        lblStep.Size = new Size(380, 35);
        lblStep.Text = L("Step 1 / 2", "Schritt 1 / 2");
        wizard.Controls.Add(lblStep);

        Label lblInstr = new Label();
        lblInstr.ForeColor = Color.FromArgb(200, 200, 200);
        lblInstr.Font = new Font("Segoe UI", 11f);
        lblInstr.Location = new Point(20, 55);
        lblInstr.Size = new Size(380, 50);
        lblInstr.Text = L("Go to the DARKEST spot in your game.\nThen press ENTER or click the button.",
                          "Gehe zur DUNKELSTEN Stelle im Spiel.\nDann druecke ENTER oder klicke den Button.");
        wizard.Controls.Add(lblInstr);

        Label lblValue = new Label();
        lblValue.ForeColor = Color.Orange;
        lblValue.Font = new Font("Consolas", 12f, FontStyle.Bold);
        lblValue.Location = new Point(20, 110);
        lblValue.Size = new Size(380, 25);
        lblValue.Text = "";
        wizard.Controls.Add(lblValue);

        Button btnCapture = new Button();
        btnCapture.Text = L("Capture", "Messen");
        btnCapture.Size = new Size(120, 36);
        btnCapture.Location = new Point(20, 145);
        btnCapture.FlatStyle = FlatStyle.Flat;
        btnCapture.ForeColor = Color.White;
        btnCapture.BackColor = Color.FromArgb(60, 60, 60);
        wizard.Controls.Add(btnCapture);

        Button btnCancel = new Button();
        btnCancel.Text = L("Cancel", "Abbrechen");
        btnCancel.Size = new Size(100, 36);
        btnCancel.Location = new Point(300, 145);
        btnCancel.FlatStyle = FlatStyle.Flat;
        btnCancel.ForeColor = Color.Gray;
        btnCancel.BackColor = Color.FromArgb(40, 40, 40);
        btnCancel.DialogResult = DialogResult.Cancel;
        wizard.Controls.Add(btnCancel);
        wizard.CancelButton = btnCancel;

        // Live brightness display
        System.Windows.Forms.Timer liveTimer = new System.Windows.Forms.Timer();
        liveTimer.Interval = 300;
        liveTimer.Tick += delegate {
            double br = MeasureBrightnessNow();
            if (br >= 0) lblValue.Text = L("Current: ", "Aktuell: ") + br.ToString("F1");
        };
        liveTimer.Start();

        double darkVal = -1;
        double brightVal = -1;
        int step = 1;

        btnCapture.Click += delegate {
            double val = MeasureBrightnessNow();
            if (val < 0) return;

            if (step == 1)
            {
                darkVal = val;
                step = 2;
                lblStep.Text = L("Step 2 / 2", "Schritt 2 / 2");
                lblInstr.Text = L("Now go to a BRIGHT / NORMAL spot.\nThen press ENTER or click the button.",
                                  "Jetzt gehe zu einer HELLEN / NORMALEN Stelle.\nDann druecke ENTER oder klicke den Button.");
                lblStep.ForeColor = Color.FromArgb(0, 255, 100);
            }
            else
            {
                brightVal = val;
                liveTimer.Stop();
                liveTimer.Dispose();

                // Ensure dark < bright
                if (darkVal > brightVal)
                {
                    double tmp = darkVal; darkVal = brightVal; brightVal = tmp;
                }

                // Distribute ALL active profiles evenly across the range
                // Profile N (darkest boost) gets the lowest range, Profile 1 (normal) gets the highest
                double range = brightVal - darkVal;
                if (range < 0.5) range = 0.5;
                double slice = range / profileCount;

                for (int pi = 0; pi < profileCount; pi++)
                {
                    // Profile 1 = brightest (highest range), last profile = darkest (lowest range)
                    int ri = profileCount - 1 - pi; // reverse index: 0=darkest slice
                    profiles[pi].BrightnessMin = Math.Round(darkVal + ri * slice + (ri > 0 ? 0.1 : 0), 1);
                    profiles[pi].BrightnessMax = (pi == 0) ? 255.0 : Math.Round(darkVal + (ri + 1) * slice, 1);
                }

                SaveConfig();

                string msg = L("Calibration complete!\n\n", "Kalibrierung abgeschlossen!\n\n")
                    + L("Dark value: ", "Dunkelwert: ") + darkVal.ToString("F1") + "\n"
                    + L("Bright value: ", "Hellwert: ") + brightVal.ToString("F1") + "\n\n";
                for (int pi = 0; pi < profileCount; pi++)
                    msg += "P" + (pi + 1) + " " + profiles[pi].Name + ": " + profiles[pi].BrightnessMin.ToString("F1") + " - " + profiles[pi].BrightnessMax.ToString("F1") + "\n";
                msg += "\n" + L("Interpolation fills the gaps smoothly.", "Interpolation gleitet stufenlos dazwischen.");

                wizard.Hide();
                MessageBox.Show(msg, "BrightRaider", MessageBoxButtons.OK, MessageBoxIcon.Information);
                wizard.DialogResult = DialogResult.OK;
                wizard.Close();
            }
        };

        // ENTER key support
        wizard.KeyPreview = true;
        wizard.KeyDown += delegate(object s, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                btnCapture.PerformClick();
                e.Handled = true;
            }
        };

        wizard.ShowDialog();
        // liveTimer may already be disposed by step 2 capture
        try { liveTimer.Stop(); liveTimer.Dispose(); } catch { }
        wizard.Dispose();
    }

    // === Profile Editor (Pro) ===
    void ShowProfileEditor()
    {
        Form editor = new Form();
        editor.Text = "BrightRaider Pro - " + L("Edit Profiles", "Profile bearbeiten");
        editor.Size = new Size(680, 450);
        editor.FormBorderStyle = FormBorderStyle.FixedDialog;
        editor.StartPosition = FormStartPosition.CenterScreen;
        editor.MaximizeBox = false;

        // Profile count selector
        Label lblCount = new Label() { Text = L("Number of profiles:", "Anzahl Profile:"), Location = new Point(15, 15), AutoSize = true };
        NumericUpDown numCount = new NumericUpDown() { Location = new Point(180, 13), Size = new Size(50, 22), Minimum = 3, Maximum = 9, Value = profileCount };

        // Interval selector
        Label lblInterval = new Label() { Text = L("Analysis speed (ms):", "Analysegeschw. (ms):"), Location = new Point(260, 15), AutoSize = true };
        NumericUpDown numInterval = new NumericUpDown() { Location = new Point(430, 13), Size = new Size(70, 22), Minimum = 200, Maximum = 10000, Increment = 100, Value = autoBrightnessInterval };
        editor.Controls.AddRange(new Control[] { lblCount, numCount, lblInterval, numInterval });

        // Profile entries
        Label[] lblNames = new Label[9];
        TextBox[] txtNames = new TextBox[9];
        TextBox[] txtGamma = new TextBox[9];
        TextBox[] txtContrast = new TextBox[9];
        TextBox[] txtVibrance = new TextBox[9];
        TextBox[] txtBrMin = new TextBox[9];
        TextBox[] txtBrMax = new TextBox[9];

        int startY = 50;
        Label hdrName = new Label() { Text = L("Name", "Name"), Location = new Point(15, startY - 5), AutoSize = true, Font = new Font("Arial", 8, FontStyle.Bold) };
        Label hdrGamma = new Label() { Text = "Gamma", Location = new Point(140, startY - 5), AutoSize = true, Font = new Font("Arial", 8, FontStyle.Bold) };
        Label hdrContrast = new Label() { Text = L("Contrast", "Kontrast"), Location = new Point(210, startY - 5), AutoSize = true, Font = new Font("Arial", 8, FontStyle.Bold) };
        Label hdrVib = new Label() { Text = L("Vibrance", "Farbe"), Location = new Point(290, startY - 5), AutoSize = true, Font = new Font("Arial", 8, FontStyle.Bold) };
        Label hdrBrMin = new Label() { Text = L("Br.Min", "H.Min"), Location = new Point(370, startY - 5), AutoSize = true, Font = new Font("Arial", 8, FontStyle.Bold) };
        Label hdrBrMax = new Label() { Text = L("Br.Max", "H.Max"), Location = new Point(440, startY - 5), AutoSize = true, Font = new Font("Arial", 8, FontStyle.Bold) };
        // Info label for brightness range
        Label hdrBrInfo = new Label() { Text = L("(0-255, -1=off)", "(0-255, -1=aus)"), Location = new Point(510, startY - 5), AutoSize = true, Font = new Font("Arial", 7, FontStyle.Italic), ForeColor = Color.Gray };
        editor.Controls.AddRange(new Control[] { hdrName, hdrGamma, hdrContrast, hdrVib, hdrBrMin, hdrBrMax, hdrBrInfo });

        for (int i = 0; i < 9; i++)
        {
            int y = startY + 15 + i * 28;
            lblNames[i] = new Label() { Text = (i + 1) + ":", Location = new Point(2, y + 3), AutoSize = true };
            txtNames[i] = new TextBox() { Text = profiles[i].Name, Location = new Point(18, y), Size = new Size(110, 22) };
            txtGamma[i] = new TextBox() { Text = profiles[i].Gamma.ToString("F1", System.Globalization.CultureInfo.InvariantCulture), Location = new Point(140, y), Size = new Size(55, 22) };
            txtContrast[i] = new TextBox() { Text = profiles[i].Contrast.ToString("F2", System.Globalization.CultureInfo.InvariantCulture), Location = new Point(210, y), Size = new Size(65, 22) };
            txtVibrance[i] = new TextBox() { Text = profiles[i].Vibrance.ToString(), Location = new Point(290, y), Size = new Size(55, 22) };
            txtBrMin[i] = new TextBox() { Text = profiles[i].BrightnessMin.ToString("F1", System.Globalization.CultureInfo.InvariantCulture), Location = new Point(370, y), Size = new Size(50, 22) };
            txtBrMax[i] = new TextBox() { Text = profiles[i].BrightnessMax.ToString("F1", System.Globalization.CultureInfo.InvariantCulture), Location = new Point(440, y), Size = new Size(50, 22) };
            editor.Controls.AddRange(new Control[] { lblNames[i], txtNames[i], txtGamma[i], txtContrast[i], txtVibrance[i], txtBrMin[i], txtBrMax[i] });
        }

        // Help text
        Label lblHelp = new Label() {
            Text = L("Brightness range: The profile activates when screen brightness is between Min and Max (0=dark, 255=bright). Set to -1 to disable auto-brightness for that profile. Hover tray icon to see current brightness.",
                      "Helligkeitsbereich: Das Profil wird aktiviert wenn die Bildschirmhelligkeit zwischen Min und Max liegt (0=dunkel, 255=hell). Auf -1 setzen um Auto-Helligkeit fuer dieses Profil zu deaktivieren. Tray-Icon hovern fuer aktuellen Wert."),
            Location = new Point(15, 320), Size = new Size(640, 35), ForeColor = Color.DimGray, Font = new Font("Arial", 7.5f)
        };
        editor.Controls.Add(lblHelp);

        Button btnSave = new Button() { Text = L("Save", "Speichern"), Location = new Point(450, 370), Size = new Size(90, 32), DialogResult = DialogResult.OK };
        Button btnCancel = new Button() { Text = L("Cancel", "Abbrechen"), Location = new Point(550, 370), Size = new Size(90, 32), DialogResult = DialogResult.Cancel };
        editor.Controls.AddRange(new Control[] { btnSave, btnCancel });
        editor.AcceptButton = btnSave;
        editor.CancelButton = btnCancel;

        if (editor.ShowDialog() == DialogResult.OK)
        {
            profileCount = (int)numCount.Value;
            autoBrightnessInterval = (int)numInterval.Value;
            // Restart timer with new interval if running
            if (autoBrightnessTimer != null)
            {
                StopAutoBrightness();
                StartAutoBrightness();
            }
            for (int i = 0; i < 9; i++)
            {
                profiles[i].Name = txtNames[i].Text;
                double g; if (double.TryParse(txtGamma[i].Text, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out g)) profiles[i].Gamma = g;
                double c; if (double.TryParse(txtContrast[i].Text, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out c)) profiles[i].Contrast = c;
                int v; if (int.TryParse(txtVibrance[i].Text, out v)) profiles[i].Vibrance = v;
                double bMin; if (double.TryParse(txtBrMin[i].Text, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out bMin)) profiles[i].BrightnessMin = bMin;
                double bMax; if (double.TryParse(txtBrMax[i].Text, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out bMax)) profiles[i].BrightnessMax = bMax;
            }
            SaveConfig();
            BuildMenu();
        }
        editor.Dispose();
    }

    // === Menu ===
    void BuildMenu()
    {
        var menu = new ContextMenuStrip();

        // Profiles
        int count = isProLicensed ? profileCount : 3;
        for (int i = 0; i < count; i++)
        {
            int idx = i;
            ProfileData p = profiles[i];
            string label = (i + 1) + ": " + p.Name;
            menu.Items.Add(label, null, (s, e) => ApplyProfile(idx + 1));
        }
        if (!isProLicensed)
        {
            ToolStripMenuItem moreItem = new ToolStripMenuItem(L("+ More Profiles [PRO]", "+ Mehr Profile [PRO]"));
            moreItem.Click += delegate { ShowProRequired(); };
            menu.Items.Add(moreItem);
        }

        menu.Items.Add(new ToolStripSeparator());

        // Auto-Brightness toggle
        ToolStripMenuItem autoBrItem = new ToolStripMenuItem(L("Auto-Brightness", "Auto-Helligkeit") + (isProLicensed ? "" : " [PRO]"));
        autoBrItem.Checked = autoBrightness;
        autoBrItem.Click += delegate {
            if (isProLicensed)
            {
                autoBrightness = !autoBrightness;
                if (autoBrightness) StartAutoBrightness(); else StopAutoBrightness();
                SaveConfig();
                BuildMenu();
            }
            else ShowProRequired();
        };
        menu.Items.Add(autoBrItem);

        // Auto-Start toggle
        ToolStripMenuItem autoStartItem = new ToolStripMenuItem(L("Auto-Start", "Auto-Start") + (isProLicensed ? "" : " [PRO]"));
        autoStartItem.Checked = autoStart;
        autoStartItem.Click += delegate {
            if (isProLicensed) { SetAutoStart(!autoStart); BuildMenu(); }
            else ShowProRequired();
        };
        menu.Items.Add(autoStartItem);

        menu.Items.Add(new ToolStripSeparator());

        // Settings submenu
        ToolStripMenuItem settingsMenu = new ToolStripMenuItem(L("Settings", "Einstellungen"));

        ToolStripMenuItem editItem = new ToolStripMenuItem(L("Edit Profiles", "Profile bearbeiten") + (isProLicensed ? "" : " [PRO]"));
        editItem.Click += delegate { if (isProLicensed) ShowProfileEditor(); else ShowProRequired(); };
        settingsMenu.DropDownItems.Add(editItem);

        ToolStripMenuItem calibItem = new ToolStripMenuItem(L("Calibrate", "Kalibrieren") + (isProLicensed ? "" : " [PRO]"));
        calibItem.Click += delegate { if (isProLicensed) ShowCalibrationWizard(); else ShowProRequired(); };
        settingsMenu.DropDownItems.Add(calibItem);

        ToolStripMenuItem overlayItem = new ToolStripMenuItem(L("Debug Overlay", "Debug-Overlay") + (isProLicensed ? "" : " [PRO]"));
        overlayItem.Click += delegate {
            if (isProLicensed) ToggleBrightnessOverlay();
            else ShowProRequired();
        };
        settingsMenu.DropDownItems.Add(overlayItem);

        if (activeDisplays.Count > 1)
        {
            settingsMenu.DropDownItems.Add(new ToolStripSeparator());
            ToolStripMenuItem allItem = new ToolStripMenuItem(L("All Monitors", "Alle Monitore"));
            allItem.Checked = (selectedDisplay == null);
            allItem.Click += delegate { SelectDisplay(null); };
            settingsMenu.DropDownItems.Add(allItem);
            for (int idx = 0; idx < activeDisplays.Count; idx++)
            {
                DisplayInfo di = activeDisplays[idx];
                ToolStripMenuItem item = new ToolStripMenuItem(di.FriendlyName);
                item.Checked = (selectedDisplay == di.DeviceName);
                item.Click += delegate { SelectDisplay(di.DeviceName); };
                settingsMenu.DropDownItems.Add(item);
            }
        }

        settingsMenu.DropDownItems.Add(new ToolStripSeparator());

        ToolStripMenuItem notifItem = new ToolStripMenuItem(L("Notifications", "Benachrichtigungen"));
        notifItem.Checked = showNotifications;
        notifItem.Click += delegate { showNotifications = !showNotifications; SaveConfig(); BuildMenu(); };
        settingsMenu.DropDownItems.Add(notifItem);

        ToolStripMenuItem langItem = new ToolStripMenuItem(L("Deutsch", "English"));
        langItem.Click += delegate { language = (language == "en") ? "de" : "en"; SaveConfig(); BuildMenu(); };
        settingsMenu.DropDownItems.Add(langItem);

        // Force submenu RIGHT using native SetWindowPos (bypasses .NET layout completely)
        settingsMenu.DropDownOpened += delegate {
            ToolStripDropDown dd = settingsMenu.DropDown;
            ToolStrip parent = settingsMenu.GetCurrentParent();
            if (parent != null && dd.Visible)
            {
                // Use parent menu's location to determine the correct screen
                Rectangle screen = Screen.FromPoint(parent.Location).WorkingArea;
                Point rightOf = parent.PointToScreen(new Point(settingsMenu.Bounds.Right, settingsMenu.Bounds.Top));
                int x = rightOf.X;
                int y = rightOf.Y;
                // Clamp to right edge of THIS screen (not second monitor)
                if (x + dd.Width > screen.Right)
                    x = screen.Right - dd.Width;
                // Vertical
                if (y + dd.Height > screen.Bottom)
                    y = screen.Bottom - dd.Height;
                if (y < screen.Top)
                    y = screen.Top;
                SetWindowPos(dd.Handle, IntPtr.Zero, x, y, 0, 0, 0x0001 | 0x0004 | 0x0010);
            }
        };

        menu.Items.Add(settingsMenu);

        menu.Items.Add(new ToolStripSeparator());

        // License
        if (isProLicensed)
        {
            string licDisplay = "PRO \u2713 " + (licenseEmail.Length > 0 ? licenseEmail : "");
            if (licDisplay.Length > 40) licDisplay = licDisplay.Substring(0, 40);
            ToolStripMenuItem licItem = new ToolStripMenuItem(licDisplay);
            licItem.Enabled = false;
            menu.Items.Add(licItem);
        }
        else
        {
            ToolStripMenuItem licItem = new ToolStripMenuItem(L("Enter License...", "Lizenz eingeben..."));
            licItem.Click += delegate { PromptLicenseKey(); };
            menu.Items.Add(licItem);
        }

        menu.Items.Add(L("Exit", "Beenden"), null, (s, e) => ExitApp());

        // Don't assign ContextMenuStrip — we handle it manually for correct positioning
        trayIcon.MouseUp += delegate(object s, MouseEventArgs e) {
            if (e.Button == MouseButtons.Right)
            {
                // Calculate position above taskbar
                Rectangle workArea = Screen.FromPoint(Cursor.Position).WorkingArea;
                Point pos = Cursor.Position;
                int x = pos.X;
                int y = pos.Y - menu.Height;
                if (x + menu.Width > workArea.Right)
                    x = workArea.Right - menu.Width;
                if (y < workArea.Top)
                    y = workArea.Top;
                if (x < workArea.Left)
                    x = workArea.Left;
                // SetForegroundWindow ensures menu closes when clicking outside
                SetForegroundWindow(this.Handle);
                menu.Show(x, y);
            }
        };
    }

    void SelectDisplay(string deviceName)
    {
        selectedDisplay = deviceName;
        SaveConfig();
        BuildMenu();
        if (currentProfile > 1) ApplyProfile(currentProfile);
    }

    static void LoadBaseIcon()
    {
        try
        {
            // Try external Icon.png first, then fall back to icon embedded in EXE
            string iconPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Icon.png");
            if (File.Exists(iconPath))
            {
                using (var fs = new FileStream(iconPath, FileMode.Open, FileAccess.Read))
                    baseIconBmp = new Bitmap(fs);
            }
            else
            {
                Icon exeIcon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
                if (exeIcon != null)
                    baseIconBmp = exeIcon.ToBitmap();
            }
        }
        catch { }
    }

    static Icon MakeIcon(string text, Color bg)
    {
        using (Bitmap bmp = new Bitmap(16, 16))
        {
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                if (baseIconBmp != null)
                {
                    g.DrawImage(baseIconBmp, 0, 0, 16, 16);
                    using (Font font = new Font("Arial", 7, FontStyle.Bold))
                    using (SolidBrush bgBrush = new SolidBrush(Color.FromArgb(180, 0, 0, 0)))
                    using (SolidBrush fgBrush = new SolidBrush(bg))
                    {
                        SizeF sz = g.MeasureString(text, font);
                        float x = 16 - sz.Width; float y = 16 - sz.Height + 1;
                        g.FillRectangle(bgBrush, x - 1, y, sz.Width + 1, sz.Height - 1);
                        g.DrawString(text, font, fgBrush, x, y);
                    }
                }
                else
                {
                    g.Clear(bg);
                    using (Font font = new Font("Arial", 9, FontStyle.Bold))
                    {
                        SizeF sz = g.MeasureString(text, font);
                        g.DrawString(text, font, Brushes.White, (16 - sz.Width) / 2, (16 - sz.Height) / 2);
                    }
                }
            }
            IntPtr hIcon = bmp.GetHicon();
            Icon icon = Icon.FromHandle(hIcon).Clone() as Icon;
            DestroyIcon(hIcon);
            return icon;
        }
    }

    // === Gamma Ramp ===
    void SetGammaRamp(double gamma, double contrast)
    {
        RAMP ramp = new RAMP();
        ramp.Red = new ushort[256]; ramp.Green = new ushort[256]; ramp.Blue = new ushort[256];
        for (int i = 0; i < 256; i++)
        {
            double val = Math.Pow(i / 255.0, 1.0 / gamma);
            val = ((val - 0.5) * contrast) + 0.5;
            int r = (int)(val * 65535.0 + 0.5);
            if (r < 0) r = 0; if (r > 65535) r = 65535;
            ramp.Red[i] = ramp.Green[i] = ramp.Blue[i] = (ushort)r;
        }
        if (selectedDisplay != null)
        {
            IntPtr hDC = CreateDC(null, selectedDisplay, null, IntPtr.Zero);
            if (hDC != IntPtr.Zero) { SetDeviceGammaRamp(hDC, ref ramp); DeleteDC(hDC); }
        }
        else
        {
            foreach (DisplayInfo di in activeDisplays)
            {
                IntPtr hDC = CreateDC(null, di.DeviceName, null, IntPtr.Zero);
                if (hDC != IntPtr.Zero) { SetDeviceGammaRamp(hDC, ref ramp); DeleteDC(hDC); }
            }
        }
    }

    // === Apply Profile ===
    void ApplyProfile(int profile)
    {
        if (profile < 1 || profile > 9) return;
        ProfileData p = profiles[profile - 1];
        SetGammaRamp(p.Gamma, p.Contrast);
        SetSaturation(p.Vibrance);
        currentProfile = profile;

        if (!exiting)
        {
            // Clean white for all profiles — professional look
            Color c = Color.White;

            trayIcon.Text = "BrightRaider - " + L("Profile ", "Profil ") + profile + " (" + p.Name + ")";
            Icon oldIcon = trayIcon.Icon;
            trayIcon.Icon = MakeIcon(profile.ToString(), c);
            if (oldIcon != null) oldIcon.Dispose();

            ShowToast(L("Profile ", "Profil ") + profile + " (" + p.Name + ")");
        }
    }

    // === Exit ===
    void ExitApp()
    {
        exiting = true;
        StopAutoBrightness();
        if (brightnessOverlay != null) { try { brightnessOverlay.Close(); brightnessOverlay.Dispose(); } catch { } }
        if (measureFrameOverlay != null) { try { measureFrameOverlay.Close(); measureFrameOverlay.Dispose(); } catch { } }
        if (hookId != IntPtr.Zero) { UnhookWindowsHookEx(hookId); hookId = IntPtr.Zero; }

        string savedDisplay = selectedDisplay;
        selectedDisplay = null;
        SetGammaRamp(1.0, 1.0);
        SetSaturation(50);
        selectedDisplay = savedDisplay;

        if (adlReady && adlMainControlDestroy != null)
            try { adlMainControlDestroy(); } catch { }

        trayIcon.Visible = false;
        trayIcon.Dispose();
        Application.Exit();
    }

    protected override void OnFormClosing(FormClosingEventArgs e) { ExitApp(); base.OnFormClosing(e); }

    [STAThread]
    static void Main(string[] args)
    {
        if (args.Length > 0 && args[0] == "--setregistry")
        { Environment.Exit(SetGammaRegistryValue() ? 0 : 1); return; }

        Application.EnableVisualStyles();
        LoadBaseIcon();
        InitNvAPI();
        if (!nvApiReady) InitADL();

        if (!EnsureGammaRegistryKey())
            MessageBox.Show(
                "The registry entry for GammaRamp could not be set.\n" +
                "Please run as administrator or set the entry manually:\n\n" +
                "HKLM\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\ICM\n" +
                "GdiIcmGammaRange = 256 (DWORD)",
                "BrightRaider - Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        Application.Run(new BrightRaider());
    }
}
