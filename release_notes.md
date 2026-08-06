# BrightRaider V1.1.0

> ✅ **V1.1 is here — stable.** Everything carries over: settings, profiles and your license are all kept. **No re-activation needed.**
>
> Coming from V9.x? Your key still works at no extra cost — see the upgrade note below.

**One display tool — now for every game.**

V1.0 replaced VibranceGUI. V1.1 takes the next step: the display and FPS features work in **any** PC game, and the Arc Raiders toolbox becomes an **optional module** you switch on only if you want it. A fresh install starts as a pure display / FPS tool; the Map Scanner can even read **other games** through downloadable data packs. Free stays free, Pro is unchanged — still a one-time **€5.49**, no subscription, no time limit.

---

## ✨ New in V1.1

- 🎮 **A tool for any game** — the Arc Raiders features (Map Scanner, QuickSelect, QuickSave, Autorun) are now an optional module. Enable it in the Setup Wizard or *Settings → App*; existing users keep it on.
- 🗺️ **Map Scanner data packs** — add other games' maps, timers and conditions with a drop-in pack, read the same way as Arc Raiders.
- 🌗 **Per-game Auto-HDR** — turn Windows HDR on automatically when a chosen game launches, off again when it closes. Opt-in per game; Alt-Tab never flips it mid-session.
- 🏃 **New "Movement" tab** — Autorun now has its own home, split out from the general hotkeys.
- 🎒 **QuickSave backpack slots** — move items to and from your backpack top row (Backpack 1–4), not just the Safe Pocket.
- 🧭 **More map conditions** — Uncovered Caches and Hidden Bunker (Spaceport) are now recognised, with the right active-evac-point counts.
- 🔑 **Deactivate on this device** — free your license's activation slot for a new PC yourself, no support ticket needed.
- 🎨 **Color-conflict warning** — a heads-up when another tool (f.lux, Iris, Windows Night Light, …) is fighting over your display colours. The most common cause of *"my colours keep reverting."*
- 🔕 **Silence the macros** — a separate switch to mute just the QuickSelect / QuickSave confirmation toasts (*Settings → App*), so they run without a pop-up every time while your other notifications stay on.
- 🛡️ **Colour-coded notifications** — green = good news, yellow = warning, red = risky mode active.
- ⚡ **QuickSelect wheel** — pick the target slot from a plain "Slot 1–10" dropdown instead of typing a number.

### 🏃 Autorun tap mode — retuned for the Looting Mk. 3 nerf

A recent Arc Raiders patch weakened the Looting Mk. 3 (Survivor) augment, and the old **990 / 169 ms** timings stopped getting you anywhere. The new defaults are **1300 ms interval / 160 ms press**, which moves you forward *and* regenerates again.

**Already using BrightRaider?** Your saved timings are kept (settings are never overwritten), so set them by hand under *Settings → Movement → Tap Interval*. BrightRaider also points this out once after updating if you're still on the old pair.

The nerf is still harsh, so I'd strongly recommend taking **Crawl Before You Walk** (yellow skill tree, top middle) — crawling gets much, much faster with it.

---

## 🔧 Fixed in V1.1

Four full rounds of code review went into this release. The ones you're most likely to have hit:

