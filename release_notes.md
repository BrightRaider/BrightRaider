# BrightRaider V1.0.0 — Pre-Release

> 🎉 **Pre-release intro price:** V1.0 is in its final testing phase. For as long as it's in pre-release it stays at the V9.x Pro price of **€5.49** — buy now and you keep that price forever. Once every open report is closed and V1.0 ships as the final build, the price moves to **€8.99** to reflect the new feature set (Footstep Booster, Background AutoMute, Audio Output Switcher, Process Optimizer, plus the upgraded Map Scanner / Auto-Brightness / QuickSelect / QuickSave).
>
> **V9.x license holders:** your existing key works on V1.0 at no extra cost — just re-enter it once (see upgrade note below). The price change does not affect you either way.

**Goodbye VibranceGUI. Hello one tool that does it all.**

BrightRaider V1.0 replaces VibranceGUI completely — and adds everything VibranceGUI never had: **per-game FPS limits, Hue control, Alt-Tab Auto-Switch**, and a real configurable hotkey system. **All free.**

If you're still running VibranceGUI alongside a profile-switcher: uninstall both. V1.0 is the single tool you keep.

---

## Why this is different

VibranceGUI was free, single-purpose, and unmaintained. BrightRaider V1.0 picks up where it stopped and goes way further — without taking anything away.

| | VibranceGUI | **BrightRaider V1.0 (Free)** | BrightRaider V1.0 Pro |
|---|---|---|---|
| Per-game Vibrance | ✅ | ✅ | ✅ |
| Per-game Gamma | ❌ | ✅ | ✅ |
| Per-game Contrast | ❌ | ✅ | ✅ |
| Per-game **Hue** | ❌ | ✅ | ✅ |
| Per-game **FPS limit** | ❌ | ✅ | ✅ |
| Alt-Tab Auto-Switch | ✅ | ✅ | ✅ |
| Hotkey-switchable profiles | ❌ | 3 | **9** |
| Original-gamma restore on exit | ❌ | ✅ | ✅ |
| Configurable hotkeys + modifiers | ❌ | ✅ | ✅ |
| Active development | ❌ | ✅ | ✅ |
| **Auto-Brightness** with Calibration Wizard | ❌ | ❌ | ✅ |
| **Map Scanner** (Arc Raiders, 13 events) | ❌ | ❌ | ✅ |
| **QuickSelect** (one-key item use) | ❌ | ❌ | ✅ |
| **QuickSave** (one-key inventory drag) | ❌ | ❌ | ✅ |
| **Crosshair Overlay** (6 styles) | ❌ | ❌ | ✅ |
| **Footstep Booster** (per-process limiter) | ❌ | ❌ | ✅ |
| **Background AutoMute** (Alt-Tab → mute) | ❌ | ❌ | ✅ |
| **Audio Output Switcher** (hotkey + game auto-switch) | ❌ | ❌ | ✅ |
| **Process Optimizer** (priority + affinity) | ❌ | ❌ | ✅ |
| **Autorun** (CapsLock → auto-W + Sprint) | ❌ | ❌ | ✅ |
| **Audio Ducking** (hold-to-dim) | ❌ | ❌ | ✅ |

Everything in the **Free** column ships with the EXE. No license, no nag screen, no time limit. **Pro** is a one-time €5.49 (€8.99 after launch week) — see Pro feature details below.

---

## ⚡ Free in V1.0 — the full list

### 🆕 New in V1.0 (Free)

- **Alt-Tab Auto-Switch** — per-game profile (Gamma / Contrast / Vibrance / Hue) **and** per-game FPS limit, applied automatically when the game enters the foreground. Reverts to your original ramps on Alt-Tab out. The killer-feature that replaces VibranceGUI.
- **Per-game FPS limit** — NVIDIA via NvAPI DRS, AMD via ADLX FRTC. Saves GPU power, lowers fan noise.
- **Per-game Hue** — fourth color axis added alongside Gamma / Contrast / Vibrance. Independent per profile and per game.
- **Configurable hotkeys** — every key reassignable, modifier support (`Ctrl+5`, `Alt+F2`, `Shift+Numpad 3`, mouse MB3/4/5, scroll-wheel).
- **Setup Wizard** — picks Numpad / TKL / AZERTY defaults on first launch, re-callable from Settings.
- **One EXE for everyone** — Numpad and Arrow-key versions merged. AZERTY-friendly out of the box.
- **Update notifier in the tray** — one-line notice when a new release is published.
- **Original gamma persistence** — your DisplayCAL / ICC calibration is saved to disk on first launch and restored on every exit, no matter how BrightRaider stops. Survives crashes.
- **Native AOT** — no .NET runtime install needed, sub-second startup.
- **Dark mode** — full dark theme for the Settings window. Toggle in *Settings → App → Theme* (Light / Dark / Follow OS).

