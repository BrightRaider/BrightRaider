# BrightRaider V1.4 — Back to light. Stronger than ever.

BrightRaider started as a tiny tool that did one thing, instantly. **V1.4 is that tool again** — small, instant, quiet in your tray — and it does more than ever: a setup that walks you through, a new settings window, the G-SYNC / FreeSync setup, updates in one click, and modules for your games.

> ### ⚡ Ready in 0.2 s · 24 MB in the tray · 14 MB on disk
> Against V1.2.3: **almost four times faster to start**, **a third of the memory**, **a fifth of the disk**.

✅ **Everything carries over** — settings, profiles, keys and your license. No re-activation.

## ⚡ Back to light

Measured V1.2.3 against V1.4 on the same PC, same settings, both with Arc Raiders:

| | V1.2.3 | **V1.4** |
|---|---|---|
| From the double-click until your keys work | 0.76 s | **0.20 s** |
| Memory while it waits in the tray | 72 MB | **24 MB** |
| On disk | 71 MB (25 MB + 46 MB it unpacks) | **14 MB** (the app 5.9 MB + Arc Raiders 8.2 MB) |

Whatever V1.2.3 unpacked on your PC is cleared away by itself.

## 🆕 What's new

| | |
|---|---|
| 🧭 **A setup that walks you through** — at the first start, and once after this update | ⚡ Free |
| 🗂 **A new settings window** — a guide beside every page, search, every key where it belongs | ⚡ Free |
| 🖥 **G-SYNC / FreeSync setup** — the Blur Busters way, with an exact undo | ⚡ Free |
| ⬇️ **Updates in one click** — from the tray, signed and checked | ⚡ Free |
| 🪟 **No Windows key in games** — and the mouse stays on the game's screen | ⚡ Free |
| 🎙 **Mute your microphone** on one key | ⚡ Free |
| ⌨ **No more double-bound keys** — found, shown, fixed in one click | ⚡ Free |
| 🎮 **Modules** — Arc Raiders downloads, loads and updates by itself | 🔓 Pro |
| 🎯 **Crosshair per game** — G-SYNC stays on where you don't need it | 🔓 Pro |
| 🗺 **Name your own exits** in the Map Scanner (#80) | 🔓 Pro |

## 🧭 A setup that walks you through

At the first start — and once after this update — a short setup goes through everything: keyboard, brightness, your games, FPS, sound, crosshair, notifications, modules. Every page already shows what you have: *Next* keeps it, only what you change is changed, and what is new since V1.2.3 is marked. It never opens over a running game. Again any time: *App & License → Run the setup…*

## 🗂 A new settings window

- **A guide beside every page** — what happens, how to set it up, what to try when something is off.
- **Every key on the page of its feature**, and **All keys** shows every one in one place.
- **Ctrl+F finds any setting.** A **size** slider (70–150 %, also Ctrl + / −) fits the window to your screen.
- A sidebar instead of tabs, rarely used options under *Advanced*, colour pickers for the crosshair and the Map Scanner, dark title bars.
- **Report a problem** (App & License) puts your version, system and log together and opens an issue for you to check and send.

## 🖥 G-SYNC / FreeSync setup

V-SYNC and low latency in the driver, an FPS limit just under your refresh rate — the way [Blur Busters](https://blurbusters.com/gsync/gsync101-input-lag-tests-and-settings/14/) recommends. Competitive games can be left out (NVIDIA), and *Undo* puts back exactly what was there. In the setup, or under *Games & FPS → Advanced*. An FPS limit you already set in your driver now shows up instead of "No limit".

## ⬇️ Updates in one click

A new version is out? The tray offers **Install update** — one click, and BrightRaider downloads it, checks its signature and restarts. Your settings stay, and nothing installs without your click. Want test versions early? *App & License → Advanced → Also offer test versions*.

## 🎮 In your games

- **Windows key blocked in games** — no Start menu mid-fight, no Sticky Keys pop-up from pressing Shift five times. On the desktop everything works as usual.
- **The mouse stays on the game's screen** — with two or more monitors, a fast flick can't slide onto the other screen and click the game away. Free again on Alt-Tab.
- **Mute your microphone** — one key, and nobody hears you, in any app, until you press again.
- **Crosshair per game** — an overlay can cost a game G-SYNC, so the crosshair is remembered per game: on in one, off in the others.
- **Calmer colour switching** — a game window that flashes up while loading no longer flips your colours, and the game's launcher, its pop-ups and the settings window keep the game's colours.
- **Notifications on the screen you choose.**

## 🎮 Modules — Arc Raiders

The tools for one game now come as a **module**. Switch Arc Raiders on in the setup or under *Modules → Arc Raiders*: it downloads by itself (8 MB) and is there at once — no restart. Switch it off, and it isn't loaded at all. When Arc needs a fix, the tray offers **Install Arc Raiders …** and only the module is downloaded. It only loads with BrightRaider's signature, and your Pro key unlocks it as before. Keys, guides, manual: **[Arc Raiders module](https://github.com/BrightRaider/BrightRaider/blob/main/docs/modules/arc-raiders/README.md)**.

- **🗺 Map Scanner** — call the exits what you call them (thanks @inf3rrno, #80); the overlay and the evac alarm use your names. No made-up overlay when the map isn't actually open — checked against nearly 300 real screenshots, no map lost.
- **♻️ Autoscrapper** — knows the items of the 2.0 update (about 50 recognised right away, the rest you name once), recognises more of your stash (items with a symbol in their corner), never touches what you marked with the eye, and never scraps or sells a blueprint, whatever a rule says.
- **💾 QuickSave** — a press while a move is still running no longer turns the direction around.

## ⌨ Keys

A key bound twice no longer goes unnoticed: BrightRaider checks its own keys and every module's, tells you, and **All keys** shows the pair at the top. Give a key that is already in use, and it moves to its new place — the other one is told it has none now, and *Cancel* undoes it.

## 🔧 Fixed

- Strong profiles before the Windows restart (#86): BrightRaider applies the strongest part Windows accepts and tells you once.
- Audio devices you picked are found again after a driver update or on another USB port.
- The Footstep Booster brings the game back to its full level after a loud moment (it could stay at about 75 %).
- Turning the game down by holding the mute key now says what it does: it stays down until you hold again — the slider is called *Turned down to*.
- Copying system info waits a moment if another program holds the clipboard.

Every setting from V1.1 to V1.2.3 was checked key by key — if anything is not where you left it, tell me in the [Discussions](https://github.com/BrightRaider/BrightRaider/discussions). Thank you to everyone who tested and reported.

<!-- STABLE ONLY - not in the pre-release (ADR 0030). At the stable release, put this line here, uncommented:
🥚 **One more thing:** V1.4 hides a secret in the settings. The first one to find it gets Pro.
-->

---

**Download `BrightRaider.exe` and double-click it.** Coming from V1.2.3: download it once more by hand — from V1.4 on, updates come in one click. Unsigned single EXE — if SmartScreen flags it, choose **More info → Run anyway** (known false positive).

<!-- TODO 1.4 release: SHA-256 of the signed BrightRaider.exe and arc_raiders.dll -->
```
BrightRaider.exe   TODO-SHA256
arc_raiders.dll    TODO-SHA256
```

<!-- TODO 1.4 release: Microsoft submission result (docs/assets/wdsi-v1.4.0.png) and VirusTotal link for this build -->
**🛡️ Checked.** TODO: Microsoft analysis and VirusTotal result for this build.

Windows 10 / 11 (64-bit) · nothing to install · 📘 [Manual](https://github.com/BrightRaider/BrightRaider/blob/main/docs/Manual.md) · [Changelog](https://github.com/BrightRaider/BrightRaider/blob/main/docs/CHANGELOG_PUBLIC.txt)
