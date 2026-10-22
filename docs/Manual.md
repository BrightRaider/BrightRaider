# BrightRaider — Manual

**Switch Gamma, Contrast, Vibrance & Hue with one keypress.**
Plus Auto-Brightness, Alt-Tab Auto-Switch, per-game Auto-HDR, dimming of your
other screens, Footstep Booster, Map Scanner, QuickSelect, QuickSave & the
Autoscrapper.

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
EXE, no installation.** A short setup walks you through everything on first
launch.

> **New in V1.3 — BrightRaider grows up.** The core is a tool for **every PC
> game**: colours, FPS, sound, crosshair, autorun. The ARC Raiders helpers (Map
> Scanner, Autoscrapper, QuickSelect, QuickSave) are its first **module**, with
> their own page and one switch: *Settings → Modules → Arc Raiders*. Every
> settings page has a **Guide** beside it. The Map Scanner can also read **other
> games** via downloadable data packs (see `pack-authoring.md`).

### Free features

- 3 switchable display profiles via Numpad 1/2/3 (or Arrow keys)
- Adjusts Gamma, Contrast, and Digital Vibrance/Saturation
- Supports NVIDIA (NvAPI) and AMD (ADL SDK); auto-detects your GPU
- Also works on Intel / other GPUs (Gamma + Contrast only)
- Per-monitor control (choose one or all monitors)
- Alt-Tab Auto-Switch — per-game profile + FPS limit, applied automatically
- Per-game Auto-HDR — turn Windows HDR on/off automatically per game
- Dim other displays — darkens your other screens while the game is in front
- **G-SYNC / FreeSync setup** the Blur Busters way — V-SYNC, low latency and the
  right FPS limit in the driver, with an exact Undo (new in V1.3)
- **Setup assistant** and a **Guide** beside every settings page (new in V1.3)
- Works with Shift held down (no conflicts)
- Notifications can be turned on/off and shown on **the screen you choose**
  (*App & License → Notifications*), **color-coded**: green = good news (update
  available), yellow = warning, red = something needs you. The QuickSelect /
  QuickSave confirmations have their own switch on the Arc Raiders page, so those
  macros can run silently while other notifications stay on
- Color-conflict warning — a heads-up when another tool (f.lux, Night Light, …)
  is fighting over your display colors
- English interface (German follows in a later version)
- Auto-Start with Windows · all settings saved automatically
- Resets everything to normal on exit
- Break Reminder — orange toast after a configurable interval; never interrupts gameplay

### Pro features (€5.49 one-time)

Every Pro page can be opened and set up without Pro — the tool runs in your
games once Pro is active.

- **Autorun** — press CapsLock to hold W automatically. Sprint Mode starts
  sprinting directly; Tap Mode (hold CapsLock 600 ms) pulses W at intervals.
  Fully configurable timing, and per game its own sprint key, hold or toggle and
  tap timing. Lives on its own **Movement** page.
- **Audio Ducking** — hold the mute key 600 ms to drop game audio to a set
  percentage. Short press still mutes/unmutes.
- **Footstep Booster / Loudness Limiter** — cap the game's audio session so you
  can crank volume for footsteps without going deaf on gunshots (see
  `FootstepBooster_Guide.md`).
- **Map Scanner** — hold M on the in-game map to read all evacuation timers
  locally, with a live colour-coded overlay; name the exits yourself (new in V1.4).
- **Evac Alarm** — red toast + sound when a timer drops below your threshold.
- **Autoscrapper** — press F5 with the stash open: reads the whole stash and
  scraps or sells by your rules — by default only after you confirm the list.
- **Mute Teammates** — one key mutes your voice chat apps (Discord, TeamSpeak …),
  with mute icons on screen.
- **Crosshair Overlay** — click-through crosshair, 6 styles, custom colour, size
  (1–50 px), optional outline; remembered per game (new in V1.4).
- **Audio Output Switcher** — cycle the default output device with a hotkey;
  optional auto-switch when a game starts/closes.
- **Up to 9 profiles** (Numpad 1–9) with a full custom editor (Gamma, Contrast,
  Vibrance, Hue — on NVIDIA the Hue slider runs 0–359° like the Control Panel).
- **Auto-Brightness** — screen analysis detects dark scenes and switches to a
  brighter profile.
- **Game Mute**, **Background Mute**, **Process Optimizer**, **QuickSelect**,
  **QuickSave**, and an **offline license** (no internet needed after
  activation).

### Default profiles

| Key    | Name     | Gamma | Contrast | Vibrance |
|--------|----------|-------|----------|----------|
| Num 1  | Normal   | 1.0   | 100 %    | 50 %     |
| Num 2  | Bright   | 1.5   | 110 %    | 60 %     |
| Num 3  | Brighter | 2.0   | 110 %    | 70 %     |
| Num 4–9 | Custom  | —     | —        | Pro users edit all values |

### System requirements

- Windows 10 or 11 (x64)
- Nothing to install — one EXE under 6 MB; the Arc Raiders module downloads
  when you switch it on
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

### Setup and the settings window (new in V1.3)

**The setup** opens at the first start — and **once after updating to 1.4**
from V1.3 or older. Every page already shows what you have: **Next** keeps it,
only what you change is changed. Pages that are new since your version are
marked *New in 1.3* or *New in 1.4*; the last page shows what changed and what
stayed. Every page fits on the screen, nothing to scroll. Closing the window
keeps everything as it is. It never opens on top of a running game. Run it again any time:
*App & License → Run the setup…* (that is also where you pick another keyboard
layout).

**The settings window** (tray → *Settings…*):

- A **sidebar**: *App & License*, then **Display** (Colour profiles, Games &
  FPS, Auto-Brightness), **Tools** (Audio, Crosshair, Movement, Performance) and
  **Modules** (Arc Raiders with Map Scanner, Autoscrapper, QuickSelect,
  QuickSave). ☰ folds it down to icons; the arrow next to a module folds its
  pages away.
- **Every key sits on the page of its feature** — the profile key next to the
  profile, the mute key under Audio. **All keys** (bottom of the sidebar) lists
  them in one place and shows conflicts.
- **A key bound twice** — BrightRaider checks its own keys and every module's
  at start and whenever you change one. A key used twice is shown once as a
  message and stays in the tray (*A key is bound twice*) until you change it;
  All keys names the pair at the top. Keys of two different games don't
  count — each only works in its own game. Give a key that is already in use
  (in the settings or the setup), and it moves there: the other one has none
  now and you are told (*Cancel* in the settings, or closing the setup without
  *Finish*, undoes it). Keys that only say how your game is bound (map key,
  sprint) stay.
- **Guide** on every page opens a short guide beside it: what happens, how to
  set it up, what to try when something is off.
- Rarely used options are folded under **Advanced: …**; what makes a feature
  work stays in view.
- **Pro pages** can be set up without Pro; a quiet line on top says the tool
  runs in your games once Pro is active.
- **Search** (Ctrl+F, top of the sidebar) jumps to any setting. **Size**
  (*App & License*, also Ctrl + / −) makes everything 70–150 % as large; on a
  wide window the cards sit side by side (new in V1.4).
- **Report a problem…** (*App & License*) puts your version, system and the end
  of the log together and opens a new issue for you to check and send
  (new in V1.4).

**Modules** (new in V1.4): the Arc Raiders tools are a module. Switch it on in
the setup or on *Modules → Arc Raiders*, and BrightRaider downloads it (8 MB)
into `%LOCALAPPDATA%\BrightRaider\modules` and loads it — only with
BrightRaider's signature. When a newer Arc Raiders module is out, a notice and
the tray line **Install Arc Raiders …** offer it: one click downloads only the
module, and BrightRaider restarts as soon as no game is in front. Nothing
installs without your click. Your Pro key keeps everything it unlocks,
Arc Raiders included.

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
  fire on every sprint. Pick `Ctrl+`/`Alt+`, or rebind Sprint (Movement page).

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
| (unbound)        | Mute / unmute teammates — voice chat (Pro)|
| (unbound)        | Dim other displays off / on (Free)        |

