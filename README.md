# BrightRaider

**See enemies in the dark. No alt-tab, no game files modified.**

BrightRaider is a lightweight Windows tray tool that lets you switch display brightness, contrast and digital vibrance with a single keypress. Built for Arc Raiders players who struggle with dark caves, shadows and low visibility — but works with any game.

One EXE, zero dependencies, 80 KB.

![Windows](https://img.shields.io/badge/Windows-10%2F11-blue) ![NVIDIA](https://img.shields.io/badge/NVIDIA-supported-green) ![AMD](https://img.shields.io/badge/AMD-supported-red) ![.NET](https://img.shields.io/badge/.NET%20Framework-4.0-purple)
[![Downloads](https://img.shields.io/github/downloads/BrightRaider/BrightRaider/total?label=Downloads&color=brightgreen)](https://github.com/BrightRaider/BrightRaider/releases)

## Before / After

**[► Interactive Before/After Slider](https://imgsli.com/NDUwMzQ2)**

| Without BrightRaider | With BrightRaider |
|---|---|
| ![Before](assets/screenshots/before.jpg) | ![After](assets/screenshots/after.jpg) |

## Screenshots

![Settings](assets/screenshots/settings.png)
![Tray Menu](assets/screenshots/tray.png)
![Submenu](assets/screenshots/submenu.png)

## Demo Videos

> **Note on video quality:** BrightRaider works by adjusting display output at the GPU level — the same way your monitor brightness works. Because of this, screen recording software cannot capture the actual brightness changes. The videos were recorded with a phone camera pointed at the monitor, which is why the quality is lower than usual. This is also proof that BrightRaider is not a cheat — it only changes display settings, nothing inside the game.

**Free version** — Profile switching with Numpad keys (starts at 0:06):
[![BrightRaider Free - Profile Switching](https://img.youtube.com/vi/ZjRZKfPi7Ok/0.jpg)](https://youtu.be/ZjRZKfPi7Ok?t=6)

**Pro version** — Auto-Brightness in action:
[![BrightRaider Pro - Auto-Brightness](https://img.youtube.com/vi/q4DPRjHs24g/0.jpg)](https://youtu.be/q4DPRjHs24g?t=4)

## Why BrightRaider?

NVIDIA Game Filters are blocked by anti-cheat (EAC). Monitor OSD is slow and clunky. Alt-tabbing to adjust settings gets you killed.

BrightRaider uses standard Windows display APIs — the same way your NVIDIA Control Panel or monitor settings work. **Safe with all anti-cheat systems** (EAC, BattlEye, Vanguard).

## Features

### Free
- **3 brightness profiles** — Normal, Bright, Brighter
- **Instant hotkey switching** — Numpad 1/2/3, works in fullscreen
- **Gamma + Contrast + Digital Vibrance** control
- **NVIDIA + AMD + Intel** support (GDI fallback for any GPU)
- **Multi-monitor** support
- **English / German** interface
- **Auto-Start with Windows**
- Portable — no installation, just one EXE

### Pro (€6.99)
- **Game Mute** — mute only the game audio with a single key (Numpad 0). Your Discord, music, everything else stays on. One press to focus, one press to hear the game again. Works instantly, even in fullscreen.
- **Crosshair Overlay** — A clean, click-through crosshair directly on your screen. Choose from 4 styles (Cross, Dot+Ring, T-Shape, Dot), pick any color and size. Toggle with Numpad+ or Insert. EAC-safe — same mechanism as Discord and GeForce Experience overlays.
- **Auto-Brightness** — automatically adjusts based on screen content. Dark area? Brightness goes up. Step outside? Back to normal. Smooth transitions, no stutter.
- **Up to 9 profiles** with full customization
- **Calibration Wizard** — two clicks to set up auto-brightness
- **Profile Editor** — fine-tune gamma, contrast, vibrance per profile

## Default Profiles

| Key | Name | Gamma | Contrast | Vibrance |
|-----|------|-------|----------|----------|
| Num 1 | Normal | 1.0 | 100% | 50% |
| Num 2 | Bright | 1.5 | 110% | 60% |
| Num 3 | Brighter | 2.0 | 110% | 70% |
| Num 4-9 | Custom [PRO] | Editable | Editable | Editable |

## Download

**[Download Latest Release](../../releases/latest)**

| File | Controls | For |
|------|----------|-----|
| `BrightRaider.exe` | Numpad 1-9, Numpad 0 | Standard keyboards with numpad |
| `BrightRaider_Arrows.exe` | Arrow keys | Laptops & TKL keyboards without numpad |

Just download and run. No installation needed.

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

1. Buy your license:
   - **[Lemon Squeezy (€6.99)](https://brightraider.lemonsqueezy.com/checkout/buy/9b93d8c0-262f-43a4-bd41-167557efb156)**
2. Right-click tray icon → **Settings** → **Enter License**
3. Enter your email and license key
4. Done — all Pro features unlocked permanently. No subscription. Internet required once for activation, offline forever after.

## Hotkeys

### Numpad version (`BrightRaider.exe`)

| Key | Action | Version |
|-----|--------|---------|
| Numpad 1-3 | Switch profile | Free |
| Numpad 4-9 | Switch profile | Pro |
| Numpad 0 | Mute/unmute game | Pro |
| Numpad + | Toggle crosshair | Pro |

Works with NumLock on or off, with or without Shift.

### Arrow Keys version (`BrightRaider_Arrows.exe`)

| Key | Action | Version |
|-----|--------|---------|
| Arrow Left | Profile 1 (Normal) | Free |
| Arrow Down | Profile 2 (Bright) | Free |
| Arrow Right | Profile 3 (Brighter) | Free |
| Arrow Up | Mute/unmute game | Pro |
| Insert | Toggle crosshair | Pro |

## How It Works

BrightRaider adjusts your display output using standard Windows APIs:

- **GDI** (`SetDeviceGammaRamp`) — gamma & contrast, works on every GPU
- **NvAPI** — NVIDIA Digital Vibrance (hardware-level saturation)
- **ADL** — AMD Radeon saturation control

Nothing is modified in the game. Nothing is injected. It's the equivalent of changing your monitor brightness — just faster and with presets.

### Auto-Brightness (Pro)

Analyzes 5 small zones across your screen (center + 4 corners) using median brightness measurement. Based on the result, it smoothly interpolates between your profiles. Darker screen = more boost, brighter screen = less. The transition is seamless.

Calibrate in two steps: measure the darkest spot, measure the brightest spot, done.

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

### Crosshair Overlay

BrightRaider's crosshair works via a transparent Windows overlay — the exact same mechanism used by Discord, GeForce Experience, and TeamSpeak overlays.

EAC (Easy Anti-Cheat) **explicitly allows** this type of overlay. It is not injected into the game, does not read game memory, and is not rendered inside the game engine. It is simply a transparent window drawn on top by Windows.

EAC-safe — no injection, no game memory access, no rendering inside the engine.

## System Requirements

- Windows 10 / 11
- .NET Framework 4.0 (pre-installed on every Windows)
- NVIDIA or AMD GPU recommended (Intel works with gamma-only)
- Numpad or numpad-emulating keyboard

## Changelog

**[View full changelog](docs/CHANGELOG_PUBLIC.txt)**

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
Free is fully functional. Pro adds automatic switching so you never take your hand off the mouse.

---

<details>
<summary><strong>Deutsche Version</strong></summary>

## BrightRaider

**Feinde im Dunkeln sehen. Kein Alt-Tab, keine Spieldateien verändert.**

BrightRaider ist ein schlankes Windows-Tray-Tool, mit dem du Helligkeit, Kontrast und Digital Vibrance per Tastendruck umschalten kannst. Entwickelt für Arc Raiders Spieler, die in dunklen Höhlen und Schatten nichts sehen — funktioniert aber mit jedem Spiel.

Eine EXE, keine Abhängigkeiten, 80 KB.

## Warum BrightRaider?

NVIDIA Game Filter werden vom Anti-Cheat (EAC) blockiert. Das Monitor-OSD ist langsam und umständlich. Alt-Tab zum Einstellen bringt dich um.

BrightRaider nutzt Standard-Windows-APIs — genau wie dein NVIDIA Control Panel oder deine Monitor-Einstellungen. **Sicher mit allen Anti-Cheat-Systemen** (EAC, BattlEye, Vanguard).

## Features

### Free
- **3 Helligkeitsprofile** — Normal, Hell, Heller
- **Sofortiges Umschalten per Hotkey** — Numpad 1/2/3, funktioniert im Vollbild
- **Gamma + Kontrast + Digital Vibrance** Steuerung
- **NVIDIA + AMD + Intel** Unterstützung (GDI-Fallback für jede GPU)
- **Multi-Monitor** Unterstützung
- **Englisch / Deutsch** Oberfläche
- **Autostart mit Windows**
- Portabel — keine Installation, nur eine EXE

### Pro (6,99 €)
- **Game Mute** — nur das Spiel stumm schalten (Numpad 0). Discord, Musik, alles andere bleibt an. Ein Druck zum Fokussieren, ein Druck zum Wiederhören. Funktioniert sofort, auch im Vollbild.
- **Crosshair-Overlay** — Ein sauberes, click-through Fadenkreuz direkt auf dem Bildschirm. 4 Stile (Kreuz, Punkt+Ring, T-Form, Punkt), freie Farbwahl und Größe. Toggle mit Numpad+ oder Einfg. EAC-sicher — gleicher Mechanismus wie Discord und GeForce Experience.
- **Auto-Helligkeit** — passt sich automatisch an den Bildschirminhalt an. Dunkler Bereich? Helligkeit geht hoch. Draußen? Zurück auf Normal. Sanfte Übergänge, kein Ruckeln.
- **Bis zu 9 Profile** mit voller Anpassung
- **Kalibrierungs-Assistent** — zwei Klicks für die Auto-Helligkeit
- **Profil-Editor** — Gamma, Kontrast, Vibrance pro Profil feintunen

## Download

**[Neueste Version herunterladen](../../releases/latest)**

| Datei | Steuerung | Für |
|-------|-----------|-----|
| `BrightRaider.exe` | Numpad 1-9, Numpad 0 | Standard-Tastaturen mit Numpad |
| `BrightRaider_Arrows.exe` | Pfeiltasten | Laptops & TKL-Tastaturen ohne Numpad |

Einfach herunterladen und starten. Keine Installation nötig.

## Schnellstart

1. `BrightRaider.exe` starten — ein Tray-Icon erscheint
2. **Numpad 1** drücken — Normale Helligkeit
3. **Numpad 2** drücken — Hell (bessere Sicht in dunklen Bereichen)
4. **Numpad 3** drücken — Heller (maximale Sicht)
5. Fertig. Jederzeit umschalten, auch im Vollbild.

## Erster Start

Beim ersten Start setzt BrightRaider einen Registry-Eintrag, um Gamma-Anpassungen freizuschalten:

```
HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\ICM → GdiIcmGammaRange = 256
```

Das erlaubt Windows, Gamma-Anpassungen vorzunehmen — wird von vielen Kalibrierungs-Tools genutzt, harmlos, jederzeit entfernbar. Ein UAC-Fenster erscheint einmalig. **PC nach dem ersten Start neu starten** (nur einmal nötig).

## Pro-Aktivierung

1. Lizenz kaufen:
   - **[Lemon Squeezy (6,99 €)](https://brightraider.lemonsqueezy.com/checkout/buy/9b93d8c0-262f-43a4-bd41-167557efb156)**
2. Rechtsklick auf Tray-Icon → **Einstellungen** → **Lizenz eingeben**
3. E-Mail und Lizenzschlüssel eingeben
4. Fertig — alle Pro-Features dauerhaft freigeschaltet. Kein Abo. Einmalig Internet nötig, danach offline für immer.

## Tastenbelegung

### Numpad-Version (`BrightRaider.exe`)

| Taste | Aktion | Version |
|-------|--------|---------|
| Numpad 1-3 | Profil wechseln | Free |
| Numpad 4-9 | Profil wechseln | Pro |
| Numpad 0 | Spiel stumm/laut | Pro |
| Numpad + | Fadenkreuz an/aus | Pro |

Funktioniert mit NumLock an oder aus, mit oder ohne Shift.

### Pfeiltasten-Version (`BrightRaider_Arrows.exe`)

| Taste | Aktion | Version |
|-------|--------|---------|
| Pfeil Links | Profil 1 (Normal) | Free |
| Pfeil Unten | Profil 2 (Hell) | Free |
| Pfeil Rechts | Profil 3 (Heller) | Free |
| Pfeil Hoch | Spiel stumm/laut | Pro |
| Einfg | Fadenkreuz an/aus | Pro |

## Funktionsweise

BrightRaider passt die Bildschirmausgabe über Standard-Windows-APIs an:

- **GDI** (`SetDeviceGammaRamp`) — Gamma & Kontrast, funktioniert mit jeder GPU
- **NvAPI** — NVIDIA Digital Vibrance (Hardware-Sättigung)
- **ADL** — AMD Radeon Sättigungssteuerung

Es wird nichts am Spiel verändert. Es wird nichts injiziert. Es ist das Gleiche wie die Monitor-Helligkeit zu ändern — nur schneller und mit Voreinstellungen.

### Auto-Helligkeit (Pro)

Analysiert 5 kleine Zonen auf dem Bildschirm (Mitte + 4 Ecken) per Median-Helligkeitsmessung. Basierend auf dem Ergebnis wird sanft zwischen deinen Profilen interpoliert. Dunklerer Bildschirm = mehr Boost, hellerer Bildschirm = weniger. Der Übergang ist nahtlos.

Kalibrierung in zwei Schritten: Dunkelste Stelle messen, hellste Stelle messen, fertig.

## Anti-Cheat Sicherheit

BrightRaider verändert **KEINE**:
- Spieldateien oder Speicher
- DLL-Injektionen in Spielprozesse
- Hooks ins Spiel
- Auslesen von Spieldaten

BrightRaider nutzt **NUR**:
- Windows GDI — wie deine Monitor-Einstellungen
- NVIDIA NvAPI — wie das NVIDIA Control Panel
- AMD ADL — wie AMD Radeon Software

Anti-Cheat-Systeme erkennen keine Bildschirm-Anpassungen.

### Crosshair-Overlay

Das BrightRaider-Fadenkreuz nutzt ein transparentes Windows-Overlay — denselben Mechanismus wie Discord, GeForce Experience und TeamSpeak-Overlays.

EAC (Easy Anti-Cheat) **erlaubt** diese Art von Overlay ausdrücklich. Es wird nichts ins Spiel injiziert, kein Spielspeicher gelesen, und es wird nicht in der Spiel-Engine gerendert. Es ist lediglich ein transparentes Fenster, das Windows über das Spiel legt.

EAC-sicher — keine Injektion, kein Spielspeicher-Zugriff, keine Spiel-Engine-Darstellung.

## FAQ

**Funktioniert das auch mit anderen Spielen?**
Ja. BrightRaider passt den Bildschirm an, nicht das Spiel.

**Werde ich gebannt?**
Nein. Es nutzt die gleichen Windows-APIs wie deine Monitor-Einstellungen.

**Brauche ich Pro?**
Free ist voll funktionsfähig. Pro fügt automatisches Umschalten hinzu, damit du nie die Hand von der Maus nehmen musst.

</details>

---

Made for the Arc Raiders community.