- **Profiles 4–9 didn't stay enabled.** Turning one on didn't survive a restart — the setting was only ever written to disk when a profile was switched *OFF*.
- **QuickSave could stop working entirely** if you had a cooldown configured for a preset — the trigger did nothing, whatever you rebound it to. *(Found and fixed during the V1.1 pre-release; never shipped in a stable build.)*
- **Alt-Tab Auto-Switch checkbox on the Game Profiles tab was ignored on Apply** (the App tab's copy won), and toggling it from the tray while Settings was open got undone on Apply. Both work now.
- **Game mute, audio ducking and the Footstep Booster only looked at your default playback device.** If your game played on a different device — or BrightRaider's own switcher had just changed the default — they silently did nothing. They now find the game on any active output.
- **Audio ducking restored the game to 100 % volume** instead of the level you actually had. It puts your own volume back now.
- **The Footstep Booster and ducking fought each other** — with the Booster running, ducking was undone within a fraction of a second. They cooperate now.
- **The Process Optimizer now really does hand priority and CPU affinity back** when you Alt-Tab out or quit, as the manual has always described.
- **Hotkey conflicts with the HDR toggle and audio-device keys** gave no warning at all, and the clashing key then did nothing. Both are in the conflict check now.
- **The first-launch admin prompt could return on every start** on systems where another calibration tool had written its own value. One-time prompt again.
- **Auto-Brightness zone boxes were drawn in the wrong place** on displays with Windows scaling (125 % / 150 %). They line up now and follow along when you switch monitors.
- **A stray timer overlay** could flash back for a moment while BrightRaider was closing.
- **Deleting your last game profile while the game was running** left the audio device switched.
- **Much quieter debug log** — no more line every time any window on your PC gets focus.
- Plus many under-the-hood reliability fixes: safer restore of your FPS limit and display calibration on exit, macro timing that no longer trips over Windows clock changes, hardened handling of downloaded game packs, and various race fixes in hotkey handling, the evac countdown and startup/shutdown.

---

## ⬆️ Coming from V1.0?

Just download and run — your settings, profiles and license carry over untouched, no re-activation. The Arc Raiders module stays on for existing users.

## ⬆️ Coming from V9.x?

Settings, profiles, hotkeys and game profiles migrate automatically on first launch. Your license key needs to be **re-entered once** (*Settings → App → Enter license*) — Lemon Squeezy re-issues the activation instantly, same email, same key, no re-purchase. V1.x updates after that don't require another re-entry.

---

## 🛡️ If Windows Defender or SmartScreen flags it

BrightRaider is an unsigned single-EXE, and that format can trip Defender's heuristic / SmartScreen reputation — it's a **known false positive**, not a real detection (the same class every BrightRaider build has been in; Microsoft's analysis of the V1.0 build returned *"no positive detection"*, and this build has been submitted as well).

- Verify it yourself — **VirusTotal scan of this exact build:** https://www.virustotal.com/gui/file/173b296bdd1aca3d22f44b7f7ee5b93872ee2cea30ca5ad64dd24e26ca558e33
- If Windows blocks it: **More info → Run anyway**, or restore it from **Windows Security → Protection history**.

## System requirements

- Windows 10 / 11 (64-bit)
- NVIDIA or AMD GPU recommended (Intel: gamma + contrast only, no vibrance / hue / FPS-limit)
- No .NET runtime install required (Native AOT — single EXE)

## SHA-256

```
BrightRaider.exe   173B296BDD1ACA3D22F44B7F7EE5B93872EE2CEA30CA5AD64DD24E26CA558E33
```

Verify on Windows: `Get-FileHash BrightRaider.exe -Algorithm SHA256`

## 📖 Documentation — read online

- 📘 **[Manual](https://github.com/BrightRaider/BrightRaider/blob/main/docs/Manual.md)** — full reference (English + German)
- 🗺️ **[Map Scanner Guide](https://github.com/BrightRaider/BrightRaider/blob/main/docs/MapScanner_Guide.md)**
- 🔆 **[Auto-Brightness Guide](https://github.com/BrightRaider/BrightRaider/blob/main/docs/AutoBrightness_Guide.md)**
- 🔊 **[Footstep Booster Guide](https://github.com/BrightRaider/BrightRaider/blob/main/docs/FootstepBooster_Guide.md)**
- ⚡ **[QuickSelect Guide](https://github.com/BrightRaider/BrightRaider/blob/main/docs/QuickSelect_Guide.md)**
- 💾 **[QuickSave Guide](https://github.com/BrightRaider/BrightRaider/blob/main/docs/QuickSave_Guide.md)**
- 🎮 **[Game Pack Authoring](https://github.com/BrightRaider/BrightRaider/blob/main/docs/pack-authoring.md)** — add other games to the Map Scanner
- 📜 **[Changelog](https://github.com/BrightRaider/BrightRaider/blob/main/docs/CHANGELOG_PUBLIC.txt)**

## 📦 Download + install

**Download `BrightRaider.exe` and double-click it.** That's it. On first launch the EXE unpacks its runtime into `%LOCALAPPDATA%\BrightRaider\` and starts from there; later launches skip unpacking and start near-instantly. To uninstall, delete the EXE and that folder.

**Troubleshooting (optional):** the `BrightRaider_tools.zip` asset holds two small launchers — one starts BrightRaider with Map Scanner diagnostics, the other with QuickSave's alternate input path for games that ignore the default drag. Drop them next to the EXE only if you're troubleshooting; normal use needs just the EXE.

---

Thanks to everyone who tested the pre-release and filed reports — especially @Tia-Nastacia, whose log pinned down the QuickSave cooldown regression. 🙏