On Free, the keys of Pro features are not used by BrightRaider: they go straight
to whatever window is in front, in every program. The same goes for the Arc
Raiders toggle keys while the Arc Raiders module is off. (QuickSelect and
QuickSave keys only act inside the game; there Free still sees the Pro notice.)

Works with or without Shift, NumLock on or off. **TKL/Laptop preset** puts
profiles 1–3 on the arrow keys ← ↓ →, mute on ↑, the crosshair on Insert, Global
ON/OFF on PageDown and the audio-device cycle on PageUp. **AZERTY preset** uses Z
as the forward key. Pick another layout any time: *App & License → Run the
setup…* → Keyboard.

### Pro activation

1. Purchase a license on the BrightRaider website
2. You receive an email with your license key
3. *App & License → Activate Pro license…* — or **I have a key** on any Pro
   page, or tray → **Enter License…**
4. Paste your key — Pro unlocks instantly and is saved locally; a short welcome
   shows what is now yours, each tool with a way straight to its page
5. **Moving to a new PC?** Use *App & License → Deactivate on this PC* to free
   your activation slot yourself, then activate on the new machine — no support
   ticket needed.

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

Autorun ends by itself when another program comes to the front, because its keys
go to whatever is in front.

- **Sprint Mode** (*Movement → Sprint while running*) starts in sprint immediately.
- **Tap Mode** (hold CapsLock 600 ms) pulses W: pressed briefly (default
  **160 ms**), then released (default **1300 ms**). Built for the **Looting
  Mk. 3 (Survivor)** augment — during the pause your character is stationary
  and regenerates, the brief tap keeps you crawling forward. Configure under
  *Movement → Advanced: tap mode timing*.

  > **Retuned in V1.1.** A recent Arc Raiders patch nerfed the augment, and the
  > old 990 / 169 pair stopped making progress. 1300 / 160 gets you moving
  > *and* regenerating again. **Upgrading from V1.0?** Your saved values are
  > kept — set them by hand under *Movement → Advanced: tap mode timing* if you want the
  > new pair. The nerf still bites, so it's well worth taking **Crawl Before
  > You Walk** (yellow skill tree, top middle) — crawling gets far faster.
- **Per game** (*Movement → Advanced: one game works differently*): a game can
  get its own forward and sprint key, sprint as a held key or a toggle, start
  sprinting or walking, and its own tap timing. It is the same game list as on
  Games & FPS; a game with its own values opens this section by itself.
- **AZERTY:** set *Forward* to Z under *Movement → Your game's keys* (or pick
  AZERTY in the setup).

### Audio Ducking (Pro)

The mute key has two modes: a **short press** mutes/unmutes; a **hold** (default
600 ms) ducks game audio to a set percentage (default 20 %) so you can hear
Discord or music. It stays down until you hold the key again. Set the volume under *Audio → Game volume while held*
(0–100 %) and the hold time under *Audio → Advanced* (200–2000 ms).

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
Per-process — your music + Discord keep playing. *Audio → Mute when you
Alt-Tab out*.

### Mute Teammates (Pro)

One key mutes or unmutes your voice chat while you play. Bind it on the
*Audio* page (*Mute teammates key*); the apps it silences are in the list right
below it. New installs start with Discord, TeamSpeak 3 and the new TeamSpeak,
Mumble, Teams and Zoom in it; **+ Add app** picks any running program,
**Remove** takes one out, and **Add the usual** puts the usual ones back. Apps that are not running are skipped. Unbound
by default. Only the listed apps are touched, every instance of them; this is
not the game's own voice chat.

The mute stays until you press the key again. It also ends by itself when the
game closes or when you switch BrightRaider off with the Global ON/OFF key, and
the tray menu has **Unmute teammates** for as long as they are muted. If
BrightRaider exits or crashes while they are muted they are unmuted again at the
next start — Windows would otherwise remember the mute. An app that is not
running yet at that point (Discord starting after BrightRaider at logon) is
unmuted as soon as it shows up.

### Mute Indicator (Pro)

While the game is muted with the mute key, a small speaker-with-a-cross icon
shows in the top-left corner of the game's screen, so you can see at a glance
why it is quiet. While *Mute teammates* has your voice chat silenced, a headset
struck through appears next to it; both can be up at once. It never takes clicks or focus and hides when you leave the
game. It shows while the game runs borderless (windowed fullscreen), which is
BrightRaider's idea of "a game is in front"; in a normal window it stays hidden.
In *exclusive* fullscreen Windows does not draw other windows over the game, so
the icon cannot show there — switch the game to borderless. On by default;
switch it off under *Audio → Mute icons on screen*.
The key reads the game's real mute state each time, so a game muted by
something else (the Windows mixer, Background Mute) is unmuted by one press.

### Audio Output Switcher (Pro)

Switch your default Windows output device on the fly (*Audio → Switch output
device*). Tick the devices you want in the rotation, then press the cycle
hotkey to step through them (a toast shows the device). Optional **Switch by
itself while a game runs** restores the previous device when the game closes.
Voice chat (Discord …) stays on its device unless you switch on *Audio →
Advanced → Voice chat follows the device switch* — leave that off with
TeamSpeak.

### Map Scanner (Pro)

Hold **M** on the in-game map — BrightRaider zooms out, reads all evacuation
timers locally on your PC, and shows a live countdown overlay.

**Supported ARC Raiders maps:** Buried City, Stella Montis, Space Port, Blue
Gate, Dam, Riven Tides, Pendola Pass (gondola points; closed ones show
**CLOSED**). **Other games** can be added via data packs
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
points, hatches closed), and the **ARC Frigate** event (all evac points open).
The condition name appears in the overlay.

**Your own names for the exits** (new in V1.4): *Advanced: your own names for
the exits* lists every exit of every map. Type what you call it — a landmark, a
building — and the overlay and the evac alarm use your name; an empty field keeps
the game's.

If the map is not actually open when you hold M (a menu, the XP screen), the
scanner says *no map detected* instead of guessing.

Configure under *Modules → Arc Raiders → Map Scanner*: the colours and the
overlay position up front; background, font size, hold time, map key and alarm
repeat under *Advanced*.

### Evac Alarm (Pro)

Alerts when a timer drops below your threshold: a red toast + a sound, once per
timer, re-armed when the timer is re-scanned. Switch it on and set the
threshold on the Map Scanner page (*Evac alarm*). Map Scanner must be active.

### QuickSelect (Pro)

Press one key to auto-use a quick-wheel item. BrightRaider runs the whole
sequence: hold Q → select slot → release Q → hold LMB for the configured time →
optionally press H to free the hand.

**Setup:** *Modules → Arc Raiders → QuickSelect* → switch it on → match your
game's keys once in the **Setup** block → per item: trigger key, slot (1–10),
**hold time in ms**, on. *Timing reference* lists the times below, *Slot
layout* shows which slot is which, *All columns* shows the after-use action.

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
transitional state) · don't want the confirmation pop-up? Switch off *Confirm
QuickSelect and QuickSave* on the Arc Raiders page (the macros still run, just
silently).

### QuickSave (Pro)

Drags items between your inventory, Safe Pocket and **backpack** with one keypress
or wheel scroll — no manual clicking.

- **What can move:** inventory slots 1–8, weapons, Safe Pocket 1–3, and
  **Backpack 1–4** (top row) — new in V1.1. Direction is fully configurable.
