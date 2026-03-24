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
using System.Net;
using System.Reflection;
using Microsoft.Win32;
// WinRT OCR — requires .NET 4.7.2 + Windows 10 SDK (Windows.winmd)
using System.Threading.Tasks;
using Windows.Globalization;
using Windows.Graphics.Imaging;
using Windows.Media.Ocr;
using Windows.Storage;
using Windows.Storage.Streams;

[assembly: AssemblyTitle("BrightRaider")]
[assembly: AssemblyDescription("Display brightness, contrast and vibrance switcher for gaming")]
[assembly: AssemblyCompany("BrightRaider")]
[assembly: AssemblyProduct("BrightRaider")]
[assembly: AssemblyCopyright("Copyright \u00a9 BrightRaider 2025-2026")]
[assembly: AssemblyVersion("9.1.0.0")]
[assembly: AssemblyFileVersion("9.1.0.0")]

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
    static extern short GetAsyncKeyState(int vk);

    [DllImport("user32.dll")]
    static extern bool PostMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

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
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate int NvAPI_GetDVCInfo_t(IntPtr handle, int outputId, ref NV_DISPLAY_DVC_INFO info);

    [StructLayout(LayoutKind.Sequential)]
    struct NV_DISPLAY_DVC_INFO { public int version; public int currentLevel; public int minLevel; public int maxLevel; }

    static NvAPI_Initialize_t nvInit;
    static NvAPI_EnumDisplay_t nvEnumDisplay;
    static NvAPI_SetDVCLevel_t nvSetDVC;
    static NvAPI_GetDVCInfo_t nvGetDVC;
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
    const uint LLKHF_UP       = 0x80; // key-up event in low-level hook
    const int VK_ADD = 0x6B;                                     // Numpad + (Crosshair-Toggle)
    const int VK_M   = 0x4D;                                     // M key (Map open/close)
    const string APP_VERSION = "9.1";

    // === OCR / Map Scanner State ===
    // Hotkey to trigger manual scan: Numpad * (0x6A) — works in-game
    const int VK_MULTIPLY = 0x6A;
    // How many scroll-down steps to zoom out the map (adjust to taste)
    const int MAP_SCROLL_STEPS = 5;
    // Delays (ms) — tune if the map is slow to open on your machine
    const int MAP_OPEN_WAIT_MS   = 300;
    const int MAP_SCROLL_WAIT_MS = 200;

    volatile bool ocrScanRunning = false;   // prevents overlapping scans
    bool          mKeyIsDown     = false;   // true while M is physically held (ignores auto-repeat)
    System.Threading.Timer mLongPressTimer  = null; // fires when M held long enough
    bool          ocrOverlayVisible = true; // toggle per Numpad*
    Form    ocrOverlay      = null;         // the evac-timer overlay window
    Windows.Media.Ocr.OcrEngine ocrEngine = null;  // WinRT OCR engine

    // Map Scanner settings (persisted in config)
    bool  mapScannerEnabled  = true;
    Color mapOverlayColor    = Color.Lime;
    int   mapOverlayPosition = 4;            // 0=ObenLinks 1=ObenMitte 2=ObenRechts 3=UntenLinks 4=UntenMitte 5=UntenRechts
    float mapOverlayFontSize = 11f;          // 8-18
    int   mapScanLongPressMs = 600;          // long-press threshold for M key auto-scan

    // Countdown state
    class EvacTimer { public string Name; public int SecondsLeft; public int RawMinutes = -1; }
    List<EvacTimer> activeTimers = null;
    System.Windows.Forms.Timer countdownTimer = null;

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

    // === State ===
    NotifyIcon trayIcon;
    int currentProfile = 1;
    bool exiting = false;
    bool isProLicensed = false;
    string licenseKey = "";
    string licenseEmail = "";
    string licenseToken = "";
    string lastSeenVersion = "";
    int originalVibrance = 50;
    bool hotkeysEnabled = true;

    string selectedDisplay = null;
    bool showNotifications = true;
    string language = "en";
    bool autoStart = false;
    bool autoBrightness = false;
    int[] zoneWeights = new int[] { 2, 1, 1, 1, 1 }; // Center, ObenLinks, ObenRechts, UntenLinks, UntenRechts
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
    bool crosshairVisible = false;
    Color crosshairColor = Color.Red;
    int crosshairSize = 20;
    int crosshairStyle = 0;               // 0=Cross 1=DotRing 2=TShape 3=Dot
    CrosshairOverlay crosshairOverlay = null;
    bool checkForUpdates = true;
    string updateAvailableVersion = "";

    string L(string en, string de) { return language == "de" ? de : en; }

    void ShowToast(string text, int duration = 1500)
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
            SizeF sz = g.MeasureString(text, toastLabel.Font, 400);
            toastForm.Size = new Size(Math.Max((int)sz.Width + 30, 220), (int)sz.Height + 20);
        }

        // Position: bottom-right above taskbar on selected display
        Rectangle workArea = GetTargetScreen().WorkingArea;
        toastForm.Location = new Point(workArea.Right - toastForm.Width - 10, workArea.Bottom - toastForm.Height - 10);

        toastForm.Show();

        // Auto-hide after 1.5 seconds
        if (toastTimer != null) { toastTimer.Stop(); toastTimer.Dispose(); }
        toastTimer = new System.Windows.Forms.Timer();
        toastTimer.Interval = duration;
        toastTimer.Tick += delegate { toastTimer.Stop(); toastTimer.Dispose(); toastTimer = null; if (toastForm != null && !toastForm.IsDisposed) toastForm.Hide(); };
        toastTimer.Start();
    }

    // === License Validation ===

    // Validate key via Lemon Squeezy validate endpoint
    static bool ValidateLicenseOnline(string key)
    {
        try
        {
            System.Net.ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)3072;
            string url = "https://api.lemonsqueezy.com/v1/licenses/validate";
            string body = "license_key=" + Uri.EscapeDataString(key);

            System.Net.HttpWebRequest req = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.Timeout = 10000;
            byte[] bodyBytes = Encoding.UTF8.GetBytes(body);
            req.ContentLength = bodyBytes.Length;
            using (Stream s = req.GetRequestStream()) s.Write(bodyBytes, 0, bodyBytes.Length);

            using (System.Net.HttpWebResponse resp = (System.Net.HttpWebResponse)req.GetResponse())
            using (StreamReader sr = new StreamReader(resp.GetResponseStream()))
            {
                string json = sr.ReadToEnd();
                if (json.Contains("\"valid\":true") && json.Contains("\"status\":\"active\""))
                    return true;
            }
        }
        catch { }
        return false;
    }

    // Activate online via Lemon Squeezy API (called once when user enters key)
    static bool ActivateLicenseOnline(string key, out string errorMsg)
    {
        errorMsg = "";
        try
        {
            System.Net.ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)3072; // TLS 1.2
            string url = "https://api.lemonsqueezy.com/v1/licenses/activate";
            string body = "license_key=" + Uri.EscapeDataString(key) +
                          "&instance_name=" + Uri.EscapeDataString("BrightRaider-" + Environment.MachineName);

            System.Net.HttpWebRequest req = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.Timeout = 10000;
            byte[] bodyBytes = Encoding.UTF8.GetBytes(body);
            req.ContentLength = bodyBytes.Length;
            using (Stream s = req.GetRequestStream()) s.Write(bodyBytes, 0, bodyBytes.Length);

            using (System.Net.HttpWebResponse resp = (System.Net.HttpWebResponse)req.GetResponse())
            using (StreamReader sr = new StreamReader(resp.GetResponseStream()))
            {
                string json = sr.ReadToEnd();
                if (json.Contains("\"activated\":true") || json.Contains("\"status\":\"active\""))
                    return true;
                // Activation failed — maybe already activated? Try validate as fallback
                if (ValidateLicenseOnline(key)) return true;

                // Extract error message
                int errIdx = json.IndexOf("\"error\":\"");
                if (errIdx >= 0)
                {
                    int start = errIdx + 9;
                    int end = json.IndexOf('"', start);
                    if (end > start) errorMsg = json.Substring(start, end - start);
                }
                if (errorMsg.Length == 0) errorMsg = "Key not valid.";
                return false;
            }
        }
        catch (System.Net.WebException ex)
        {
            // HTTP error (400, 403, etc.) — try validate as fallback
            if (ex.Response != null)
            {
                if (ValidateLicenseOnline(key)) return true;

                try
                {
                    using (StreamReader sr = new StreamReader(ex.Response.GetResponseStream()))
                    {
                        string json = sr.ReadToEnd();
                        int errIdx = json.IndexOf("\"error\":\"");
                        if (errIdx >= 0)
                        {
                            int start = errIdx + 9;
                            int end = json.IndexOf('"', start);
                            if (end > start) { errorMsg = json.Substring(start, end - start); return false; }
                        }
                    }
                }
                catch { }
            }
            errorMsg = "No internet connection. Please try again.";
            return false;
        }
        catch { errorMsg = "Activation failed. Please try again."; return false; }
    }

    // === New LS method that also returns email + instanceId for V8 .lic format ===
    static bool ActivateLicenseSqueezeLemon(string key, out string outEmail, out string outInstanceId, out string errorMsg)
    {
        outEmail = ""; outInstanceId = ""; errorMsg = "";
        try
        {
            System.Net.ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)3072; // TLS 1.2
            string url = "https://api.lemonsqueezy.com/v1/licenses/activate";
            string body = "license_key=" + Uri.EscapeDataString(key)
                        + "&instance_name=" + Uri.EscapeDataString("BrightRaider-" + Environment.MachineName);
            System.Net.HttpWebRequest req = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            req.Method = "POST"; req.ContentType = "application/x-www-form-urlencoded"; req.Timeout = 10000;
            byte[] bodyBytes = Encoding.UTF8.GetBytes(body);
            req.ContentLength = bodyBytes.Length;
            using (Stream s = req.GetRequestStream()) s.Write(bodyBytes, 0, bodyBytes.Length);
            try
            {
                using (System.Net.HttpWebResponse resp = (System.Net.HttpWebResponse)req.GetResponse())
                using (StreamReader sr = new StreamReader(resp.GetResponseStream()))
                {
                    string json = sr.ReadToEnd();
                    if (json.Contains("\"activated\":true") || json.Contains("\"status\":\"active\""))
                    {
                        // Extract instance id
                        int instIdx = json.IndexOf("\"instance\":{");
                        if (instIdx >= 0) outInstanceId = ExtractJsonString(json.Substring(instIdx), "id");
                        // Extract email
                        outEmail = ExtractJsonString(json, "user_email");
                        if (outEmail.Length == 0) outEmail = ExtractJsonString(json, "customer_email");
                        return true;
                    }
                    // Fallback: try validate
                    if (ValidateLicenseOnline(key)) return true;
                    errorMsg = ExtractJsonString(json, "error");
                    if (errorMsg.Length == 0) errorMsg = "Key not valid.";
                    return false;
                }
            }
            catch (System.Net.WebException ex)
            {
                if (ex.Response != null)
                {
                    if (ValidateLicenseOnline(key)) return true;
                    try
                    {
                        using (StreamReader sr = new StreamReader(ex.Response.GetResponseStream()))
                        {
                            string errJson = sr.ReadToEnd();
                            errorMsg = ExtractJsonString(errJson, "error");
                            if (errorMsg.Length == 0) errorMsg = "Key not valid.";
                        }
                    }
                    catch { }
                    return false;
                }
                errorMsg = "No internet connection. Please try again.";
                return false;
            }
        }
        catch { errorMsg = "Activation failed. Please try again."; return false; }
    }

    // Validate locally from saved .lic file (no internet needed)
    static bool ValidateLicenseLocal(string key)
    {
        // Key must be non-empty and match what was saved
        return key.Length > 8;
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

    // === V8 Crypto Seeds ===
    const string LIC_SEED_V7 = "BrightRaider-v7-Pro-2025-LicFile"; // Migration only
    const string CFG_SEED_V7 = "BrightRaider-v7-Config-2025";      // Migration only
    const string LIC_SEED_V8 = "BrightRaider-v8-Pro-2026-LicFile";
    const string CFG_SEED_V8 = "BrightRaider-v8-Config-2026";

    static byte[] GetHmacSeedBytes()
    {
        // Encodes "BrightRaider-v8-Pro-HMAC-2026" XOR 0xA5
        // No readable string literal in decompiled output
        byte[] x = new byte[] {
            0xE7,0xD7,0xCC,0xC2,0xCD,0xD1,0xF7,0xC4,0xCC,0xC1,0xC0,0xD7,
            0x88,0xD3,0x9D,0x88,0xF5,0xD7,0xCA,0x88,0xED,0xE8,0xE4,0xE6,
            0x88,0x97,0x95,0x97,0x93
        };
        byte[] r = new byte[x.Length];
        for (int i = 0; i < x.Length; i++) r[i] = (byte)(x[i] ^ 0xA5);
        return r;
    }

    static string ComputeLicenseHmac(string email, string key, string platform, string token)
    {
        byte[] seed = GetHmacSeedBytes();
        string payload = email + "|" + key + "|" + platform + "|" + token;
        using (System.Security.Cryptography.HMACSHA256 hmac = new System.Security.Cryptography.HMACSHA256(seed))
        {
            byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(payload));
            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }
    }

    static string ExtractJsonString(string json, string field)
    {
        string needle = "\"" + field + "\":\"";
        int idx = json.IndexOf(needle);
        if (idx < 0) return "";
        int start = idx + needle.Length;
        int end = json.IndexOf('"', start);
        return end > start ? json.Substring(start, end - start) : "";
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
            string hmac = ComputeLicenseHmac(licenseEmail, licenseKey, "ls", licenseToken);
            string payload = licenseEmail + "\n"
                           + licenseKey + "\n"
                           + "ls\n"
                           + licenseToken + "\n"
                           + hmac;
            File.WriteAllBytes(licFilePath, AesEncrypt(Encoding.UTF8.GetBytes(payload), LIC_SEED_V8));
        }
        catch { }
    }

    void LoadLicenseFile()
    {
        try
        {
            if (!File.Exists(licFilePath)) return;
            byte[] cipher = File.ReadAllBytes(licFilePath);

            string payload = null;
            bool isV7Migration = false;

            // Try V8 seed first
            try { payload = Encoding.UTF8.GetString(AesDecrypt(cipher, LIC_SEED_V8)); }
            catch { }

            // Fall back to V7 seed (migration)
            if (payload == null)
            {
                try
                {
                    payload = Encoding.UTF8.GetString(AesDecrypt(cipher, LIC_SEED_V7));
                    isV7Migration = true;
                }
                catch { return; }
            }

            string[] parts = payload.Split('\n');

            if (isV7Migration)
            {
                // Old format: email\nkey — trust once, re-save in V8 format immediately
                if (parts.Length >= 2 && parts[1].Trim().Length > 8)
                {
                    licenseEmail    = parts[0].Trim();
                    licenseKey      = parts[1].Trim();
                    licenseToken    = "";
                    isProLicensed   = true;
                    SaveLicenseFile(); // re-save in V8 format with HMAC
                }
                return;
            }

            // V8 format: email\nkey\nplatform\ntoken\nhmac
            if (parts.Length < 5) return;

            string email       = parts[0].Trim();
            string key         = parts[1].Trim();
            string platform    = parts[2].Trim();
            string token       = parts[3].Trim();
            string storedHmac  = parts[4].Trim();

            // HMAC verification — reject tampered .lic files
            string expectedHmac = ComputeLicenseHmac(email, key, platform, token);
            if (!string.Equals(storedHmac, expectedHmac, StringComparison.OrdinalIgnoreCase))
                return; // HMAC mismatch → reject

            if (key.Length > 8)
            {
                licenseEmail    = email;
                licenseKey      = key;
                licenseToken    = token;
                isProLicensed   = true;
            }
        }
        catch { }
    }

    void ShowProRequired()
    {
        MessageBox.Show(
            L("This feature requires BrightRaider Pro.\n\nGet your license at:\nhttps://brightraider.lemonsqueezy.com/checkout/buy/9b93d8c0-262f-43a4-bd41-167557efb156",
              "Diese Funktion erfordert BrightRaider Pro.\n\nLizenz erhalten:\nhttps://brightraider.lemonsqueezy.com/checkout/buy/9b93d8c0-262f-43a4-bd41-167557efb156"),
            "BrightRaider Pro",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    void PromptLicenseKey()
    {
        Form dialog = new Form();
        dialog.Text = "BrightRaider — " + L("Enter License", "Lizenz eingeben");
        dialog.Size = new System.Drawing.Size(440, 260);
        dialog.FormBorderStyle = FormBorderStyle.FixedDialog;
        dialog.StartPosition = FormStartPosition.CenterScreen;
        dialog.MaximizeBox = false; dialog.MinimizeBox = false;

        Label lblEmail = new Label() { Text = "Email:", Location = new System.Drawing.Point(15, 15), AutoSize = true };
        TextBox txtEmail = new TextBox() { Location = new System.Drawing.Point(15, 35), Size = new System.Drawing.Size(395, 22), Text = licenseEmail };

        Label lblKey = new Label() { Text = L("License Key:", "Lizenzschlüssel:"), Location = new System.Drawing.Point(15, 67), AutoSize = true };
        TextBox txtKey = new TextBox() { Location = new System.Drawing.Point(15, 87), Size = new System.Drawing.Size(395, 22), Text = licenseKey };

        Label lblBuyLS = new Label() { Text = L("Buy on Lemon Squeezy: https://brightraider.lemonsqueezy.com/checkout/buy/9b93d8c0-262f-43a4-bd41-167557efb156", "Kaufen auf Lemon Squeezy: https://brightraider.lemonsqueezy.com/checkout/buy/9b93d8c0-262f-43a4-bd41-167557efb156"), Location = new System.Drawing.Point(15, 120), Size = new System.Drawing.Size(395, 34), ForeColor = System.Drawing.Color.Gray };
        lblBuyLS.Font = new System.Drawing.Font(lblBuyLS.Font.FontFamily, 7.5f);

        Button btnOk = new Button() { Text = "OK", Location = new System.Drawing.Point(245, 175), Size = new System.Drawing.Size(75, 28), DialogResult = DialogResult.OK };
        Button btnCancel = new Button() { Text = L("Cancel", "Abbrechen"), Location = new System.Drawing.Point(330, 175), Size = new System.Drawing.Size(90, 28), DialogResult = DialogResult.Cancel };

        dialog.Controls.AddRange(new System.Windows.Forms.Control[] { lblEmail, txtEmail, lblKey, txtKey, lblBuyLS, btnOk, btnCancel });
        dialog.AcceptButton = btnOk; dialog.CancelButton = btnCancel;

        if (dialog.ShowDialog() == DialogResult.OK)
        {
            string email = txtEmail.Text.Trim();
            string key = txtKey.Text.Trim();

            if (key.Length < 8)
            {
                MessageBox.Show(L("Please enter your license key.", "Bitte Lizenzschlüssel eingeben."), "BrightRaider", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                dialog.Cursor = Cursors.WaitCursor;
                string errMsg = "", outEmail = email, outToken = "";
                string instanceId;
                bool ok = ActivateLicenseSqueezeLemon(key, out outEmail, out instanceId, out errMsg);
                outToken = instanceId;
                if (ok && outEmail.Length == 0) outEmail = email;
                dialog.Cursor = Cursors.Default;

                if (ok)
                {
                    isProLicensed = true;
                    licenseEmail = outEmail.Length > 0 ? outEmail : email;
                    licenseKey = key;
                    licenseToken = outToken;
                    SaveLicenseFile();
                    BuildMenu();
                    MessageBox.Show(
                        L("License activated!\nRegistered to: ", "Lizenz aktiviert!\nRegistriert auf: ") + licenseEmail + "\n\n" +
                        L("All Pro features are now unlocked.", "Alle Pro-Funktionen sind freigeschaltet."),
                        "BrightRaider Pro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(
                        L("Activation failed: ", "Aktivierung fehlgeschlagen: ") + errMsg + "\n\n" +
                        L("Check your key and internet connection.", "Bitte Key und Internetverbindung prüfen."),
                        "BrightRaider", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
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
            IntPtr pGetDVC = QueryInterface(NVAPI_ID_GET_DVC_INFO);
            if (pEnum == IntPtr.Zero || pSetDVC == IntPtr.Zero) return false;
            nvEnumDisplay = (NvAPI_EnumDisplay_t)Marshal.GetDelegateForFunctionPointer(pEnum, typeof(NvAPI_EnumDisplay_t));
            nvSetDVC = (NvAPI_SetDVCLevel_t)Marshal.GetDelegateForFunctionPointer(pSetDVC, typeof(NvAPI_SetDVCLevel_t));
            if (pGetDVC != IntPtr.Zero)
                nvGetDVC = (NvAPI_GetDVCInfo_t)Marshal.GetDelegateForFunctionPointer(pGetDVC, typeof(NvAPI_GetDVCInfo_t));
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
    void ReadCurrentVibrance()
    {
        int targetIdx = -1;
        if (selectedDisplay != null)
            for (int i = 0; i < activeDisplays.Count; i++)
                if (activeDisplays[i].DeviceName == selectedDisplay) { targetIdx = i; break; }

        try
        {
            if (nvApiReady && nvGetDVC != null)
            {
                int idx = 0; IntPtr handle = IntPtr.Zero;
                while (nvEnumDisplay(idx, ref handle) == 0)
                {
                    if (targetIdx < 0 || targetIdx == idx)
                    {
                        NV_DISPLAY_DVC_INFO info = new NV_DISPLAY_DVC_INFO();
                        info.version = Marshal.SizeOf(typeof(NV_DISPLAY_DVC_INFO)) | (1 << 16);
                        if (nvGetDVC(handle, 0, ref info) == 0)
                        {
                            int range = info.maxLevel - info.minLevel;
                            if (range > 0)
                                originalVibrance = 50 + (int)((info.currentLevel - info.minLevel) * 50.0 / range);
                        }
                        break;
                    }
                    idx++;
                }
            }
            else if (adlReady && adlDisplayColorGet != null && adlDisplayTargets.Count > 0)
            {
                int ti = (targetIdx >= 0 && targetIdx < adlDisplayTargets.Count) ? targetIdx : 0;
                ADLDisplayTarget t = adlDisplayTargets[ti];
                int current = 0, def = 0, min = 0, max = 0, step = 0;
                if (adlDisplayColorGet(t.AdapterIndex, t.DisplayIndex, ADL_DISPLAY_COLOR_SATURATION, ref current, ref def, ref min, ref max, ref step) == ADL_OK)
                {
                    int range = max - min;
                    if (range > 0)
                        originalVibrance = (int)((current - min) * 100.0 / range);
                }
            }
        }
        catch { }
    }

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
                string decrypted;
                try { decrypted = Encoding.UTF8.GetString(AesDecrypt(cipher, CFG_SEED_V8)); }
                catch { decrypted = Encoding.UTF8.GetString(AesDecrypt(cipher, CFG_SEED_V7)); }
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
                else if (k.StartsWith("ZoneWeight")) { int idx; int w; if (int.TryParse(k.Substring(10), out idx) && idx >= 0 && idx < 5 && int.TryParse(v, out w)) zoneWeights[idx] = Math.Max(0, Math.Min(10, w)); }
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
                else if (k == "CrosshairVisible") crosshairVisible = v == "1";
                else if (k == "OcrOverlayVisible") ocrOverlayVisible = v != "0";
                else if (k == "CrosshairColor") { int argb; if (int.TryParse(v, out argb)) crosshairColor = System.Drawing.Color.FromArgb(argb); }
                else if (k == "CrosshairSize") { int sz; if (int.TryParse(v, out sz) && sz >= 10 && sz <= 50) crosshairSize = sz; }
                else if (k == "CrosshairStyle") { int st; if (int.TryParse(v, out st) && st >= 0 && st <= 3) crosshairStyle = st; }
                else if (k == "CheckForUpdates") checkForUpdates = v == "1";
                else if (k == "LastSeenVer") lastSeenVersion = v;
                else if (k == "HotkeysEnabled") hotkeysEnabled = v != "0";
                else if (k == "MapScannerEnabled") mapScannerEnabled = v != "0";
                else if (k == "MapOverlayColor") { int argb; if (int.TryParse(v, out argb)) mapOverlayColor = System.Drawing.Color.FromArgb(argb); }
                else if (k == "MapOverlayPosition") { int p; if (int.TryParse(v, out p) && p >= 0 && p <= 5) mapOverlayPosition = p; }
                else if (k == "MapOverlayFontSize") { float f; if (float.TryParse(v, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out f) && f >= 8f && f <= 18f) mapOverlayFontSize = f; }
                else if (k == "MapScanLongPressMs") { int ms; if (int.TryParse(v, out ms) && ms >= 200 && ms <= 1500) mapScanLongPressMs = ms; }
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
            for (int i = 0; i < 5; i++) lines.Add("ZoneWeight" + i + "=" + zoneWeights[i]);
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
            lines.Add("CrosshairVisible=" + (crosshairVisible ? "1" : "0"));
            lines.Add("OcrOverlayVisible=" + (ocrOverlayVisible ? "1" : "0"));
            lines.Add("CrosshairColor=" + crosshairColor.ToArgb().ToString());
            lines.Add("CrosshairSize=" + crosshairSize.ToString());
            lines.Add("CrosshairStyle=" + crosshairStyle.ToString());
            lines.Add("CheckForUpdates=" + (checkForUpdates ? "1" : "0"));
            lines.Add("LastSeenVer=" + lastSeenVersion);
            lines.Add("HotkeysEnabled=" + (hotkeysEnabled ? "1" : "0"));
            lines.Add("MapScannerEnabled=" + (mapScannerEnabled ? "1" : "0"));
            lines.Add("MapOverlayColor=" + mapOverlayColor.ToArgb().ToString());
            lines.Add("MapOverlayPosition=" + mapOverlayPosition.ToString());
            lines.Add("MapOverlayFontSize=" + mapOverlayFontSize.ToString("F0", System.Globalization.CultureInfo.InvariantCulture));
            lines.Add("MapScanLongPressMs=" + mapScanLongPressMs.ToString());
            string content = string.Join("\n", lines.ToArray());
            File.WriteAllBytes(configPath, AesEncrypt(Encoding.UTF8.GetBytes(content), CFG_SEED_V8));
        }
        catch { }
    }

    // === Keyboard Hook ===
    IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
    {

        if (!hotkeysEnabled)
            return CallNextHookEx(hookId, nCode, wParam, lParam);

        // M keydown: start long-press timer on FIRST press (ignore auto-repeat)
        if (nCode >= 0 && (wParam == (IntPtr)WM_KEYDOWN || wParam == (IntPtr)WM_SYSKEYDOWN) && isProLicensed && mapScannerEnabled)
        {
            KBDLLHOOKSTRUCT kbM = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));
            if ((int)kbM.vkCode == VK_M && !mKeyIsDown && !ocrScanRunning)
            {
                mKeyIsDown = true;
                // Timer feuert nach Haltezeit → Scan startet WÄHREND M noch gedrückt ist
                System.Threading.Timer t = null;
                t = new System.Threading.Timer(delegate {
                    t.Dispose();
                    if (mKeyIsDown && !exiting && !this.IsDisposed)
                        this.BeginInvoke(new Action(delegate {
                            if (!ocrOverlayVisible) { ocrOverlayVisible = true; SaveConfig(); BuildMenu(); }
                            TriggerMapScan(manual: false);
                        }));
                }, null, mapScanLongPressMs, System.Threading.Timeout.Infinite);
                mLongPressTimer = t;
            }
        }
        // M keyup: cancel timer (falls M vor Ablauf losgelassen)
        if (nCode >= 0 && wParam == (IntPtr)0x0101 && isProLicensed && mapScannerEnabled)
        {
            KBDLLHOOKSTRUCT kbM = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));
            if ((int)kbM.vkCode == VK_M)
            {
                mKeyIsDown = false;
                System.Threading.Timer t = mLongPressTimer;
                if (t != null) { mLongPressTimer = null; t.Dispose(); }
            }
        }
        // Numpad* keydown: toggle OCR overlay
        if (nCode >= 0 && (wParam == (IntPtr)WM_KEYDOWN || wParam == (IntPtr)WM_SYSKEYDOWN) && isProLicensed && mapScannerEnabled)
        {
            KBDLLHOOKSTRUCT kbS = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));
            if ((int)kbS.vkCode == VK_MULTIPLY)
            {
                if (!exiting && !this.IsDisposed)
                    this.BeginInvoke(new Action(delegate { ToggleOcrOverlay(); }));
            }
        }
        if (nCode >= 0 && (wParam == (IntPtr)WM_KEYDOWN || wParam == (IntPtr)WM_SYSKEYDOWN))
        {
            KBDLLHOOKSTRUCT kb = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));
            int profile = 0;
            bool isMute = false;
            bool isCrosshair = false;

            if ((int)kb.vkCode == VK_ADD)
            {
                isCrosshair = true;
            }
            else if ((int)kb.vkCode == VK_MULTIPLY && isProLicensed && mapScannerEnabled)
            {
                // handled below via long-press logic
            }
            // Numpad with NumLock ON
            else if ((int)kb.vkCode >= VK_NUMPAD0 && (int)kb.vkCode <= VK_NUMPAD9)
            {
                int num = (int)kb.vkCode - VK_NUMPAD0;
                if (num == 0) isMute = true;
                else profile = num;
            }
            // Numpad with NumLock OFF — also fired by Shift+Numpad (NumLock ON) because Windows
            // strips Shift and sends cursor VKs. We restore Shift after swallowing so game sprint continues.
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

            if (isCrosshair && isProLicensed)
            {
                this.BeginInvoke(new Action(delegate { ToggleCrosshair(); }));
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
    const int WS_EX_LAYERED     = 0x80000;
    const int WS_EX_TRANSPARENT = 0x20;
    const int WS_EX_TOOLWINDOW  = 0x80;     // hides from Alt+Tab

    void ToggleBrightnessOverlay()
    {
        if (brightnessOverlay != null && brightnessOverlay.Visible)
        {
            brightnessOverlay.Hide();
            if (measureFrameOverlay != null) measureFrameOverlay.Hide();
            return;
        }

        Screen targetScreen = GetTargetScreen();
        Rectangle bounds = targetScreen.Bounds;
        int screenW = bounds.Width;
        int screenH = bounds.Height;

        // Create measurement zone overlay (5 zones, click-through)
        if (measureFrameOverlay == null || measureFrameOverlay.IsDisposed)
        {
            measureFrameOverlay = new Form();
            measureFrameOverlay.FormBorderStyle = FormBorderStyle.None;
            measureFrameOverlay.TopMost = true;
            measureFrameOverlay.ShowInTaskbar = false;
            measureFrameOverlay.TransparencyKey = Color.Magenta;
            measureFrameOverlay.BackColor = Color.Magenta;
            // Full screen overlay on selected display
            measureFrameOverlay.Location = new Point(bounds.X, bounds.Y);
            measureFrameOverlay.Size = new Size(screenW, screenH);
            measureFrameOverlay.StartPosition = FormStartPosition.Manual;

            // Draw 5 measurement zone rectangles (local coordinates, no offset)
            measureFrameOverlay.Paint += delegate(object s, PaintEventArgs pe) {
                int[][] zones = GetMeasurementZones(screenW, screenH, 0, 0);
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
            brightnessOverlay.Location = new Point(bounds.Right - 330, bounds.Top + 10);

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

    void ToggleOcrOverlay()
    {
        ocrOverlayVisible = !ocrOverlayVisible;
        if (!ocrOverlayVisible)
        {
            if (ocrOverlay != null && !ocrOverlay.IsDisposed) ocrOverlay.Hide();
        }
        else
        {
            if (activeTimers != null) UpdateOcrOverlay();
        }
        SaveConfig();
        BuildMenu();
        ShowToast(ocrOverlayVisible ? L("Timer overlay ON", "Timer-Overlay AN") : L("Timer overlay OFF", "Timer-Overlay AUS"));
    }

    void ToggleCrosshair()
    {
        if (!isProLicensed) { ShowProRequired(); return; }
        crosshairVisible = !crosshairVisible;
        if (crosshairVisible)
        {
            if (crosshairOverlay == null || crosshairOverlay.IsDisposed)
                crosshairOverlay = new CrosshairOverlay(GetTargetScreen(), crosshairColor, crosshairSize, (CrosshairOverlay.CrosshairStyle)crosshairStyle);
            crosshairOverlay.Show();
        }
        else
        {
            if (crosshairOverlay != null && !crosshairOverlay.IsDisposed)
                crosshairOverlay.Hide();
        }
        ShowToast(crosshairVisible ? L("Crosshair ON", "Fadenkreuz AN") : L("Crosshair OFF", "Fadenkreuz AUS"));
        SaveConfig();
        BuildMenu();
    }

    void ShowCrosshairSettings()
    {
        // Ensure overlay exists for live preview
        bool hadOverlay = crosshairOverlay != null && !crosshairOverlay.IsDisposed && crosshairOverlay.Visible;
        if (crosshairVisible && (crosshairOverlay == null || crosshairOverlay.IsDisposed))
        {
            crosshairOverlay = new CrosshairOverlay(GetTargetScreen(), crosshairColor, crosshairSize, (CrosshairOverlay.CrosshairStyle)crosshairStyle);
            crosshairOverlay.Show();
        }

        // Working copies for live preview (only commit on OK)
        System.Drawing.Color previewColor = crosshairColor;

        System.Windows.Forms.Form dlg = new System.Windows.Forms.Form();
        dlg.Text = "BrightRaider — " + L("Crosshair Settings", "Fadenkreuz-Einstellungen");
        dlg.Size = new System.Drawing.Size(370, 270);
        dlg.FormBorderStyle = FormBorderStyle.FixedDialog;
        dlg.StartPosition = FormStartPosition.CenterScreen;
        dlg.MaximizeBox = false; dlg.MinimizeBox = false;

        System.Windows.Forms.CheckBox chkEnable = new System.Windows.Forms.CheckBox() {
            Text = L("Enable Crosshair", "Fadenkreuz aktivieren"),
            Location = new System.Drawing.Point(15, 15), AutoSize = true, Checked = crosshairVisible
        };

        System.Windows.Forms.Button btnColor = new System.Windows.Forms.Button() {
            Text = L("Color...", "Farbe..."), Location = new System.Drawing.Point(15, 50),
            Size = new System.Drawing.Size(90, 28), BackColor = crosshairColor
        };

        System.Windows.Forms.Label lblSize = new System.Windows.Forms.Label() {
            Text = L("Size:", "Größe:"), Location = new System.Drawing.Point(15, 95), AutoSize = true
        };
        System.Windows.Forms.TrackBar tbSize = new System.Windows.Forms.TrackBar() {
            Location = new System.Drawing.Point(15, 112), Size = new System.Drawing.Size(220, 40),
            Minimum = 10, Maximum = 50, Value = Math.Max(10, Math.Min(50, crosshairSize)),
            TickFrequency = 5, TickStyle = System.Windows.Forms.TickStyle.BottomRight
        };
        System.Windows.Forms.Label lblSizeVal = new System.Windows.Forms.Label() {
            Location = new System.Drawing.Point(240, 118), AutoSize = true, Text = tbSize.Value + "px"
        };

        System.Windows.Forms.Label lblStyle = new System.Windows.Forms.Label() {
            Text = L("Style:", "Stil:"), Location = new System.Drawing.Point(15, 162), AutoSize = true
        };
        string[] styleNames = new string[] { "Cross", "Dot + Ring", "T-Shape", "Dot" };
        System.Windows.Forms.ComboBox cboStyle = new System.Windows.Forms.ComboBox() {
            Location = new System.Drawing.Point(15, 180), Size = new System.Drawing.Size(150, 22),
            DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        };
        cboStyle.Items.AddRange(styleNames);
        cboStyle.SelectedIndex = Math.Max(0, Math.Min(3, crosshairStyle));

        // Events — alle Controls sind jetzt deklariert
        btnColor.Click += delegate {
            using (System.Windows.Forms.ColorDialog cd = new System.Windows.Forms.ColorDialog()) {
                cd.Color = previewColor;
                if (cd.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                    previewColor = cd.Color;
                    btnColor.BackColor = cd.Color;
                    if (crosshairOverlay != null) crosshairOverlay.UpdateSettings(previewColor, tbSize.Value, (CrosshairOverlay.CrosshairStyle)cboStyle.SelectedIndex);
                }
            }
        };
        tbSize.ValueChanged += delegate {
            lblSizeVal.Text = tbSize.Value + "px";
            if (crosshairOverlay != null) crosshairOverlay.UpdateSettings(previewColor, tbSize.Value, (CrosshairOverlay.CrosshairStyle)cboStyle.SelectedIndex);
        };
        cboStyle.SelectedIndexChanged += delegate {
            if (crosshairOverlay != null) crosshairOverlay.UpdateSettings(previewColor, tbSize.Value, (CrosshairOverlay.CrosshairStyle)cboStyle.SelectedIndex);
        };

        System.Windows.Forms.Button btnOk = new System.Windows.Forms.Button() {
            Text = "OK", Location = new System.Drawing.Point(185, 195),
            Size = new System.Drawing.Size(75, 28), DialogResult = System.Windows.Forms.DialogResult.OK
        };
        System.Windows.Forms.Button btnCancel = new System.Windows.Forms.Button() {
            Text = L("Cancel", "Abbrechen"), Location = new System.Drawing.Point(268, 195),
            Size = new System.Drawing.Size(85, 28), DialogResult = System.Windows.Forms.DialogResult.Cancel
        };

        // On cancel: restore preview to original
        dlg.FormClosing += delegate(object s2, System.Windows.Forms.FormClosingEventArgs e2) {
            if (dlg.DialogResult != System.Windows.Forms.DialogResult.OK)
                if (crosshairOverlay != null) crosshairOverlay.UpdateSettings(crosshairColor, crosshairSize, (CrosshairOverlay.CrosshairStyle)crosshairStyle);
        };

        dlg.Controls.AddRange(new System.Windows.Forms.Control[] {
            chkEnable, btnColor, lblSize, tbSize, lblSizeVal, lblStyle, cboStyle, btnOk, btnCancel
        });
        dlg.AcceptButton = btnOk; dlg.CancelButton = btnCancel;

        if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        {
            crosshairColor = previewColor;
            crosshairSize = tbSize.Value;
            crosshairStyle = cboStyle.SelectedIndex;
            bool wasVisible = crosshairVisible;
            crosshairVisible = chkEnable.Checked;

            // Rebuild overlay with final settings
            if (crosshairOverlay != null && !crosshairOverlay.IsDisposed)
            { crosshairOverlay.Close(); crosshairOverlay.Dispose(); crosshairOverlay = null; }

            if (crosshairVisible)
            {
                crosshairOverlay = new CrosshairOverlay(GetTargetScreen(), crosshairColor, crosshairSize, (CrosshairOverlay.CrosshairStyle)crosshairStyle);
                crosshairOverlay.Show();
            }
            SaveConfig();
            BuildMenu();
        }
        else
        {
            // Cancelled — if overlay wasn't visible before, hide it
            if (!hadOverlay && crosshairOverlay != null && !crosshairOverlay.IsDisposed)
                crosshairOverlay.Hide();
        }
        dlg.Dispose();
    }

    void ShowMapScannerSettings()
    {
        // Save originals for cancel/restore
        Color origColor = mapOverlayColor;
        int origPos = mapOverlayPosition;
        float origFont = mapOverlayFontSize;
        int origPressMs = mapScanLongPressMs;

        // Save active timers, show preview with sample data
        List<EvacTimer> savedTimers = activeTimers;
        string savedMapName = countdownMapName;

        System.Windows.Forms.Form dlg = new System.Windows.Forms.Form();
        dlg.Text = "BrightRaider — Map Scanner";
        dlg.Size = new System.Drawing.Size(370, 370);
        dlg.FormBorderStyle = FormBorderStyle.FixedDialog;
        dlg.StartPosition = FormStartPosition.CenterScreen;
        dlg.MaximizeBox = false; dlg.MinimizeBox = false;

        System.Windows.Forms.CheckBox chkEnable = new System.Windows.Forms.CheckBox() {
            Text = L("Enable Map Scanner (M key)", "Map Scanner aktivieren (M-Taste)"),
            Location = new System.Drawing.Point(15, 15), AutoSize = true, Checked = mapScannerEnabled
        };

        System.Windows.Forms.Label lblColor = new System.Windows.Forms.Label() {
            Text = L("Overlay Color:", "Overlay-Farbe:"), Location = new System.Drawing.Point(15, 50), AutoSize = true
        };
        System.Windows.Forms.Button btnColor = new System.Windows.Forms.Button() {
            Text = L("Color...", "Farbe..."), Location = new System.Drawing.Point(15, 70),
            Size = new System.Drawing.Size(90, 28), BackColor = mapOverlayColor
        };

        System.Windows.Forms.Label lblPos = new System.Windows.Forms.Label() {
            Text = L("Position:", "Position:"), Location = new System.Drawing.Point(15, 112), AutoSize = true
        };
        string[] positions = new string[] {
            L("Top-Left","Oben-Links"), L("Top-Center","Oben-Mitte"), L("Top-Right","Oben-Rechts"),
            L("Bottom-Left","Unten-Links"), L("Bottom-Center","Unten-Mitte"), L("Bottom-Right","Unten-Rechts")
        };
        System.Windows.Forms.ComboBox cboPos = new System.Windows.Forms.ComboBox() {
            Location = new System.Drawing.Point(15, 130), Size = new System.Drawing.Size(180, 22),
            DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        };
        cboPos.Items.AddRange(positions);
        cboPos.SelectedIndex = Math.Max(0, Math.Min(5, mapOverlayPosition));

        System.Windows.Forms.Label lblFont = new System.Windows.Forms.Label() {
            Text = L("Font Size:", "Schriftgröße:"), Location = new System.Drawing.Point(15, 165), AutoSize = true
        };
        System.Windows.Forms.TrackBar tbFont = new System.Windows.Forms.TrackBar() {
            Location = new System.Drawing.Point(15, 183), Size = new System.Drawing.Size(220, 40),
            Minimum = 8, Maximum = 18, Value = Math.Max(8, Math.Min(18, (int)mapOverlayFontSize)),
            TickFrequency = 2, TickStyle = System.Windows.Forms.TickStyle.BottomRight
        };
        System.Windows.Forms.Label lblFontVal = new System.Windows.Forms.Label() {
            Location = new System.Drawing.Point(240, 189), AutoSize = true, Text = tbFont.Value + "pt"
        };

        System.Windows.Forms.Label lblPress = new System.Windows.Forms.Label() {
            Text = L("Long-press time:", "Haltezeit:"), Location = new System.Drawing.Point(15, 232), AutoSize = true
        };
        int initPressVal = Math.Max(200, Math.Min(1500, (mapScanLongPressMs / 50) * 50));
        System.Windows.Forms.TrackBar tbPress = new System.Windows.Forms.TrackBar() {
            Location = new System.Drawing.Point(15, 250), Size = new System.Drawing.Size(220, 40),
            Minimum = 200, Maximum = 1500, Value = initPressVal,
            TickFrequency = 100, TickStyle = System.Windows.Forms.TickStyle.BottomRight,
            SmallChange = 50, LargeChange = 100
        };
        System.Windows.Forms.Label lblPressVal = new System.Windows.Forms.Label() {
            Location = new System.Drawing.Point(240, 256), AutoSize = true, Text = tbPress.Value + "ms"
        };

        // Live preview helper
        System.Action showPreview = delegate {
            activeTimers = new List<EvacTimer> {
                new EvacTimer { Name = "Station A", SecondsLeft = 720 },
                new EvacTimer { Name = "Station B", SecondsLeft = 400 },
                new EvacTimer { Name = "Station C", SecondsLeft = 90 },
                new EvacTimer { Name = "Station D", SecondsLeft = 30 },
            };
            countdownMapName = "Preview";
            UpdateOcrOverlay();
            activeTimers = savedTimers;
            countdownMapName = savedMapName;
        };

        // Live preview: update overlay immediately on any change
        btnColor.Click += delegate {
            using (System.Windows.Forms.ColorDialog cd = new System.Windows.Forms.ColorDialog()) {
                cd.Color = btnColor.BackColor;
                if (cd.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                    btnColor.BackColor = cd.Color;
                    mapOverlayColor = cd.Color;
                    showPreview();
                }
            }
        };
        cboPos.SelectedIndexChanged += delegate {
            mapOverlayPosition = cboPos.SelectedIndex;
            showPreview();
        };
        tbFont.ValueChanged += delegate {
            lblFontVal.Text = tbFont.Value + "pt";
            mapOverlayFontSize = (float)tbFont.Value;
            showPreview();
        };
        tbPress.ValueChanged += delegate {
            // snap to nearest 50ms
            int snapped = (tbPress.Value / 50) * 50;
            if (tbPress.Value != snapped) tbPress.Value = snapped;
            lblPressVal.Text = snapped + "ms";
            mapScanLongPressMs = snapped;
        };

        System.Windows.Forms.Button btnOk = new System.Windows.Forms.Button() {
            Text = "OK", Location = new System.Drawing.Point(185, 298),
            Size = new System.Drawing.Size(75, 28), DialogResult = System.Windows.Forms.DialogResult.OK
        };
        System.Windows.Forms.Button btnCancel = new System.Windows.Forms.Button() {
            Text = L("Cancel", "Abbrechen"), Location = new System.Drawing.Point(268, 298),
            Size = new System.Drawing.Size(85, 28), DialogResult = System.Windows.Forms.DialogResult.Cancel
        };

        dlg.Controls.AddRange(new System.Windows.Forms.Control[] {
            chkEnable, lblColor, btnColor, lblPos, cboPos, lblFont, tbFont, lblFontVal,
            lblPress, tbPress, lblPressVal, btnOk, btnCancel
        });
        dlg.AcceptButton = btnOk; dlg.CancelButton = btnCancel;

        if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        {
            mapScannerEnabled  = chkEnable.Checked;
            mapOverlayColor    = btnColor.BackColor;
            mapOverlayPosition = cboPos.SelectedIndex;
            mapOverlayFontSize   = (float)tbFont.Value;
            mapScanLongPressMs   = (tbPress.Value / 50) * 50;
            SaveConfig();
            BuildMenu();
        }
        else
        {
            // Cancel: restore original values
            mapOverlayColor    = origColor;
            mapOverlayPosition = origPos;
            mapOverlayFontSize = origFont;
            mapScanLongPressMs = origPressMs;
        }
        // Restore active timers and re-show overlay if scan was running, else hide
        activeTimers      = savedTimers;
        countdownMapName  = savedMapName;
        if (savedTimers != null) UpdateOcrOverlay();
        else if (ocrOverlay != null && !ocrOverlay.IsDisposed) ocrOverlay.Hide();
        dlg.Dispose();
    }

    void CheckForUpdatesAsync()
    {
        if (!checkForUpdates) return;
        System.Threading.Thread t = new System.Threading.Thread(delegate()
        {
            try
            {
                System.Net.ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)3072; // TLS 1.2
                System.Net.HttpWebRequest req = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(
                    "https://api.github.com/repos/BrightRaider/BrightRaider/releases/latest");
                req.UserAgent = "BrightRaider/" + APP_VERSION;
                req.Timeout = 8000;
                using (System.Net.HttpWebResponse resp = (System.Net.HttpWebResponse)req.GetResponse())
                using (System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream()))
                {
                    string json = sr.ReadToEnd();
                    string tag = ExtractJsonString(json, "tag_name"); // e.g. "v8.1"
                    string latest = tag.TrimStart('v');               // "8.1"
                    Version current = new Version(APP_VERSION);
                    Version remote;
                    if (Version.TryParse(latest, out remote) && remote > current)
                    {
                        updateAvailableVersion = tag;
                        this.BeginInvoke(new Action(delegate { BuildMenu(); ShowUpdateToast(tag); }));
                    }
                }
            }
            catch { } // No internet or API error — silently ignore
        });
        t.IsBackground = true;
        t.Start();
    }

    void ShowUpdateToast(string version)
    {
        ShowToast(L("Update available: ", "Update verfügbar: ") + version);
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
    // offsetX/offsetY shift zones to target display (use 0,0 for local/overlay coordinates)
    static int[][] GetMeasurementZones(int screenW, int screenH, int offsetX, int offsetY)
    {
        int zW = (int)(screenW * 0.03);
        int zH = (int)(screenH * 0.03);
        int marginX = (int)(screenW * 0.15); // 15% from edges
        int marginY = (int)(screenH * 0.15);

        return new int[][] {
            new int[] { offsetX + screenW / 2 - zW / 2, offsetY + screenH / 2 - zH / 2, zW, zH },                   // 0: Center
            new int[] { offsetX + marginX, offsetY + marginY, zW, zH },                                                // 1: Top-Left
            new int[] { offsetX + screenW - marginX - zW, offsetY + marginY, zW, zH },                                 // 2: Top-Right
            new int[] { offsetX + marginX, offsetY + screenH - marginY - zH, zW, zH },                                 // 3: Bottom-Left
            new int[] { offsetX + screenW - marginX - zW, offsetY + screenH - marginY - zH, zW, zH }                   // 4: Bottom-Right
        };
    }

    void CheckScreenBrightness()
    {
        try
        {
            Screen targetScreen = GetTargetScreen();
            Rectangle bounds = targetScreen.Bounds;
            int screenW = bounds.Width;
            int screenH = bounds.Height;
            int[][] zones = GetMeasurementZones(screenW, screenH, bounds.X, bounds.Y);

            // Measure all 5 zones
            IntPtr hdcScreen = GetDC(IntPtr.Zero);
            List<double> zoneBrightness = new List<double>();
            for (int i = 0; i < zones.Length; i++)
                zoneBrightness.Add(MeasureZone(hdcScreen, zones[i][0], zones[i][1], zones[i][2], zones[i][3]));
            ReleaseDC(IntPtr.Zero, hdcScreen);

            // Weighted average using configurable zoneWeights
            double wSum = 0, wTotal = 0;
            for (int i = 0; i < zoneBrightness.Count; i++) { wSum += zoneBrightness[i] * zoneWeights[i]; wTotal += zoneWeights[i]; }
            double avgBrightness = wTotal > 0 ? wSum / wTotal : 0;

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
        ReadCurrentVibrance();
        profiles[0].Vibrance = originalVibrance;  // "Normal" = wie vor BrightRaider

        this.ShowInTaskbar = false;
        this.WindowState = FormWindowState.Minimized;
        this.FormBorderStyle = FormBorderStyle.None;
        this.Opacity = 0;

        trayIcon = new NotifyIcon();
        trayIcon.Text = "BrightRaider - Profile 1 (" + profiles[0].Name + ")";
        trayIcon.Icon = MakeIcon("1", Color.White);
        trayIcon.Visible = true;

            // Update check — delayed 5s so app fully starts first
            System.Windows.Forms.Timer updateTimer = new System.Windows.Forms.Timer();
            updateTimer.Interval = 5000;
            updateTimer.Tick += delegate { updateTimer.Stop(); updateTimer.Dispose(); CheckForUpdatesAsync(); };
            updateTimer.Start();

            // Update check — once per day while app is running
            System.Windows.Forms.Timer dailyUpdateTimer = new System.Windows.Forms.Timer();
            dailyUpdateTimer.Interval = 86400000; // 24h
            dailyUpdateTimer.Tick += delegate { CheckForUpdatesAsync(); };
            dailyUpdateTimer.Start();

            // What's New toast — 2s after launch, once per version
            System.Windows.Forms.Timer wnTimer = new System.Windows.Forms.Timer();
            wnTimer.Interval = 2000;
            wnTimer.Tick += delegate {
                wnTimer.Stop(); wnTimer.Dispose();
                if (lastSeenVersion != APP_VERSION)
                {
                    lastSeenVersion = APP_VERSION;
                    SaveConfig();
                    ShowWhatsNewToast();
                }
            };
            wnTimer.Start();

        BuildMenu();

        hookProc = new LowLevelKeyboardProc(HookCallback);
        using (Process curProcess = Process.GetCurrentProcess())
        using (ProcessModule curModule = curProcess.MainModule)
            hookId = SetWindowsHookEx(WH_KEYBOARD_LL, hookProc, GetModuleHandle(curModule.ModuleName), 0);

        if (autoBrightness && isProLicensed)
            StartAutoBrightness();

        // === OCR Map Scanner init ===
        InitOcrEngine();
    }

    // === Calibration Wizard (Pro) ===
    double MeasureBrightnessNow()
    {
        try
        {
            Screen targetScreen = GetTargetScreen();
            Rectangle bounds = targetScreen.Bounds;
            int screenW = bounds.Width;
            int screenH = bounds.Height;
            int[][] zones = GetMeasurementZones(screenW, screenH, bounds.X, bounds.Y);
            IntPtr hdcScreen = GetDC(IntPtr.Zero);
            List<double> values = new List<double>();
            for (int i = 0; i < zones.Length; i++)
                values.Add(MeasureZone(hdcScreen, zones[i][0], zones[i][1], zones[i][2], zones[i][3]));
            ReleaseDC(IntPtr.Zero, hdcScreen);
            double wSum2 = 0, wTotal2 = 0;
            for (int i = 0; i < values.Count; i++) { wSum2 += values[i] * zoneWeights[i]; wTotal2 += zoneWeights[i]; }
            return wTotal2 > 0 ? wSum2 / wTotal2 : 0;
        }
        catch { return -1; }
    }

    void ShowZoneWeightsDialog()
    {
        int[] origWeights = (int[])zoneWeights.Clone();

        Form dlg = new Form();
        dlg.Text = "BrightRaider — " + L("Measurement Zones", "Messzonen");
        dlg.Size = new Size(370, 360);
        dlg.FormBorderStyle = FormBorderStyle.FixedDialog;
        dlg.StartPosition = FormStartPosition.CenterScreen;
        dlg.MaximizeBox = false; dlg.MinimizeBox = false;

        Label lblHint = new Label() {
            Text = L("Zone weight (0 = ignore, 10 = max influence):", "Zonen-Gewichtung (0 = ignoriert, 10 = max):"),
            Location = new Point(15, 12), AutoSize = true
        };
        dlg.Controls.Add(lblHint);

        string[] zoneNames = new string[] {
            L("Center", "Mitte"),
            L("Top Left", "Oben Links"),
            L("Top Right", "Oben Rechts"),
            L("Bottom Left", "Unten Links"),
            L("Bottom Right", "Unten Rechts")
        };

        TrackBar[] sliders = new TrackBar[5];
        Label[] valLabels  = new Label[5];

        for (int i = 0; i < 5; i++)
        {
            int idx = i;
            int y = 42 + i * 46;

            Label lbl = new Label() { Text = zoneNames[i], Location = new Point(15, y + 4), Size = new Size(110, 20) };
            dlg.Controls.Add(lbl);

            TrackBar tb = new TrackBar() {
                Minimum = 0, Maximum = 10, Value = zoneWeights[i],
                TickFrequency = 1, LargeChange = 1,
                Location = new Point(130, y), Size = new Size(170, 35)
            };
            sliders[i] = tb;
            dlg.Controls.Add(tb);

            Label valLbl = new Label() { Text = zoneWeights[i].ToString(), Location = new Point(308, y + 4), Size = new Size(30, 20) };
            valLabels[i] = valLbl;
            dlg.Controls.Add(valLbl);

            tb.ValueChanged += delegate { valLabels[idx].Text = sliders[idx].Value.ToString(); };
        }

        Button btnOk = new Button() {
            Text = "OK", Location = new Point(175, 295),
            Size = new Size(75, 28), DialogResult = DialogResult.OK
        };
        Button btnCancel = new Button() {
            Text = L("Cancel", "Abbrechen"), Location = new Point(258, 295),
            Size = new Size(85, 28), DialogResult = DialogResult.Cancel
        };
        dlg.Controls.Add(btnOk); dlg.Controls.Add(btnCancel);
        dlg.AcceptButton = btnOk; dlg.CancelButton = btnCancel;

        if (dlg.ShowDialog() == DialogResult.OK)
        {
            for (int i = 0; i < 5; i++) zoneWeights[i] = sliders[i].Value;
            SaveConfig();
        }
        else
        {
            zoneWeights = origWeights;
        }
        dlg.Dispose();
    }

    void ShowCalibrationWizard()
    {
        // Topmost overlay for calibration steps
        Form wizard = new Form();
        wizard.Text = "BrightRaider - " + L("Calibration", "Kalibrierung");
        wizard.FormBorderStyle = FormBorderStyle.None;
        wizard.BackColor = Color.FromArgb(20, 20, 20);
        wizard.Size = new Size(420, 200);
        wizard.StartPosition = FormStartPosition.Manual;
        Screen targetScreen = GetTargetScreen();
        wizard.Location = new Point(
            targetScreen.Bounds.X + (targetScreen.Bounds.Width - 420) / 2,
            targetScreen.Bounds.Y + (targetScreen.Bounds.Height - 200) / 2);
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

                // Update available notice (shown at top if available)
                if (updateAvailableVersion != "")
                {
                    ToolStripMenuItem updateNotice = new ToolStripMenuItem("⬆ " + L("Update available: ", "Update verfügbar: ") + updateAvailableVersion);
                    updateNotice.Font = new System.Drawing.Font(updateNotice.Font, System.Drawing.FontStyle.Bold);
                    updateNotice.ForeColor = System.Drawing.Color.DarkOrange;
                    updateNotice.Click += delegate {
                        System.Diagnostics.Process.Start("https://github.com/BrightRaider/BrightRaider/releases/latest");
                    };
                    menu.Items.Add(updateNotice);
                    menu.Items.Add(new ToolStripSeparator());
                }

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

        // Auto-Start toggle (free for all)
        ToolStripMenuItem autoStartItem = new ToolStripMenuItem(L("Auto-Start", "Auto-Start"));
        autoStartItem.Checked = autoStart;
        autoStartItem.Click += delegate { SetAutoStart(!autoStart); BuildMenu(); };
        menu.Items.Add(autoStartItem);

        menu.Items.Add(new ToolStripSeparator());

        // Settings submenu
        ToolStripMenuItem settingsMenu = new ToolStripMenuItem(L("Settings", "Einstellungen"));

        // Brightness group
        ToolStripMenuItem editItem = new ToolStripMenuItem(L("Edit Profiles", "Profile bearbeiten") + (isProLicensed ? "" : " [PRO]"));
        editItem.Click += delegate { if (isProLicensed) ShowProfileEditor(); else ShowProRequired(); };
        settingsMenu.DropDownItems.Add(editItem);

        ToolStripMenuItem calibItem = new ToolStripMenuItem(L("Calibrate", "Kalibrieren") + (isProLicensed ? "" : " [PRO]"));
        calibItem.Click += delegate { if (isProLicensed) ShowCalibrationWizard(); else ShowProRequired(); };
        settingsMenu.DropDownItems.Add(calibItem);

        ToolStripMenuItem zonesItem = new ToolStripMenuItem(L("Zones...", "Zonen...") + (isProLicensed ? "" : " [PRO]"));
        zonesItem.Click += delegate { if (isProLicensed) ShowZoneWeightsDialog(); else ShowProRequired(); };
        settingsMenu.DropDownItems.Add(zonesItem);

        settingsMenu.DropDownItems.Add(new ToolStripSeparator());

        // Crosshair
        ToolStripMenuItem crosshairItem = new ToolStripMenuItem(isProLicensed ? L("Crosshair...", "Fadenkreuz...") : L("Crosshair... [PRO]", "Fadenkreuz... [PRO]"));
        crosshairItem.Click += delegate { if (isProLicensed) ShowCrosshairSettings(); else ShowProRequired(); };
        settingsMenu.DropDownItems.Add(crosshairItem);

        // Map Scanner
        ToolStripMenuItem mapScanItem = new ToolStripMenuItem(isProLicensed ? L("Map Scanner...", "Map Scanner...") : L("Map Scanner... [PRO]", "Map Scanner... [PRO]"));
        mapScanItem.Click += delegate { if (isProLicensed) ShowMapScannerSettings(); else ShowProRequired(); };
        settingsMenu.DropDownItems.Add(mapScanItem);

        // Debug Overlay
        ToolStripMenuItem overlayItem = new ToolStripMenuItem(L("Debug Overlay", "Debug-Overlay") + (isProLicensed ? "" : " [PRO]"));
        overlayItem.Checked = brightnessOverlay != null && !brightnessOverlay.IsDisposed && brightnessOverlay.Visible;
        overlayItem.Click += delegate { if (isProLicensed) ToggleBrightnessOverlay(); else ShowProRequired(); };
        settingsMenu.DropDownItems.Add(overlayItem);

        settingsMenu.DropDownItems.Add(new ToolStripSeparator());

        ToolStripMenuItem notifItem = new ToolStripMenuItem(L("Notifications", "Benachrichtigungen"));
        notifItem.Checked = showNotifications;
        notifItem.Click += delegate { showNotifications = !showNotifications; SaveConfig(); BuildMenu(); };
        settingsMenu.DropDownItems.Add(notifItem);

        ToolStripMenuItem hotkeysItem = new ToolStripMenuItem(L("Hotkeys", "Hotkeys"));
        hotkeysItem.Checked = hotkeysEnabled;
        hotkeysItem.Click += delegate {
            hotkeysEnabled = !hotkeysEnabled;
            SaveConfig();
            BuildMenu();
            ShowToast(hotkeysEnabled ? L("Hotkeys enabled", "Hotkeys aktiviert") : L("Hotkeys paused", "Hotkeys pausiert"));
        };
        settingsMenu.DropDownItems.Add(hotkeysItem);

        ToolStripMenuItem langItem = new ToolStripMenuItem(L("Deutsch", "English"));
        langItem.Click += delegate { language = (language == "en") ? "de" : "en"; SaveConfig(); BuildMenu(); };
        settingsMenu.DropDownItems.Add(langItem);

                // Update Check toggle
                settingsMenu.DropDownItems.Add(new ToolStripSeparator());
                ToolStripMenuItem updateCheckItem = new ToolStripMenuItem(L("Check for Updates", "Nach Updates suchen"));
                updateCheckItem.Checked = checkForUpdates;
                updateCheckItem.Click += delegate {
                    checkForUpdates = !checkForUpdates;
                    SaveConfig();
                    BuildMenu();
                };
                settingsMenu.DropDownItems.Add(updateCheckItem);

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

        if (activeDisplays.Count > 1)
        {
            ToolStripMenuItem allItem = new ToolStripMenuItem(L("All Monitors", "Alle Monitore"));
            allItem.Checked = (selectedDisplay == null);
            allItem.Click += delegate { SelectDisplay(null); };
            menu.Items.Add(allItem);
            for (int idx = 0; idx < activeDisplays.Count; idx++)
            {
                DisplayInfo di = activeDisplays[idx];
                ToolStripMenuItem item = new ToolStripMenuItem(di.FriendlyName);
                item.Checked = (selectedDisplay == di.DeviceName);
                item.Click += delegate { SelectDisplay(di.DeviceName); };
                menu.Items.Add(item);
            }
            menu.Items.Add(new ToolStripSeparator());
        }
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
        // Dispose cached overlay forms so they get recreated on the new display
        if (measureFrameOverlay != null && !measureFrameOverlay.IsDisposed)
        { measureFrameOverlay.Dispose(); measureFrameOverlay = null; }
        if (brightnessOverlay != null && !brightnessOverlay.IsDisposed)
        { brightnessOverlay.Dispose(); brightnessOverlay = null; }
        if (crosshairOverlay != null && !crosshairOverlay.IsDisposed)
        {
            crosshairOverlay.Close();
            crosshairOverlay.Dispose();
            crosshairOverlay = null;
        }
        // If crosshair was visible, re-create it on new monitor
        if (crosshairVisible)
        {
            crosshairOverlay = new CrosshairOverlay(GetTargetScreen(), crosshairColor, crosshairSize, (CrosshairOverlay.CrosshairStyle)crosshairStyle);
            crosshairOverlay.Show();
        }
        SaveConfig();
        BuildMenu();
        if (currentProfile > 1) ApplyProfile(currentProfile);
    }

    Screen GetTargetScreen()
    {
        if (selectedDisplay != null)
        {
            foreach (Screen s in Screen.AllScreens)
                if (s.DeviceName == selectedDisplay) return s;
        }
        return Screen.PrimaryScreen;
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

    void DisposeOverlay()
    {
        if (crosshairOverlay != null && !crosshairOverlay.IsDisposed)
        { crosshairOverlay.Close(); crosshairOverlay.Dispose(); crosshairOverlay = null; }
    }

    void ShowWhatsNewToast()
    {
        ShowToast(
            L("What's New in V9.1:\n• Map Scanner — ultrawide screen fix",
              "Neu in V9.1:\n• Map Scanner — Ultrawide-Support verbessert"),
            6000);
    }

    // === Exit ===
    void ExitApp()
    {
        exiting = true;
        System.Threading.Timer lt = mLongPressTimer;
        if (lt != null) { mLongPressTimer = null; lt.Dispose(); }
        StopAutoBrightness();
        if (brightnessOverlay != null) { try { brightnessOverlay.Close(); brightnessOverlay.Dispose(); } catch { } }
        if (measureFrameOverlay != null) { try { measureFrameOverlay.Close(); measureFrameOverlay.Dispose(); } catch { } }
        if (countdownTimer != null) { try { countdownTimer.Stop(); countdownTimer.Dispose(); } catch { } }
        if (toastTimer != null)    { try { toastTimer.Stop();     toastTimer.Dispose();     } catch { } }
        if (toastForm != null)     { try { toastForm.Close();     toastForm.Dispose();      } catch { } }
        if (ocrOverlay != null)    { try { ocrOverlay.Close();    ocrOverlay.Dispose();     } catch { } }
        try { DisposeOverlay(); } catch { }
        if (hookId != IntPtr.Zero) { UnhookWindowsHookEx(hookId); hookId = IntPtr.Zero; }

        string savedDisplay = selectedDisplay;
        selectedDisplay = null;
        SetGammaRamp(1.0, 1.0);
        SetSaturation(originalVibrance);
        selectedDisplay = savedDisplay;

        if (adlReady && adlMainControlDestroy != null)
            try { adlMainControlDestroy(); } catch { }

        trayIcon.Visible = false;
        trayIcon.Dispose();
        Application.Exit();
    }

    protected override void OnFormClosing(FormClosingEventArgs e) { ExitApp(); base.OnFormClosing(e); }

    [DllImport("user32.dll")]
    static extern bool SetProcessDPIAware();

    [STAThread]
    static void Main(string[] args)
    {
        if (args.Length > 0 && args[0] == "--setregistry")
        { Environment.Exit(SetGammaRegistryValue() ? 0 : 1); return; }

        // DPI-Awareness aktivieren — verhindert dass Windows Fenstermaße
        // automatisch skaliert. Notwendig für korrekte Crosshair-Position
        // und OCR-Koordinaten bei QHD/4K mit Windows-Skalierung > 100%.
        SetProcessDPIAware();

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

    // ═══════════════════════════════════════════════════════════════════
    // === OCR MAP SCANNER — Arc Raiders Evac Timer Reader            ===
    // ═══════════════════════════════════════════════════════════════════
    //
    // HOW TO SET UP:
    //   1. Start a match, open the map (M), zoom out 2x with mouse wheel
    //   2. Open Paint, paste a screenshot (Win+Shift+S then Ctrl+V)
    //   3. Hover mouse over each evac timer number → note X, Y at bottom-left
    //   4. Divide all coordinates by 1.333 (because you have QHD 2560×1440
    //      and coordinates below are 1920×1080 base)
    //   5. Fill in the MapProfiles below for each of your 5 maps
    //   6. Recompile
    //
    // TRIGGER:
    //   Auto:   fires once when fade-in from loading screen is detected
    //   Manual: press Numpad * at any time to re-scan
    //
    // ───────────────────────────────────────────────────────────────────

    // --- Map Profile definitions (1920×1080 base coordinates) ---------

    class OcrMapRegion
    {
        public string Name;
        public Rectangle Rect;       // 1080p base coords — auto-scaled to your resolution
        public bool MinutesOnly;     // true = Icon verdeckt Sekunden, nur Minuten lesen
    }

    class OcrMapProfile
    {
        public string DisplayName; // shown in overlay
        public string[] NameKeywords; // matched against OCR of NameRegion (lowercase)
        public List<OcrMapRegion> EvacPoints;
    }

    // Region where the map NAME is shown (right info panel, top)
    static readonly Rectangle MapNameRegion = new Rectangle(1500, 25, 400, 90);

    static readonly List<OcrMapProfile> MapProfiles = new List<OcrMapProfile>
    {
        new OcrMapProfile {
            DisplayName = "Buried City",
            NameKeywords = new[] { "begrabene", "buried" },
            EvacPoints  = new List<OcrMapRegion> {
                new OcrMapRegion { Name = "North", Rect = new Rectangle( 952, 391, 130, 32) },
                new OcrMapRegion { Name = "West",  Rect = new Rectangle( 833, 538, 130, 32) },
                new OcrMapRegion { Name = "East",  Rect = new Rectangle(1057, 649, 130, 32) },
                new OcrMapRegion { Name = "South", Rect = new Rectangle( 973, 753, 130, 32) },
            }
        },
        new OcrMapProfile {
            DisplayName = "Stella Montis",
            NameKeywords = new[] { "stella" },
            EvacPoints  = new List<OcrMapRegion> {
                new OcrMapRegion { Name = "Seedvault",    Rect = new Rectangle(1099, 719, 130, 32) },
                new OcrMapRegion { Name = "Lobby",        Rect = new Rectangle(1065, 330, 130, 32) },
                new OcrMapRegion { Name = "Loading Dock", Rect = new Rectangle( 814, 620, 130, 32) },
            }
        },
        new OcrMapProfile {
            DisplayName = "Space Port",
            NameKeywords = new[] { "raumhafen", "space" },
            EvacPoints  = new List<OcrMapRegion> {
                new OcrMapRegion { Name = "Central", Rect = new Rectangle( 941, 406, 130, 32) },
                new OcrMapRegion { Name = "West",    Rect = new Rectangle( 820, 474, 130, 32) },
                new OcrMapRegion { Name = "South",   Rect = new Rectangle( 928, 556, 130, 32) },
                new OcrMapRegion { Name = "East",    Rect = new Rectangle(1095, 550, 130, 32) },
            }
        },
        new OcrMapProfile {
            DisplayName = "Blue Gate",
            NameKeywords = new[] { "blaue", "blue gate" },
            EvacPoints  = new List<OcrMapRegion> {
                new OcrMapRegion { Name = "Cliff",     Rect = new Rectangle( 912, 451, 128, 32) },
                new OcrMapRegion { Name = "Forest",    Rect = new Rectangle( 819, 580, 130, 32) },
                new OcrMapRegion { Name = "Warehouse", Rect = new Rectangle(1087, 484,  82, 32), MinutesOnly = true },
                new OcrMapRegion { Name = "Overlook",  Rect = new Rectangle(1022, 586, 130, 32) },
            }
        },
        new OcrMapProfile {
            DisplayName = "Damm",
            NameKeywords = new[] { "damm" },
            EvacPoints  = new List<OcrMapRegion> {
                new OcrMapRegion { Name = "North Complex",   Rect = new Rectangle(1062, 350, 130, 32) },
                new OcrMapRegion { Name = "Swamp Center",    Rect = new Rectangle( 865, 463, 130, 32) },
                new OcrMapRegion { Name = "Water Treatment", Rect = new Rectangle( 913, 547, 130, 32) },
                new OcrMapRegion { Name = "Red Lakes",       Rect = new Rectangle(1087, 610, 130, 32) },
            }
        },
    };

    // --- Resolution scaling --------------------------------------------

    // Scale a 1080p-base rect to the selected screen resolution
    Rectangle ScaleRect(Rectangle r)
    {
        Rectangle sb = GetTargetScreen().Bounds;
        float sy = (float)sb.Height / 1080f;
        // Ultrawide: game renders map in centered 16:9 viewport (pillarbox)
        float effectiveW = sb.Width;
        int offsetX = 0;
        if ((float)sb.Width / sb.Height > 16f / 9f * 1.05f)
        {
            effectiveW = sb.Height * (16f / 9f);
            offsetX = (int)((sb.Width - effectiveW) / 2f);
        }
        float sx = effectiveW / 1920f;
        return new Rectangle(
            offsetX + (int)Math.Round(r.X * sx), (int)Math.Round(r.Y * sy),
            (int)Math.Round(r.Width * sx), (int)Math.Round(r.Height * sy));
    }

    // --- OCR engine init -----------------------------------------------

    void InitOcrEngine()
    {
        try
        {
            var lang = new Language("en-US");
            ocrEngine = Windows.Media.Ocr.OcrEngine.IsLanguageSupported(lang)
                ? Windows.Media.Ocr.OcrEngine.TryCreateFromLanguage(lang)
                : Windows.Media.Ocr.OcrEngine.TryCreateFromUserProfileLanguages();
        }
        catch { ocrEngine = null; }
    }

    // Recognize text in a screen region (captures screen)
    string OcrRecognize(Rectangle screenRect)
    {
        if (ocrEngine == null) return "";
        try
        {
            using (Bitmap bmp = new Bitmap(screenRect.Width, screenRect.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                    g.CopyFromScreen(screenRect.Location, Point.Empty, screenRect.Size);
                return OcrFromBitmap(bmp);
            }
        }
        catch { return ""; }
    }

    // Block on a WinRT IAsyncOperation<T> without System.Runtime.WindowsRuntime.dll
    static T WinRTWait<T>(Windows.Foundation.IAsyncOperation<T> op)
    {
        using (var mre = new System.Threading.ManualResetEventSlim(false))
        {
            op.Completed = delegate(Windows.Foundation.IAsyncOperation<T> o, Windows.Foundation.AsyncStatus s) { mre.Set(); };
            mre.Wait();
            return op.GetResults();
        }
    }

    // Recognize text from a sub-region of an existing bitmap (no screen capture)
    string OcrFromBitmap(Bitmap bmp)
    {
        if (ocrEngine == null) return "";
        try
        {
            // Save to temp PNG — avoids IBuffer/WinRT type-universe conflicts
            string tmp = Path.Combine(Path.GetTempPath(), "br_ocr_tmp.png");
            using (Bitmap up = new Bitmap(bmp.Width * 3, bmp.Height * 3, System.Drawing.Imaging.PixelFormat.Format32bppArgb))
            using (Graphics ug = Graphics.FromImage(up))
            {
                ug.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                ug.PixelOffsetMode   = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                ug.DrawImage(bmp, 0, 0, bmp.Width * 3, bmp.Height * 3);
                up.Save(tmp, System.Drawing.Imaging.ImageFormat.Png);
            }

            StorageFile   file    = WinRTWait(StorageFile.GetFileFromPathAsync(tmp));
            IRandomAccessStream stream  = WinRTWait(file.OpenReadAsync());
            BitmapDecoder decoder = WinRTWait(BitmapDecoder.CreateAsync(stream));
            SoftwareBitmap softBmp = WinRTWait(
                decoder.GetSoftwareBitmapAsync(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Premultiplied));
            OcrResult ocrResult = WinRTWait(ocrEngine.RecognizeAsync(softBmp));

            softBmp.Dispose();
            try { File.Delete(tmp); } catch { }
            return ocrResult != null ? ocrResult.Text : "";
        }
        catch { return ""; }
    }

    // Capture full screen once, return bitmap
    Bitmap CaptureScreen()
    {
        Rectangle sb = GetTargetScreen().Bounds;
        Bitmap bmp = new Bitmap(sb.Width, sb.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        using (Graphics g = Graphics.FromImage(bmp))
            g.CopyFromScreen(sb.Location, Point.Empty, sb.Size);
        return bmp;
    }

    // Crop a sub-region from a full-screen bitmap
    Bitmap CropBitmap(Bitmap src, Rectangle rect)
    {
        // Clamp to source bounds
        int x = Math.Max(0, Math.Min(rect.X, src.Width - 1));
        int y = Math.Max(0, Math.Min(rect.Y, src.Height - 1));
        int w = Math.Min(rect.Width, src.Width - x);
        int h = Math.Min(rect.Height, src.Height - y);
        if (w <= 0 || h <= 0) return new Bitmap(1, 1);
        return src.Clone(new Rectangle(x, y, w, h), src.PixelFormat);
    }

    // Wenn genau 1 Timer fehlt (-1) und die anderen eine gleichmäßige Sequenz bilden,
    // wird der fehlende Wert berechnet (Abstand ±30s Toleranz).
    static void InterpolateMissingTimers(List<EvacTimer> timers)
    {
        if (timers == null) return;
        List<int> known   = new List<int>();
        List<int> missing = new List<int>(); // index in timers
        for (int i = 0; i < timers.Count; i++)
        {
            if (timers[i].SecondsLeft >= 0) known.Add(timers[i].SecondsLeft);
            else                             missing.Add(i);
        }
        if (missing.Count != 1 || known.Count < 2) return; // nur bei genau 1 Fehlwert

        known.Sort();

        // Abstand zwischen bekannten Werten prüfen
        int gap = 0;
        bool consistent = true;
        for (int i = 1; i < known.Count; i++)
        {
            int d = known[i] - known[i - 1];
            if (gap == 0) gap = d;
            else if (Math.Abs(d - gap) > 30) { consistent = false; break; }
        }
        if (!consistent || gap < 60) return; // kein klares Muster

        // Fehlenden Wert bestimmen: entweder smallest-gap oder largest+gap
        int candidate;
        if (known[0] - gap > 0)
            candidate = known[0] - gap;    // vor dem kleinsten
        else
            candidate = known[known.Count - 1] + gap; // nach dem größten

        timers[missing[0]].SecondsLeft = candidate;
    }

    // Extrahiert nur die führenden Ziffern (Minuten) wenn MM:SS nicht lesbar.
    static int ParseMinutesOnly(string raw)
    {
        if (string.IsNullOrWhiteSpace(raw)) return -1;
        string cleaned = raw.Replace('O','0').Replace('o','0').Replace('l','1').Replace('I','1');
        System.Text.RegularExpressions.Match m =
            System.Text.RegularExpressions.Regex.Match(cleaned, @"^\s*(\d{1,2})");
        if (!m.Success) return -1;
        int mins;
        return int.TryParse(m.Groups[1].Value, out mins) && mins <= 59 ? mins : -1;
    }

    // Füllt Timer mit RawMinutes auf: Sekunden = Durchschnitt der anderen gültigen Timer.
    static void FillMinutesFromContext(List<EvacTimer> timers)
    {
        if (timers == null) return;
        // Sekundenanteil der gültigen Timer sammeln
        List<int> knownSecs = new List<int>();
        foreach (EvacTimer et in timers)
            if (et.SecondsLeft > 0) knownSecs.Add(et.SecondsLeft % 60);
        if (knownSecs.Count == 0) return;
        int avgSec = 0;
        foreach (int s in knownSecs) avgSec += s;
        avgSec /= knownSecs.Count;

        foreach (EvacTimer et in timers)
        {
            if (et.SecondsLeft >= 0) continue;   // schon bekannt
            if (et.RawMinutes < 0)  continue;    // keine Minuten lesbar
            int calculated = et.RawMinutes * 60 + avgSec;
            // Plausibilitätsprüfung: darf nicht "ZU" simulieren wenn Minuten > 0
            if (calculated > 0) et.SecondsLeft = calculated;
        }
    }

    // Parse OCR timer text → total seconds. Returns -1 if unreadable.
    // First tries regex for MM:SS pattern, then falls back to digit extraction.
    static int ParseTimerSeconds(string raw)
    {
        if (string.IsNullOrWhiteSpace(raw)) return -1;

        // Fix common OCR mistakes before regex
        string cleaned = raw;
        cleaned = cleaned.Replace('O', '0').Replace('o', '0');
        cleaned = cleaned.Replace('l', '1').Replace('I', '1');
        cleaned = cleaned.Replace('S', '5').Replace('s', '5');
        cleaned = cleaned.Replace('B', '8');
        cleaned = cleaned.Replace(';', ':'); // OCR sometimes reads : as ;

        // Try to find MM:SS or M:SS pattern via regex (allows spaces around separator)
        System.Text.RegularExpressions.Match m = System.Text.RegularExpressions.Regex.Match(cleaned, @"(\d{1,2})\s*[:\.,;]\s*(\d{2})");
        if (m.Success)
        {
            int minutes = 0, seconds = 0;
            int.TryParse(m.Groups[1].Value, out minutes);
            int.TryParse(m.Groups[2].Value, out seconds);
            if (seconds < 60) // sanity: valid seconds
                return minutes * 60 + seconds; // 0 = ZU, >0 = running
        }

        return -1; // kein gültiges Timer-Muster erkannt
    }

    static string FormatTimer(int totalSeconds)
    {
        if (totalSeconds <= 0) return "---";
        return (totalSeconds / 60).ToString("D2") + ":" + (totalSeconds % 60).ToString("D2");
    }

    // --- ScreenWatcher — detects game fade-in -------------------------

    // --- Scan sequence ------------------------------------------------

    // --- Input: Taste + Mausrad ans Spielfenster senden ---------------

    // Sendet Taste an Vordergrund-Fenster via SendInput (nutzt vorhandene user32-Imports)
    [DllImport("user32.dll", EntryPoint = "MapVirtualKeyW")]
    static extern uint MapVkW(uint uCode, uint uMapType);

    [StructLayout(LayoutKind.Sequential)] struct MI { public int dx,dy; public uint mouseData,dwFlags,time; public IntPtr extra; }
    [StructLayout(LayoutKind.Sequential)] struct KI { public ushort wVk,wScan; public uint dwFlags,time; public IntPtr extra; }
    [StructLayout(LayoutKind.Explicit)]   struct SI
    {
        [FieldOffset(0)] public uint type;
        [FieldOffset(4)] public MI mi;
        [FieldOffset(4)] public KI ki;
    }
    [DllImport("user32.dll", EntryPoint="SendInput", SetLastError=true)]
    static extern uint SendInputW(uint n, SI[] inputs, int size);

    [DllImport("user32.dll")] static extern bool SetCursorPos(int x, int y);

    void SendKey(ushort vk, bool up)
    {
        SI[] inp = new SI[1];
        inp[0].type       = 1; // INPUT_KEYBOARD
        inp[0].ki.wVk     = vk;
        inp[0].ki.wScan   = (ushort)MapVkW(vk, 0);
        inp[0].ki.dwFlags = up ? (0x0008u | 0x0002u) : 0x0008u; // KEYEVENTF_SCANCODE for game compatibility
        SendInputW(1, inp, Marshal.SizeOf(typeof(SI)));
        Thread.Sleep(40);
    }

    void ScrollDown()
    {
        IntPtr hwnd = GetForegroundWindow();
        if (hwnd != IntPtr.Zero)
            PostMessage(hwnd, 0x020A, (IntPtr)unchecked((int)0xFF880000), IntPtr.Zero);
        Thread.Sleep(80);
    }

    void ScrollUp()
    {
        IntPtr hwnd = GetForegroundWindow();
        if (hwnd != IntPtr.Zero)
            PostMessage(hwnd, 0x020A, (IntPtr)0x00780000, IntPtr.Zero); // delta=+120
        Thread.Sleep(80);
    }

    // manual=true → kein Zoom-out (Nutzer macht das selbst)
    // quiet=true  → kein Fehler-Overlay bei Fehlschlag (nur für Auto-Scan vom ScreenWatcher)
    void TriggerMapScan(bool manual, bool quiet = false)
    {
        if (ocrEngine == null) { ShowToast(L("OCR not available","OCR nicht verfügbar")); return; }
        if (ocrScanRunning) return;
        ocrScanRunning = true;
        System.Threading.Timer lt = mLongPressTimer;
        if (lt != null) { mLongPressTimer = null; lt.Dispose(); }

        Thread t = new Thread(delegate() { RunMapScan(manual, quiet); }) { IsBackground = true };
        t.Start();
    }

    void RunMapScan(bool manual, bool quiet = false)
    {
        try
        {
            // Zoom out before scan (only auto-scan; manual = user already zoomed)
            if (!manual)
            {
                Rectangle sb = GetTargetScreen().Bounds;
                SetCursorPos(sb.X + sb.Width * 3 / 4, sb.Y + sb.Height / 4);
                Thread.Sleep(50);
                for (int i = 0; i < MAP_SCROLL_STEPS; i++) { ScrollDown(); }
                Thread.Sleep(MAP_SCROLL_WAIT_MS);
            }

            // 1. Capture full screen ONCE
            using (Bitmap screen = CaptureScreen())
            {
            // 2. Auto-detect map: scan all profiles, pick the one with most valid timers
            OcrMapProfile bestProfile = null;
            List<EvacTimer> bestTimers = null;
            int bestCount = 0;

            foreach (OcrMapProfile p in MapProfiles)
            {
                List<EvacTimer> timers = new List<EvacTimer>();
                int validCount = 0;
                foreach (OcrMapRegion region in p.EvacPoints)
                {
                    Rectangle sr = ScaleRect(region.Rect);
                    string raw;
                    using (Bitmap sub = CropBitmap(screen, sr))
                    {
                        raw = OcrFromBitmap(sub);
                    }
                    int secs, rawMin;
                    if (region.MinutesOnly)
                    {
                        // Icon verdeckt Sekunden → direkt Minuten lesen, Sekunden werden berechnet
                        rawMin = ParseMinutesOnly(raw);
                        secs   = -1; // wird von FillMinutesFromContext gefüllt
                    }
                    else
                    {
                        secs   = ParseTimerSeconds(raw);
                        rawMin = secs < 0 ? ParseMinutesOnly(raw) : -1;
                    }
                    if (secs > 0) validCount++;
                    timers.Add(new EvacTimer { Name = region.Name, SecondsLeft = secs, RawMinutes = rawMin });
                }
                if (validCount > bestCount) { bestCount = validCount; bestProfile = p; bestTimers = timers; }
            }

            if (bestCount < 2)
            {
                // Einmal automatisch wiederholen (sofort, anderer Screenshot-Moment)
                bestProfile = null; bestTimers = null; bestCount = 0;
                using (Bitmap screen2 = CaptureScreen())
                {
                    foreach (OcrMapProfile p in MapProfiles)
                    {
                        List<EvacTimer> timers = new List<EvacTimer>();
                        int validCount = 0;
                        foreach (OcrMapRegion region in p.EvacPoints)
                        {
                            Rectangle sr = ScaleRect(region.Rect);
                            string raw2;
                            using (Bitmap sub = CropBitmap(screen2, sr))
                                raw2 = OcrFromBitmap(sub);
                            int secs = ParseTimerSeconds(raw2);
                            int rawMin2 = secs < 0 ? ParseMinutesOnly(raw2) : -1;
                            if (secs > 0) validCount++;
                            timers.Add(new EvacTimer { Name = region.Name, SecondsLeft = secs, RawMinutes = rawMin2 });
                        }
                        if (validCount > bestCount) { bestCount = validCount; bestProfile = p; bestTimers = timers; }
                    }
                }
                if (bestCount < 2)
                {
                    // Fehler anzeigen — außer bei stillem Auto-Scan (ScreenWatcher)
                    if (!quiet)
                    {
                        if (!exiting && !this.IsDisposed)
                            this.BeginInvoke(new Action(delegate {
                                ShowOcrOverlay(L("⚠ Map not recognized","⚠ Karte unbekannt"));
                            }));
                    }
                    return;
                }
            }

            // 3. Fehlende Timer per Sequenz-Interpolation berechnen
            FillMinutesFromContext(bestTimers);   // erst Minuten+Sekunden aus Kontext
            InterpolateMissingTimers(bestTimers); // dann vollständige Sequenz-Interpolation

            // 4. Start countdown
            string mapName = bestProfile.DisplayName;
            if (!exiting && !this.IsDisposed)
                this.BeginInvoke(new Action(delegate {
                    activeTimers = bestTimers;
                    StartCountdown(mapName);
                }));
            } // dispose screen bitmap
        }
        catch (Exception ex)
        {
            if (!exiting && !this.IsDisposed)
                this.BeginInvoke(new Action(delegate { ShowToast("OCR Error: " + ex.Message); }));
        }
        finally
        {
            ocrScanRunning = false;
        }
    }

    string countdownMapName = "";

    void StartCountdown(string mapName)
    {
        countdownMapName = mapName;
        // Stop existing countdown
        if (countdownTimer != null) { countdownTimer.Stop(); countdownTimer.Dispose(); }
        // Render initial state
        UpdateOcrOverlay();
        // Start 1-second tick
        countdownTimer = new System.Windows.Forms.Timer();
        countdownTimer.Interval = 1000;
        countdownTimer.Tick += delegate {
            if (activeTimers == null) { countdownTimer.Stop(); return; }
            bool anyActive = false;
            foreach (EvacTimer et in activeTimers)
            {
                if (et.SecondsLeft > 0) { et.SecondsLeft--; anyActive = true; }
            }
            UpdateOcrOverlay();
            if (!anyActive) { countdownTimer.Stop(); }
        };
        countdownTimer.Start();
    }

    void UpdateOcrOverlay()
    {
        if (activeTimers == null || exiting) return;

        bool needsRecreate = ocrOverlay == null || ocrOverlay.IsDisposed;
        if (needsRecreate)
        {
            ocrOverlay = new Form();
            ocrOverlay.FormBorderStyle = FormBorderStyle.None;
            ocrOverlay.BackColor       = Color.Black;
            ocrOverlay.TransparencyKey = Color.Black;
            ocrOverlay.TopMost         = true;
            ocrOverlay.ShowInTaskbar   = false;
            ocrOverlay.StartPosition   = FormStartPosition.Manual;
            // kein Owner — Hauptfenster ist hidden (Tray), Owner würde Overlay mitverbergen
            // WS_EX_TOOLWINDOW im Shown-Event reicht für Alt+Tab-Ausblendung
            ocrOverlay.Shown += delegate {
                int es = GetWindowLong(ocrOverlay.Handle, GWL_EXSTYLE);
                SetWindowLong(ocrOverlay.Handle, GWL_EXSTYLE, es | WS_EX_LAYERED | WS_EX_TRANSPARENT | WS_EX_TOOLWINDOW);
            };
        }

        // Dispose existing labels
        // Erst aus Collection entfernen, DANN disposen — verhindert Paint auf disposed Controls
        Control[] old = new Control[ocrOverlay.Controls.Count];
        ocrOverlay.Controls.CopyTo(old, 0);
        ocrOverlay.Controls.Clear();
        foreach (Control c in old) c.Dispose();

        int lineH = (int)(mapOverlayFontSize * 2.2f);
        int y = 6;

        // Header: map name
        AddOcrLine(countdownMapName, mapOverlayColor, ref y, lineH);

        foreach (EvacTimer et in activeTimers)
        {
            string time;
            Color col;
            if      (et.SecondsLeft < 0)         { time = "---";                       col = Color.Gray; }
            else if (et.SecondsLeft == 0)         { time = L("CLOSED","ZU");            col = Color.Gray; }
            else if (et.SecondsLeft <= 60)        { time = FormatTimer(et.SecondsLeft); col = Color.Red; }
            else if (et.SecondsLeft <= 300)       { time = FormatTimer(et.SecondsLeft); col = Color.Orange; }
            else if (et.SecondsLeft <= 600)       { time = FormatTimer(et.SecondsLeft); col = Color.Yellow; }
            else                                  { time = FormatTimer(et.SecondsLeft); col = Color.Lime; }
            AddOcrLine(et.Name + ":  " + time, col, ref y, lineH);
        }

        ocrOverlay.Size = new Size(260, y + 6);
        PositionOcrOverlay();
        if (ocrOverlayVisible) ocrOverlay.Show();
    }

    void AddOcrLine(string text, Color color, ref int y, int lineH)
    {
        Label lbl = new Label();
        lbl.Text      = text;
        lbl.ForeColor = color;
        lbl.BackColor = Color.Black;
        lbl.Font      = new Font("Consolas", mapOverlayFontSize, FontStyle.Bold); // Label owns font
        lbl.AutoSize  = false;
        lbl.Location  = new Point(8, y);
        lbl.Size      = new Size(244, lineH);
        lbl.TextAlign = ContentAlignment.MiddleLeft;
        ocrOverlay.Controls.Add(lbl);
        y += lineH;
    }

    void PositionOcrOverlay()
    {
        Rectangle sb = GetTargetScreen().Bounds;
        int cx = sb.X + (sb.Width - ocrOverlay.Width) / 2;
        int px, py;
        switch (mapOverlayPosition)
        {
            case 1:  px = cx;                                  py = sb.Y + 10; break;                          // Oben-Mitte
            case 2:  px = sb.Right - ocrOverlay.Width - 10;   py = sb.Y + 10; break;                          // Oben-Rechts
            case 3:  px = sb.X + 10;                           py = sb.Bottom - ocrOverlay.Height - 10; break; // Unten-Links
            case 4:  px = cx;                                  py = sb.Bottom - ocrOverlay.Height - 10; break; // Unten-Mitte
            case 5:  px = sb.Right - ocrOverlay.Width - 10;   py = sb.Bottom - ocrOverlay.Height - 10; break; // Unten-Rechts
            default: px = sb.X + 10;                           py = sb.Y + 10; break;                          // Oben-Links
        }
        ocrOverlay.Location = new Point(px, py);
    }

    // --- Evac-Timer Overlay -------------------------------------------

    void ShowOcrOverlay(string text)
    {
        if (exiting) return;

        if (ocrOverlay == null || ocrOverlay.IsDisposed)
        {
            ocrOverlay = new Form();
            ocrOverlay.FormBorderStyle = FormBorderStyle.None;
            ocrOverlay.BackColor       = Color.Black;
            ocrOverlay.TransparencyKey = Color.Black;
            ocrOverlay.TopMost         = true;
            ocrOverlay.ShowInTaskbar   = false;
            ocrOverlay.StartPosition   = FormStartPosition.Manual;
            // kein Owner — Hauptfenster ist hidden (Tray), Owner würde Overlay mitverbergen
            // WS_EX_TOOLWINDOW im Shown-Event reicht für Alt+Tab-Ausblendung
            ocrOverlay.Shown += delegate {
                int es = GetWindowLong(ocrOverlay.Handle, GWL_EXSTYLE);
                SetWindowLong(ocrOverlay.Handle, GWL_EXSTYLE, es | WS_EX_LAYERED | WS_EX_TRANSPARENT | WS_EX_TOOLWINDOW);
            };
        }

        // Erst aus Collection entfernen, DANN disposen — verhindert Paint auf disposed Controls
        Control[] old = new Control[ocrOverlay.Controls.Count];
        ocrOverlay.Controls.CopyTo(old, 0);
        ocrOverlay.Controls.Clear();
        foreach (Control c in old) c.Dispose();

        int lineH = (int)(mapOverlayFontSize * 2.2f);
        int y = 6;
        foreach (string line in text.Split('\n'))
        {
            int ref_y = y;
            Label lbl = new Label();
            lbl.Text = line; lbl.ForeColor = mapOverlayColor; lbl.BackColor = Color.Black;
            lbl.Font = new Font("Consolas", mapOverlayFontSize, FontStyle.Bold); // Label owns font
            lbl.AutoSize = false;
            lbl.Location = new Point(8, ref_y); lbl.Size = new Size(244, lineH);
            lbl.TextAlign = ContentAlignment.MiddleLeft;
            ocrOverlay.Controls.Add(lbl);
            y += lineH;
        }
        ocrOverlay.Size = new Size(280, y + 6);

        PositionOcrOverlay();
        if (ocrOverlayVisible) ocrOverlay.Show();
    }

    // ═══════════════════════════════════════════════════════════════════

    // === Crosshair Overlay (Pro only) ===
    class CrosshairOverlay : System.Windows.Forms.Form
    {
        public enum CrosshairStyle { Cross = 0, DotRing = 1, TShape = 2, Dot = 3 }

        System.Drawing.Color _color;
        int _size;
        CrosshairStyle _style;

        public CrosshairOverlay(System.Windows.Forms.Screen screen, System.Drawing.Color color, int size, CrosshairStyle style)
        {
            _color = color; _size = size; _style = style;
            FormBorderStyle = FormBorderStyle.None;
            TopMost = true;
            ShowInTaskbar = false;
            BackColor = System.Drawing.Color.Black;
            TransparencyKey = System.Drawing.Color.Black;
            StartPosition = FormStartPosition.Manual;
            System.Drawing.Rectangle b = screen.Bounds;
            Location = new System.Drawing.Point(b.X, b.Y);
            Size = new System.Drawing.Size(b.Width, b.Height);
        }

        protected override System.Windows.Forms.CreateParams CreateParams
        {
            get
            {
                System.Windows.Forms.CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x80000 | 0x20 | 0x8000000; // WS_EX_LAYERED | WS_EX_TRANSPARENT | WS_EX_NOACTIVATE
                return cp;
            }
        }

        public void UpdateSettings(System.Drawing.Color color, int size, CrosshairStyle style)
        {
            _color = color; _size = size; _style = style;
            Invalidate();
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            System.Drawing.Graphics g = e.Graphics;
            int cx = ClientSize.Width / 2, cy = ClientSize.Height / 2, half = _size / 2;
            using (System.Drawing.Pen pen = new System.Drawing.Pen(_color, 1f))
            using (System.Drawing.SolidBrush brush = new System.Drawing.SolidBrush(_color))
            {
                switch (_style)
                {
                    case CrosshairStyle.Cross:
                        g.DrawLine(pen, cx - half, cy, cx + half, cy);
                        g.DrawLine(pen, cx, cy - half, cx, cy - 3);
                        g.DrawLine(pen, cx, cy + 3, cx, cy + half);
                        break;
                    case CrosshairStyle.DotRing:
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        g.FillEllipse(brush, cx - 2, cy - 2, 5, 5);
                        using (System.Drawing.Pen pen2 = new System.Drawing.Pen(_color, 2f))
                            g.DrawEllipse(pen2, cx - half, cy - half, _size, _size);
                        break;
                    case CrosshairStyle.TShape:
                        g.DrawLine(pen, cx - half, cy, cx + half, cy);
                        g.DrawLine(pen, cx, cy - half, cx, cy);
                        break;
                    case CrosshairStyle.Dot:
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        g.FillEllipse(brush, cx - 3, cy - 3, 7, 7);
                        break;
                }
            }
        }

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == 0x84) { m.Result = (System.IntPtr)(-1); return; } // WM_NCHITTEST → HTTRANSPARENT
            base.WndProc(ref m);
        }
    }
}
