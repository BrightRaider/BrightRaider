# BrightRaider

**See enemies in the dark. No alt-tab, no game files modified.**

BrightRaider is a lightweight Windows tray tool that lets you switch display brightness, contrast and digital vibrance with a single keypress. Built for Arc Raiders players who struggle with dark caves, shadows and low visibility — but works with any game.

One EXE, zero dependencies, ~21 MB (Native AOT — no .NET runtime install required). Works with any keyboard — numpad, TKL, or fully custom bindings.

![Windows](https://img.shields.io/badge/Windows-10%2F11-blue) ![NVIDIA](https://img.shields.io/badge/NVIDIA-supported-green) ![AMD](https://img.shields.io/badge/AMD-supported-red) ![.NET](https://img.shields.io/badge/.NET-9%20AOT-purple)
[![Downloads](https://img.shields.io/github/downloads/BrightRaider/BrightRaider/total?label=Downloads&color=brightgreen)](https://github.com/BrightRaider/BrightRaider/releases)

## Before / After

![Normal vs Bright](assets/screenshots/comparison2.png)

## Screenshots — V1.0

Two hero shots — the rest is one click away.

<table>
<tr>
  <td valign="top" width="40%">
    <strong>Tray menu</strong><br>
    Per-profile color marker, quick toggles for Alt-Tab Auto-Switch and Auto-Brightness, monitor picker, license status, one-click Settings.
    <br><br>
    <img src="assets/screenshots/v1.0/tray.png" alt="Tray menu" width="280">
  </td>
  <td valign="top" width="60%">
    <strong>Map Scanner overlay — in-game</strong><br>
    Long-press M → ~100 % OCR hit rate, detects all 13 current map conditions, color-coded evac timers, audible alarm below threshold.
    <br><br>
    <img src="assets/screenshots/v1.0/mapscanner-overlay.png" alt="Map Scanner Overlay" width="500">
  </td>
</tr>
</table>

<details>
<summary><b>📸 See every Settings tab</b> (click to expand — 10 screenshots)</summary>

<br>

**Game Profiles — the Free killer feature.** Per-game profile + FPS limit, auto-switched on Alt-Tab.

<img src="assets/screenshots/v1.0/settings-gameprofiles.png" alt="Game Profiles tab" width="780">

**Display + Profiles.** 9 profiles, each with Gamma / Contrast / Vibrance / Hue and an optional brightness range for Auto-Brightness.

<img src="assets/screenshots/v1.0/settings-display.png" alt="Display tab" width="780">

**Auto-Brightness (Pro).** 5-zone screen sampling, Calibration Wizard, optional debug overlay.

<img src="assets/screenshots/v1.0/settings-autobrightness.png" alt="Auto-Brightness tab" width="780">

**Audio (Pro).** Footstep Booster (per-process limiter), Audio Ducking, Background AutoMute.

<img src="assets/screenshots/v1.0/settings-audio.png" alt="Audio tab" width="780">

**Map Scanner (Pro).** Per-state threshold colors, overlay position + background opacity, Evac alarm threshold.

<img src="assets/screenshots/v1.0/settings-mapscanner.png" alt="Map Scanner tab" width="780">

**Crosshair (Pro).** Click-through overlay, 6 styles, custom color + outline.

<img src="assets/screenshots/v1.0/settings-crosshair.png" alt="Crosshair tab" width="780">

**QuickSelect (Pro).** 8 slots, 8 LMB timer presets, modifier-key bindings, MB3/4/5 + wheel triggers.

<img src="assets/screenshots/v1.0/settings-quickselect.png" alt="QuickSelect tab" width="780">

**QuickSave (Pro).** 5 drag presets, toggle direction, configurable timing.

<img src="assets/screenshots/v1.0/settings-quicksave.png" alt="QuickSave tab" width="780">

**Hotkeys.** Every key reassignable. Modifier-key support (Ctrl / Alt / Shift). Mouse MB3/4/5 + scroll wheel.

<img src="assets/screenshots/v1.0/settings-hotkeys.png" alt="Hotkeys tab" width="780">

**App + Performance.** Theme, language, license, Break reminder, display-reset emergency buttons. Performance tab: optional High priority + physical-cores-only affinity for the foreground game.

<img src="assets/screenshots/v1.0/settings-app.png" alt="App tab" width="780">
<img src="assets/screenshots/v1.0/settings-performance.png" alt="Performance tab" width="780">

</details>

## Demo Videos

> **Note on video quality:** BrightRaider works by adjusting display output at the GPU level — the same way your monitor brightness works. Because of this, screen recording software cannot capture the actual brightness changes. The videos were recorded with a phone camera pointed at the monitor, which is why the quality is lower than usual. This is also proof that BrightRaider is not a cheat — it only changes display settings, nothing inside the game.

**Free version** — Profile switching with hotkeys (starts at 0:06):
[![BrightRaider Free - Profile Switching](https://img.youtube.com/vi/ZjRZKfPi7Ok/0.jpg)](https://youtu.be/ZjRZKfPi7Ok?t=6)

**Pro version** — Auto-Brightness in action:
[![BrightRaider Pro - Auto-Brightness](https://img.youtube.com/vi/q4DPRjHs24g/0.jpg)](https://youtu.be/q4DPRjHs24g?t=4)

## Why BrightRaider?

Monitor OSD is slow and clunky. Alt-tabbing to adjust settings gets you killed.

BrightRaider uses standard Windows display APIs — the same way your NVIDIA Control Panel or monitor settings work.

> **VibranceGUI is no longer needed.** BrightRaider V1.0 auto-switches vibrance and FPS limits per game as you alt-tab — everything VibranceGUI does, in one place. You can uninstall it.

> ⚠️ **Antivirus false positive?** Some AV tools flag BrightRaider due to its global keyboard hook (same mechanism as Logitech GHub, Razer Synapse, Discord Push-to-Talk). No data is logged or transmitted. 100–200 downloads daily, zero reports — [see stats](https://github-release-stats.ghostbyte.dev/BrightRaider/BrightRaider).

## Features

### Free
- **3 brightness profiles** — Normal, Bright, Brighter
- **Instant hotkey switching** — works in fullscreen
- **Gamma + Contrast + Digital Vibrance** control
- **FPS Limit per game** — set a cap per process, BrightRaider switches it automatically as you alt-tab. Cap Arc Raiders at your monitor refresh rate, set CS2 to unlimited. Reduces GPU power draw and heat significantly — less noise, cooler hardware. Set once, forget it. → [Optimal FPS cap settings (Blur Busters gsync 101)](https://blurbusters.com/gsync/gsync101-input-lag-tests-and-settings/)
- **Automatic Vibrance Switching** — gaming vibrance when your game is in focus, normal desktop vibrance when you alt-tab. Replaces VibranceGUI entirely.
- **Rebindable hotkeys** — all keys configurable in Settings → Input. Works with numpad, TKL, QWERTZ, AZERTY — any keyboard.
- **NVIDIA + AMD + Intel** support (GDI fallback for any GPU)
- **Multi-monitor** support
- **English / German** interface
- **Break Reminder** — shows an orange reminder toast after a configurable interval (default: 45 min). Configure under Settings → Break Reminder. Does not interrupt gameplay.
- **Auto-Start with Windows**
- Portable — no installation, just one EXE

### Pro (€5.49)
- **QuickSave** — one keypress drags an item from your inventory to your Safe Pocket (or back). Handles everything: open inventory → drag → close. 5 independent presets, configurable slots, toggle direction per preset. 📖 [Setup Guide](docs/QuickSave_Guide.md)
- **QuickSelect** — press a single key to automatically use an item from your quick-use wheel. Handles everything: hold Q → select slot via mouse → release Q → hold LMB → press H. 8 independent slots, 6 LMB timer presets (Adrenalin Syringe, Bandage, Shield Charger…), all keys freely rebindable. 📖 [Setup Guide](docs/QuickSelect_Guide.md)
- **Auto-Brightness** — automatically adjusts based on screen content. Dark area? Brightness goes up. Step outside? Back to normal. Smooth transitions, no stutter. Zone weights configurable (Settings → Zones...) — reduce corner influence if your HUD covers them.
- **Map Scanner** — hold M on the map to automatically read all evacuation timers. Takes a screenshot, reads the timers via Windows built-in OCR (no game files or memory touched), and shows a live color-coded countdown overlay on screen. Detects active events (Night, Hurricane, Electromagnetic Storm) and adjusts active evac points automatically. Supports Buried City, Stella Montis, Space Port, Blue Gate, Damm and Riven Tides.
- **Evac Alarm** — get a red toast + sound alert when an evac timer drops below your configured threshold (set in minutes + seconds). Configure inside Settings → Map Scanner → Settings.
- **Autorun** — short press CapsLock to hold the forward key (walk or sprint). Hold CapsLock 600ms for **Tap Mode** — pulses forward key at intervals, perfect for the **Looting Mk. 3 (Survivor)** augment (keeps health at 75% while moving). Shift toggles sprint. C triggers a slide. Forward key is rebindable — works with any layout (QWERTZ, AZERTY, etc.).
- **Crosshair Overlay** — A clean, click-through crosshair directly on your screen. 6 styles: Cross, Dot+Ring, T-Shape, Dot, Ring, Cross-with-gap. Optional outline for visibility on bright backgrounds. Same overlay mechanism as Discord and GeForce Experience.
- **Audio Ducking** — hold the mute key 600ms to duck game audio to 20%. Short press still mutes/unmutes. Configure volume and hold duration in Settings.
- **Game Mute** — mute only the game audio with a single key. Your Discord, music, everything else stays on.
- **Hue per Profile** — per-profile color temperature control alongside Vibrance. AMD supported in V1.0.
- **Up to 9 profiles** with full customization
- **Calibration Wizard** — two clicks to set up auto-brightness
- **Profile Editor** — fine-tune gamma, contrast, vibrance, hue per profile

## Default Profiles

| Key (default) | Name | Gamma | Contrast | Vibrance |
|---------------|------|-------|----------|----------|
| Num 1 | Normal | 1.0 | 100% | 50% |
| Num 2 | Bright | 1.5 | 110% | 60% |
| Num 3 | Brighter | 2.0 | 110% | 70% |
| Num 4–9 | Custom [PRO] | Editable | Editable | Editable |

All hotkeys are rebindable. Defaults shown above use the numpad preset.

## Download

**[Download Latest Release](../../releases/latest)**

| File | For |
|------|-----|
| `BrightRaider.exe` | All keyboards — numpad, TKL, or custom |

A short setup wizard appears on first launch: choose your keyboard type (numpad or TKL) and your hotkeys are configured automatically. You can re-run the wizard at any time from Settings → Input.

> **Previously released as two separate files** (`BrightRaider.exe` for numpad and `BrightRaider_Arrows.exe` for TKL keyboards). V1.0 combines both into one EXE with fully rebindable hotkeys.

Just download and run. No installation needed.

## Quick Start

1. Run `BrightRaider.exe` — a setup wizard appears
2. Choose your keyboard type (numpad or TKL) — defaults are configured automatically
3. Press your profile key — switch brightness instantly, even in fullscreen
4. That's it. Switch anytime.

## First Launch

On first launch, BrightRaider sets one registry entry to unlock gamma adjustment:

```
HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\ICM → GdiIcmGammaRange = 256
```

This tells Windows to allow gamma adjustments — used by many display calibration tools, harmless, can be removed anytime. A UAC prompt appears once. **Restart your PC after the first launch** (only needed once).

## Pro Activation

1. Buy your license:
   - **[Lemon Squeezy (€5.49)](https://brightraider.lemonsqueezy.com/checkout/buy/9b93d8c0-262f-43a4-bd41-167557efb156)**
2. Right-click tray icon → **Settings** → **Enter License**
3. Enter your email and license key
4. Done — all Pro features unlocked permanently. No subscription. Internet required once for activation, offline forever after.

## Hotkeys (default)

> All hotkeys are fully rebindable in **Settings → Input**.

| Numpad (default) | TKL (default) | Action | Version |
|------------------|---------------|--------|---------|
| Numpad 1–3 | ← ↓ → | Switch profile | Free |
| Numpad 4–9 | — | Switch profile (4–9) | Pro |
| Numpad 0 (short) | ↑ (short) | Mute/unmute game | Pro |
| Numpad 0 (hold 600ms) | ↑ (hold 600ms) | Audio Ducking on/off | Pro |
| Numpad + | Insert | Toggle crosshair | Pro |
| Numpad * | Delete | Toggle timer overlay | Pro |
| Numpad − | Home | Toggle QuickSelect on/off | Pro |
| Numpad / | End | Toggle QuickSave on/off | Pro |
| Numpad Del | Page Down | Toggle ALL hotkeys on/off | Pro |
| CapsLock | CapsLock | Toggle autorun | Pro |
| M (hold on map) | M (hold on map) | Scan evacuation timers | Pro |

Works with NumLock on or off. **Tip: keep NumLock off while playing** — with NumLock on, Windows briefly interrupts the Shift key when you switch profiles, which can slow your character if Shift is your sprint key.

## How It Works

BrightRaider adjusts your display output using standard Windows APIs:

- **GDI** (`SetDeviceGammaRamp`) — gamma & contrast, works on every GPU
- **NvAPI** — NVIDIA Digital Vibrance; per-game FPS limits via NvAPI DRS
- **ADL** — AMD Radeon saturation + hue control
- **ADLX** — AMD per-game FPS limits via ADLX FRTC

Nothing is modified in the game. Nothing is injected. It's the equivalent of changing your monitor brightness — just faster and with presets.

### Auto-Brightness (Pro)

Analyzes 5 small zones across your screen (center + 4 corners) using median brightness measurement. Based on the result, it smoothly interpolates between your profiles. Darker screen = more boost, brighter screen = less. The transition is seamless.

Calibrate in two steps: measure the darkest spot, measure the brightest spot, done.

## System Requirements

- Windows 10 / 11
- .NET Framework 4.7.2 (pre-installed on Windows 10/11)
- NVIDIA or AMD GPU recommended (Intel works with gamma-only)

## Changelog

**[View full changelog](docs/CHANGELOG_PUBLIC.txt)**

## Manual

**[View full manual](docs/Manual.txt)**

## Guides

- 📖 [QuickSave Setup Guide](docs/QuickSave_Guide.md)
- 📖 [QuickSelect Setup Guide](docs/QuickSelect_Guide.md)

## Uninstall

1. Exit BrightRaider (right-click tray → Exit)
2. Delete the folder
3. Optional: Remove `GdiIcmGammaRange` from `HKLM\...\ICM`
4. Optional: Remove auto-start from `HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Run`

## Feedback & Issues

Found a bug or have an idea? [Open an issue](https://github.com/BrightRaider/BrightRaider/issues/new/choose).

> This is a solo hobby project maintained in my free time. I read everything, but response times vary — there is no support contract.

## FAQ

**Does this work with other games?**
Yes. BrightRaider adjusts your display, not the game.

**Do I need Pro?**
Free is fully functional. Pro adds QuickSave (drag to Safe Pocket with one key), QuickSelect (auto-use items), auto-brightness, map scanner, and more — so you never take your hand off the mouse.

**I used VibranceGUI before. Do I still need it?**
No. BrightRaider V1.0 replaces it completely. Set your vibrance per game in Settings → Alt-Tab, and BrightRaider handles switching automatically.

---

<details>
<summary><strong>Deutsche Version</strong></summary>

## BrightRaider

**Feinde im Dunkeln sehen. Kein Alt-Tab, keine Spieldateien verändert.**

BrightRaider ist ein schlankes Windows-Tray-Tool, mit dem du Helligkeit, Kontrast und Digital Vibrance per Tastendruck umschalten kannst. Entwickelt für Arc Raiders Spieler, die in dunklen Höhlen und Schatten nichts sehen — funktioniert aber mit jedem Spiel.

Eine EXE, keine Abhängigkeiten, ~290 KB. Funktioniert mit jeder Tastatur — Numpad, TKL oder komplett selbst belegt.

## Warum BrightRaider?

Das Monitor-OSD ist langsam und umständlich. Alt-Tab zum Einstellen bringt dich um.

BrightRaider nutzt Standard-Windows-APIs — genau wie dein NVIDIA Control Panel oder deine Monitor-Einstellungen.

> **VibranceGUI wird nicht mehr benötigt.** BrightRaider V1.0 schaltet Vibrance und FPS-Limit automatisch pro Spiel beim Alt-Tab — alles was VibranceGUI macht, an einem Ort. Du kannst es deinstallieren.

## Features

### Free
- **3 Helligkeitsprofile** — Normal, Hell, Heller
- **Sofortiges Umschalten per Hotkey** — funktioniert im Vollbild
- **Gamma + Kontrast + Digital Vibrance** Steuerung
- **FPS-Limit pro Spiel** — einmal einstellen, BrightRaider schaltet automatisch beim Alt-Tab um. Arc Raiders auf Monitor-Hz begrenzen, CS2 unlimitiert lassen. Reduziert GPU-Leistungsaufnahme und Wärmeentwicklung spürbar — leiser, kühler. → [Optimale FPS-Cap-Einstellungen (Blur Busters gsync 101)](https://blurbusters.com/gsync/gsync101-input-lag-tests-and-settings/)
- **Automatisches Vibrance-Switching** — Spiel im Fokus → Gaming-Vibrance. Alt-Tab → normale Desktop-Vibrance. Ersetzt VibranceGUI vollständig.
- **Belegbare Hotkeys** — alle Tasten in Einstellungen → Input konfigurierbar. Funktioniert mit Numpad, TKL, QWERTZ, AZERTY — jede Tastatur.
- **NVIDIA + AMD + Intel** Unterstützung (GDI-Fallback für jede GPU)
- **Multi-Monitor** Unterstützung
- **Englisch / Deutsch** Oberfläche
- **Pausen-Erinnerung** — zeigt nach einem konfigurierbaren Intervall einen orangen Toast (Standard: 45 Min). Unterbricht das Spiel nicht.
- **Autostart mit Windows**
- Portabel — keine Installation, nur eine EXE

### Pro (5,49 €)
- **QuickSave** — ein Tastendruck zieht ein Item aus dem Inventar in die Sicherheitstasche (oder zurück). Alles automatisch: Inventar öffnen → ziehen → schließen. 5 unabhängige Presets, Slots konfigurierbar. 📖 [Anleitung](docs/QuickSave_Guide.md)
- **QuickSelect** — eine Taste drücken, um ein Item aus dem Schnellrad automatisch zu benutzen. Q halten → Slot per Maus wählen → Q loslassen → LMB halten → H drücken. 8 Slots, 6 LMB-Timer-Presets, alle Tasten frei belegbar. 📖 [Anleitung](docs/QuickSelect_Guide.md)
- **Auto-Helligkeit** — passt sich automatisch an den Bildschirminhalt an. Dunkler Bereich? Helligkeit geht hoch. Draußen? Zurück auf Normal. Sanfte Übergänge, kein Ruckeln.
- **Map Scanner** — M auf der Karte gedrückt halten, um alle Evakuierungs-Timer automatisch auszulesen. Screenshot → Windows-OCR → farbkodierter Live-Countdown. Erkennt aktive Events (Nacht, Hurrikan, Elektr. Sturm). Unterstützt Buried City, Stella Montis, Space Port, Blue Gate, Damm und Riven Tides.
- **Evac-Alarm** — roter Toast + Sound wenn ein Evac-Timer unter den konfigurierten Schwellwert fällt.
- **Autorun** — kurz CapsLock drücken um die Vorwärtstaste zu halten. 600ms halten für Tap-Modus (ideal für Looting Mk. 3). Vorwärtstaste frei belegbar — funktioniert mit QWERTZ, AZERTY usw.
- **Crosshair-Overlay** — Click-through Fadenkreuz direkt auf dem Bildschirm. 6 Stile.
- **Audio Ducking** — Mute-Taste 600ms halten um Spiel-Audio auf 20% zu reduzieren.
- **Game Mute** — nur das Spiel stumm schalten. Discord, Musik, alles andere bleibt an.
- **Hue pro Profil** — Farbtemperatur pro Profil neben Vibrance. AMD unterstützt in V1.0.
- **Bis zu 9 Profile** mit voller Anpassung
- **Kalibrierungs-Assistent** — zwei Klicks für die Auto-Helligkeit
- **Profil-Editor** — Gamma, Kontrast, Vibrance, Hue pro Profil feintunen

## Download

**[Neueste Version herunterladen](../../releases/latest)**

| Datei | Für |
|-------|-----|
| `BrightRaider.exe` | Alle Tastaturen — Numpad, TKL oder eigene Belegung |

Beim ersten Start erscheint ein kurzer Einrichtungsassistent: Tastaturtyp wählen, Hotkeys werden automatisch gesetzt. Jederzeit neu starten über Einstellungen → Input.

> **Früher als zwei separate Dateien** (`BrightRaider.exe` für Numpad, `BrightRaider_Arrows.exe` für TKL). V1.0 vereint beide in einer EXE mit frei belegbaren Hotkeys.

## Tastenbelegung (Standard)

> Alle Hotkeys sind in **Einstellungen → Input** vollständig belegbar.

| Numpad (Standard) | TKL (Standard) | Aktion | Version |
|-------------------|----------------|--------|---------|
| Numpad 1–3 | ← ↓ → | Profil wechseln | Free |
| Numpad 4–9 | — | Profil wechseln (4–9) | Pro |
| Numpad 0 (kurz) | ↑ (kurz) | Spiel stumm/laut | Pro |
| Numpad 0 (600ms) | ↑ (600ms) | Audio Ducking an/aus | Pro |
| Numpad + | Einfg | Fadenkreuz an/aus | Pro |
| Numpad * | Entf | Timer-Overlay an/aus | Pro |
| Numpad − | Pos1 | QuickSelect an/aus | Pro |
| Numpad / | Ende | QuickSave an/aus | Pro |
| Numpad Entf | Bild↓ | ALLE Hotkeys an/aus | Pro |
| CapsLock | CapsLock | Autorun an/aus | Pro |
| M (halten auf Karte) | M (halten auf Karte) | Evakuierungs-Timer scannen | Pro |

## FAQ

**Funktioniert das auch mit anderen Spielen?**
Ja. BrightRaider passt den Bildschirm an, nicht das Spiel.

**Brauche ich Pro?**
Free ist voll funktionsfähig. Pro fügt QuickSave, QuickSelect, Auto-Helligkeit, Map Scanner und mehr hinzu.

**Ich hatte VibranceGUI. Brauche ich das noch?**
Nein. BrightRaider V1.0 ersetzt es vollständig. Vibrance und FPS-Limit pro Spiel in Einstellungen → Alt-Tab einstellen, BrightRaider übernimmt den Rest.

</details>

---

Made for the Arc Raiders community.