- **How it works:** opens inventory (Tab) → moves cursor to source → holds LMB
  and drags to destination → releases → closes inventory (Tab).
- **Where it aims:** at the raid inventory, as in V1.0 — on 4:3 and 16:10
  screens too, where it sits in the centre. No look first, no cursor jump. In
  the hideout the inventory sits elsewhere, so QuickSave is for raids.
- **Toggle direction (⇄):** first press moves From→To, second To→From. A press
  while a move is still running is ignored and does not turn the direction.
- **Timing** (if a drag fails, *QuickSave → Advanced: inventory key and
  timings*): Open (250 ms), Hover (0 ms), Drag (0 ms), Drop (120 ms), Cooldown
  (0 ms).
- **Toggle on/off:** Numpad / (Numpad) or End (Arrow).

> **Known limitation:** occasional drag failures come from the Unreal Engine
> Slate UI that ARC Raiders uses — the same thing happens with manual drag-drop.
> It can't be fixed from outside the game; just trigger again.

### Autoscrapper (Pro)

Reads your stash from the screen and scraps or sells what your rules say —
by default after you have seen the list and confirmed it ("Run without the
review" skips that). It sends simulated clicks, cursor moves and scrolling to
the game — the most exposed input feature, see the README's anti-cheat
section. Full walkthrough: [Autoscrapper Guide](Autoscrapper_Guide.md).

- **Rules:** *Modules → Arc Raiders → Autoscrapper* — an item (or a whole category), how
  many to keep, and what happens to the rest: leave alone, scrap or sell.
  Anything not listed is never touched.
- **Run:** open the stash in game and press **F5**. Keep your hands off the
  mouse while it scrolls (~10 s for 300 slots).
- **Review:** the list comes up in stash order. **Enter** runs it, **Esc**
  changes nothing; what you set there is saved as rules. Hold **Esc** to stop
  a running pass.
- **Test run** is the default: it goes up to the game's confirmation box and
  cancels it. Switch to **Live** once it behaves.
- **Unrecognised tiles** stay unnamed and untouched. Name them once with
  *Name unrecognised items…* and they are known from the next scan on.

### Crosshair Overlay (Pro)

A transparent, click-through crosshair drawn on screen (works in fullscreen; same
overlay mechanism as Discord / GeForce Experience). Toggle with Numpad + (or
Insert). 6 styles, custom colour and size (1–50 px), optional outline, and *Aim
pixel* for games that aim one pixel up and left. *Settings → Crosshair*.

**Per game** (new in V1.4): any overlay on top of a game can cost it G-SYNC, so
the crosshair is remembered per game — switched on in one game, it stays off in
the others until you switch it on there too.

### Auto-Brightness (Pro)

Analyses 5 screen zones (centre-weighted, 4 corners) by median brightness and
smoothly switches profiles — dark screen = more boost. Switch it on on the
*Auto-Brightness* page (or in the tray). *Set up the ranges → Start…*
calibrates in two steps (darkest, then brightest spot); the calibration window
sits below all zones so it doesn't skew results. Zone weights (0–10 each) under
*Advanced: screen zones and debug view*. Full walkthrough in
`AutoBrightness_Guide.md`.

### Dim Other Displays (Free)

*Colour profiles → Screens → Dim other screens.* While a game is in front, every
display except the one the game is on is darkened by the slider's amount, so a
bright second screen stops pulling your eye; Alt-Tab brings them straight back.
*Tip:* set it so you can still read the other screen but it is clearly calmer —
you get more focus and contrast on the game without losing the chat or a guide.
Watching a film on the other screen? Bind the *Dimming on/off key* right below: one
press brings the other screens back to normal, the next dims them again. The
switch is for the current session only; the slider and the next start are not
affected.
It applies like the other colour settings: only while the game is in front, and
only after Apply / OK. *Test on screen* flashes the dimming together with the
profile, so you can judge it without a game (pick one display above for that).
0 = off. With only one display connected there is nothing to dim, and the
slider is greyed out. It dims the display's own calibrated curve, down to a tenth at the slider's end — it does
not lower the backlight. Going that dark needs the extended gamma range (the
one-time Windows permission, *Full brightness range → Unlock* on the same page); without it
Windows refuses very dark curves and the slider stops at about 60 %.

If BrightRaider is killed or crashes while the other displays are dimmed, a small
helper process (`BrightRaider.exe --watchdog`, started with the dimming and gone
when BrightRaider ends normally) puts them back to their original curve within a
second or two. It does the same for an FPS limit BrightRaider had set in the
NVIDIA driver. If even that did not run, the next start does it.

### Alt-Tab Auto-Switch (Free)

Per-game profile + FPS limit + crosshair, applied automatically when a configured
game comes to the foreground — no more manual switching or running VibranceGUI
alongside.

1. *Games & FPS* → **Switch automatically** on → **+ Add game**
2. Optional per game: *Own colours for this game* and its own FPS limit
   (*FPS limit for all games* applies to the rest)
3. Launch the game — the profile, crosshair and FPS limit apply automatically
4. Alt-Tab out — original ramps + vibrance + hue restored, crosshair hidden

**FPS limit per game:** NVIDIA via NvAPI DRS (no DirectX hook), AMD via ADLX FRTC
(`amdadlx64.dll`), Intel has no public API (UI shows a note). BrightRaider sets
the limit as soon as the game starts and puts back exactly what it changed when
the game closes (a limit on the shared base profile, when the game has no profile
of its own, already when you tab out); a cap you set yourself in the NVIDIA
Control Panel is not touched. The driver reads the limit when the game starts, so **after you add or
change a limit, close the game once and start it again.**

### G-SYNC / FreeSync setup (Free) — new in V1.3

The variable-refresh setup the experts recommend ([Blur Busters — G-SYNC 101](https://blurbusters.com/gsync/gsync101-input-lag-tests-and-settings/14/)),
done in the driver for you — in the setup, or under *Games & FPS → Advanced:
G-SYNC / FreeSync setup*.

- **NVIDIA:** V-SYNC on in the driver, Low Latency Mode on, and an FPS limit
  just under your refresh rate (144 Hz → 138, 180 Hz → 171, 240 Hz → 224).
  G-SYNC itself is switched on in the NVIDIA Control Panel (*Set up G-SYNC*).
- **AMD:** FreeSync on, Wait for Vertical Refresh always on, Anti-Lag on, and the
  same limit.
- **Then turn V-SYNC off in your games** — the driver handles it now.
- **Competitive games** (Counter-Strike 2, Valorant, Fortnite, Apex Legends,
  Overwatch 2, Rainbow Six Siege, League of Legends) can be left out on NVIDIA:
  V-SYNC off and no limit, for the lowest delay. That needs the game in the
  driver's own list (most are); if not, add it in the NVIDIA Control Panel
  (*Manage 3D settings → Program Settings*) and set up again.
- **Undo** puts back exactly the values that were there before; a value you
  changed yourself in the meantime stays yours.
- BrightRaider's own FPS limit for all games is set to none while this is on,
  so the two never limit twice.

### Per-game Auto-HDR (Free) — new in V1.1

Turns Windows HDR **on** when a chosen game launches and **off** when it exits.
Opt-in per game (*Games & FPS → HDR while it runs*). Alt-Tabbing in and
out never flips HDR — it only switches on real game start/exit, so you never pay
the 1–2 s black flash mid-session. If HDR was already on (yours), BrightRaider
leaves it alone. Needs *Switch automatically* on.

### Color-conflict warning — new in V1.1

If another tool that controls your display colours is running (f.lux, Iris,
Windows Night Light, …), BrightRaider tells you at startup and on the Colour
profiles page. Those tools and BrightRaider fight over the same gamma pipeline — this is
the most common cause of *"my colours keep reverting."* Read-only: BrightRaider
never touches the other tool, it just warns you.

### Process Optimizer (Pro, opt-in)

For a detected foreground game, BrightRaider can raise Windows process priority
and pin it to physical cores (Hyperthreading off) for smoother frametimes.
*Performance → High priority for the game* and *Physical cores only*; both off by
default; reverts on Alt-Tab out / game exit.

### Only run hotkeys while a game is focused

Optional switch (*All keys → Keys only while a game is in front*, also in the
setup). When on, every hotkey passes
straight through on the desktop or in other apps, and reactivates automatically
when a fullscreen game is in the foreground. Default off — leave it off for
windowed-mode play. The one exception is the Global ON/OFF key when it sits on
the numpad (the default): it keeps working everywhere. If you move it to an
ordinary key, it follows this switch too.

### Global ON/OFF (Numpad Del — Free)

The key and an on/off switch sit on *App & License → Hotkeys*. Two flavours,
picked under *App & License → Advanced → Hotkeys off also resets the display*:

- **On (default) = FULL OFF** — hides every overlay, restores your original
  gamma/vibrance/hue, pauses Auto-Brightness, stops the Footstep Booster. The
  display behaves as if BrightRaider weren't running. Press again to re-apply.
- **Off = HOTKEY PAUSE** — only the hotkeys pause; your colour profile,
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
  default inventory drag. Off in normal use. It uses synthetic input, which
  anti-cheat may flag — use at your own risk. The launcher shows this for 8 seconds
  before it starts.

Each launcher just sets an environment variable before starting the app, so you
can open one in Notepad to see exactly what it does. You never need these for
normal use — running `BrightRaider.exe` on its own is the standard way.

BrightRaider self-elevates once on first launch to set the gamma registry key —
that is automatic and needs no launcher.

### Notes

- Numpad keys are captured while the tool runs; regular number keys are not affected.
- Vibrance (NVIDIA) / Saturation (AMD) changes show in the GPU control panel;
  Gamma/Contrast changes do not (different API — this is normal).
- *Games & FPS* says so if your GPU's driver cannot be reached (then gamma and
  contrast work, vibrance and FPS limits don't).
- If something looks wrong: just exit the app — everything resets automatically.

### Uninstall

1. Turn off *App & License → Start with Windows*, then exit BrightRaider
   (tray → Exit)
2. Delete `BrightRaider.exe`
3. Delete the folders `%LOCALAPPDATA%\BrightRaider` (downloaded modules) and
   `%APPDATA%\BrightRaider` (settings, license, log)
4. *Optional:* remove `HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\ICM →
   GdiIcmGammaRange` (the one-time gamma setting)
5. *Optional:* remove `HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Run →
   BrightRaider` if "Start with Windows" was still on

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
keine Installation.** Ein kurzer Einrichtungsassistent führt beim ersten Start
durch alles.

> **Neu in V1.3 — BrightRaider wird erwachsen.** Der Kern ist ein Tool für
> **jedes PC-Spiel**: Farben, FPS, Ton, Fadenkreuz, Autorun. Die
> ARC-Raiders-Helfer (Map Scanner, Autoscrapper, QuickSelect, QuickSave) sind
> sein erstes **Modul**, mit eigener Seite und einem Schalter: *Einstellungen →
> Modules → Arc Raiders*. Jede Einstellungsseite hat einen **Guide** daneben. Der
> Map Scanner liest per Daten-Pack auch **andere Spiele** (siehe
> `pack-authoring.md`).

### Kostenlose Funktionen

- 3 umschaltbare Display-Profile über Numpad 1/2/3 (oder Pfeiltasten)
- Ändert Gamma, Kontrast und Digitale Farbanpassung/Sättigung
- Unterstützt NVIDIA (NvAPI) und AMD (ADL SDK); automatische GPU-Erkennung
- Läuft auch auf Intel / anderen GPUs (nur Gamma + Kontrast)
- Einzelne Monitore oder alle gleichzeitig steuerbar
- Alt-Tab Auto-Switch — Profil + FPS-Limit automatisch pro Spiel
- Auto-HDR pro Spiel — Windows-HDR automatisch an/aus pro Spiel
- Andere Bildschirme abdunkeln — dunkelt deine anderen Bildschirme, solange das Spiel vorne ist
- **G-SYNC-/FreeSync-Einrichtung** nach Blur Busters — V-SYNC, niedrige Latenz
  und das passende FPS-Limit im Treiber, mit exaktem Rückgängig (neu in V1.3)
- **Einrichtungsassistent** und ein **Guide** neben jeder Einstellungsseite (neu in V1.3)
- Funktioniert auch mit gedrückter Shift-Taste
- Benachrichtigungen ein/ausschaltbar und auf **dem Bildschirm deiner Wahl**
  (*App & License → Notifications*), **farbkodiert**: grün = gute Nachricht
  (Update verfügbar), gelb = Warnung, rot = braucht dich. Die QuickSelect- /
  QuickSave-Bestätigungen haben einen eigenen Schalter auf der Arc-Raiders-Seite,
  damit diese Makros lautlos laufen, während andere Meldungen anbleiben
- Farbkonflikt-Warnung — Hinweis, wenn ein anderes Tool (f.lux, Night Light, …)
  um deine Display-Farben kämpft
- Englische Oberfläche (Deutsch folgt in einer späteren Version)
- Autostart mit Windows · alle Einstellungen automatisch gespeichert
- Setzt beim Beenden alles auf Normal zurück
- Pausen-Erinnerung — oranger Toast nach einstellbarem Intervall; stört nie das Spiel

### Pro-Funktionen (5,49 € einmalig)

Jede Pro-Seite lässt sich auch ohne Pro öffnen und einrichten — im Spiel läuft
das Werkzeug, sobald Pro aktiv ist.

- **Autorun** — CapsLock kurz drücken, um W automatisch zu halten. Sprint-Modus
  startet direkt im Sprint; Tap-Modus (CapsLock 600 ms halten) tippt W in
  Intervallen. Timing voll einstellbar, und pro Spiel eigene Sprint-Taste,
  Halten oder Umschalten und eigenes Tap-Timing. Eigene Seite **Movement**.
- **Audio Ducking** — Stumm-Taste 600 ms halten, um Spiel-Audio auf einen
  Prozentsatz zu reduzieren. Kurzer Druck schaltet wie gewohnt stumm/laut.
- **Footstep Booster / Loudness Limiter** — begrenzt die Spiel-Audio-Session,
  damit du Schritte hörst, ohne dass Schüsse dir das Trommelfell zerlegen
  (siehe `FootstepBooster_Guide.md`).
- **Map Scanner** — M auf der Karte halten, um alle Evakuierungs-Timer lokal
  auszulesen, mit farbkodiertem Live-Overlay.
- **Evac-Alarm** — roter Toast + Sound, wenn ein Timer unter den Schwellwert fällt.
- **Autoscrapper** — F5 bei offenem Lager: liest das ganze Lager und verschrottet
  oder verkauft nach deinen Regeln — standardmäßig erst, nachdem du die Liste
  bestätigt hast.
- **Mitspieler stummschalten** — eine Taste schaltet deine Voice-Chat-Apps
  (Discord, TeamSpeak …) stumm, mit Stumm-Symbolen auf dem Bildschirm.
- **Crosshair-Overlay** — Click-through-Fadenkreuz, 6 Stile, freie Farbe, Größe
  (1–50 px), optionaler Umriss; pro Spiel gemerkt (neu in V1.4).
- **Audio-Ausgabe-Umschalter** — Standard-Ausgabegerät per Hotkey durchwechseln;
  optional automatisch beim Spielstart/-ende.
- **Bis zu 9 Profile** (Numpad 1–9) mit vollem Editor (Gamma, Kontrast, Vibrance,
  Hue — auf NVIDIA läuft der Hue-Regler 0–359° wie im Control Panel).
- **Auto-Helligkeit** — Bildschirmanalyse erkennt dunkle Szenen und wechselt das Profil.
- **Spiel stumm**, **Background Mute**, **Process Optimizer**, **QuickSelect**,
  **QuickSave** und eine **Offline-Lizenz** (nach Aktivierung kein Internet
  nötig).

### Profile (Standard)

| Taste  | Name    | Gamma | Kontrast | Vibrance |
|--------|---------|-------|----------|----------|
| Num 1  | Normal  | 1.0   | 100 %    | 50 %     |
| Num 2  | Hell    | 1.5   | 110 %    | 60 %     |
| Num 3  | Heller  | 2.0   | 110 %    | 70 %     |
| Num 4–9 | Eigen  | —     | —        | Pro-Nutzer ändern alle Werte |

### Systemvoraussetzungen

- Windows 10 oder 11 (x64)
- Nichts zu installieren — eine EXE unter 6 MB; das Arc-Raiders-Modul kommt,
  wenn du es einschaltest
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

### Einrichtung und Einstellungsfenster (neu in V1.3)

**Der Einrichtungsassistent** öffnet beim ersten Start — und **einmal nach dem
Update auf 1.4** von V1.3 oder älter. Jede Seite zeigt schon, was du hast:
**Next** behält es, nur was du änderst, wird geändert. Seiten, die seit deiner
Version neu sind, tragen *New in 1.3* oder *New in 1.4*; die letzte Seite zeigt,
was sich geändert hat und was blieb. Jede Seite passt auf den Bildschirm, nichts
zu scrollen. Schließen behält alles, wie es ist. Er öffnet nie über einem laufenden Spiel. Jederzeit wieder: *App & License →
Run the setup…* (dort wählst du auch ein anderes Tastaturlayout).

**Das Einstellungsfenster** (Tray → *Settings…*):

- Eine **Seitenleiste**: *App & License*, dann **Display** (Colour profiles,
  Games & FPS, Auto-Brightness), **Tools** (Audio, Crosshair, Movement,
  Performance) und **Modules** (Arc Raiders mit Map Scanner, Autoscrapper,
  QuickSelect, QuickSave). ☰ klappt sie auf Symbole zusammen; der Pfeil neben
  einem Modul klappt seine Seiten ein.
- **Jede Taste sitzt auf der Seite ihrer Funktion** — die Profiltaste beim
  Profil, die Stumm-Taste unter Audio. **All keys** (unten in der Seitenleiste)
  zeigt alle auf einen Blick, mit Konflikten.
- **Doppelt belegte Tasten** — BrightRaider prüft beim Start und nach jeder
  Änderung seine eigenen Tasten und die aller Module. Eine doppelte Taste meldet
  es einmal und zeigt sie im Tray (*A key is bound twice*), bis du sie änderst;
  All keys nennt das Paar ganz oben. Tasten zweier verschiedener Spiele zählen
  nicht — jede wirkt nur in ihrem Spiel. Vergibst du eine schon belegte Taste
  (in den Einstellungen oder im Assistenten), wandert sie dorthin: die andere hat
  dann keine, und du bekommst einen Hinweis (*Cancel* in den Einstellungen oder
  den Assistenten ohne *Finish* schließen macht es rückgängig).
  Tasten, die nur beschreiben, wie dein Spiel belegt ist (Kartentaste, Sprint),
  bleiben.
- **Guide** auf jeder Seite öffnet daneben eine kurze Anleitung: was passiert, wie
  man es einrichtet, was hilft, wenn etwas nicht stimmt.
- Selten Gebrauchtes ist unter **Advanced: …** eingeklappt; was eine Funktion
  zum Laufen bringt, bleibt sichtbar.
- **Pro-Seiten** lassen sich ohne Pro einrichten; eine ruhige Zeile oben sagt,
  dass das Werkzeug im Spiel läuft, sobald Pro aktiv ist.
- **Suche** (Strg+F, oben in der Seitenleiste) springt zu jeder Einstellung.
  **Größe** (*App & License*, auch Strg + / −) macht alles 70–150 % groß; im
  breiten Fenster stehen die Karten nebeneinander (neu in V1.4).
- **Report a problem…** (*App & License*) stellt Version, System und das Ende
  des Logs zusammen und öffnet ein neues Issue, das du prüfst und abschickst
  (neu in V1.4).

**Module** (neu in V1.4): Die Arc-Raiders-Werkzeuge sind ein Modul. Im
Einrichtungsassistenten oder unter *Modules → Arc Raiders* einschalten, und
BrightRaider lädt es herunter (8 MB) nach `%LOCALAPPDATA%\BrightRaider\modules`
und lädt es — nur mit BrightRaiders Signatur. Gibt es ein neueres
Arc-Raiders-Modul, bieten es eine Meldung und die Tray-Zeile **Install Arc
Raiders …** an: Ein Klick lädt nur das Modul, BrightRaider startet neu, sobald
kein Spiel vorne ist. Ohne deinen Klick wird nichts installiert. Dein
Pro-Schlüssel behält alles, was er freischaltet, Arc Raiders eingeschlossen.

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
  auf Shift liegt. `Strg+`/`Alt+` wählen oder Sprint umbelegen (Seite Movement).

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
| (nicht belegt)   | Mitspieler stumm/laut — Voice-Chat (Pro)   |
| (nicht belegt)   | Andere Bildschirme abdunkeln aus/an (Kostenlos) |

Ohne Pro nutzt BrightRaider die Tasten der Pro-Funktionen nicht: Sie gehen
unverändert an das Fenster, das vorne ist, in jedem Programm. Dasselbe gilt für
die Arc-Raiders-Tasten, solange das Modul aus ist. (QuickSelect- und
QuickSave-Tasten wirken nur im Spiel; dort sieht Free weiter den Pro-Hinweis.)

Funktioniert mit/ohne Shift, NumLock an oder aus. **TKL-/Laptop-Preset** legt
Profil 1–3 auf die Pfeiltasten ← ↓ →, Stumm auf ↑, das Fadenkreuz auf Einfg,
Global EIN/AUS auf Bild↓ und den Geräte-Wechsel auf Bild↑. **AZERTY-Preset**
nutzt Z als Vorwärts-Taste. Anderes Layout jederzeit: *App & License → Run the
setup…* → Keyboard.

### Pro-Aktivierung

1. Lizenz auf der BrightRaider-Website kaufen
2. Du erhältst eine E-Mail mit deinem Lizenzschlüssel
3. *App & License → Activate Pro license…* — oder **I have a key** auf jeder
   Pro-Seite, oder Tray → **Enter License…**
4. Schlüssel einfügen — Pro ist sofort freigeschaltet und lokal gespeichert; ein
   kurzes Willkommen zeigt, was jetzt dir gehört, jedes Werkzeug mit einem Weg
   direkt zu seiner Seite
5. **Neuer PC?** Über *App & License → Deactivate on this PC* gibst du
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

Autorun endet von selbst, sobald ein anderes Programm in den Vordergrund kommt,
denn seine Tasten gehen an das vorderste Fenster.

- **Sprint-Modus** (*Movement → Sprint while running*) startet sofort im Sprint.
- **Tap-Modus** (CapsLock 600 ms halten) tippt W: kurz gedrückt (Standard
  **160 ms**), dann losgelassen (Standard **1300 ms**). Gebaut fürs **Looting
  Mk. 3 (Survivor)** Augment — in der Pause steht dein Charakter still und
  regeneriert, der kurze Tipp hält dich am Kriechen. Konfiguration:
  *Movement → Advanced: tap mode timing*.

  > **Neu abgestimmt in V1.1.** Ein aktueller Arc-Raiders-Patch hat das Augment
  > generft, und mit den alten 990 / 169 kam man nicht mehr voran. Mit
  > 1300 / 160 bewegt man sich wieder *und* gewinnt Leben dazu. **Upgrade von
  > V1.0?** Deine gespeicherten Werte bleiben erhalten — für das neue Paar
  > trage sie unter *Movement → Advanced: tap mode timing* von Hand ein. Der Nerf ist
  > trotzdem hart: Nimm unbedingt **Crawl Before You Walk** (gelber Skill-Baum,
  > oben in der Mitte) — damit kriecht man deutlich schneller.
- **Pro Spiel** (*Movement → Advanced: one game works differently*): Ein Spiel
  kann eigene Vorwärts- und Sprint-Tasten bekommen, Sprint als gehaltene Taste
  oder als Umschalter, Start im Sprint oder im Gehen und ein eigenes Tap-Timing.
  Es ist dieselbe Spieleliste wie auf Games & FPS; hat ein Spiel eigene Werte,
  klappt der Abschnitt von selbst auf.
- **AZERTY:** *Forward* unter *Movement → Your game's keys* auf Z setzen (oder
  im Einrichtungsassistenten AZERTY wählen).

### Audio Ducking (Pro)

Die Stumm-Taste hat zwei Modi: **kurzer Druck** = stumm/laut; **Halten** (Standard
600 ms) = Spiel-Audio auf einen Prozentsatz reduzieren (Standard 20 %), damit du
Discord oder Musik hörst. Es bleibt leise, bis du die Taste erneut hältst. Lautstärke unter *Audio → Game volume while held*
(0–100 %), Haltezeit unter *Audio → Advanced* (200–2000 ms).

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
Zurückwechseln auf. Per-Prozess — Musik + Discord laufen weiter. *Audio → Mute
when you Alt-Tab out*.

### Mitspieler stummschalten (Pro)

Eine Taste schaltet deinen Voice-Chat während des Spiels stumm und wieder laut.
Belegen auf der Seite *Audio* (*Mute teammates key*); welche Apps stumm
geschaltet werden, steht direkt darunter in der Liste. Neuinstallationen starten
mit Discord, TeamSpeak 3 und dem neuen TeamSpeak, Mumble, Teams und Zoom;
**+ Add app** wählt ein laufendes Programm, **Remove** nimmt eines heraus,
**Add the usual** stellt die üblichen wieder her. Apps, die nicht laufen, werden übersprungen. Standardmäßig nicht belegt.
Angefasst werden nur die gelisteten Apps (alle Instanzen davon) — nicht der
Voice-Chat im Spiel selbst.

Die Stummschaltung bleibt, bis du die Taste erneut drückst. Sie endet auch von
selbst, wenn das Spiel geschlossen wird oder du BrightRaider mit der Global
EIN/AUS-Taste ausschaltest, und das Tray-Menü hat **Unmute teammates**, solange
sie stumm sind. Endet BrightRaider oder stürzt ab, während sie stumm sind, werden
sie beim nächsten Start wieder laut — Windows würde sich die Stummschaltung sonst
merken. Eine App, die dann noch nicht läuft (Discord startet nach BrightRaider),
wird laut geschaltet, sobald sie auftaucht.

### Stumm-Symbol (Pro)

Solange das Spiel mit der Stumm-Taste stumm ist, zeigt ein kleines
durchgestrichenes Lautsprecher-Symbol oben links auf dem Spielbildschirm, warum es
still ist. Solange *Mute teammates* deinen Voice-Chat stumm hat, erscheint
daneben ein durchgestrichenes Headset; beide können gleichzeitig zu sehen sein.
Das Symbol nimmt nie Klicks oder den Fokus und verschwindet, wenn du das Spiel
verlässt. Es erscheint, wenn das Spiel randlos (Vollbild im Fenster) läuft; im
*exklusiven* Vollbild zeichnet Windows keine anderen Fenster über das Spiel —
dafür das Spiel auf randlos stellen. Standardmäßig an; abschalten unter
*Audio → Mute icons on screen*. Die Taste liest jedes Mal den echten
Stumm-Zustand des Spiels, ein von anderer Stelle stumm geschaltetes Spiel wird
also mit einem Druck wieder laut.

### Audio-Ausgabe-Umschalter (Pro)

Wechselt dein Standard-Ausgabegerät im Betrieb (*Audio → Switch output
device*). Gewünschte Geräte einschalten, dann per Hotkey durchwechseln (ein Toast
zeigt das Gerät). Optional stellt **Switch by itself while a game runs** beim
Spiel-Ende das vorherige Gerät wieder her. Der Voice-Chat (Discord …) bleibt auf
seinem Gerät, außer du schaltest *Audio → Advanced → Voice chat follows the
device switch* ein — mit TeamSpeak aus lassen.

### Map Scanner (Pro)

**M** auf der Karte halten — BrightRaider zoomt heraus, liest alle
Evakuierungs-Timer lokal auf deinem PC aus und zeigt einen Live-Countdown.

**Unterstützte ARC-Raiders-Karten:** Buried City, Stella Montis, Space Port, Blue
Gate, Dam, Riven Tides, Pendola Pass (Gondel-Punkte; geschlossene zeigen
**CLOSED**). **Andere Spiele** per Daten-Pack (`pack-authoring.md`).

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
Hatches zu) und das Event **ARC Frigate** (alle Evakuierungspunkte offen). Der
Name erscheint im Overlay.

**Eigene Namen für die Ausgänge** (neu in V1.4): *Advanced: your own names for
the exits* listet jeden Ausgang jeder Karte. Schreib hinein, wie du ihn nennst —
ein Wahrzeichen, ein Gebäude —, und Overlay und Evac-Alarm nehmen deinen Namen;
ein leeres Feld behält den Namen aus dem Spiel.

Ist beim Halten von M gar keine Karte offen (ein Menü, der XP-Bildschirm), meldet
der Scanner *no map detected*, statt zu raten.

Konfiguration unter *Modules → Arc Raiders → Map Scanner*: Farben und
Overlay-Position vorne; Hintergrund, Schriftgröße, Haltedauer, Kartentaste und
Alarm-Wiederholung unter *Advanced*.

### Evac-Alarm (Pro)

Meldet, wenn ein Timer unter den Schwellwert fällt: roter Toast + Sound, einmal
pro Timer, neu scharf beim erneuten Scan. Einschalten und Schwellwert auf der
Map-Scanner-Seite (*Evac alarm*). Map Scanner muss aktiv sein.

### QuickSelect (Pro)

Eine Taste drücken, um ein Schnellrad-Item automatisch zu nutzen. BrightRaider
führt die Sequenz aus: Q halten → Slot wählen → Q loslassen → LMB für die
eingestellte Zeit halten → optional H für freie Hand.

**Einrichten:** *Modules → Arc Raiders → QuickSelect* → einschalten → einmal im
Block **Setup** deine Spieltasten eintragen → pro Item: Trigger-Taste, Slot
(1–10), **Haltezeit in ms**, an. *Timing reference* zeigt die Zeiten unten,
*Slot layout* zeigt, welcher Slot welcher ist, *All columns* zeigt die Aktion
nach der Nutzung.

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
Übergangszustand) · kein Bestätigungs-Popup gewünscht? *Confirm QuickSelect and
QuickSave* auf der Arc-Raiders-Seite ausschalten (die Makros laufen weiter, nur
lautlos).

