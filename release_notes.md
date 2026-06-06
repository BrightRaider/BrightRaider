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
| Alt-Tab Auto-Switch (all color axes + FPS) | vibrance only | ✅ | ✅ |
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
- **QuickSelect** — per-slot **LMB hold time in milliseconds** (the old preset table is gone), per-slot Use / Q / Holster toggles, MB3/4/5 + scroll-wheel rebind, and a mouse-movement slot path for the mouse-only wheel positions (7–10).
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

> 💙 **Special thanks** to **@DBasic**, **@Tia-Nastacia** and **@dawuus** — your detailed bug reports, ideas and patient back-and-forth shaped this pre-release more than anything else. A huge part of what's fixed and improved below came directly from you three. Thank you.


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
- **"Only run hotkeys while a game is focused" + Settings window** — switching from the game into the BrightRaider Settings window now correctly counts as "not in game", so the gate releases the keys instead of staying stuck on. *(#58)*
- **Desktop no longer mistaken for a game** — the Windows desktop / taskbar (the full-screen shell windows) were sometimes classified as a fullscreen game when alt-tabbing around, which leaked your game profile onto the desktop and kept the hotkey gate "in-game". Shell windows are now excluded — this was the root cause behind the "binds/brightness don't turn off when the game loses focus" reports. *(#58)*
- **Settings layout fixes** — the QuickSelect tab no longer runs off the right edge (its cards stack instead of sitting side-by-side), QuickSave/QuickSelect checkbox columns are centered under their headers, the Audio sliders now shrink with the window instead of overlapping, the left tab bar is tighter, and the window has a sensible minimum size so tabs can't be squeezed into overlap. *(#55)*
- **Auto-Brightness center weight fixed** — the "Center" zone weight was being applied to the wrong screen zone, so turning it up didn't actually make the middle of the screen count more. Center now correctly counts double by default, as intended. *(#65)*
- **"Slot layout" help button** — a new button in the QuickSave and QuickSelect tabs opens the in-game loadout screen with the slot numbers, so you can match BrightRaider's slots to the game at a glance. *(#65)*
- **In-app thank-you** — the App tab now has a small thanks card crediting the testers who shaped this build. Grab the newest build and have a look. 💛
- **QuickSelect / QuickSave fixed on multi-monitor** — the wheel cursor and inventory drag now land on the monitor your game is actually on, instead of always targeting the primary screen. (Fullscreen / borderless; windowed play stays fullscreen-calibrated as before.)
- **QuickSelect "After" action relabeled** — the post-use option is now correctly labeled **Holster (H)** (it was mislabeled "Heal"); same key, clearer name. *(#62)*
- **QuickSelect — full rework for newcomers** *(#65)* — each slot now takes its **LMB hold time directly in ms** (the old "pick a preset T1–T6 + a separate preset table" is gone), the redundant "#" column is removed, the post-use dropdown shows **Holster (your actual key)**, there's a **⏱ Timing reference** button with recommended heal times, and the five common heals come pre-filled. Your existing timings are migrated automatically.
- **QuickSave — full rework for newcomers** *(#65)* — From/To are now simple **S1–S8 / W1–W2 / P1–P3** dropdowns (inventory slots / weapons / safe pockets) instead of raw numbers, the model covers the current 10-slot + 3-pocket loadout, and the trigger toast reads e.g. "W1 → P1". Existing presets are migrated automatically so they keep pointing at the same items.
- **"Slot layout" buttons** — QuickSave and QuickSelect each have a button that opens an annotated in-game screenshot showing exactly which label maps to which slot.
- **"📖 Guide" buttons** — QuickSave, QuickSelect, Auto-Brightness and Map Scanner each have a button that opens that feature's full guide in your browser.
- **Setup Wizard** — the forward key ("W") no longer shows a false "already bound" warning.
- **QuickSelect settings now persist reliably** — fixed a case where the per-slot timer could reset when reopening Settings.
- **Global ON/OFF simplified** — the extra "Global toggle OFF also resets the display" checkbox is gone. Turning BrightRaider off now always means *fully* off (overlays hidden, original display restored) — "off is off". This was already the default, so nothing changes for almost everyone.
- **Multi-monitor overlays fixed** — when you pin a specific monitor in the tray, the crosshair and Map Scanner overlays now reliably stay on that monitor instead of occasionally following another window onto a second screen.
- **Map Scanner polish** — a map with no condition now just shows its name (no redundant "Normal" label), and condition-icon detection is more robust on non-16:9 resolutions (1920×1440, 2560×1600, etc.).
- **Crosshair + Map Scanner respect "only run hotkeys while a game is focused"** — with that option on, both the crosshair *and* the map-scanner overlay now hide whenever no game is in focus (e.g. on the desktop), independent of Alt-Tab Auto-Switch. Previously the crosshair could stay stuck on the desktop and the two overlays behaved inconsistently. *(#66)*
- **Break reminder — minutes *and* seconds** — the interval now has a seconds field too, so it doubles as a quick egg-timer (e.g. 0 min 30 sec). Existing settings are unchanged.
- **Stop the break reminder from the tray** — while it's running, the tray menu shows a one-click **⏹ Stop break reminder**. It only appears when the reminder is active, so it never clutters the menu. *(#66)*
- **Tray click toggles Settings** — left-clicking the tray icon now opens *and* closes the Settings window — a second click closes it instead of doing nothing.
- **QuickSave layout tidy-up** — small spacing fix so the Trigger and From columns no longer touch. *(#66)*
- **Evac alarm — now actually audible** — the alarm used the Windows system "Exclamation" sound, which is silent if you have that event set to "(None)". It now plays its own sharp synthesized tone, independent of your Windows sound scheme. *(#66)*
- **Evac alarm — repeat option** — the toast + beep can now repeat a configurable number of times a few seconds apart (Map Scanner tab), so an evac warning isn't missed mid-firefight. *(#66)*
- **Evac alarm — color-coded toast** — the alarm toast is now tinted to the timer's urgency, matching the map-scanner colors (orange as it nears, red under a minute). *(#66)*
- **Evac alarm — fixed alarms going quiet after the first** — an expired evac point could keep its label "pinned" and silently swallow every later alarm for that point this session; each point now re-arms correctly. *(#66)*
- **Fixed a stuck Shift key** — with Autorun running, BrightRaider swallows your physical Shift so only its sprint state reaches the game. Key-repeat could leak the Shift-down to Windows while the release was still swallowed, leaving Shift "held" system-wide — even after closing BrightRaider. Shift handling is now symmetric, so it can't latch.
- **Modifier + mouse bindings now work** — binding a modifier together with the wheel or a side button (e.g. "Ctrl + Wheel up", "Shift + MB4") was silently broken: the combo couldn't be captured, didn't install the mouse hook, and the modifier state was tracked unreliably. All three are fixed, so modifier+mouse bindings capture and fire reliably. *(#66)*
- **QuickSave toast shows a two-way arrow for toggle presets** — a preset with "toggle direction" on now reads e.g. "W1 ↔ P1", making it clear the trigger alternates both ways. *(#66)*
- **Heads-up when binding a modifier to QuickSave/QuickSelect** — those run a macro that sends its own keys, and a physically-held modifier (the game uses Shift = Sprint, Ctrl = Crouch) gets mixed into the macro and makes it misfire. The rebind now warns and suggests a plain key or a mouse side button instead. *(#66)*

## System requirements

- Windows 10 / 11 (64-bit)
- NVIDIA or AMD GPU recommended (Intel: gamma + contrast only, no vibrance/hue/FPS-limit)
- No .NET runtime install required (Native AOT — single EXE)

## SHA-256

```
BrightRaider.exe              D8199B9F589A89255F225C6A6AFA16C1909BE2E985E67240ED57D8436AF2067A
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
