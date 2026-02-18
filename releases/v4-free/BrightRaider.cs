using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.Security.Principal;
using System.IO;
using Microsoft.Win32;

class BrightRaider : Form
{
    // === GDI / Display API ===
    [DllImport("gdi32.dll")]
    static extern bool SetDeviceGammaRamp(IntPtr hDC, ref RAMP lpRamp);

    [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
    static extern IntPtr CreateDC(string lpszDriver, string lpszDevice, string lpszOutput, IntPtr lpInitData);

    [DllImport("gdi32.dll")]
    static extern bool DeleteDC(IntPtr hdc);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    static extern bool EnumDisplayDevices(string lpDevice, uint iDevNum, ref DISPLAY_DEVICE lpDisplayDevice, uint dwFlags);

    [DllImport("user32.dll")]
    static extern bool DestroyIcon(IntPtr handle);

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

    // === NvAPI ===
    [DllImport("nvapi64.dll", EntryPoint = "nvapi_QueryInterface", CallingConvention = CallingConvention.Cdecl)]
    static extern IntPtr NvAPI64_QueryInterface(uint interfaceId);

    [DllImport("nvapi.dll", EntryPoint = "nvapi_QueryInterface", CallingConvention = CallingConvention.Cdecl)]
    static extern IntPtr NvAPI32_QueryInterface(uint interfaceId);

    // NvAPI function IDs
    const uint NVAPI_ID_INITIALIZE = 0x0150E828;
    const uint NVAPI_ID_ENUM_DISPLAY = 0x9ABDD40D;
    const uint NVAPI_ID_GET_DVC_INFO = 0x4085DE45;
    const uint NVAPI_ID_SET_DVC_LEVEL = 0x172409B4;

    // NvAPI delegates
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

    // ADL delegate types
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

    // ADL Adapter/Display pairs for saturation control
    struct ADLDisplayTarget
    {
        public int AdapterIndex;
        public int DisplayIndex;
    }
    static List<ADLDisplayTarget> adlDisplayTargets = new List<ADLDisplayTarget>();

    [StructLayout(LayoutKind.Sequential)]
    struct ADLAdapterInfo
    {
        public int Size;
        public int AdapterIndex;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string UDID;
        public int BusNumber;
        public int DeviceNumber;
        public int FunctionNumber;
        public int VendorID;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string AdapterName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string DisplayName;
        public int Present;
        public int Exist;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string DriverPath;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string DriverPathExt;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string PNPString;
        public int OSDisplayIndex;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct ADLDisplayID
    {
        public int DisplayLogicalIndex;
        public int DisplayPhysicalIndex;
        public int DisplayLogicalAdapterIndex;
        public int DisplayPhysicalAdapterIndex;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct ADLDisplayInfo
    {
        public ADLDisplayID DisplayID;
        public int DisplayControllerIndex;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string DisplayName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string DisplayManufacturerName;
        public int DisplayType;
        public int DisplayOutputType;
        public int DisplayConnector;
        public int DisplayInfoMask;
        public int DisplayInfoValue;
    }

    // === Constants ===
    const int WH_KEYBOARD_LL = 13;
    const int WM_KEYDOWN = 0x0100;
    const int WM_SYSKEYDOWN = 0x0104;

    const int VK_NUMPAD1 = 0x61;
    const int VK_NUMPAD2 = 0x62;
    const int VK_NUMPAD3 = 0x63;
    const int VK_END = 0x23;
    const int VK_DOWN = 0x28;
    const int VK_NEXT = 0x22;

    const uint LLKHF_EXTENDED = 0x01;

    [StructLayout(LayoutKind.Sequential)]
    struct KBDLLHOOKSTRUCT
    {
        public uint vkCode;
        public uint scanCode;
        public uint flags;
        public uint time;
        public IntPtr dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    struct DISPLAY_DEVICE
    {
        public int cb;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string DeviceName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string DeviceString;
        public int StateFlags;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string DeviceID;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string DeviceKey;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct RAMP
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public ushort[] Red;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public ushort[] Green;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public ushort[] Blue;
    }

    struct DisplayInfo
    {
        public string DeviceName;
        public string FriendlyName;
    }

    NotifyIcon trayIcon;
    int currentProfile = 1;
    bool exiting = false;

    string selectedDisplay = null;
    bool showNotifications = true;
    string language = "en";
    List<DisplayInfo> activeDisplays;
    string configPath;

    static Bitmap baseIconBmp = null;

    IntPtr hookId = IntPtr.Zero;
    LowLevelKeyboardProc hookProc;

    string L(string en, string de) { return language == "de" ? de : en; }

    // === NvAPI Init ===
    static IntPtr QueryInterface(uint id)
    {
        try
        {
            if (Environment.Is64BitProcess)
                return NvAPI64_QueryInterface(id);
            else
                return NvAPI32_QueryInterface(id);
        }
        catch { return IntPtr.Zero; }
    }

    static bool InitNvAPI()
    {
        try
        {
            IntPtr pInit = QueryInterface(NVAPI_ID_INITIALIZE);
            if (pInit == IntPtr.Zero) return false;

            nvInit = (NvAPI_Initialize_t)Marshal.GetDelegateForFunctionPointer(
                pInit, typeof(NvAPI_Initialize_t));
            if (nvInit() != 0) return false;

            IntPtr pEnum = QueryInterface(NVAPI_ID_ENUM_DISPLAY);
            IntPtr pSetDVC = QueryInterface(NVAPI_ID_SET_DVC_LEVEL);
            if (pEnum == IntPtr.Zero || pSetDVC == IntPtr.Zero) return false;

            nvEnumDisplay = (NvAPI_EnumDisplay_t)Marshal.GetDelegateForFunctionPointer(
                pEnum, typeof(NvAPI_EnumDisplay_t));
            nvSetDVC = (NvAPI_SetDVCLevel_t)Marshal.GetDelegateForFunctionPointer(
                pSetDVC, typeof(NvAPI_SetDVCLevel_t));

            nvApiReady = true;
            return true;
        }
        catch
        {
            return false;
        }
    }

    // === AMD ADL Init ===
    static IntPtr ADL_Main_Memory_Alloc(int size)
    {
        return Marshal.AllocCoTaskMem(size);
    }

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
            // Try to load AMD ADL library
            string dllName = Environment.Is64BitProcess ? "atiadlxx.dll" : "atiadlxy.dll";
            IntPtr hModule = LoadLibrary(dllName);
            if (hModule == IntPtr.Zero) return false;

            // Get function pointers
            adlMainControlCreate = GetADLDelegate<ADL_Main_Control_Create_t>(hModule, "ADL_Main_Control_Create");
            adlMainControlDestroy = GetADLDelegate<ADL_Main_Control_Destroy_t>(hModule, "ADL_Main_Control_Destroy");
            adlAdapterNumberGet = GetADLDelegate<ADL_Adapter_NumberOfAdapters_Get_t>(hModule, "ADL_Adapter_NumberOfAdapters_Get");
            adlAdapterInfoGet = GetADLDelegate<ADL_Adapter_AdapterInfo_Get_t>(hModule, "ADL_Adapter_AdapterInfo_Get");
            adlAdapterActiveGet = GetADLDelegate<ADL_Adapter_Active_Get_t>(hModule, "ADL_Adapter_Active_Get");
            adlDisplayInfoGet = GetADLDelegate<ADL_Display_DisplayInfo_Get_t>(hModule, "ADL_Display_DisplayInfo_Get");
            adlDisplayColorSet = GetADLDelegate<ADL_Display_Color_Set_t>(hModule, "ADL_Display_Color_Set");
            adlDisplayColorGet = GetADLDelegate<ADL_Display_Color_Get_t>(hModule, "ADL_Display_Color_Get");

            if (adlMainControlCreate == null || adlMainControlDestroy == null ||
                adlAdapterNumberGet == null || adlDisplayColorSet == null)
                return false;

            // Initialize ADL (1 = enumerate only connected adapters)
            ADL_Main_Memory_Alloc_Delegate memAlloc = new ADL_Main_Memory_Alloc_Delegate(ADL_Main_Memory_Alloc);
            int result = adlMainControlCreate(memAlloc, 1);
            if (result != ADL_OK) return false;

            // Get number of adapters
            int adapterCount = 0;
            result = adlAdapterNumberGet(ref adapterCount);
            if (result != ADL_OK || adapterCount <= 0) return false;

            // Get adapter info
            int adapterInfoSize = Marshal.SizeOf(typeof(ADLAdapterInfo));
            IntPtr adapterInfoPtr = Marshal.AllocCoTaskMem(adapterInfoSize * adapterCount);
            result = adlAdapterInfoGet(adapterInfoPtr, adapterInfoSize * adapterCount);
            if (result != ADL_OK)
            {
                Marshal.FreeCoTaskMem(adapterInfoPtr);
                return false;
            }

            // Find active adapters and their displays
            // Track unique adapter indices to avoid duplicates
            HashSet<int> processedAdapters = new HashSet<int>();

            for (int i = 0; i < adapterCount; i++)
            {
                ADLAdapterInfo adapterInfo = (ADLAdapterInfo)Marshal.PtrToStructure(
                    new IntPtr(adapterInfoPtr.ToInt64() + i * adapterInfoSize),
                    typeof(ADLAdapterInfo));

                // Skip duplicate adapters (same physical adapter can appear multiple times)
                if (processedAdapters.Contains(adapterInfo.AdapterIndex))
                    continue;
                processedAdapters.Add(adapterInfo.AdapterIndex);

                // Check if adapter is active
                if (adlAdapterActiveGet != null)
                {
                    int active = 0;
                    if (adlAdapterActiveGet(adapterInfo.AdapterIndex, ref active) != ADL_OK || active == 0)
                        continue;
                }

                // Get displays for this adapter
                if (adlDisplayInfoGet != null)
                {
                    int numDisplays = 0;
                    IntPtr displayInfoPtr;
                    if (adlDisplayInfoGet(adapterInfo.AdapterIndex, ref numDisplays, out displayInfoPtr, 0) == ADL_OK && numDisplays > 0)
                    {
                        int displayInfoSize = Marshal.SizeOf(typeof(ADLDisplayInfo));
                        for (int j = 0; j < numDisplays; j++)
                        {
                            ADLDisplayInfo displayInfo = (ADLDisplayInfo)Marshal.PtrToStructure(
                                new IntPtr(displayInfoPtr.ToInt64() + j * displayInfoSize),
                                typeof(ADLDisplayInfo));

                            // Check if display is connected and mapped (bit 0 = connected, bit 1 = mapped)
                            if ((displayInfo.DisplayInfoValue & 0x01) != 0 &&
                                (displayInfo.DisplayInfoValue & 0x02) != 0)
                            {
                                ADLDisplayTarget target;
                                target.AdapterIndex = adapterInfo.AdapterIndex;
                                target.DisplayIndex = displayInfo.DisplayID.DisplayLogicalIndex;
                                adlDisplayTargets.Add(target);
                            }
                        }

                        // Free display info memory allocated by ADL callback
                        if (displayInfoPtr != IntPtr.Zero)
                            Marshal.FreeCoTaskMem(displayInfoPtr);
                    }
                }
            }

            Marshal.FreeCoTaskMem(adapterInfoPtr);

            if (adlDisplayTargets.Count > 0)
            {
                adlReady = true;
                return true;
            }

            return false;
        }
        catch
        {
            return false;
        }
    }

    // === Unified Saturation Control (NVIDIA + AMD) ===
    // panelLevel = User-facing value (NVIDIA scale: 0-100, default 50)
    void SetSaturation(int panelLevel)
    {
        // Determine which display index to target (-1 = all)
        int targetIdx = -1;
        if (selectedDisplay != null)
        {
            for (int i = 0; i < activeDisplays.Count; i++)
            {
                if (activeDisplays[i].DeviceName == selectedDisplay)
                {
                    targetIdx = i;
                    break;
                }
            }
        }

        if (nvApiReady)
        {
            // NVIDIA: Internal range 0-63, Panel 50 = 0, Panel 100 = 63
            try
            {
                int internalLevel = (int)Math.Round((panelLevel - 50) * 63.0 / 50.0);
                if (internalLevel < 0) internalLevel = 0;
                if (internalLevel > 63) internalLevel = 63;

                int idx = 0;
                IntPtr handle = IntPtr.Zero;
                while (nvEnumDisplay(idx, ref handle) == 0)
                {
                    if (targetIdx < 0 || targetIdx == idx)
                        nvSetDVC(handle, 0, internalLevel);
                    idx++;
                }
            }
            catch { }
        }
        else if (adlReady)
        {
            // AMD: Range 0-200, Default = 100
            // Mapping: NVIDIA Panel 50 (default) -> AMD 100
            // Each +1 on NVIDIA panel = +2 on AMD
            try
            {
                int amdLevel = (panelLevel - 50) * 2 + 100;
                if (amdLevel < 0) amdLevel = 0;
                if (amdLevel > 200) amdLevel = 200;

                for (int i = 0; i < adlDisplayTargets.Count; i++)
                {
                    if (targetIdx < 0 || targetIdx == i)
                    {
                        ADLDisplayTarget target = adlDisplayTargets[i];
                        adlDisplayColorSet(target.AdapterIndex, target.DisplayIndex,
                            ADL_DISPLAY_COLOR_SATURATION, amdLevel);
                    }
                }
            }
            catch { }
        }
    }

    // === Registry ===
    static bool IsAdmin()
    {
        WindowsIdentity identity = WindowsIdentity.GetCurrent();
        WindowsPrincipal principal = new WindowsPrincipal(identity);
        return principal.IsInRole(WindowsBuiltInRole.Administrator);
    }

    static bool EnsureGammaRegistryKey()
    {
        try
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(
                @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\ICM", false);
            if (key != null)
            {
                object val = key.GetValue("GdiIcmGammaRange");
                key.Close();
                if (val != null && (int)val == 256) return true;
            }
            if (!IsAdmin())
            {
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = Application.ExecutablePath;
                psi.Arguments = "--setregistry";
                psi.Verb = "runas";
                psi.UseShellExecute = true;
                try
                {
                    Process p = Process.Start(psi);
                    p.WaitForExit();
                    return p.ExitCode == 0;
                }
                catch { return false; }
            }
            else
            {
                return SetGammaRegistryValue();
            }
        }
        catch { return false; }
    }

    static bool SetGammaRegistryValue()
    {
        try
        {
            RegistryKey key = Registry.LocalMachine.CreateSubKey(
                @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\ICM");
            key.SetValue("GdiIcmGammaRange", 256, RegistryValueKind.DWord);
            key.Close();
            return true;
        }
        catch { return false; }
    }

    // === Display detection ===
    static List<DisplayInfo> DetectDisplays()
    {
        List<DisplayInfo> displays = new List<DisplayInfo>();
        uint devNum = 0;
        DISPLAY_DEVICE dev = new DISPLAY_DEVICE();
        dev.cb = Marshal.SizeOf(dev);
        while (EnumDisplayDevices(null, devNum, ref dev, 0))
        {
            if ((dev.StateFlags & 1) != 0)
            {
                DisplayInfo info;
                info.DeviceName = dev.DeviceName;
                info.FriendlyName = "Display " + (displays.Count + 1) + " (" + dev.DeviceString + ")";
                displays.Add(info);
            }
            dev.cb = Marshal.SizeOf(dev);
            devNum++;
        }
        return displays;
    }

    // === Config ===
    void LoadConfig()
    {
        try
        {
            if (File.Exists(configPath))
            {
                string[] lines = File.ReadAllLines(configPath);
                foreach (string line in lines)
                {
                    int eq = line.IndexOf('=');
                    if (eq < 0) continue;
                    string k = line.Substring(0, eq).Trim();
                    string v = line.Substring(eq + 1).Trim();
                    if (k == "SelectedDisplay")
                        selectedDisplay = v.Length > 0 ? v : null;
                    else if (k == "ShowNotifications")
                        showNotifications = v != "0";
                    else if (k == "Language")
                        language = (v == "de") ? "de" : "en";
                }
            }
        }
        catch { }
    }

    void SaveConfig()
    {
        try
        {
            string[] lines = new string[] {
                "SelectedDisplay=" + (selectedDisplay != null ? selectedDisplay : ""),
                "ShowNotifications=" + (showNotifications ? "1" : "0"),
                "Language=" + language
            };
            File.WriteAllLines(configPath, lines);
        }
        catch { }
    }

    // === Keyboard Hook ===
    IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
    {
        if (nCode >= 0 && (wParam == (IntPtr)WM_KEYDOWN || wParam == (IntPtr)WM_SYSKEYDOWN))
        {
            KBDLLHOOKSTRUCT kb = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(
                lParam, typeof(KBDLLHOOKSTRUCT));

            int profile = 0;

            switch ((int)kb.vkCode)
            {
                case VK_NUMPAD1: profile = 1; break;
                case VK_NUMPAD2: profile = 2; break;
                case VK_NUMPAD3: profile = 3; break;
                case VK_END:
                    if ((kb.flags & LLKHF_EXTENDED) == 0) profile = 1;
                    break;
                case VK_DOWN:
                    if ((kb.flags & LLKHF_EXTENDED) == 0) profile = 2;
                    break;
                case VK_NEXT:
                    if ((kb.flags & LLKHF_EXTENDED) == 0) profile = 3;
                    break;
            }

            if (profile > 0)
            {
                this.BeginInvoke(new Action(delegate { ApplyProfile(profile); }));
                return (IntPtr)1;
            }
        }
        return CallNextHookEx(hookId, nCode, wParam, lParam);
    }

    // === Constructor ===
    public BrightRaider()
    {
        configPath = Path.Combine(
            Path.GetDirectoryName(Application.ExecutablePath),
            "BrightRaider.cfg");

        activeDisplays = DetectDisplays();
        LoadConfig();

        this.ShowInTaskbar = false;
        this.WindowState = FormWindowState.Minimized;
        this.FormBorderStyle = FormBorderStyle.None;
        this.Opacity = 0;

        trayIcon = new NotifyIcon();
        trayIcon.Text = "BrightRaider - Profile 1 (Normal)";
        trayIcon.Icon = MakeIcon("1", Color.FromArgb(118, 185, 0));
        trayIcon.Visible = true;

        BuildMenu();

        hookProc = new LowLevelKeyboardProc(HookCallback);
        using (Process curProcess = Process.GetCurrentProcess())
        using (ProcessModule curModule = curProcess.MainModule)
        {
            hookId = SetWindowsHookEx(WH_KEYBOARD_LL, hookProc,
                GetModuleHandle(curModule.ModuleName), 0);
        }
    }

    // === Menu ===
    void BuildMenu()
    {
        var menu = new ContextMenuStrip();

        menu.Items.Add(L("Profile 1: Normal (Num1)", "Profil 1: Normal (Num1)"), null, (s, e) => ApplyProfile(1));
        menu.Items.Add(L("Profile 2: C55 G1.5 DV60 (Num2)", "Profil 2: K55 G1.5 DV60 (Num2)"), null, (s, e) => ApplyProfile(2));
        menu.Items.Add(L("Profile 3: C55 G2.0 DV70 (Num3)", "Profil 3: K55 G2.0 DV70 (Num3)"), null, (s, e) => ApplyProfile(3));
        menu.Items.Add(new ToolStripSeparator());

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

        ToolStripMenuItem notifItem = new ToolStripMenuItem(L("Notifications", "Benachrichtigungen"));
        notifItem.Checked = showNotifications;
        notifItem.Click += delegate {
            showNotifications = !showNotifications;
            SaveConfig();
            BuildMenu();
        };
        menu.Items.Add(notifItem);

        ToolStripMenuItem langItem = new ToolStripMenuItem(L("Deutsch", "English"));
        langItem.Click += delegate {
            language = (language == "en") ? "de" : "en";
            SaveConfig();
            BuildMenu();
        };
        menu.Items.Add(langItem);

        menu.Items.Add(new ToolStripSeparator());

        // Show GPU status at bottom
        string gpuStatus = "GPU: ";
        if (nvApiReady) gpuStatus += "NVIDIA";
        else if (adlReady) gpuStatus += "AMD";
        else gpuStatus += L("None (Gamma only)", "Keiner (nur Gamma)");
        ToolStripMenuItem gpuItem = new ToolStripMenuItem(gpuStatus);
        gpuItem.Enabled = false;
        menu.Items.Add(gpuItem);

        menu.Items.Add(L("Exit", "Beenden"), null, (s, e) => ExitApp());

        trayIcon.ContextMenuStrip = menu;
    }

    void SelectDisplay(string deviceName)
    {
        selectedDisplay = deviceName;
        SaveConfig();
        BuildMenu();
        if (currentProfile > 1)
            ApplyProfile(currentProfile);
    }

    static void LoadBaseIcon()
    {
        try
        {
            string iconPath = Path.Combine(
                Path.GetDirectoryName(Application.ExecutablePath), "Icon.png");
            if (File.Exists(iconPath))
            {
                using (var fs = new FileStream(iconPath, FileMode.Open, FileAccess.Read))
                {
                    baseIconBmp = new Bitmap(fs);
                }
            }
        }
        catch { }
    }

    static Icon MakeIcon(string text, Color bg)
    {
        Bitmap bmp = new Bitmap(16, 16);
        Graphics g = Graphics.FromImage(bmp);
        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

        if (baseIconBmp != null)
        {
            // Basis-Icon zeichnen
            g.DrawImage(baseIconBmp, 0, 0, 16, 16);

            // Profilnummer als Overlay unten rechts
            Font font = new Font("Arial", 7, FontStyle.Bold);
            SizeF sz = g.MeasureString(text, font);
            float x = 16 - sz.Width;
            float y = 16 - sz.Height + 1;

            // Schwarzer Hintergrund fuer Lesbarkeit
            g.FillRectangle(new SolidBrush(Color.FromArgb(180, 0, 0, 0)),
                x - 1, y, sz.Width + 1, sz.Height - 1);
            g.DrawString(text, font, new SolidBrush(bg), x, y);
            font.Dispose();
        }
        else
        {
            // Fallback: farbiges Quadrat mit Nummer
            g.Clear(bg);
            Font font = new Font("Arial", 9, FontStyle.Bold);
            SizeF sz = g.MeasureString(text, font);
            g.DrawString(text, font, Brushes.White, (16 - sz.Width) / 2, (16 - sz.Height) / 2);
            font.Dispose();
        }

        g.Flush();
        g.Dispose();
        IntPtr hIcon = bmp.GetHicon();
        Icon icon = Icon.FromHandle(hIcon).Clone() as Icon;
        DestroyIcon(hIcon);
        bmp.Dispose();
        return icon;
    }

    // === Gamma Ramp ===
    void SetGammaRamp(double gamma, double contrast)
    {
        RAMP ramp = new RAMP();
        ramp.Red = new ushort[256];
        ramp.Green = new ushort[256];
        ramp.Blue = new ushort[256];

        for (int i = 0; i < 256; i++)
        {
            double val = i / 255.0;
            val = Math.Pow(val, 1.0 / gamma);
            val = ((val - 0.5) * contrast) + 0.5;
            int result = (int)(val * 65535.0 + 0.5);
            if (result < 0) result = 0;
            if (result > 65535) result = 65535;
            ramp.Red[i] = (ushort)result;
            ramp.Green[i] = (ushort)result;
            ramp.Blue[i] = (ushort)result;
        }

        if (selectedDisplay != null)
        {
            IntPtr hDC = CreateDC(null, selectedDisplay, null, IntPtr.Zero);
            if (hDC != IntPtr.Zero)
            {
                SetDeviceGammaRamp(hDC, ref ramp);
                DeleteDC(hDC);
            }
        }
        else
        {
            foreach (DisplayInfo di in activeDisplays)
            {
                IntPtr hDC = CreateDC(null, di.DeviceName, null, IntPtr.Zero);
                if (hDC != IntPtr.Zero)
                {
                    SetDeviceGammaRamp(hDC, ref ramp);
                    DeleteDC(hDC);
                }
            }
        }
    }

    // === Profile Application ===
    void ApplyProfile(int profile)
    {
        double gamma;
        double contrast;
        int vibrance;

        switch (profile)
        {
            case 1:
                gamma = 1.0;
                contrast = 1.0;
                vibrance = 50; // Standard
                break;
            case 2:
                gamma = 1.5;
                contrast = 1.1;
                vibrance = 60;
                break;
            case 3:
                gamma = 2.0;
                contrast = 1.1;
                vibrance = 70;
                break;
            default:
                return;
        }

        SetGammaRamp(gamma, contrast);
        SetSaturation(vibrance);
        currentProfile = profile;

        if (!exiting)
        {
            string[] namesEn = { "", "1 (Normal)", "2 (C55 G1.5 DV60)", "3 (C55 G2.0 DV70)" };
            string[] namesDe = { "", "1 (Normal)", "2 (K55 G1.5 DV60)", "3 (K55 G2.0 DV70)" };
            string[] names = (language == "de") ? namesDe : namesEn;
            Color[] colors = { Color.Black, Color.FromArgb(118, 185, 0), Color.Orange, Color.Red };

            trayIcon.Text = "BrightRaider - " + L("Profile ", "Profil ") + names[profile];
            Icon oldIcon = trayIcon.Icon;
            trayIcon.Icon = MakeIcon(profile.ToString(), colors[profile]);
            if (oldIcon != null) oldIcon.Dispose();

            if (showNotifications)
            {
                trayIcon.BalloonTipTitle = "BrightRaider";
                trayIcon.BalloonTipText = L("Profile ", "Profil ") + names[profile] +
                    L(" activated", " aktiviert");
                trayIcon.ShowBalloonTip(1500);
            }
        }
    }

    // === Exit ===
    void ExitApp()
    {
        exiting = true;

        if (hookId != IntPtr.Zero)
        {
            UnhookWindowsHookEx(hookId);
            hookId = IntPtr.Zero;
        }

        // Reset alles auf Normal
        string savedDisplay = selectedDisplay;
        selectedDisplay = null;
        SetGammaRamp(1.0, 1.0);
        SetSaturation(50);
        selectedDisplay = savedDisplay;

        // Cleanup AMD ADL
        if (adlReady && adlMainControlDestroy != null)
        {
            try { adlMainControlDestroy(); } catch { }
        }

        trayIcon.Visible = false;
        trayIcon.Dispose();
        Application.Exit();
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        ExitApp();
        base.OnFormClosing(e);
    }

    [STAThread]
    static void Main(string[] args)
    {
        if (args.Length > 0 && args[0] == "--setregistry")
        {
            bool ok = SetGammaRegistryValue();
            Environment.Exit(ok ? 0 : 1);
            return;
        }

        Application.EnableVisualStyles();

        // Load icon and GPU APIs
        LoadBaseIcon();
        InitNvAPI();
        if (!nvApiReady) InitADL();

        bool regOk = EnsureGammaRegistryKey();
        if (!regOk)
        {
            MessageBox.Show(
                "The registry entry for GammaRamp could not be set.\n" +
                "Please run as administrator or set the entry manually:\n\n" +
                "HKLM\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\ICM\n" +
                "GdiIcmGammaRange = 256 (DWORD)",
                "BrightRaider - Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }

        Application.Run(new BrightRaider());
    }
}