### QuickSave (Pro)

Zieht Items zwischen Inventar, Sicherheitstasche und **Rucksack** — mit einem
Tastendruck oder Mausradscrollen.

- **Was sich verschieben lässt:** Inventar-Slots 1–8, Waffen, Sicherheitstasche
  1–3 und **Rucksack 1–4** (oberste Reihe) — neu in V1.1. Richtung frei einstellbar.
- **So funktioniert es:** Inventar öffnen (Tab) → Cursor zum Quell-Slot → LMB
  halten und zum Ziel ziehen → loslassen → Inventar schließen (Tab).
- **Wohin es zielt:** auf das Raid-Inventar, wie in V1.0 — auch bei 4:3 und
  16:10, wo es in der Mitte sitzt. Kein Nachschauen, kein Cursor-Sprung. In der
  Basis sitzt das Inventar woanders, QuickSave ist also für den Raid.
- **Richtung umkehren (⇄):** erster Druck Von→Nach, zweiter Nach→Von. Ein Druck,
  während noch ein Zug läuft, wird ignoriert und dreht die Richtung nicht um.
- **Timing** (falls Drag fehlschlägt, *QuickSave → Advanced: inventory key and
  timings*): Öffnen (250 ms), Hover (0 ms), Ziehen (0 ms), Ablegen (120 ms),
  Cooldown (0 ms).
