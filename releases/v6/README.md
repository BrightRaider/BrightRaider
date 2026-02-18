# BrightRaider V6

**See enemies in the dark. No alt-tab, no game files modified.**

BrightRaider is a lightweight Windows tray tool that lets you switch display brightness, contrast and digital vibrance with a single keypress. Built for Arc Raiders players who struggle with dark caves, shadows and low visibility — but works with any game.

One EXE, zero dependencies, ~65 KB.

![Windows](https://img.shields.io/badge/Windows-10%2F11-blue) ![NVIDIA](https://img.shields.io/badge/NVIDIA-supported-green) ![AMD](https://img.shields.io/badge/AMD-supported-red) ![.NET](https://img.shields.io/badge/.NET%20Framework-4.0-purple)

## Before / After

**[► Interactive Before/After Slider](https://imgsli.com/NDUwMzQ2)**

## What's new in V6

- **Lemon Squeezy licensing** — Buy Pro, get your key instantly by email. No manual delivery.
- **Online activation** — One-time internet connection to activate. Offline forever after.

## Features

### Free
- **3 brightness profiles** — Normal, Bright, Brighter
- **Instant hotkey switching** — Numpad 1/2/3, works in fullscreen
- **Gamma + Contrast + Digital Vibrance** control
- **NVIDIA + AMD + Intel** support
- **Multi-monitor** support
- **English / German** interface
- Portable — no installation, just one EXE

### Pro ($4.99)
- **Up to 9 profiles** with full customization
- **Auto-Brightness** — automatically adjusts based on screen content
- **Calibration Wizard** — two clicks to set up auto-brightness
- **Profile Editor** — fine-tune gamma, contrast, vibrance per profile
- **Game Mute** — mute only the game audio with Numpad 0
- **Auto-Start** with Windows

## Pro Activation

1. **[Buy Pro License ($4.99)](https://brightraider.lemonsqueezy.com/checkout/buy/a7d69bd7-a83c-40d7-828d-01851f47d317)**
2. Check your email for the license key
3. Right-click tray icon → **Settings** → **Enter License**
4. Enter your email and license key
5. Done — requires internet once, offline forever after

## Hotkeys

| Key | Action | Version |
|-----|--------|---------|
| Numpad 1-3 | Switch profile | Free |
| Numpad 4-9 | Switch profile | Pro |
| Numpad 0 | Mute/unmute game | Pro |

## First Launch

On first launch, BrightRaider sets one registry entry to unlock gamma adjustment:

```
HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\ICM → GdiIcmGammaRange = 256
```

A UAC prompt appears once. **Restart your PC after the first launch** (only needed once).

## Anti-Cheat Safety

BrightRaider does **NOT** modify game files, inject DLLs, or read game data.
It only uses Windows GDI, NVIDIA NvAPI and AMD ADL — the same as your monitor settings.

---

Made for the Arc Raiders community.
