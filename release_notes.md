# BrightRaider V1.2.3

> ✅ **Everything carries over** — settings, profiles and your license are kept, whether you come from V1.1.1 or V1.0. No re-activation needed.

**The Frozen Trail update:** your second screen stops blinding you, Alt-Tab is far quicker, the new Autoscrapper does your scrapping, and the Map Scanner knows Pendola Pass and the ARC Frigate.

## ✨ What's new

| | |
|---|---|
| 🌙 **Dim other displays** — darkens the screens you don't play on, only while the game is in front | ⚡ Free |
| ⚡ **Much faster Alt-Tab** — into and out of the game | ⚡ Free |
| 🔔 **Notifications on your game's monitor** — and hotkeys no longer get in the way outside the game | ⚡ Free |
| 🧹 **Autoscrapper** — reads your stash and scraps or sells by your rules | 🔓 Pro |
| 🗺 **Map Scanner** — Pendola Pass with its four gondola points, and the ARC Frigate event | 🔓 Pro |
| 🎧 **Mute teammates** — one key for Discord, TeamSpeak & co., with mute icons on screen | 🔓 Pro |
| 🎯 **QuickSave** — the weapon slots grab the weapon, not its barrel | 🔓 Pro |

## 🌙 Dim other displays

A bright second screen — a browser, a map, a stream — costs you contrast on the game. **Dim other displays** darkens every screen except the one the game is on, **only while the game is in front**; Alt-Tab out and they are back at once. Set it so you can still read the other screen but it is clearly calmer.

**Settings → Display.** *Test on screen* shows it without a game. Watching a film on the other screen? **Dim on/off** in Settings → Hotkeys switches it off for the session.

## 🧹 Autoscrapper

📖 **[Autoscrapper Guide](https://github.com/BrightRaider/BrightRaider/blob/main/docs/Autoscrapper_Guide.md)** — rules, the review screen, test run vs. live, naming unrecognised tiles.

- Press **F5** with the stash open: BrightRaider reads the whole stash (around 300 slots in ten seconds, 1080p to 5K, 16:10, 4:3, 21:9) and lists what it would scrap or sell.
- **By default nothing happens until you press Run**, and an item that is not on your list is never touched. *Test run* does everything except the last click. If you trust your rules, you can switch on *run without the review*.
- The review screen is the rule list: set "keep 200, scrap the rest" on the real item and Run saves it as a rule. Amounts are exact — "keep 40" leaves exactly 40.

**A word from me.** The Frozen Trail update added a long list of new items the Autoscrapper has not seen yet. Everything it already knew — all items that are not weapons — is recognised as reliably as before. **A tile it cannot name is never touched**; name it once under **Settings → Autoscrapper → Name unrecognised items…** and it is recognised from then on. Weapons are the hard part, because the picture changes with every attachment — that is the next big piece of work. Until then an unnamed weapon stays untouched.

## 🗺 Map Scanner

📖 [Map Scanner Guide](https://github.com/BrightRaider/BrightRaider/blob/main/docs/MapScanner_Guide.md)

- **Pendola Pass** is recognised by name, and its **Redirection** condition names the map in any game language. Open gondola points run the raid time, closed ones show **CLOSED**; at night only two are open.
- **ARC Frigate:** all evac points open, "No Hatches" in the header.

## 🎧 Audio

- **Mute teammates** (Settings → Hotkeys): one key mutes your voice chat apps — pick them from a list; Discord, TeamSpeak, Mumble, Teams and Zoom are in it. They are unmuted by themselves when the game closes, and the tray menu has **Unmute teammates** while they are muted.
- **Mute icons** (Settings → Audio): a speaker while the game is muted, a headset while your voice chat is.
- **Game mute** brings the sound back with one press, also after a restart of the game, and a game never stays silent after a crash.

## 🔧 Fixes and improvements

- **QuickSelect, QuickSave and the Map Scanner key only act while the game is in front** — everywhere else the key goes through untouched. Autorun stops when you switch to another program or switch hotkeys off.
- **FPS limit:** BrightRaider puts back exactly what it changed; your own cap in the NVIDIA Control Panel stays.
- **After a crash or a kill** your other screens and the FPS limit are put back at once. A small helper process does that — you may see BrightRaider twice in Task Manager; the second one ends with the app.
- **QuickSave** (#83): W1/W2 land on the weapon, not its barrel. On 4:3 and 16:10 it aims at the centred raid inventory (#85).
- **Notifications** appear on the monitor you chose or the one the game runs on. **Free:** keys of Pro features no longer get eaten in other programs.
- The **Windows lock screen** no longer switches to your game profile.
- When Windows refuses a colour curve (HDR on, or the extended gamma range not active yet) you get one clear notice instead of nothing.
- **Autorun per game** (sprint key, hold or toggle, tap timings), a **GPU and monitor picker**, and a safe start when an older version ran in between.

## 💡 Good to know

- **First start:** BrightRaider asks once for admin rights to unlock the full brightness range. **Restart Windows once** after that — until then strong profiles may be refused.
- **FPS limit:** the driver reads it when the game starts. After you add or change one, close the game once and start it again.
- **Mute icons** need borderless or windowed mode; in *exclusive* fullscreen Windows shows no overlays.

---

## 📦 Download + install

**Download `BrightRaider.exe` and double-click it.** To uninstall, turn off "Start with Windows", exit BrightRaider, then delete the EXE and the folders `%LOCALAPPDATA%\BrightRaider` and `%APPDATA%\BrightRaider`.

If Windows Defender or SmartScreen flags it: it is an unsigned single-EXE — a **known false positive**. Choose **More info → Run anyway**. Earlier releases were submitted to Microsoft and came back clean.

```
BrightRaider.exe   6511B932EDE70461746BB86E52EDE8FEDD1843784B6F6EE5862C87D256371903
```

Windows 10 / 11 (64-bit) · NVIDIA or AMD recommended (Intel: gamma and contrast only) · no .NET install needed · 📘 [Manual](https://github.com/BrightRaider/BrightRaider/blob/main/docs/Manual.md) · 📜 [Changelog](https://github.com/BrightRaider/BrightRaider/blob/main/docs/CHANGELOG_PUBLIC.txt) · [V1.1.1 notes](https://github.com/BrightRaider/BrightRaider/releases/tag/v1.1.1)
