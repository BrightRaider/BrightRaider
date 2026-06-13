# BrightRaider V1.0.0 — Pre-Release

> 🎉 **Pre-release intro price:** V1.0 is in its final testing phase. For as long as it's in pre-release it stays at the V9.x Pro price of **€5.49** — buy now and you keep that price forever. Once every open report is closed and V1.0 ships as the final build, the price moves to **€8.99** to reflect the new feature set (Footstep Booster, Background AutoMute, Audio Output Switcher, Process Optimizer, plus the upgraded Map Scanner / Auto-Brightness / QuickSelect / QuickSave).
>
> **V9.x license holders:** your existing key works on V1.0 at no extra cost — just re-enter it once (see upgrade note below). The price change does not affect you either way.

**Goodbye VibranceGUI. Hello one tool that does it all.**

BrightRaider V1.0 replaces VibranceGUI completely — and adds everything VibranceGUI never had: **per-game FPS limits, Hue control, Alt-Tab Auto-Switch**, and a real configurable hotkey system. **All free.**

The free tier alone makes it one of the most complete color + FPS gaming tools out there — Vibrance, Gamma, Contrast, Hue, per-game FPS limits and Alt-Tab Auto-Switch, no strings.

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
| **HDR toggle hotkey** | ❌ | ✅ | ✅ |
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
- **HDR toggle hotkey** — flip Windows HDR on/off with one key instead of digging through Windows Settings (especially painful on Windows 10). Works on the pinned monitor or all HDR-capable ones.
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
- **Audio Output Switcher** — switch your default output device with one hotkey, cycling through the devices you choose (2, a few, or all). Optional auto-switch to a chosen device when a game starts, restoring the previous device when it closes (alt-tabbing out does **not** switch back). Switches the game/media device by default; an opt-in *"Also switch the communications device"* makes Discord/voice follow too (off by default so it can't disrupt voice apps like TeamSpeak).

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


- **🆕 Audio Output Switcher (Pro)** — switch speakers ↔ headphones (or any set of devices) with one hotkey, plus optional auto-switch when a game starts/closes. Moves the game/media device by default; opt-in to also switch the communications device (see the refinement further down).
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
- **Keep your colours when toggling off (optional)** — the **"Global toggle OFF also resets the display"** checkbox (App tab, under "Hotkeys enabled") controls what Global On/Off does. On by default = "off is off" (turning off restores your normal display). **Uncheck it** to keep your colour profile applied while BrightRaider is off — only the hotkeys and overlays turn off, so your colours stay until you switch back on (the classic PageDown behaviour). *(#66)*
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
- **Evac alarm follows the Map Scanner** — the beep + toast no longer fire while the Map Scanner overlay is hidden (toggled off, BrightRaider globally off, alt-tabbed out of the game, or the game-focus gate). The alarm is part of the scanner, so it stays silent whenever the scanner isn't shown. *(#66)*
- **Fixed a stuck Shift key** — with Autorun running, BrightRaider swallows your physical Shift so only its sprint state reaches the game. Key-repeat could leak the Shift-down to Windows while the release was still swallowed, leaving Shift "held" system-wide — even after closing BrightRaider. Shift handling is now symmetric, so it can't latch.
- **Modifier + mouse bindings now work** — binding a modifier together with the wheel or a side button (e.g. "Ctrl + Wheel up", "Shift + MB4") was silently broken: the combo couldn't be captured, didn't install the mouse hook, and the modifier state was tracked unreliably. All three are fixed, so modifier+mouse bindings capture and fire reliably. *(#66)*
- **Global On/Off key no longer leaks to other apps** — the dedicated toggle key is now swallowed on every edge (press, auto-repeat, release) and works even with "only run hotkeys while a game is focused" on. Note: an app that captures keys globally on its own (e.g. TeamSpeak's hotkeys) can still see the same physical key — Windows lets two global captures coexist and one tool can't block another's. If your toggle key triggers something there (e.g. deleting a channel), rebind BrightRaider's toggle to another key, or change that app's hotkey. *(#66)*
- **Rebinding no longer fires the key you press** — while a rebind field is waiting for input, all hotkeys and macros are paused, so pressing a key that's already bound to (say) QuickSelect now captures it cleanly instead of firing the macro. *(#66)*
- **Ignore apps that aren't games** — BrightRaider treats any fullscreen window as a game so vibrance/FPS limits work without setup, which also caught things like fullscreen video in a browser. There's now an "Apps to ignore" list on the Game Profiles tab — anything on it is left completely alone (no overlays, no colour change, no FPS cap). Common browsers and media players are excluded out of the box, and you can add your own with one click. *(#66)*
- **Version line in Settings is now live** — the version at the bottom of the Settings window shows your version and links to the website. When a newer release is out it turns into a gentle orange "update available" notice and links straight to the download — no more wondering if you're current.
- **Right-click the version to copy full system info** — version, OS, your exact GPU model + driver version, FPS-limit support, and every monitor's resolution, scaling and orientation (portrait flagged) — all to the clipboard in one go. Turns a bug report into a one-click paste.
- **Lower-overhead Auto-Brightness sampling** — the screen sampler now reuses one GDI surface + buffer across all five zones instead of recreating them per zone every tick, cutting the per-sample allocation and GDI churn ~5× (the sampler runs several times a second while Auto-Brightness is on).
- **QuickSave toast shows a two-way arrow for toggle presets** — a preset with "toggle direction" on now reads e.g. "W1 ↔ P1", making it clear the trigger alternates both ways. *(#66)*
- **Heads-up when binding a modifier to QuickSave/QuickSelect** — those run a macro that sends its own keys, and a physically-held modifier (the game uses Shift = Sprint, Ctrl = Crouch) gets mixed into the macro and makes it misfire. The rebind now warns and suggests a plain key or a mouse side button instead. *(#66)*
- **Browse the Pro tabs before you buy** — the Pro tabs (Auto-Brightness, Audio, Crosshair, Performance, Map Scanner, QuickSelect, QuickSave) now open on Free too, so you can see exactly what each feature looks like. The controls stay greyed out with a short "activate a license to enable" note until you go Pro.
- **Free profile fixes** — profiles 4–9 can no longer be switched on without Pro (they're the Pro presets), and the **"Test on screen"** preview button on the Display tab now works on Free — flash a profile against neutral and see the effect instantly without launching a game.
- **Pro hotkeys behave consistently on Free** — pressing a Pro feature's hotkey (game mute, crosshair, map overlay, QuickSelect / QuickSave toggles) on the free version now shows the same "this is a Pro feature" notice as everything else, instead of silently doing nothing or quietly toggling the feature.
- **Audio device switch no longer crashes voice apps** — by default the switch now leaves your **communications** device alone and only moves game/media audio, so TeamSpeak (and other voice apps) no longer crash when you cycle devices. A new opt-in on the Audio tab — *"Also switch the communications device"* — restores the old "voice chat follows too" behaviour if you want it. Rapid hotkey presses are also handled cleanly now.
- **License validation hardening + reliability fixes** — under-the-hood robustness improvements to license handling.
- **No more input freeze on Alt-Tab (and no "stuck" mouse button)** — the keyboard/mouse hooks now run on their own thread, so switching in and out of the game can't briefly stall your input — and can't leave a mouse button latched (the "map drags / aim stays held after Alt-Tab" issue). The held-click macro path was hardened the same way.
- **Settings backup now actually recovers** — if the config file is ever corrupted (crash, power loss mid-write), BrightRaider now restores your settings from its automatic backup instead of silently resetting everything to defaults.
- **Newly connected monitors get their colors back** — a monitor plugged in after the first launch now correctly returns to its original gamma on Alt-Tab-out and on exit, instead of staying tinted until reboot.
- **Pinned-monitor startup fix** — with a specific monitor selected, vibrance/hue no longer briefly land on *all* monitors during startup.
- **Stability pass from a full code review** — additional hardening across input hooks, audio device switching, macro injection and the map scanner.
- **NEW: HDR toggle hotkey (Free)** — flip Windows HDR on/off with one key instead of digging through Windows Settings every time (especially painful on Windows 10). Bind it on the Hotkeys tab ("HDR on/off", unbound by default), works on the pinned monitor or all HDR-capable ones. The short black flash when it switches is Windows changing the display mode — your color profile is re-applied automatically right after.
- **Sharper icon at small sizes** — the app icon now stays recognizable in Task Manager, Explorer detail view and window title bars instead of collapsing into a dark blob at 16px.
- **Quick Select is reliable again while you're moving the mouse** — the wheel slots that have a number key (3–6: bandage / shield / adrenaline / grenade) are now selected by that key, like in 9.6.1, so moving the mouse mid-fight no longer pulls the selection onto the wrong slot. (Mouse-only wheel positions and AZERTY layouts use the cursor with a tighter hold.) Also fixes the wheel flicking open and closed on a stutter frame. (#66)
- **QuickSave labels now match the in-game inventory** — the From/To dropdowns, the trigger toast and the Slot-layout reference now read **Slot 1–5 / Augment 1–3** instead of S1–S8 — exactly the names the game uses. Existing presets are unaffected.
- **Crouch no longer blocked after taking over from Autorun** — pressing W during an Autorun sprint hands control over seamlessly as before, but the game no longer thinks Shift is still held afterwards (which silently blocked crouch until you tapped Shift once).
- **Slide and keep running with Autorun** — tapping C during an Autorun sprint now slides and continues the run, exactly like a manual "sprint, tap crouch" slide. Previously a slide always cancelled Autorun. (S, the Autorun key or pressing W still stop it as before.)
- **Number fields reject letters** — every timing / value field now ignores letters and other non-numeric keystrokes instead of accepting them and going blank.

## System requirements

- Windows 10 / 11 (64-bit)
- NVIDIA or AMD GPU recommended (Intel: gamma + contrast only, no vibrance/hue/FPS-limit)
- No .NET runtime install required (Native AOT — single EXE)

## SHA-256

```
BrightRaider.exe              6A4F28F9B98DD70C4EDB3A4AB4E264C0864B95D036105DBF76876DCFD88481EB
```

Verify on Windows: `Get-FileHash BrightRaider.exe -Algorithm SHA256`

---

## 📖 Documentation — read online or download

Read directly on GitHub (no download needed):

- 📘 **[Manual](https://github.com/BrightRaider/BrightRaider/blob/main/docs/Manual.txt)** — full reference (English + German)
- 🗺️ **[Map Scanner Guide](https://github.com/BrightRaider/BrightRaider/blob/main/docs/MapScanner_Guide.md)** — maps, events, threshold colors, alarm
- 🔆 **[Auto-Brightness Guide](https://github.com/BrightRaider/BrightRaider/blob/main/docs/AutoBrightness_Guide.md)** — Calibration Wizard walkthrough, zone weights
- 🔊 **[Footstep Booster Guide](https://github.com/BrightRaider/BrightRaider/blob/main/docs/FootstepBooster_Guide.md)** — threshold/attack/release tuning
- ⚡ **[QuickSelect Guide](https://github.com/BrightRaider/BrightRaider/blob/main/docs/QuickSelect_Guide.md)** — per-slot LMB hold time, modifier bindings
- 💾 **[QuickSave Guide](https://github.com/BrightRaider/BrightRaider/blob/main/docs/QuickSave_Guide.md)** — drag presets, toggle direction
- 📜 **[Changelog](https://github.com/BrightRaider/BrightRaider/blob/main/docs/CHANGELOG_PUBLIC.txt)** — public release history (V4 → V1.0)

## 📦 Download + install

**Download `BrightRaider.exe` and double-click it.** That's it.

On first launch the EXE unpacks its runtime into `%LOCALAPPDATA%\BrightRaider\` and starts the app from there. Subsequent launches skip unpacking and start near-instantly — feels like any single-EXE tool.

Want to fully uninstall? Delete the EXE and the folder `%LOCALAPPDATA%\BrightRaider\`.

Documentation lives online — see the Documentation section below for clickable links.