- **An/aus:** Numpad / (Numpad) oder Ende (Pfeiltasten).

> **Bekannte Einschränkung:** gelegentliche Drag-Fehler kommen vom Unreal-Engine-
> Slate-UI, das ARC Raiders nutzt — dasselbe passiert beim manuellen Ziehen. Von
> außen nicht behebbar; einfach nochmal auslösen.

### Autoscrapper (Pro)

Liest dein Lager vom Bildschirm und verschrottet oder verkauft, was deine Regeln
sagen — standardmäßig, nachdem du die Liste gesehen und bestätigt hast („Run
without the review" überspringt das). Er sendet simulierte Klicks,
Cursorbewegungen und Scrollen ans Spiel — die am stärksten exponierte
Eingabe-Funktion, siehe den Anti-Cheat-Abschnitt der README. Ausführlich:
[Autoscrapper Guide (EN)](Autoscrapper_Guide.md).

- **Regeln:** *Modules → Arc Raiders → Autoscrapper* — ein Gegenstand (oder eine ganze
  Kategorie), wie viele bleiben und was mit dem Rest passiert: liegen lassen,
  verschrotten oder verkaufen. Was nicht in der Liste steht, wird nie angefasst.
- **Start:** Lager im Spiel öffnen und **F5** drücken. Maus in Ruhe lassen,
  solange gescrollt wird (~10 s für 300 Plätze).
- **Prüfen:** Die Liste erscheint in Lager-Reihenfolge. **Enter** führt aus,
  **Esc** ändert nichts; was du dort einstellst, wird als Regel gespeichert.
  **Esc** gedrückt halten bricht einen laufenden Durchgang ab.
- **Testlauf** ist voreingestellt: geht bis zur Bestätigungsbox des Spiels und
  bricht dort ab. Auf **Live** umstellen, wenn alles passt.
- **Unerkannte Kacheln** bleiben unbenannt und unangetastet. Einmal über
  *Name unrecognised items…* benennen, ab dem nächsten Scan sind sie bekannt.

### Crosshair-Overlay (Pro)

Ein transparentes, click-through Fadenkreuz auf dem Bildschirm (Vollbild-tauglich;
gleicher Mechanismus wie Discord / GeForce Experience). Umschalten mit Numpad +
(oder Einfg). 6 Stile, freie Farbe und Größe (1–50 px), optionaler Umriss und
*Aim pixel* für Spiele, die einen Pixel links oben zielen. *Einstellungen →
Crosshair*.

**Pro Spiel** (neu in V1.4): Jedes Overlay über einem Spiel kann ihm G-SYNC
kosten, deshalb merkt sich BrightRaider das Fadenkreuz pro Spiel — in einem
Spiel eingeschaltet, bleibt es in den anderen aus, bis du es dort auch
einschaltest.

### Auto-Helligkeit (Pro)

Analysiert 5 Bildschirmzonen (Mitte gewichtet, 4 Ecken) per Median-Helligkeit und
wechselt sanft die Profile — dunkler Bildschirm = mehr Boost. Einschalten auf
der Seite *Auto-Brightness* (oder im Tray). *Set up the ranges → Start…*
kalibriert in zwei Schritten (dunkelste, dann hellste Stelle); das Fenster sitzt
unter allen Zonen, um das Ergebnis nicht zu verfälschen. Zonen-Gewichte (0–10)
unter *Advanced: screen zones and debug view*. Komplette
Anleitung in `AutoBrightness_Guide.md`.

### Andere Bildschirme abdunkeln (Kostenlos)

*Colour profiles → Screens → Dim other screens.* Solange ein Spiel vorne ist, wird
jeder Bildschirm außer dem, auf dem das Spiel läuft, um den eingestellten Wert
abgedunkelt — ein heller Zweitmonitor zieht den Blick nicht mehr ab; beim Alt-Tab
ist er sofort wieder normal. *Tipp:* so einstellen, dass du den anderen
Bildschirm noch lesen kannst, er aber deutlich ruhiger ist.
Film auf dem anderen Bildschirm? Die *Dimming on/off key* direkt darunter belegen:
Ein Druck holt die anderen Bildschirme zurück, der nächste dunkelt sie wieder ab
— nur für die laufende Sitzung. *Test on screen* zeigt die Wirkung ohne Spiel.
0 = aus. Mit nur einem Bildschirm gibt es nichts abzudunkeln, der Regler ist
dann ausgegraut. Ganz dunkel (bis ein Zehntel) geht nur mit dem erweiterten
Gamma-Bereich (die einmalige Windows-Freigabe, *Full brightness range → Unlock*
auf derselben Seite, danach Windows einmal neu starten); ohne ihn endet der Regler bei etwa
60 %.

Wird BrightRaider hart beendet oder stürzt ab, während andere Bildschirme
abgedunkelt sind, stellt ein kleiner Hilfsprozess (`BrightRaider.exe
--watchdog`, startet mit dem Abdunkeln und endet mit BrightRaider) sie innerhalb
von ein, zwei Sekunden zurück — ebenso ein FPS-Limit, das BrightRaider im
NVIDIA-Treiber gesetzt hatte. Deshalb kann BrightRaider im Task-Manager zweimal
erscheinen.

### Alt-Tab Auto-Switch (Kostenlos)

Pro Spiel automatisch Profil + FPS-Limit + Fadenkreuz, sobald das Spiel in den
Vordergrund kommt — Schluss mit manuellem Wechsel oder parallelem VibranceGUI.

1. *Games & FPS* → **Switch automatically** an → **+ Add game**
2. Optional pro Spiel: *Own colours for this game* und ein eigenes FPS-Limit
   (*FPS limit for all games* gilt für den Rest)
3. Spiel starten — Profil, Fadenkreuz und FPS-Limit greifen automatisch
4. Alt-Tab raus — Original-Ramps + Vibrance + Hue wiederhergestellt, Fadenkreuz weg

**FPS-Limit pro Spiel:** NVIDIA über NvAPI DRS (kein DirectX-Hook), AMD über ADLX
FRTC (`amdadlx64.dll`), Intel hat keine öffentliche API (UI zeigt Hinweis).
BrightRaider setzt das Limit, sobald das Spiel startet, und stellt beim
Spiel-Ende genau das zurück, was es geändert hat; ein Limit, das du selbst in der
NVIDIA-Systemsteuerung gesetzt hast, bleibt unberührt. Der Treiber liest das
Limit beim Spielstart — **nach dem Hinzufügen oder Ändern das Spiel einmal neu
starten.**

### G-SYNC-/FreeSync-Einrichtung (Kostenlos) — neu in V1.3

Die Variable-Refresh-Einrichtung, die die Experten empfehlen ([Blur Busters —
G-SYNC 101](https://blurbusters.com/gsync/gsync101-input-lag-tests-and-settings/14/)),
im Treiber für dich erledigt — im Einrichtungsassistenten oder unter *Games & FPS
→ Advanced: G-SYNC / FreeSync setup*.

- **NVIDIA:** V-SYNC im Treiber an, Low Latency Mode an und ein FPS-Limit knapp
  unter deiner Bildwiederholrate (144 Hz → 138, 180 Hz → 171, 240 Hz → 224).
  G-SYNC selbst schaltest du in der NVIDIA-Systemsteuerung ein (*G-SYNC
  einrichten*).
- **AMD:** FreeSync an, Wait for Vertical Refresh immer an, Anti-Lag an und
  dasselbe Limit.
- **Dann V-SYNC in deinen Spielen ausschalten** — das übernimmt jetzt der Treiber.
- **Kompetitive Spiele** (Counter-Strike 2, Valorant, Fortnite, Apex Legends,
  Overwatch 2, Rainbow Six Siege, League of Legends) lassen sich auf NVIDIA
  ausnehmen: V-SYNC aus und kein Limit, für die geringste Verzögerung. Dafür muss
  das Spiel in der Liste des Treibers stehen (die meisten tun es); sonst in der
  NVIDIA-Systemsteuerung anlegen (*3D-Einstellungen verwalten →
  Programmeinstellungen*) und neu einrichten.
- **Undo** stellt genau die Werte zurück, die vorher dastanden; einen Wert, den
  du inzwischen selbst geändert hast, lässt es in Ruhe.
- BrightRaiders eigenes FPS-Limit für alle Spiele steht solange auf keins, damit
  nicht doppelt begrenzt wird.

### Auto-HDR pro Spiel (Kostenlos) — neu in V1.1

Schaltet Windows-HDR **an**, wenn ein gewähltes Spiel startet, und **aus**, wenn
es endet. Opt-in pro Spiel (*Games & FPS → HDR while it runs*). Alt-Tab
rein/raus schaltet HDR nie um — nur echter Spielstart/-ende, damit du nie den
1–2 s Schwarz-Flash mitten in der Session zahlst. War HDR schon an (deins), lässt
BrightRaider es in Ruhe. Braucht *Switch automatically*.

### Farbkonflikt-Warnung — neu in V1.1

Läuft ein anderes Tool, das deine Display-Farben steuert (f.lux, Iris, Windows
Night Light, …), sagt BrightRaider es dir beim Start und auf der Seite Colour
profiles. Diese
Tools und BrightRaider kämpfen um dieselbe Gamma-Pipeline — die häufigste Ursache
für *„meine Farben springen immer zurück."* Nur-lesend: BrightRaider fasst das
andere Tool nie an, es warnt nur.

### Process Optimizer (Pro, opt-in)

Für ein erkanntes Vordergrund-Spiel kann BrightRaider die Windows-Prozess-
Priorität anheben und auf physische Kerne pinnen (Hyperthreading aus) —
glattere Frametimes. *Performance → High priority for the game* und *Physical
cores only*; beide aus per Default; setzt
beim Alt-Tab raus / Spiel-Ende zurück.

### Nur Hotkeys bei Spiel-Fokus

Optionaler Schalter (*All keys → Keys only while a game is in front*, auch im
Einrichtungsassistenten). Wenn an, gehen alle Hotkeys auf dem Desktop / in
anderen Apps durch und reaktivieren automatisch, sobald ein Vollbild-Spiel im
Vordergrund ist. Default aus — für Fenster-Modus ausgelassen. Einzige Ausnahme
ist die Global-EIN/AUS-Taste, solange sie auf dem Ziffernblock liegt (Standard):
sie wirkt überall. Liegt sie auf einer normalen Taste, folgt sie dem Schalter
auch.

### Global EIN/AUS (Numpad Entf — Kostenlos)

Taste und Ein/Aus-Schalter sitzen auf *App & License → Hotkeys*. Zwei Modi,
einstellbar unter *App & License → Advanced → Hotkeys off also resets the
display*:

- **An (Default) = KOMPLETT AUS** — versteckt jedes Overlay, stellt deine
  Original-Gamma/-Vibrance/-Hue wieder her, pausiert Auto-Helligkeit, stoppt den
  Footstep Booster. Das Display verhält sich, als wäre BrightRaider nicht da.
  Nochmals drücken wendet das aktive Profil wieder an.
- **Aus = NUR HOTKEYS PAUSIEREN** — nur die Hotkeys pausieren; Farbprofil,
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
  QuickSaves Standard-Inventar-Drag nicht registriert. Im Normalbetrieb aus. Er
  nutzt synthetische Eingaben, die Anti-Cheat erkennen kann — Nutzung auf eigenes
  Risiko. Der Starter zeigt das 8 Sekunden lang vor dem Start.

Jeder Starter setzt nur eine Umgebungsvariable vor dem App-Start — du kannst einen
in Notepad öffnen, um genau zu sehen, was er tut. Für den Normalbetrieb brauchst
du sie nie — `BrightRaider.exe` allein ist der Standardweg.

BrightRaider eleviert sich beim ersten Start einmalig selbst für den
Gamma-Registry-Eintrag — das ist automatisch und braucht keinen Starter.

### Hinweise

- Numpad-Tasten sind belegt, solange das Tool läuft; die normalen Zahlentasten nicht.
- Vibrance (NVIDIA) / Sättigung (AMD) wird im GPU-Control-Panel angezeigt;
  Gamma/Kontrast nicht (andere API — das ist normal).
- *Games & FPS* sagt Bescheid, wenn der Grafiktreiber nicht erreichbar ist
  (dann gehen Gamma und Kontrast, Vibrance und FPS-Limit nicht).
- Falls etwas komisch aussieht: einfach die App beenden — alles wird automatisch
  zurückgesetzt.

### Deinstallation

1. *App & License → Start with Windows* ausschalten, dann BrightRaider beenden
   (Tray → Exit)
2. `BrightRaider.exe` löschen
3. Die Ordner `%LOCALAPPDATA%\BrightRaider` (geladene Module) und
   `%APPDATA%\BrightRaider` (Einstellungen, Lizenz, Log) löschen
4. *Optional:* `HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\ICM →
   GdiIcmGammaRange` entfernen (die einmalige Gamma-Einstellung)
5. *Optional:* `HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Run → BrightRaider`
   entfernen, falls „Start with Windows" noch an war

---

## Files / Dateien

| Path | Purpose |
|------|---------|
| `BrightRaider.exe` | Single EXE / Einzelne EXE |
| `%APPDATA%\BrightRaider\BrightRaider2.cfg` | Settings (V1.4, auto) / Einstellungen; `BrightRaider.cfg` = V1.3 and older, kept |
| `%LOCALAPPDATA%\BrightRaider\modules\` | Downloaded modules (Arc Raiders) / Geladene Module |
| `%APPDATA%\BrightRaider\BrightRaider.lic` | License / Lizenzdatei |
| `%APPDATA%\BrightRaider\debug.log` | Diagnostic log / Diagnose-Log |

**Guides (`docs/`):** `QuickSelect_Guide.md` · `QuickSave_Guide.md` ·
`MapScanner_Guide.md` · `AutoBrightness_Guide.md` · `FootstepBooster_Guide.md` ·
`pack-authoring.md` (add other games to the Map Scanner)