### ⬆️ Improved in V1.0 (Free)

- **3 hotkey-switchable display profiles** — now with Hue control + Original-gamma restore.
- **Per-monitor selection** — apply to one specific display or all together.
- **Break reminder** — configurable interval, audible cue optional, live toggle (no app restart needed).

---

## 🔓 Pro features (€5.49 one-time)

For users who want the full Arc Raiders / power-user toolbox.

### 🆕 New in V1.0

- **Footstep Booster** — per-process audio limiter. Crank in-game volume to hear footsteps without going deaf on gunshots. Configurable threshold / attack / release.
- **Background AutoMute** — the game's audio session is muted automatically when you Alt-Tab out, unmuted on return. Discord and music stay untouched.
- **Process Optimizer** — opt-in High process priority + physical-cores-only affinity (Hyperthreading off) for the foreground game.
- **Audio Output Switcher** — switch your default output device with one hotkey, cycling through the devices you choose (2, a few, or all). Optional auto-switch to a chosen device when a game starts, restoring the previous device when it closes (alt-tabbing out does **not** switch back). Switches all roles including communications, so Discord voice follows too.

### ⬆️ Significantly improved in V1.0

- **Map Scanner** (Arc Raiders) — now **~100 % OCR hit rate** with the new template-matching pipeline (replaces WinRT OCR). Detects all **13 current map conditions** including Night Raid, Hurricane, Electromagnetic Storm, Harvester, Lush Blooms, Matriarch, Husk Graveyard, Close Scrutiny, Bird City, Locked Gate, Launch Tower Loot, Beachcombing, and the base no-event state. Per-state threshold colors, configurable overlay background opacity, audible Evac alarm with separate min/sec threshold.
- **Auto-Brightness** — Calibration Wizard streamlined to two captures (darkest / brightest), Profile ranges distribute automatically. 5-zone weighted screen sampler with per-zone weights and a debug overlay showing the actual sample rectangles in real time.
- **QuickSelect** — 8 LMB-timer presets (was 6), per-slot Use/Q/H toggles, MB3/4/5 + scroll-wheel rebind, mouse-movement slot path for wheel positions 7 and 8.
- **QuickSave** — 5 drag presets with full timing controls, toggle-direction per preset, Tab-key rebindable, MB / wheel triggers supported.
- **Crosshair Overlay** — 6 styles, separate outline color, configurable size 4–50 px.
- **Autorun** — Sprint mode + Tap mode (CapsLock-hold 600 ms) for Looting Mk. 3, AZERTY (Z-forward) support.
- **Audio Ducking** — hold the mute key to dim the game to a configurable level.

### 🔓 Always included

- **Profiles 4–9** (six more hotkey-switchable color presets, all axes editable)

Activate via *Settings → App → Enter license*.

---

## ⚠️ Upgrade note for V9.x users

Your **settings, profiles, hotkeys, and game profiles migrate automatically** on first V1.0 launch. Nothing to re-configure.

**Your license key must be re-entered once.** Open *Settings → App → Enter license*, paste your existing key. Lemon Squeezy re-issues your activation instantly — same email, same key, no re-purchase.

V1.x updates after this won't require another re-entry.

---

## 🔧 Updated during pre-release week

Thanks to everyone testing and sending feedback — here's what changed since the first pre-release build:

