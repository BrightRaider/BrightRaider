# BrightRaider

**Switch Gamma, Contrast & Digital Vibrance with one keypress.**

A lightweight system tray tool for **NVIDIA and AMD** graphics cards. Instantly toggle between display profiles using your Numpad keys — perfect for gaming, video editing, or boosting visibility in dark scenes.

![Windows](https://img.shields.io/badge/Windows-10%2F11-blue) ![NVIDIA](https://img.shields.io/badge/NVIDIA-supported-green) ![AMD](https://img.shields.io/badge/AMD-supported-red) ![.NET](https://img.shields.io/badge/.NET%20Framework-4.0-purple)

## Features

- **3 display profiles** switchable via Numpad 1 / 2 / 3
- Adjusts **Gamma**, **Contrast**, and **Digital Vibrance / Saturation** together
- **NVIDIA + AMD** — auto-detects your GPU and uses the right API
- Works with Shift held down, NumLock on or off
- Per-monitor control (choose one or all monitors)
- Notifications toggle
- English / German language support
- GPU status indicator in tray menu
- Custom tray icon with colored profile number overlay
- All settings saved automatically
- Resets everything to normal on exit
- **Portable** — no installation, just one EXE + icon file

## Profiles

| Key | Name | Gamma | Contrast | Digital Vibrance |
|-----|------|-------|----------|-----------------|
| Num 1 | Normal | 1.0 | 50% | 50% (NVIDIA DV / AMD Sat) |
| Num 2 | Bright | 1.5 | 55% | 60% |
| Num 3 | Brighter | 2.0 | 55% | 70% |

## Requirements

- Windows 10 or 11
- **NVIDIA** (GeForce, RTX, Quadro) or **AMD** (Radeon, RX) graphics card
- .NET Framework 4.0 (pre-installed on Windows 10/11)
- Also works on Intel/other GPUs (Gamma + Contrast only, no saturation)

## First Launch

On first launch, BrightRaider needs to set **one registry entry** to unlock gamma adjustment:

```
HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\ICM
GdiIcmGammaRange = 256
```

**What does this do?** Windows blocks apps from adjusting display gamma by default. This entry simply tells Windows: *"Allow gamma adjustments."* It does not change any display settings, does not affect system stability, and is used by many display calibration tools. It can be safely removed at any time.

A UAC prompt will appear once — click **Yes**. Then **restart your PC** (only needed once). After that, the app runs without admin rights.

## Usage

**Hotkeys:** Press Numpad 1, 2, or 3 to switch profiles.

**Tray Menu (right-click):**
- Switch profiles
- Select monitor (one or all)
- Toggle notifications
- Switch language
- Exit

## Build from Source

Compile with the .NET Framework C# compiler (no Visual Studio needed):

```cmd
C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe /target:winexe /win32icon:Icon.ico BrightRaider.cs
```

To generate `Icon.ico` from `Icon.png`, use any PNG-to-ICO converter.

## Files

| File | Description |
|------|-------------|
| `BrightRaider.exe` | The compiled program |
| `BrightRaider.cs` | Full source code |
| `Icon.png` | App icon |
| `Icon.ico` | Icon for EXE embedding |
| `BrightRaider.cfg` | Settings (auto-created at runtime) |
| `Anleitung - Manual.txt` | Manual in English & German |
| `CHANGELOG.txt` | Version history |

## Uninstall

1. Exit BrightRaider (right-click tray -> Exit)
2. Delete the folder
3. Optional: Remove the registry entry `GdiIcmGammaRange` from `HKLM\...\ICM` (harmless if left in place)

## License

Free to use and modify.
