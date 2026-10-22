# BrightRaider V1.4.0 — Lighter, faster, and Arc Raiders as a module

> ✅ **Everything carries over** — settings, profiles, keys and your license, whether you come from V1.2.3 or the V1.3 test version. No re-activation needed.

**V1.4 makes BrightRaider light.** It is ready a tenth of a second after you start it, waits in the tray with a fifth of the memory, and takes a fifth of the disk space. Arc Raiders becomes a **module that downloads and updates by itself** — a fix for Arc no longer needs a new BrightRaider. Coming from V1.2.3? You also get everything the V1.3 test version brought: a setup that walks you through, a new settings window with a guide beside every page, and the G-SYNC / FreeSync setup.

| | |
|---|---|
| ⚡ **Lighter and faster** — ready in 0.11 s, 21 MB in the tray | ⚡ Free |
| 🧭 **Setup assistant** — at the first start and once after this update, filled with what you already have | ⚡ Free |
| 🗂 **New settings window** — sidebar, search, a guide beside every page, every key on the page of its feature | ⚡ Free |
| 🖥 **G-SYNC / FreeSync setup** — the Blur Busters way, with an exact Undo | ⚡ Free |
| 🎮 **Arc Raiders as a module** — downloads by itself, updates by itself | 🔓 Pro |
| 🗺 **Map Scanner: your own names for the exits** (#80) | 🔓 Pro |
| 🎯 **Crosshair per game** — G-SYNC stays on where you don't need the crosshair | 🔓 Pro |

**⚡ Lighter and faster** — measured V1.3 against V1.4, same settings, both with Arc Raiders:

| | V1.3 | V1.4 |
|---|---|---|
| From start until your keys work | 0.58 s | **0.11 s** |
| Memory while it waits in the tray | 105 MB | **21 MB** |
| CPU while it waits | up to 0.26 % of a core | **0.03 %** |
| On disk | 74 MB | **14 MB** (app 5.7 MB + Arc Raiders module 8.1 MB) |

The 47 MB V1.3 unpacked on its first start are cleared away by themselves.

**🎮 Arc Raiders as a module** — switch it on in the setup or under *Settings → Modules → Arc Raiders*, and BrightRaider downloads it (8 MB) and loads it. Without Arc Raiders you don't get it at all. When Arc needs a fix, the tray offers **Install Arc Raiders …** and only the module is downloaded — nothing installs without your click, and never while a game is in front. A module only loads when it carries BrightRaider's signature. Your Pro key keeps everything it unlocks today, Arc Raiders included.

**🗺 Map Scanner** — call the exits what you call them (thanks @inf3rrno, #80): *Map Scanner → Advanced → your own names for the exits*. The overlay and the evac alarm use your names; an empty field keeps the game's. And no more made-up overlay when the map is not actually open — the XP screen, the skill tree or a menu stay quiet. Checked against nearly 300 real screenshots, no map lost.

**🎯 Crosshair per game** — an overlay on top of a game can cost it G-SYNC, so the crosshair is now remembered per game: on in one stays off in the others until you want it there too.

**🗂 Settings window** — the setup fits every page on the screen, no scrolling. An FPS limit you already set in your graphics driver now shows up (Games & FPS, setup, tray) instead of "No limit". **Ctrl+F** finds any setting, a **size** slider (70–150 %, also Ctrl + / −) fits the window to your screen, and **Report a problem** (App & License) puts your version, system and the end of the log together and opens a new issue for you to check and send.

**⌨ Keys** — a key bound twice no longer goes unnoticed: BrightRaider checks its own keys and every module's at start and whenever you change one, tells you, and **All keys** shows the pair at the top. Keys of two different games don't count — each only works in its own game. Give a key that is already in use, and it moves to its new place; the other one is told it has none now.

**🔧 Fixed** — QuickSave toggle presets: a press while a move was still running turned the direction around, so the next move went the same way again. The Footstep Booster brings the game back to its full level after a loud moment (it could stay at about 75 %). Copying system info waits a moment if another program holds the clipboard.

<details>
<summary><b>New since V1.2.3 — what the V1.3 test version brought</b></summary>

<br>

- **Setup** — every page already shows your settings: *Next* keeps them, only what you change is changed. Pages that are new since your version are marked. It never opens over a running game. Again any time: *App & License → Run the setup…*
- **Settings window** — a sidebar instead of tabs, every key on the page of its feature, **All keys** with conflicts in one place, a **Guide** beside every page, rarely used options under *Advanced*, switches instead of checkboxes, colour pickers for the crosshair and the Map Scanner.
- **G-SYNC / FreeSync** — V-SYNC and low latency in the driver, an FPS limit just under your refresh rate, competitive games can be left out (NVIDIA), Undo puts back exactly what was there. Why: [Blur Busters — G-SYNC 101](https://blurbusters.com/gsync/gsync101-input-lag-tests-and-settings/14/).
- **Notifications** on the screen you choose; strong profiles apply as far as Windows allows before its restart (#86); dark title bars everywhere.

</details>

Every setting from V1.1 to V1.3 was checked key by key — if anything is not where you left it, tell me in the [Discussions](https://github.com/BrightRaider/BrightRaider/discussions). Thank you to everyone who tested and reported.

---

**Download `BrightRaider.exe` and double-click it.** Unsigned single EXE — if SmartScreen flags it, choose **More info → Run anyway** (known false positive). The Arc Raiders module comes by itself when you switch it on.

<!-- TODO 1.4 release: SHA-256 of the signed BrightRaider.exe and arc_raiders.dll -->
```
BrightRaider.exe   TODO-SHA256
arc_raiders.dll    TODO-SHA256
```

<!-- TODO 1.4 release: Microsoft submission result (docs/assets/wdsi-v1.4.0.png) and VirusTotal link for this build -->
**🛡️ Checked.** TODO: Microsoft analysis and VirusTotal result for this build.

Windows 10 / 11 (64-bit) · nothing to install · 📘 [Manual](https://github.com/BrightRaider/BrightRaider/blob/main/docs/Manual.md) · [Changelog](https://github.com/BrightRaider/BrightRaider/blob/main/docs/CHANGELOG_PUBLIC.txt)
