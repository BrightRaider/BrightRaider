# BrightRaider V1.4 — Five times lighter. Ready for every game.

> ### ⚡ Ready in 0.11 s · 21 MB in the tray · 14 MB on disk
> BrightRaider starts **five times faster**, needs **a fifth of the memory** and **a fifth of the disk** — and from now on it **updates itself in one click**.

✅ **Everything carries over** — settings, profiles, keys and your license, from V1.2.3 or the V1.3 test version. No re-activation.

| | |
|---|---|
| ⚡ **Five times lighter** — ready in 0.11 s, 21 MB in the tray | ⚡ Free |
| ⬇️ **Updates in one click** — from the tray, signed and checked | ⚡ Free |
| 🪟 **Windows key blocked in games** — and the mouse stays on the game's screen | ⚡ Free |
| 🎙 **Mute your microphone** on one key | ⚡ Free |
| ⌨ **No more double-bound keys** — found, shown, fixed in one click | ⚡ Free |
| 🔎 **Find any setting** — Ctrl+F | ⚡ Free |
| 🎮 **Arc Raiders as a module** — downloads and updates by itself | 🔓 Pro |
| 🎯 **Crosshair per game** — G-SYNC stays on where you don't need it | 🔓 Pro |
| 🗺 **Name your own exits** in the Map Scanner (#80) | 🔓 Pro |

## ⚡ Five times lighter

Measured V1.3 against V1.4, same settings, both with Arc Raiders:

| | V1.3 | **V1.4** |
|---|---|---|
| From start until your keys work | 0.58 s | **0.11 s** |
| Memory while it waits in the tray | 105 MB | **21 MB** |
| CPU while it waits | up to 0.26 % of a core | **0.03 %** |
| On disk | 74 MB | **14 MB** |

The 47 MB V1.3 unpacked on its first start are cleared away by themselves.

## ⬇️ Updates in one click

A new version is out? The tray offers **Install update** — one click, and BrightRaider downloads it, checks its signature and restarts. Your settings stay, and nothing installs without your click. Want test versions early? *App & License → Advanced → Also offer test versions*.

## 🎮 In your games

- **Windows key blocked in games** — no Start menu mid-fight, no Sticky Keys pop-up from pressing Shift five times. On the desktop everything works as usual.
- **The mouse stays on the game's screen** — with two or more monitors, a fast flick can't slide onto the other screen and click the game away. Free again on Alt-Tab.
- **Mute your microphone** — one key, and nobody hears you, in any app, until you press again.
- **Crosshair per game** — an overlay can cost a game G-SYNC, so the crosshair is remembered per game: on in one, off in the others.
- **Calmer colour switching** — a game window that flashes up while loading no longer flips your colours, and the game's launcher, its pop-ups and the settings window keep the game's colours.
- **Your driver's FPS limit** shows up (Games & FPS, setup, tray) instead of "No limit".

## 🎮 Arc Raiders — now a module

Switch it on in the setup or under *Modules → Arc Raiders*, and it downloads by itself (8 MB). When Arc needs a fix, the tray offers **Install Arc Raiders …** and only the module is downloaded — no new BrightRaider needed. It only loads with BrightRaider's signature, and your Pro key unlocks it as before. Everything about it — keys, guides, manual: **[Arc Raiders module](https://github.com/BrightRaider/BrightRaider/blob/main/docs/modules/arc-raiders/README.md)**.

- **🗺 Map Scanner** — call the exits what you call them (thanks @inf3rrno, #80); the overlay and the evac alarm use your names. And no made-up overlay when the map isn't actually open — checked against nearly 300 real screenshots, no map lost.
- **♻️ Autoscrapper** — recognises more of your stash (items with a symbol in their corner), never touches what you marked with the eye, and never scraps or sells a blueprint, whatever a rule says.
- **💾 QuickSave** — a press while a move is still running no longer turns the direction around.

## ⌨ Keys

A key bound twice no longer goes unnoticed: BrightRaider checks its own keys and every module's, tells you, and **All keys** shows the pair at the top. Give a key that is already in use, and it moves to its new place — the other one is told it has none now, and *Cancel* undoes it.

## 🗂 Settings

**Ctrl+F** finds any setting. A **size** slider (70–150 %, also Ctrl + / −) fits the window to your screen. The setup fits every page on the screen — no scrolling. **Report a problem** (App & License) puts your version, system and log together and opens an issue for you to check and send.

## 🔧 Fixed

- Audio devices you picked are found again after a driver update or on another USB port.
- The Footstep Booster brings the game back to its full level after a loud moment (it could stay at about 75 %).
- Turning the game down by holding the mute key now says what it does: it stays down until you hold again — the slider is called *Turned down to*.
- Copying system info waits a moment if another program holds the clipboard.

<details>
<summary><b>New since V1.2.3 — what the V1.3 test version brought</b></summary>

<br>

- **Setup** — every page already shows your settings: *Next* keeps them, only what you change is changed. Pages that are new since your version are marked. It never opens over a running game. Again any time: *App & License → Run the setup…*
- **Settings window** — a sidebar instead of tabs, every key on the page of its feature, **All keys** with conflicts in one place, a **Guide** beside every page, rarely used options under *Advanced*, switches instead of checkboxes, colour pickers for the crosshair and the Map Scanner.
- **G-SYNC / FreeSync** — V-SYNC and low latency in the driver, an FPS limit just under your refresh rate, competitive games can be left out (NVIDIA), Undo puts back exactly what was there. Why: [Blur Busters — G-SYNC 101](https://blurbusters.com/gsync/gsync101-input-lag-tests-and-settings/14/).
- **Notifications** on the screen you choose; strong profiles apply as far as Windows allows before its restart (#86); dark title bars everywhere.

</details>

Every setting from V1.1 to V1.3 was checked key by key — if anything is not where you left it, tell me in the [Discussions](https://github.com/BrightRaider/BrightRaider/discussions). Thank you to everyone who tested and reported.

<!-- STABLE ONLY - not in the pre-release (ADR 0030). At the stable release, put this line here, uncommented:
🥚 **One more thing:** V1.4 hides a secret in the settings. The first one to find it gets Pro.
-->

---

**Download `BrightRaider.exe` and double-click it.** Coming from V1.2.3 or V1.3: download it once more by hand — from V1.4 on, updates come in one click. Unsigned single EXE — if SmartScreen flags it, choose **More info → Run anyway** (known false positive).

<!-- TODO 1.4 release: SHA-256 of the signed BrightRaider.exe and arc_raiders.dll -->
```
BrightRaider.exe   TODO-SHA256
arc_raiders.dll    TODO-SHA256
```

<!-- TODO 1.4 release: Microsoft submission result (docs/assets/wdsi-v1.4.0.png) and VirusTotal link for this build -->
**🛡️ Checked.** TODO: Microsoft analysis and VirusTotal result for this build.

Windows 10 / 11 (64-bit) · nothing to install · 📘 [Manual](https://github.com/BrightRaider/BrightRaider/blob/main/docs/Manual.md) · [Changelog](https://github.com/BrightRaider/BrightRaider/blob/main/docs/CHANGELOG_PUBLIC.txt)
