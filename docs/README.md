# BrightRaider

**See enemies in the dark. No alt-tab, no game files modified.**

BrightRaider is a lightweight Windows tray tool that lets you switch display brightness, contrast and digital vibrance with a single keypress. Built for Arc Raiders players who struggle with dark caves, shadows and low visibility — but works with any game.

One EXE, zero dependencies, ~330 KB.

![Windows](https://img.shields.io/badge/Windows-10%2F11-blue) ![NVIDIA](https://img.shields.io/badge/NVIDIA-supported-green) ![AMD](https://img.shields.io/badge/AMD-supported-red) ![.NET](https://img.shields.io/badge/.NET%20Framework-4.7.2-purple)

## Why BrightRaider?

NVIDIA Game Filters are blocked by anti-cheat (EAC). Monitor OSD is slow and clunky. Alt-tabbing to adjust settings gets you killed.

BrightRaider uses standard Windows display APIs — the same way your NVIDIA Control Panel or monitor settings work. **Safe with all anti-cheat systems** (EAC, BattlEye, Vanguard).

## Features

### Free
- **3 brightness profiles** — Normal, Bright, Brighter
- **Instant hotkey switching** — works in fullscreen
- **Gamma + Contrast + Digital Vibrance** control
- **NVIDIA + AMD + Intel** support (GDI fallback for any GPU)
- **Multi-monitor** support
- **English / German** interface
- **Break Reminder** — orange toast after a configurable interval
- Portable — no installation, just one EXE

### Pro (€5.49 one-time)
- **QuickSelect** — press a key to auto-use a quick-wheel item (Q-hold → slot → LMB). 5 slots, 6 item-specific timer presets, H-key to free hand
- **Auto-Brightness** — automatically adjusts based on screen content. Dark area? Brightness goes up. Step outside? Back to normal
- **Autorun** — CapsLock to hold W automatically. Sprint, Walk, or Tap Mode (pulses W at intervals — for the Looting Mk.3 Survivor augment)
- **Map Scanner** — hold M on the in-game map to OCR-read all evacuation timers. Live countdown overlay on screen
- **Evac Alarm** — red toast + sound when a timer drops below your threshold
- **Audio Ducking** — hold mute key 600ms to reduce game volume to a set %. Short press still mutes
- **Crosshair Overlay** — click-through, 4 styles, fully customizable. EAC-safe
- **Calibration Wizard** — two clicks to set up auto-brightness
- **Profile Editor** — fine-tune gamma, contrast, vibrance per profile (up to 9 profiles)
- **Game Mute** — mute only the game audio, not Discord
- **Hotkey Pause** — temporarily disable all hotkeys
- **Vibrance Restore** — original vibrance restored on exit

## Two Versions

| | `BrightRaider.exe` | `BrightRaider_Arrows.exe` |
|-|---|---|
| Profile switch | Numpad 1/2/3 | Arrow Left/Down/Right |
| Mute (short) | Numpad 0 | Arrow Up |
| Audio Ducking (hold) | Numpad 0 | Arrow Up |
| Crosshair | Numpad + | Insert |
| Map overlay toggle | Numpad * | Delete |
| Autorun | CapsLock | CapsLock |
| QuickSelect trigger | 3/4/5 (default) | 3/4/5 (default) |
| QuickSelect toggle | Numpad Minus | Pos1 (Home) |
| Designed for | Full keyboard / Numpad | TKL / Laptop |

## Default Profiles

| Key | Name | Gamma | Contrast | Vibrance |
|-----|------|-------|----------|----------|
| Num 1 / ← | Normal | 1.0 | 100% | 50% |
| Num 2 / ↓ | Bright | 1.5 | 110% | 60% |
| Num 3 / → | Brighter | 2.0 | 110% | 70% |
| Num 4-9 | Custom [PRO] | Editable | Editable | Editable |

## Download

**[Download Latest Release](../../releases/latest)**

Two files available: `BrightRaider.exe` (Numpad) and `BrightRaider_Arrows.exe` (Arrow keys). Run either directly — no installation needed.

## Quick Start

1. Run `BrightRaider.exe` — a tray icon appears
2. Press **Numpad 1** — Normal brightness
3. Press **Numpad 2** — Bright (better visibility in dark areas)
4. Press **Numpad 3** — Brighter (maximum visibility)
5. That's it. Switch anytime, even in fullscreen.

## First Launch

On first launch, BrightRaider sets one registry entry to unlock gamma adjustment:

```
HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\ICM → GdiIcmGammaRange = 256
```

This tells Windows to allow gamma adjustments — used by many display calibration tools, harmless, can be removed anytime. A UAC prompt appears once. **Restart your PC after the first launch** (only needed once).

## Pro Activation

1. **[Buy Pro License (€5.49)](https://brightraider.lemonsqueezy.com/checkout/buy/9b93d8c0-262f-43a4-bd41-167557efb156)**
2. Right-click tray icon → **Settings** → **Enter License**
3. Enter your email and the license key from the purchase confirmation
4. Done — all Pro features unlocked permanently. No subscription, no internet required after activation.

## How It Works

BrightRaider adjusts your display output using standard Windows APIs:

- **GDI** (`SetDeviceGammaRamp`) — gamma & contrast, works on every GPU
- **NvAPI** — NVIDIA Digital Vibrance (hardware-level saturation)
- **ADL** — AMD Radeon saturation control

Nothing is modified in the game. Nothing is injected. It's the equivalent of changing your monitor brightness — just faster and with presets.

### Auto-Brightness (Pro)

Analyzes 5 small zones across your screen (center + 4 corners) using median brightness measurement. Based on the result, it smoothly interpolates between your profiles. Darker screen = more boost, brighter screen = less. The transition is seamless.

Calibrate in two steps: measure the darkest spot, measure the brightest spot, done.

### QuickSelect (Pro)

Intercepts your configured trigger keys, then sends: Q-hold → slot key → Q-up → LMB-hold (per-item duration) → H (optional). Everything via PostMessage to the game window — confirmed working in Arc Raiders.

📖 [QuickSelect Setup Guide](docs/QuickSelect_Guide.md)

## Anti-Cheat Safety

BrightRaider does **NOT**:
- Modify game files or memory
- Inject DLLs into game processes
- Hook into the game in any way
- Read game data

BrightRaider **ONLY** uses:
- Windows GDI — same as your monitor settings
- NVIDIA NvAPI — same as NVIDIA Control Panel
- AMD ADL — same as AMD Radeon Software

Anti-cheat systems do not flag display adjustments.

## System Requirements

- Windows 10 / 11
- .NET Framework 4.7.2 (pre-installed on every Windows 10/11)
- NVIDIA or AMD GPU recommended (Intel works with gamma-only)

## Uninstall

1. Exit BrightRaider (right-click tray → Exit)
2. Delete the folder
3. Optional: Remove `GdiIcmGammaRange` from `HKLM\...\ICM`
4. Optional: Remove auto-start from `HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Run`

## FAQ

**Does this work with other games?**
Yes. BrightRaider adjusts your display, not the game.

**Will I get banned?**
No. It uses the same Windows APIs as your monitor settings.

**Do I need Pro?**
Free is fully functional. Pro adds automatic switching, QuickSelect item usage, and advanced features so you never take your hand off the mouse.

**Why does my antivirus flag it?**
BrightRaider uses a low-level keyboard hook — the same mechanism as Logitech GHub, Razer Synapse, and Discord Push-to-Talk. No data is logged or transmitted. Without a code signing certificate (~$150/year), some AV engines will always flag tools like this. 2,000+ downloads, zero actual issues.