- **🆕 Audio Output Switcher (Pro)** — switch speakers ↔ headphones (or any set of devices) with one hotkey, plus optional auto-switch when a game starts/closes. Switches all roles incl. communications.
- **Brightness profiles fixed** — the gamma/contrast curve is back to how it felt in 9.6.1; "Brighter" is usable again. *(#60)*
- **Hue now 0–359° on Nvidia** — matches the Control Panel exactly, no mental math. Range adapts to your GPU. *(#39)*
- **Crosshair outline thickness** — new 1–5 slider next to the outline color. *(#61)*
- **Sliders take the mouse wheel + type-in fields** — every slider can be scrolled or have an exact value typed. *(#59)*
- **"Only run hotkeys while a game is focused"** — optional switch so BrightRaider's keys never interfere on the desktop. *(#58)*
- **Overlays follow the monitor you pick** — when you select a specific display in the tray (or Settings → Display), the crosshair, map-scanner and timer overlays all stay on that monitor and no longer jump when you click another screen. Vibrance/Hue now apply only to the chosen monitor too. On "All Monitors" the overlays anchor to the game's monitor instead of chasing whatever window you click, and (in Alt-Tab mode) hide cleanly when you tab out and return instantly when you tab back. *(#55)*
- Fixed a Setup-Wizard re-run that could be overwritten on the next Apply.
- **License persistence fix** — in a rare case where the store didn't return an email with the activation, the saved license file could be dropped, sending you back to Free after a restart. The key alone now persists correctly.
- **License activation now unlocks Pro live** — activating a key unlocks the features, the greyed-out controls **and** clears the gold PRO badges immediately, no restart needed. *(#62)*
- **Duplicate-key warning is now visible** — binding a key that's already in use marks that rebind button with a red outline (the easy-to-miss toast wasn't enough). *(#62)*
- **"Advanced" toggle** — a checkbox in the Settings footer hides the QuickSelect / QuickSave millisecond-timing fields for a simpler page. Off for new installs, on for existing users. *(#63)*
- **Focus self-heal** — on a fast window switch (borderless multi-monitor) BrightRaider could briefly stop reacting until you hit Apply; it now re-syncs automatically. *(#58, #62)*
- **Settings layout fixes** — the QuickSelect tab no longer runs off the right edge (its cards stack instead of sitting side-by-side), QuickSave/QuickSelect checkbox columns are centered under their headers, the Audio sliders now shrink with the window instead of overlapping, the left tab bar is tighter, and the window has a sensible minimum size so tabs can't be squeezed into overlap. *(#55)*

## System requirements

- Windows 10 / 11 (64-bit)
- NVIDIA or AMD GPU recommended (Intel: gamma + contrast only, no vibrance/hue/FPS-limit)
- No .NET runtime install required (Native AOT — single EXE)

## SHA-256

```
BrightRaider.exe              EAF91C581A2B313A7C17445F45FDA343EECF35F0029BEF7C7F98A1F91E0740A9
```

Verify on Windows: `Get-FileHash BrightRaider.exe -Algorithm SHA256`

---

## 📖 Documentation — read online or download

Read directly on GitHub (no download needed):

- 📘 **[Manual](https://github.com/BrightRaider/BrightRaider/blob/main/docs/Manual.txt)** — full reference (English + German)
- 🗺️ **[Map Scanner Guide](https://github.com/BrightRaider/BrightRaider/blob/main/docs/MapScanner_Guide.md)** — maps, events, threshold colors, alarm
- 🔆 **[Auto-Brightness Guide](https://github.com/BrightRaider/BrightRaider/blob/main/docs/AutoBrightness_Guide.md)** — Calibration Wizard walkthrough, zone weights
- 🔊 **[Footstep Booster Guide](https://github.com/BrightRaider/BrightRaider/blob/main/docs/FootstepBooster_Guide.md)** — threshold/attack/release tuning
- ⚡ **[QuickSelect Guide](https://github.com/BrightRaider/BrightRaider/blob/main/docs/QuickSelect_Guide.md)** — LMB timer presets, modifier bindings
- 💾 **[QuickSave Guide](https://github.com/BrightRaider/BrightRaider/blob/main/docs/QuickSave_Guide.md)** — drag presets, toggle direction
- 📜 **[Changelog](https://github.com/BrightRaider/BrightRaider/blob/main/docs/CHANGELOG_PUBLIC.txt)** — public release history (V4 → V1.0)

## 📦 Download + install

**Download `BrightRaider.exe` and double-click it.** That's it.

On first launch the EXE unpacks its runtime into `%LOCALAPPDATA%\BrightRaider\` and starts the app from there. Subsequent launches skip unpacking and start near-instantly — feels like any single-EXE tool.

Want to fully uninstall? Delete the EXE and the folder `%LOCALAPPDATA%\BrightRaider\`.

Documentation lives online — see the Documentation section below for clickable links.
