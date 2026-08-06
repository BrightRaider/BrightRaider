# BrightRaider — Manual

**Switch Gamma, Contrast, Vibrance & Hue with one keypress.**
Plus Auto-Brightness, Alt-Tab Auto-Switch, per-game Auto-HDR, Footstep Booster,
Map Scanner, QuickSelect & QuickSave.

> 🇬🇧 **English** below · 🇩🇪 **[Deutsch weiter unten](#deutsch)**

---

## English

### What is BrightRaider?

BrightRaider is a lightweight Windows tray tool that instantly switches between
display profiles using hotkeys. Each profile changes **gamma, contrast, and
digital vibrance/saturation** — perfect for gaming, or quickly boosting
visibility in dark scenes.

It auto-detects your GPU (NVIDIA or AMD) and uses the correct driver API
automatically. On systems without a supported GPU, gamma and contrast still work
via the Windows API.

The app runs silently in your system tray and uses almost no resources. **One
EXE, no installation.** A Setup Wizard picks sensible defaults on first launch
(Numpad, TKL, AZERTY).

> **New in V1.1 — a tool for any PC game.** The display and FPS features work in
> every game. The ARC Raiders helpers (Map Scanner, QuickSelect, QuickSave,
> Autorun) are now an **optional module** you enable in the Setup Wizard or under
> *Settings → App*. The Map Scanner can also read **other games** via downloadable
> data packs (see `pack-authoring.md`).

### Free features

- 3 switchable display profiles via Numpad 1/2/3 (or Arrow keys)
- Adjusts Gamma, Contrast, and Digital Vibrance/Saturation
- Supports NVIDIA (NvAPI) and AMD (ADL SDK); auto-detects your GPU
- Also works on Intel / other GPUs (Gamma + Contrast only)
- Per-monitor control (choose one or all monitors)
- Alt-Tab Auto-Switch — per-game profile + FPS limit, applied automatically
- Per-game Auto-HDR — turn Windows HDR on/off automatically per game
- Works with Shift held down (no conflicts)
- Notifications can be turned on/off — with a **separate switch to mute just the
  QuickSelect / QuickSave confirmation toasts** (*Settings → App*), so those macros
  can run silently while other notifications stay on — and are **color-coded**:
  green = good news (update available), yellow = warning, red = a risky mode is active
- Color-conflict warning — a heads-up when another tool (f.lux, Night Light, …)
  is fighting over your display colors
- English and German
- Auto-Start with Windows · all settings saved automatically
- Resets everything to normal on exit
- Break Reminder — orange toast after a configurable interval; never interrupts gameplay

### Pro features (€5.49 one-time)

- **Autorun** — press CapsLock to hold W automatically. Sprint Mode starts
  sprinting directly; Tap Mode (hold CapsLock 600 ms) pulses W at intervals.
  Fully configurable timing. Lives on its own **Movement** tab.
- **Audio Ducking** — hold the mute key 600 ms to drop game audio to a set
  percentage. Short press still mutes/unmutes.
- **Footstep Booster / Loudness Limiter** — cap the game's audio session so you
  can crank volume for footsteps without going deaf on gunshots (see
  `FootstepBooster_Guide.md`).
- **Map Scanner** — hold M on the in-game map to read all evacuation timers
  locally, with a live colour-coded overlay.
- **Evac Alarm** — red toast + sound when a timer drops below your threshold.
- **Crosshair Overlay** — click-through crosshair, 6 styles, custom colour, size
  (4–50 px), optional outline.
- **Audio Output Switcher** — cycle the default output device with a hotkey;
  optional auto-switch when a game starts/closes.
- **Up to 9 profiles** (Numpad 1–9) with a full custom editor (Gamma, Contrast,
  Vibrance, Hue — on NVIDIA the Hue slider runs 0–359° like the Control Panel).
- **Auto-Brightness** — screen analysis detects dark scenes and switches to a
  brighter profile.
- **Game Mute**, **Hotkey Pause**, **QuickSelect**, **QuickSave**, and an
  **offline license** (no internet needed after activation).

### Default profiles

| Key    | Name     | Gamma | Contrast | Vibrance |
|--------|----------|-------|----------|----------|
| Num 1  | Normal   | 1.0   | 100 %    | 50 %     |
| Num 2  | Bright   | 1.5   | 110 %    | 60 %     |
| Num 3  | Brighter | 2.0   | 110 %    | 70 %     |
| Num 4–9 | Custom  | —     | —        | Pro users edit all values |

### System requirements

- Windows 10 or 11 (x64)
- No .NET runtime required (Native AOT — single EXE)
- NVIDIA (GeForce, RTX, Quadro) or AMD (Radeon, RX) GPU
- Also works on Intel / other GPUs (Gamma + Contrast only)

### First launch — important

On first launch, BrightRaider makes one small, safe registry change so Windows
allows gamma adjustments:

1. Double-click `BrightRaider.exe`
2. Windows shows a UAC prompt — click **Yes**
3. This creates a single registry entry:
   ```
   HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\ICM
   Name:  GdiIcmGammaRange
   Value: 256
   ```
   Windows blocks apps from adjusting display gamma by default; this entry simply
   tells Windows *"allow gamma adjustments."* It changes no display settings
   itself, affects no system stability or security, is used by many calibration
   tools, and can be removed at any time.
4. **Restart your PC** (only needed once)
5. Start `BrightRaider.exe` again — done!

The UAC prompt appears only once. After that, the app runs without admin rights.

### Modifier bindings (Ctrl / Alt / Shift)

Every rebindable field accepts modifier + key combinations:

1. Click the field (it shows *"Press a key…"*)
2. Hold Ctrl, Alt, and/or Shift
3. Press the trigger key — e.g. `Shift+5`, `Ctrl+Numpad 5`, `Alt+F2`
4. Esc cancels without changing anything

- **Bindable:** A–Z, 0–9, F1–F24, Numpad keys, OEM punctuation, arrow + nav keys,
  Mouse MB3/MB4/MB5, Wheel up/down.
- **Not bindable alone** (modifier-only): Shift, Ctrl, Alt, Win, Esc, Tab, Enter.
- **OS-reserved combos** (you can bind them, but Windows eats them first):
  Win+L/D/E/R/S/I/Tab, Alt+F4, Alt+Tab, Ctrl+Alt+Del, Ctrl+Shift+Esc —
  BrightRaider shows a warning toast.
- **Sprint conflict:** binding anything with `Shift+` while Sprint is Shift will
  fire on every sprint. Pick `Ctrl+`/`Alt+`, or rebind Sprint (Movement tab).

### Playing with a controller?

**Controller buttons cannot be bound directly.** BrightRaider listens to the
keyboard and mouse through a low-level Windows hook, which lets it *swallow* a
key — tapping CapsLock for Autorun never reaches the game as a CapsLock. A
gamepad talks to Windows through a separate channel with no equivalent hook, so
a button could only be observed, never intercepted: it would trigger BrightRaider
**and** its in-game action at the same time. On a keyboard you can spare one of
100+ keys for that; on a controller you can't.

Two things work regardless:

- **The display features need no hotkeys at all.** Per-game profiles with
  Alt-Tab Auto-Switch apply themselves when the game comes to the foreground —
  vibrance, gamma, contrast, hue and the FPS limit. This is the largest part of
  BrightRaider and it is completely controller-agnostic.
- **For the hotkey features, use a remapper.** Steam Input, DS4Windows or
  reWASD consume the controller button on their side and send a keyboard key.
  BrightRaider then sees a normal key press, swallowing included, and behaves
  exactly as designed. This isn't a workaround — the remapper can intercept
  where BrightRaider can't.

### Default hotkeys (Numpad preset)

| Key              | Action                                    |
|------------------|-------------------------------------------|
| Numpad 1–3       | Profile 1–3 (Free)                        |
| Numpad 4–9       | Profile 4–9 (Pro)                         |
| Numpad 0 (short) | Mute / unmute game audio (Pro)            |
| Numpad 0 (hold)  | Audio Ducking on/off (Pro)                |
| Numpad +         | Toggle crosshair overlay (Pro)            |
| Numpad *         | Toggle timer overlay (Pro)                |
| CapsLock         | Toggle Autorun (Pro)                       |
| M (hold)         | Scan evacuation timers on the map (Pro)   |
| 4 / 5 / 6        | QuickSelect: auto-use slot item (Pro)     |
| Numpad −         | Toggle QuickSelect (Pro)                   |
| Numpad /         | Toggle QuickSave (Pro)                     |
| Numpad Del       | Global ON/OFF (see below)                 |
| Numpad Enter     | Cycle audio output device (Pro)           |

Works with or without Shift, NumLock on or off. **TKL/Laptop preset** maps
profiles to F1–F9 (audio-device cycle on PageUp). **AZERTY preset** uses Z as the
forward key. Re-run the wizard any time via *Settings → Hotkeys → "Apply preset…"*.

### Pro activation

1. Purchase a license on the BrightRaider website
2. You receive an email with your license key
3. Right-click tray → **Settings → "Enter License…"**
4. Enter your email and key — Pro unlocks instantly, saved locally
5. **Moving to a new PC?** Use *Settings → App → "Deactivate on this device"* to
   free your activation slot yourself, then activate on the new machine — no
   support ticket needed.

### Autorun (Pro)

Press CapsLock briefly to start autorunning — BrightRaider holds W so your
character moves (or sprints) forward automatically. While autorunning:

| Key      | Effect                                                      |
|----------|-------------------------------------------------------------|
| CapsLock | Stop autorun                                                |
| Shift    | Toggle sprint                                               |
| W        | Stops injection — character keeps running until you release W |
| S        | Cancel autorun fully                                        |
| C        | Slide first (while moving), then stop                       |

- **Sprint Mode** (*Movement → Mode: Sprint*) starts in sprint immediately.
- **Tap Mode** (hold CapsLock 600 ms) pulses W: pressed briefly (default
  **160 ms**), then released (default **1300 ms**). Built for the **Looting
  Mk. 3 (Survivor)** augment — during the pause your character is stationary
  and regenerates, the brief tap keeps you crawling forward. Configure under
  *Movement → Tap Interval…*.

  > **Retuned in V1.1.** A recent Arc Raiders patch nerfed the augment, and the
  > old 990 / 169 pair stopped making progress. 1300 / 160 gets you moving
  > *and* regenerating again. **Upgrading from V1.0?** Your saved values are
  > kept — set them by hand under *Movement → Tap Interval…* if you want the
  > new pair. The nerf still bites, so it's well worth taking **Crawl Before
  > You Walk** (yellow skill tree, top middle) — crawling gets far faster.
- **AZERTY:** enable *"Z = forward"* under Movement.

### Audio Ducking (Pro)

The mute key has two modes: a **short press** mutes/unmutes; a **hold** (default
600 ms) ducks game audio to a set percentage (default 20 %) so you can hear
Discord or music. Configure volume (0–100 %) and hold duration (200–2000 ms)
under *Settings → Audio*.

### Footstep Booster / Loudness Limiter (Pro)

Caps the game's audio session at a threshold so you can crank in-game volume for
footsteps without gunshots deafening you. Per-process — Discord, music and
browser stay untouched.

- **Threshold (10–90 %)** — level above which the limiter kicks in (start ~35–45 %)
- **Attack (1–200 ms)** — how fast it reacts to a loud transient (start ~10–20 ms)
- **Release (50–2000 ms)** — how fast it returns to baseline (start ~250–400 ms)

Full tuning steps in `FootstepBooster_Guide.md`.

### Background Mute (Pro)

Auto-mutes the game's audio session when you Alt-Tab out, unmutes on return.
Per-process — your music + Discord keep playing. *Settings → Audio → "Auto-mute
game when Alt-Tab'd out"*.

### Audio Output Switcher (Pro)

Switch your default Windows output device on the fly. Tick the devices you want
in the rotation, then press the cycle hotkey to step through them (a toast shows
the device). Optional **auto-switch on game start** restores the previous device
when the game closes. Switching sets all roles, so Discord follows too.

### Map Scanner (Pro)

Hold **M** on the in-game map — BrightRaider zooms out, reads all evacuation
timers locally on your PC, and shows a live countdown overlay.

**Supported ARC Raiders maps:** Buried City, Stella Montis, Space Port, Blue
Gate, Dam, Riven Tides. **Other games** can be added via data packs
(`pack-authoring.md`).

**Colour coding:**

| Colour | Meaning              |
|--------|----------------------|
| Green  | more than 10 minutes |
| Yellow | more than 5 minutes  |
| Orange | more than 1 minute   |
| Red    | less than 1 minute   |
| Gray   | closed / unknown     |

**Event detection** — the scanner recognises active map conditions and adjusts
the number of open evacuation points: **Night Raid** (2 points, hatches closed),
**Electromagnetic Storm** (3 points, hatches closed), **Husk Graveyard**,
**Locked Gate**, **Uncovered Caches**, and **Hidden Bunker** (Spaceport — 3
points, hatches closed). The condition name appears in the overlay.

Configure via *tray → Map Scanner Settings*: hold duration, overlay position (6
spots), font size/colour, and the Evac Alarm threshold.

### Evac Alarm (Pro)

Alerts when a timer drops below your threshold: a red toast + a sound, once per
timer, re-armed when the timer is re-scanned. Configure inside *Map Scanner
Settings → Evac Alarm* (separate minute/second fields). Map Scanner must be active.

### QuickSelect (Pro)

Press one key to auto-use a quick-wheel item. BrightRaider runs the whole
sequence: hold Q → select slot → release Q → hold LMB for the configured time →
optionally press H to free the hand.

**Setup:** *tray → Settings → QuickSelect…* → enable → bind a trigger key per
slot → set the wheel slot (1–10) → **type the LMB hold time in ms** → optionally
enable H per slot.

**Recommended LMB hold times (ms):**

| Item                     | ms   |
|--------------------------|------|
| Adrenaline Syringe       | 1300 |
| Herb Bandage / Bandage   | 1800 |
| Shield Charger           | 2300 |
| Vita Syringe             | 4300 |
| Instant Shield Charger   | 5300 |

*(If an item is only partially used, add 100–200 ms.)*

**Tips:** test with a low-value item first · slots 7–10 use cursor movement,
slots 3–6 use their number key (immune to mouse movement) · H can bind to
MB3/4/5 · toggle QuickSelect off (Numpad −) when typing in chat · after pressing
H mid-use, wait a moment before re-triggering (the game holds the item in a brief
transitional state) · don't want the confirmation pop-up? Mute the QuickSelect /
QuickSave toasts under *Settings → App* (the macros still run, just silently).

### QuickSave (Pro)

Drags items between your inventory, Safe Pocket and **backpack** with one keypress
or wheel scroll — no manual clicking.

- **What can move:** inventory slots 1–8, weapons, Safe Pocket 1–3, and
  **Backpack 1–4** (top row) — new in V1.1. Direction is fully configurable.
- **How it works:** opens inventory (Tab) → moves cursor to source → holds LMB
  and drags to destination → releases → closes inventory (Tab).
- **Toggle direction (⇄):** first press moves From→To, second To→From.
- **Timing** (if a drag fails): Open (250 ms), Hover (0 ms), Hold (0 ms), Drop
  (120 ms), Cooldown (0 ms).
- **Toggle on/off:** Numpad / (Numpad) or End (Arrow).

> **Known limitation:** occasional drag failures come from the Unreal Engine
> Slate UI that ARC Raiders uses — the same thing happens with manual drag-drop.
> It can't be fixed from outside the game; just trigger again.

### Crosshair Overlay (Pro)

A transparent, click-through crosshair drawn on screen (works in fullscreen; same
overlay mechanism as Discord / GeForce Experience). Toggle with Numpad + (or
Insert). 6 styles, custom colour and size (4–50 px), optional outline.

### Auto-Brightness (Pro)

Analyses 5 screen zones (centre-weighted, 4 corners) by median brightness and
smoothly switches profiles — dark screen = more boost. Enable via *tray →
Auto-Brightness*. Calibrate in two steps (darkest, then brightest spot); the
calibration window sits below all zones so it doesn't skew results. Zone weights
(0–10 each) under *Settings → Zones…*. Full walkthrough in
`AutoBrightness_Guide.md`.

### Alt-Tab Auto-Switch (Free)

Per-game profile + FPS limit + crosshair, applied automatically when a configured
game comes to the foreground — no more manual switching or running VibranceGUI
alongside.

1. Add a game under *Settings → Game Profiles → "Add…"*
2. Pick a base profile (optional per-game overrides: Gamma/Contrast/Vibrance/Hue/FPS)
3. Launch the game — the profile, crosshair and FPS limit apply automatically
4. Alt-Tab out — original ramps + vibrance + hue restored, crosshair hidden

**FPS limit per game:** NVIDIA via NvAPI DRS (no DirectX hook), AMD via ADLX FRTC
(`amdadlx64.dll`), Intel has no public API (UI shows a note).

### Per-game Auto-HDR (Free) — new in V1.1

Turns Windows HDR **on** when a chosen game launches and **off** when it exits.
Opt-in per game (*Game Profiles → "Auto-HDR for this game"*). Alt-Tabbing in and
out never flips HDR — it only switches on real game start/exit, so you never pay
the 1–2 s black flash mid-session. If HDR was already on (yours), BrightRaider
leaves it alone. Needs Alt-Tab Auto-Switch enabled.

### Color-conflict warning — new in V1.1

If another tool that controls your display colours is running (f.lux, Iris,
Windows Night Light, …), BrightRaider tells you at startup and on the Display
tab. Those tools and BrightRaider fight over the same gamma pipeline — this is
the most common cause of *"my colours keep reverting."* Read-only: BrightRaider
never touches the other tool, it just warns you.

### Process Optimizer (Pro, opt-in)

For a detected foreground game, BrightRaider can raise Windows process priority
and pin it to physical cores (Hyperthreading off) for smoother frametimes.
*Settings → Performance*; both off by default; reverts on Alt-Tab out / game exit.

### Hotkey Pause (Pro)

Temporarily disables all BrightRaider hotkeys — the app keeps running, it just
stops intercepting keys. *Settings → Pause Hotkeys*; a toast confirms.

### Only run hotkeys while a game is focused

Optional switch (*Settings → Hotkeys → Behaviour*). When on, every hotkey passes
straight through on the desktop or in other apps, and reactivates automatically
when a fullscreen game is in the foreground. Default off — leave it off for
windowed-mode play.

### Global ON/OFF (Numpad Del — Free)

Two flavours, picked under *Settings → App → "Toggle OFF also resets the display"*:

- **Checked (default) = FULL OFF** — hides every overlay, restores your original
  gamma/vibrance/hue, pauses Auto-Brightness, stops the Footstep Booster. The
  display behaves as if BrightRaider weren't running. Press again to re-apply.
- **Unchecked = HOTKEY PAUSE** — only the hotkeys pause; your colour profile,
  overlays and FPS limit stay. Same as V9.x on Page Down / Numpad Del.

### Startup modes (the tools download)

For everyday use, just run `BrightRaider.exe`. The optional **tools download**
(`BrightRaider_tools.zip` on the releases page) adds two double-click launchers
for special situations — put them in the same folder as `BrightRaider.exe` and
run the one you need:

- **`BrightRaider (debug scan).cmd`** — starts BrightRaider with Map Scanner
  diagnostics enabled. Open the in-game map and scan once; it writes a PNG +
  `.txt` into `%LocalAppData%\BrightRaider\debug`. Send me those files if a map
  timer ever reads wrong.
- **`BrightRaider (sendinput mode).cmd`** — starts QuickSave with its alternate,
  lower-level input path. Use it only if a game doesn't register QuickSave's
  default inventory drag. Off in normal use; a notice shows at startup while it's
  active.

Each launcher just sets an environment variable before starting the app, so you
can open one in Notepad to see exactly what it does. You never need these for
normal use — running `BrightRaider.exe` on its own is the standard way.

BrightRaider self-elevates once on first launch to set the gamma registry key —
that is automatic and needs no launcher.

### Notes

- Numpad keys are captured while the tool runs; regular number keys are not affected.
- Vibrance (NVIDIA) / Saturation (AMD) changes show in the GPU control panel;
  Gamma/Contrast changes do not (different API — this is normal).
- The tray menu shows your detected GPU at the bottom.
- If something looks wrong: just exit the app — everything resets automatically.

### Uninstall

1. Exit BrightRaider (tray → Exit)
2. Delete the folder
3. *Optional:* remove `HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\ICM →
   GdiIcmGammaRange`
4. *Optional:* remove `HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Run →
   BrightRaider`

---

<a name="deutsch"></a>

## Deutsch

### Was ist BrightRaider?

BrightRaider ist ein schlankes Windows-Tray-Tool, mit dem du per Tastendruck
zwischen Display-Profilen wechselst. Jedes Profil ändert **Gamma, Kontrast und
Digital Vibrance/Sättigung** gleichzeitig — perfekt fürs Gaming oder um in
dunklen Szenen mehr zu erkennen.

Es erkennt deine GPU (NVIDIA oder AMD) automatisch und nutzt die richtige
Treiber-API. Ohne unterstützte GPU funktionieren Gamma und Kontrast trotzdem über
die Windows-API.

Die App läuft leise im System-Tray und verbraucht kaum Ressourcen. **Eine EXE,
keine Installation.** Ein Setup-Assistent setzt beim ersten Start sinnvolle
Defaults (Numpad, TKL, AZERTY).

> **Neu in V1.1 — ein Tool für jedes PC-Spiel.** Die Display- und FPS-Funktionen
> laufen in jedem Spiel. Die ARC-Raiders-Helfer (Map Scanner, QuickSelect,
> QuickSave, Autorun) sind jetzt ein **abschaltbares Modul** (Setup-Assistent
> oder *Einstellungen → App*). Der Map Scanner liest per Daten-Pack auch **andere
> Spiele** (siehe `pack-authoring.md`).

### Kostenlose Funktionen

- 3 umschaltbare Display-Profile über Numpad 1/2/3 (oder Pfeiltasten)
- Ändert Gamma, Kontrast und Digitale Farbanpassung/Sättigung
- Unterstützt NVIDIA (NvAPI) und AMD (ADL SDK); automatische GPU-Erkennung
- Läuft auch auf Intel / anderen GPUs (nur Gamma + Kontrast)
- Einzelne Monitore oder alle gleichzeitig steuerbar
- Alt-Tab Auto-Switch — Profil + FPS-Limit automatisch pro Spiel
- Auto-HDR pro Spiel — Windows-HDR automatisch an/aus pro Spiel
- Funktioniert auch mit gedrückter Shift-Taste
- Benachrichtigungen ein/ausschaltbar — mit einem **separaten Schalter, nur die
  QuickSelect- / QuickSave-Bestätigungs-Toasts stummzuschalten** (*Einstellungen →
  App*), damit diese Makros lautlos laufen, während andere Meldungen anbleiben —
  und **farbkodiert**: grün = gute Nachricht (Update verfügbar), gelb = Warnung,
  rot = riskanter Modus aktiv
- Farbkonflikt-Warnung — Hinweis, wenn ein anderes Tool (f.lux, Night Light, …)
  um deine Display-Farben kämpft
- Englisch und Deutsch
- Autostart mit Windows · alle Einstellungen automatisch gespeichert
- Setzt beim Beenden alles auf Normal zurück
- Pausen-Erinnerung — oranger Toast nach einstellbarem Intervall; stört nie das Spiel

### Pro-Funktionen (5,49 € einmalig)

- **Autorun** — CapsLock kurz drücken, um W automatisch zu halten. Sprint-Modus
  startet direkt im Sprint; Tap-Modus (CapsLock 600 ms halten) tippt W in
  Intervallen. Timing voll einstellbar. Eigener **Movement**-Tab.
- **Audio Ducking** — Stumm-Taste 600 ms halten, um Spiel-Audio auf einen
  Prozentsatz zu reduzieren. Kurzer Druck schaltet wie gewohnt stumm/laut.
- **Footstep Booster / Loudness Limiter** — begrenzt die Spiel-Audio-Session,
  damit du Schritte hörst, ohne dass Schüsse dir das Trommelfell zerlegen
  (siehe `FootstepBooster_Guide.md`).
- **Map Scanner** — M auf der Karte halten, um alle Evakuierungs-Timer lokal
  auszulesen, mit farbkodiertem Live-Overlay.
- **Evac-Alarm** — roter Toast + Sound, wenn ein Timer unter den Schwellwert fällt.
- **Crosshair-Overlay** — Click-through-Fadenkreuz, 6 Stile, freie Farbe, Größe
  (4–50 px), optionaler Umriss.
- **Audio-Ausgabe-Umschalter** — Standard-Ausgabegerät per Hotkey durchwechseln;
  optional automatisch beim Spielstart/-ende.
- **Bis zu 9 Profile** (Numpad 1–9) mit vollem Editor (Gamma, Kontrast, Vibrance,
  Hue — auf NVIDIA läuft der Hue-Regler 0–359° wie im Control Panel).
- **Auto-Helligkeit** — Bildschirmanalyse erkennt dunkle Szenen und wechselt das Profil.
- **Spiel stumm**, **Hotkey-Pause**, **QuickSelect**, **QuickSave** und eine
  **Offline-Lizenz** (nach Aktivierung kein Internet nötig).

### Profile (Standard)

| Taste  | Name    | Gamma | Kontrast | Vibrance |
|--------|---------|-------|----------|----------|
| Num 1  | Normal  | 1.0   | 100 %    | 50 %     |
| Num 2  | Hell    | 1.5   | 110 %    | 60 %     |
| Num 3  | Heller  | 2.0   | 110 %    | 70 %     |
| Num 4–9 | Eigen  | —     | —        | Pro-Nutzer ändern alle Werte |

### Systemvoraussetzungen

- Windows 10 oder 11 (x64)
- Keine .NET-Runtime nötig (Native AOT — eine einzige EXE)
- NVIDIA (GeForce, RTX, Quadro) oder AMD (Radeon, RX) GPU
- Läuft auch auf Intel / anderen GPUs (nur Gamma + Kontrast)

### Erster Start — wichtig

Beim ersten Start nimmt BrightRaider eine kleine, sichere Registry-Änderung vor,
damit Windows Gamma-Anpassungen erlaubt:

1. `BrightRaider.exe` doppelklicken
2. Windows zeigt eine UAC-Abfrage — **Ja** klicken
3. Es wird ein einziger Registry-Eintrag erstellt:
   ```
   HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\ICM
   Name:  GdiIcmGammaRange
   Wert:  256
   ```
   Windows hindert Apps standardmäßig daran, die Display-Gamma zu ändern; dieser
   Eintrag sagt Windows einfach *„Erlaube Gamma-Anpassungen."* Er ändert keine
   Bildschirmeinstellungen selbst, ist kein Sicherheitsrisiko, wird von vielen
   Kalibrierungs-Tools genutzt und kann jederzeit entfernt werden.
4. **PC neu starten** (nur einmalig nötig)
5. `BrightRaider.exe` erneut starten — fertig!

Die UAC-Abfrage kommt nur **einmal**. Danach läuft die App ohne Admin-Rechte.

### Modifier-Belegungen (Strg / Alt / Shift)

Jedes belegbare Feld akzeptiert Modifier + Taste:

1. Auf das Feld klicken (zeigt *„Taste drücken…"*)
2. Strg, Alt und/oder Shift halten
3. Trigger-Taste drücken — z. B. `Shift+5`, `Strg+Numpad 5`, `Alt+F2`
4. Esc bricht ohne Änderung ab

- **Belegbar:** A–Z, 0–9, F1–F24, Numpad-Tasten, OEM-Zeichen, Pfeil-/Nav-Tasten,
  Maus MB3/MB4/MB5, Mausrad hoch/runter.
- **Nicht als Einzeltaste** (nur Modifier): Shift, Strg, Alt, Win, Esc, Tab, Enter.
- **OS-reservierte Kombis** (belegbar, aber Windows fängt sie ab): Win+L/D/E/R/S/I/Tab,
  Alt+F4, Alt+Tab, Strg+Alt+Entf, Strg+Shift+Esc — BrightRaider zeigt einen Warn-Toast.
- **Sprint-Konflikt:** `Shift+` als Modifier feuert bei jedem Sprint, wenn Sprint
  auf Shift liegt. `Strg+`/`Alt+` wählen oder Sprint umbelegen (Movement-Tab).

### Du spielst mit Controller?

**Controller-Tasten lassen sich nicht direkt belegen.** BrightRaider lauscht
per Low-Level-Hook auf Tastatur und Maus und kann eine Taste dadurch
*schlucken* — ein CapsLock-Tipp für Autorun kommt im Spiel nie als CapsLock an.
Ein Gamepad spricht über einen getrennten Kanal mit Windows, für den es keinen
solchen Hook gibt: Eine Taste ließe sich nur beobachten, nie abfangen. Sie würde
also BrightRaider **und** ihre Spielfunktion gleichzeitig auslösen. Auf einer
Tastatur opfert man dafür eine von über hundert Tasten, auf einem Controller
gibt es keine freie.

Zwei Dinge funktionieren trotzdem:

- **Die Anzeigefunktionen brauchen überhaupt keine Hotkeys.** Spielprofile mit
  Alt-Tab-Auto-Switch greifen von selbst, sobald das Spiel in den Vordergrund
  kommt — Vibrance, Gamma, Kontrast, Hue und FPS-Limit. Das ist der größte Teil
  von BrightRaider und völlig unabhängig vom Eingabegerät.
- **Für die Hotkey-Funktionen einen Remapper nutzen.** Steam Input, DS4Windows
  oder reWASD fangen die Controller-Taste auf ihrer Ebene ab und senden eine
  Tastatureingabe. BrightRaider sieht dann einen normalen Tastendruck,
  Schlucken inklusive, und verhält sich exakt wie vorgesehen. Das ist keine
  Notlösung — der Remapper kann abfangen, wo BrightRaider es nicht kann.

### Standard-Hotkeys (Numpad-Preset)

| Taste            | Aktion                                     |
|------------------|--------------------------------------------|
| Numpad 1–3       | Profil 1–3 (Kostenlos)                     |
| Numpad 4–9       | Profil 4–9 (Pro)                           |
| Numpad 0 (kurz)  | Spiel stumm/laut (Pro)                     |
| Numpad 0 (halten)| Audio Ducking an/aus (Pro)                 |
| Numpad +         | Fadenkreuz-Overlay an/aus (Pro)            |
| Numpad *         | Timer-Overlay an/aus (Pro)                 |
| CapsLock         | Autorun an/aus (Pro)                       |
| M (halten)       | Evakuierungs-Timer scannen (Pro)           |
| 4 / 5 / 6        | QuickSelect: Slot-Item automatisch (Pro)   |
| Numpad −         | QuickSelect an/aus (Pro)                   |
| Numpad /         | QuickSave an/aus (Pro)                     |
| Numpad Entf      | Globaler EIN/AUS (siehe unten)             |
| Numpad Enter     | Audio-Ausgabegerät wechseln (Pro)          |

Funktioniert mit/ohne Shift, NumLock an oder aus. **TKL-/Laptop-Preset** legt
Profile auf F1–F9 (Geräte-Wechsel auf PageUp). **AZERTY-Preset** nutzt Z als
Vorwärts-Taste. Assistent jederzeit über *Einstellungen → Hotkeys → „Preset
anwenden…"*.

### Pro-Aktivierung

1. Lizenz auf der BrightRaider-Website kaufen
2. Du erhältst eine E-Mail mit deinem Lizenzschlüssel
3. Rechtsklick Tray → **Einstellungen → „Lizenz eingeben…"**
4. E-Mail und Schlüssel eingeben — Pro sofort freigeschaltet, lokal gespeichert
5. **Neuer PC?** Über *Einstellungen → App → „Deactivate on this device"* gibst du
   deinen Aktivierungs-Slot selbst frei und aktivierst dann am neuen Gerät — kein
   Support-Ticket nötig.

### Autorun (Pro)

CapsLock kurz drücken, um Autorun zu starten — BrightRaider hält W, dein
Charakter läuft (oder rennt) automatisch vorwärts. Während Autorun aktiv:

| Taste    | Wirkung                                                       |
|----------|--------------------------------------------------------------|
| CapsLock | Autorun stoppen                                              |
| Shift    | Sprint an/aus                                                |
| W        | Stoppt Injektion — Charakter läuft weiter, bis W losgelassen |
| S        | Autorun komplett abbrechen                                   |
| C        | Slide (während Bewegung), dann stoppen                       |

- **Sprint-Modus** (*Movement → Modus: Sprint*) startet sofort im Sprint.
- **Tap-Modus** (CapsLock 600 ms halten) tippt W: kurz gedrückt (Standard
  **160 ms**), dann losgelassen (Standard **1300 ms**). Gebaut fürs **Looting
  Mk. 3 (Survivor)** Augment — in der Pause steht dein Charakter still und
  regeneriert, der kurze Tipp hält dich am Kriechen. Konfiguration:
  *Movement → Tap-Abstand…*.

  > **Neu abgestimmt in V1.1.** Ein aktueller Arc-Raiders-Patch hat das Augment
  > generft, und mit den alten 990 / 169 kam man nicht mehr voran. Mit
  > 1300 / 160 bewegt man sich wieder *und* gewinnt Leben dazu. **Upgrade von
  > V1.0?** Deine gespeicherten Werte bleiben erhalten — für das neue Paar
  > trage sie unter *Movement → Tap-Abstand…* von Hand ein. Der Nerf ist
  > trotzdem hart: Nimm unbedingt **Crawl Before You Walk** (gelber Skill-Baum,
  > oben in der Mitte) — damit kriecht man deutlich schneller.
- **AZERTY:** *„Z = vorwärts"* unter Movement aktivieren.

### Audio Ducking (Pro)

Die Stumm-Taste hat zwei Modi: **kurzer Druck** = stumm/laut; **Halten** (Standard
600 ms) = Spiel-Audio auf einen Prozentsatz reduzieren (Standard 20 %), damit du
Discord oder Musik hörst. Lautstärke (0–100 %) und Haltezeit (200–2000 ms) unter
*Einstellungen → Audio*.

### Footstep Booster / Loudness Limiter (Pro)

Begrenzt die Spiel-Audio-Session bei einem Schwellwert, damit du die Lautstärke
für Schritte hochdrehen kannst, ohne dass Schüsse dich taub machen. Per-Prozess —
Discord, Musik und Browser bleiben unberührt.

- **Schwellwert (10–90 %)** — ab wann der Limiter eingreift (Start ~35–45 %)
- **Attack (1–200 ms)** — wie schnell er auf einen lauten Transienten reagiert (~10–20 ms)
- **Release (50–2000 ms)** — wie schnell er zurückfährt (~250–400 ms)

Detaillierte Schritte in `FootstepBooster_Guide.md`.

### Background Mute (Pro)

Schaltet die Spiel-Audio-Session beim Alt-Tab raus automatisch stumm, hebt es beim
Zurückwechseln auf. Per-Prozess — Musik + Discord laufen weiter. *Einstellungen →
Audio → „Spiel beim Alt-Tab stumm"*.

### Audio-Ausgabe-Umschalter (Pro)

Wechselt dein Standard-Ausgabegerät im Betrieb. Gewünschte Geräte ankreuzen, dann
per Hotkey durchwechseln (ein Toast zeigt das Gerät). Optionaler **Auto-Wechsel
beim Spielstart** stellt das vorherige Gerät beim Spiel-Ende wieder her. Der
Wechsel setzt alle Rollen, also folgt auch Discord.

### Map Scanner (Pro)

**M** auf der Karte halten — BrightRaider zoomt heraus, liest alle
Evakuierungs-Timer lokal auf deinem PC aus und zeigt einen Live-Countdown.

**Unterstützte ARC-Raiders-Karten:** Buried City, Stella Montis, Space Port, Blue
Gate, Dam, Riven Tides. **Andere Spiele** per Daten-Pack (`pack-authoring.md`).

**Farbkodierung:**

| Farbe  | Bedeutung             |
|--------|-----------------------|
| Grün   | mehr als 10 Minuten   |
| Gelb   | mehr als 5 Minuten    |
| Orange | mehr als 1 Minute     |
| Rot    | weniger als 1 Minute  |
| Grau   | geschlossen / unbekannt |

**Event-Erkennung** — der Scanner erkennt aktive Karten-Bedingungen und passt die
Anzahl offener Evakuierungspunkte an: **Night Raid** (2 Punkte, Hatches zu),
**Elektromagnetischer Sturm** (3 Punkte, Hatches zu), **Husk Graveyard**,
**Locked Gate**, **Uncovered Caches** und **Hidden Bunker** (Spaceport — 3 Punkte,
Hatches zu). Der Name erscheint im Overlay.

Konfiguration über *Tray → Map Scanner Einstellungen*: Haltedauer, Overlay-Position
(6 Optionen), Schriftgröße/Farbe, Evac-Alarm-Schwellwert.

### Evac-Alarm (Pro)

Meldet, wenn ein Timer unter den Schwellwert fällt: roter Toast + Sound, einmal
pro Timer, neu scharf beim erneuten Scan. Konfiguration in *Map Scanner
Einstellungen → Evac-Alarm* (getrennte Minuten-/Sekunden-Felder). Map Scanner muss
aktiv sein.

### QuickSelect (Pro)

Eine Taste drücken, um ein Schnellrad-Item automatisch zu nutzen. BrightRaider
führt die Sequenz aus: Q halten → Slot wählen → Q loslassen → LMB für die
eingestellte Zeit halten → optional H für freie Hand.

**Einrichten:** *Tray → Einstellungen → QuickSelect…* → aktivieren → Trigger-Taste
pro Slot belegen → Rad-Slot (1–10) setzen → **LMB-Haltezeit in ms eintippen** →
optional H pro Slot.

**Empfohlene LMB-Haltezeiten (ms):**

| Item                     | ms   |
|--------------------------|------|
| Adrenalinspritze         | 1300 |
| Kräuterverband / Verband | 1800 |
| Schildauflader           | 2300 |
| Vita-Spritze             | 4300 |
| Sofort-Schildauflader    | 5300 |

*(Wird ein Item nur teilweise benutzt, 100–200 ms zugeben.)*

**Tipps:** erst mit einem günstigen Item testen · Slots 7–10 per Cursor, Slots 3–6
per Zifferntaste (immun gegen Mausbewegung) · H auch auf MB3/4/5 belegbar ·
QuickSelect beim Tippen im Chat pausieren (Numpad −) · nach H mitten in der Nutzung
kurz warten, bevor du neu triggerst (das Spiel hält das Item kurz in einem
Übergangszustand) · kein Bestätigungs-Popup gewünscht? Die QuickSelect- /
QuickSave-Toasts unter *Einstellungen → App* stummschalten (die Makros laufen
weiter, nur lautlos).

### QuickSave (Pro)

Zieht Items zwischen Inventar, Sicherheitstasche und **Rucksack** — mit einem
Tastendruck oder Mausradscrollen.

- **Was sich verschieben lässt:** Inventar-Slots 1–8, Waffen, Sicherheitstasche
  1–3 und **Rucksack 1–4** (oberste Reihe) — neu in V1.1. Richtung frei einstellbar.
- **So funktioniert es:** Inventar öffnen (Tab) → Cursor zum Quell-Slot → LMB
  halten und zum Ziel ziehen → loslassen → Inventar schließen (Tab).
- **Richtung umkehren (⇄):** erster Druck Von→Nach, zweiter Nach→Von.
- **Timing** (falls Drag fehlschlägt): Öffnen (250 ms), Hover (0 ms), Halten
  (0 ms), Ablegen (120 ms), Cooldown (0 ms).
- **An/aus:** Numpad / (Numpad) oder Ende (Pfeiltasten).

> **Bekannte Einschränkung:** gelegentliche Drag-Fehler kommen vom Unreal-Engine-
> Slate-UI, das ARC Raiders nutzt — dasselbe passiert beim manuellen Ziehen. Von
> außen nicht behebbar; einfach nochmal auslösen.

### Crosshair-Overlay (Pro)

Ein transparentes, click-through Fadenkreuz auf dem Bildschirm (Vollbild-tauglich;
gleicher Mechanismus wie Discord / GeForce Experience). Umschalten mit Numpad +
(oder Einfg). 6 Stile, freie Farbe und Größe (4–50 px), optionaler Umriss.

### Auto-Helligkeit (Pro)

Analysiert 5 Bildschirmzonen (Mitte gewichtet, 4 Ecken) per Median-Helligkeit und
wechselt sanft die Profile — dunkler Bildschirm = mehr Boost. Aktivieren über
*Tray → Auto-Helligkeit*. Kalibrierung in zwei Schritten (dunkelste, dann hellste
Stelle); das Fenster sitzt unter allen Zonen, um das Ergebnis nicht zu
verfälschen. Zonen-Gewichte (0–10) unter *Einstellungen → Zonen…*. Komplette
Anleitung in `AutoBrightness_Guide.md`.

### Alt-Tab Auto-Switch (Kostenlos)

Pro Spiel automatisch Profil + FPS-Limit + Fadenkreuz, sobald das Spiel in den
Vordergrund kommt — Schluss mit manuellem Wechsel oder parallelem VibranceGUI.

1. Spiel hinzufügen: *Einstellungen → Game Profiles → „Hinzufügen…"*
2. Basis-Profil wählen (optionale Overrides: eigenes Gamma/Kontrast/Vibrance/Hue/FPS)
3. Spiel starten — Profil, Fadenkreuz und FPS-Limit greifen automatisch
4. Alt-Tab raus — Original-Ramps + Vibrance + Hue wiederhergestellt, Fadenkreuz weg

**FPS-Limit pro Spiel:** NVIDIA über NvAPI DRS (kein DirectX-Hook), AMD über ADLX
FRTC (`amdadlx64.dll`), Intel hat keine öffentliche API (UI zeigt Hinweis).

### Auto-HDR pro Spiel (Kostenlos) — neu in V1.1

Schaltet Windows-HDR **an**, wenn ein gewähltes Spiel startet, und **aus**, wenn
es endet. Opt-in pro Spiel (*Game Profiles → „Auto-HDR for this game"*). Alt-Tab
rein/raus schaltet HDR nie um — nur echter Spielstart/-ende, damit du nie den
1–2 s Schwarz-Flash mitten in der Session zahlst. War HDR schon an (deins), lässt
BrightRaider es in Ruhe. Braucht aktiven Alt-Tab Auto-Switch.

### Farbkonflikt-Warnung — neu in V1.1

Läuft ein anderes Tool, das deine Display-Farben steuert (f.lux, Iris, Windows
Night Light, …), sagt BrightRaider es dir beim Start und im Display-Tab. Diese
Tools und BrightRaider kämpfen um dieselbe Gamma-Pipeline — die häufigste Ursache
für *„meine Farben springen immer zurück."* Nur-lesend: BrightRaider fasst das
andere Tool nie an, es warnt nur.

### Process Optimizer (Pro, opt-in)

Für ein erkanntes Vordergrund-Spiel kann BrightRaider die Windows-Prozess-
Priorität anheben und auf physische Kerne pinnen (Hyperthreading aus) —
glattere Frametimes. *Einstellungen → Performance*; beide aus per Default; setzt
beim Alt-Tab raus / Spiel-Ende zurück.

### Hotkey-Pause (Pro)

Deaktiviert vorübergehend alle BrightRaider-Hotkeys — die App läuft weiter, fängt
nur keine Tasten mehr ab. *Einstellungen → Hotkeys pausieren*; ein Toast bestätigt.

### Nur Hotkeys bei Spiel-Fokus

Optionaler Schalter (*Einstellungen → Hotkeys → Verhalten*). Wenn an, gehen alle
Hotkeys auf dem Desktop / in anderen Apps durch und reaktivieren automatisch,
sobald ein Vollbild-Spiel im Vordergrund ist. Default aus — für Fenster-Modus
ausgelassen.

### Global EIN/AUS (Numpad Entf — Kostenlos)

Zwei Modi, einstellbar unter *Einstellungen → App → „Toggle OFF also resets the
display"*:

- **Angehakt (Default) = KOMPLETT AUS** — versteckt jedes Overlay, stellt deine
  Original-Gamma/-Vibrance/-Hue wieder her, pausiert Auto-Helligkeit, stoppt den
  Footstep Booster. Das Display verhält sich, als wäre BrightRaider nicht da.
  Nochmals drücken wendet das aktive Profil wieder an.
- **Nicht angehakt = NUR HOTKEYS PAUSIEREN** — nur die Hotkeys pausieren; Farbprofil,
  Overlays und FPS-Limit bleiben. Wie V9.x auf Page Down / Numpad Entf.

### Start-Modi (das Tools-Paket)

Für den Alltag startest du einfach `BrightRaider.exe`. Das optionale
**Tools-Paket** (`BrightRaider_tools.zip` auf der Releases-Seite) bringt zwei
Doppelklick-Starter für Sonderfälle — leg sie in denselben Ordner wie
`BrightRaider.exe` und starte den, den du brauchst:

- **`BrightRaider (debug scan).cmd`** — startet BrightRaider mit
  Map-Scanner-Diagnose. Öffne die Spielkarte und scanne einmal; es schreibt ein
  PNG + `.txt` nach `%LocalAppData%\BrightRaider\debug`. Schick mir diese Dateien,
  falls ein Karten-Timer mal falsch angezeigt wird.
- **`BrightRaider (sendinput mode).cmd`** — startet QuickSave mit seinem
  alternativen, tiefer liegenden Eingabepfad. Nur nutzen, wenn ein Spiel
  QuickSaves Standard-Inventar-Drag nicht registriert. Im Normalbetrieb aus; beim
  Start erscheint ein Hinweis, solange er aktiv ist.

Jeder Starter setzt nur eine Umgebungsvariable vor dem App-Start — du kannst einen
in Notepad öffnen, um genau zu sehen, was er tut. Für den Normalbetrieb brauchst
du sie nie — `BrightRaider.exe` allein ist der Standardweg.

BrightRaider eleviert sich beim ersten Start einmalig selbst für den
Gamma-Registry-Eintrag — das ist automatisch und braucht keinen Starter.

### Hinweise

- Numpad-Tasten sind belegt, solange das Tool läuft; die normalen Zahlentasten nicht.
- Vibrance (NVIDIA) / Sättigung (AMD) wird im GPU-Control-Panel angezeigt;
  Gamma/Kontrast nicht (andere API — das ist normal).
- Im Tray-Menü wird unten die erkannte GPU angezeigt.
- Falls etwas komisch aussieht: einfach die App beenden — alles wird automatisch
  zurückgesetzt.

### Deinstallation

1. BrightRaider beenden (Tray → Beenden)
2. Den Ordner löschen
3. *Optional:* `HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\ICM →
   GdiIcmGammaRange` entfernen
4. *Optional:* `HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Run → BrightRaider`
   entfernen

---

## Files / Dateien

| Path | Purpose |
|------|---------|
| `BrightRaider.exe` | Single EXE / Einzelne EXE |
| `%APPDATA%\BrightRaider\BrightRaider.cfg` | Settings (auto) / Einstellungen |
| `%APPDATA%\BrightRaider\BrightRaider.lic` | License / Lizenzdatei |
| `%APPDATA%\BrightRaider\debug.log` | Diagnostic log / Diagnose-Log |

**Guides (`docs/`):** `QuickSelect_Guide.md` · `QuickSave_Guide.md` ·
`MapScanner_Guide.md` · `AutoBrightness_Guide.md` · `FootstepBooster_Guide.md` ·
`pack-authoring.md` (add other games to the Map Scanner)
