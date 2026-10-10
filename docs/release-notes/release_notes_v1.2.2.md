# BrightRaider V1.2.2

> 🧪 **Pre-release.** Settings, profiles and license carry over. These notes cover **everything since V1.1.1** (V1.2.0, V1.2.1 and V1.2.2).

**The Frozen Trail update:** your second screen stops blinding you, Alt-Tab got far quicker, the Autoscrapper does your scrapping, and the Map Scanner learns Pendola Pass and the ARC Frigate. **Free for everyone:** the first three rows below.

## ✨ What's new

| | |
|---|---|
| 🌙 **Dim other displays** — darkens the screens you don't play on, only while the game is in front | ⚡ Free |
| ⚡ **Much faster Alt-Tab** — into and out of the game | ⚡ Free |
| 🔔 **Notifications on your game's monitor**, no more swallowed keys on Free, no more lock-screen profile | ⚡ Free |
| 🧹 **Autoscrapper** — reads your stash and scraps or sells by your rules, only after you press Run. About twice as fast now | 🔓 Pro |
| 🗺 **Map Scanner** — Pendola Pass with its four gondola points, and the ARC Frigate event | 🔓 Pro |
| 🎧 **Mute teammates** — one key for Discord, TeamSpeak & co., with mute icons on screen | 🔓 Pro |
| 🔇 **Audio fixes** — game mute with one press, headsets with several devices, recovery after a crash | 🔓 Pro |
| 🎯 **QuickSave on 4:3 and 16:10** — finds where the inventory sits | 🔓 Pro |

## 🌙 Dim other displays

A bright second screen — a browser, a map, a stream — costs you contrast on the game. **Dim other displays** darkens every screen except the one you play on, **only while the game is in front**; Alt-Tab out and they are back that instant. Set it so you can still read the other screen but it is clearly calmer: more focus, more contrast, and a white web page no longer blinds you next to a dark raid. It works on your display's own calibrated curve, down to a tenth of normal brightness (with the one-time extended gamma permission, asked once; about 60 % without).
**Settings → Display.** *Test on screen* shows it without a game. Watching a film on the other screen? **Dim on/off** in Settings → Hotkeys switches it off and on for the session.

## 🧹 Autoscrapper

📖 **[Autoscrapper Guide](https://github.com/BrightRaider/BrightRaider/blob/main/docs/Autoscrapper_Guide.md)** — rules, the review screen, test run vs. live, naming unrecognised tiles.

- Reads the stash off the screen (around 300 slots in ten seconds, 1080p to 5K, 16:10, 4:3, 21:9) and shows what it would scrap or sell. **Nothing happens until you press Run, and an item not on your list is never touched.**
- The review screen is the rule list: set "keep 200, scrap the rest" on the real item, Run saves it as a rule. *Test run* does everything except the last click.
- V1.2.1: optional run without the review. V1.2.2: twice as fast, and **exact amounts** — "keep 40" over stacks of 10, 10, 10, 10, 10, 10, 7 and 5 leaves exactly 40, not 42.

**A word from me.** I have put easily 100 hours into the Autoscrapper. Then the Frozen Trail update added a long list of new items it has not seen yet.
- **The everyday items it already knew — everything that is not a weapon — are recognised as reliably as before**, and those are usually the ones you want handled automatically.
- **A tile it cannot name is never touched.** Name it once under **Settings → Autoscrapper → Name unrecognised items…** and it is recognised from then on; your names stay in your own file. I will teach the rest step by step, like I did with the Map Scanner.
- **Weapons are the hard part.** The weapon picture shrinks with every attachment, and everybody runs different ones, so a weapon has no single picture to learn. That needs a new approach — recognising the weapon itself, whatever is attached — and it is the next big piece of work. Until then an unnamed weapon stays untouched.

## 🗺 Map Scanner and 🎧 Audio

📖 [Map Scanner Guide](https://github.com/BrightRaider/BrightRaider/blob/main/docs/MapScanner_Guide.md)

- **Pendola Pass** is recognised by name: open gondola points run the raid time, closed ones show **CLOSED**, at night only two are open. **ARC Frigate:** all evac points open, "No Hatches" in the header.
- **Mute teammates** (Settings → Hotkeys): one key mutes your voice chat apps — pick them from a list, Discord, TeamSpeak, Mumble, Teams and Zoom are in it. **Mute icons** (Settings → Audio): a speaker while the game is muted, a headset while your voice chat is.
- **Game mute** brings the sound back with one press, even if something else had muted the game. Headsets with several playback devices are covered, and a game never stays silent after a crash (**Reset game audio** undoes it by hand).

## 🔧 Also fixed

- **QuickSave on 4:3 and 16:10** (#83, #85): the inventory is not in the same place in the hideout and in a raid, and on these screens it sits at the top or in the centre. QuickSave now looks at the open inventory and aims where it really is. W1/W2 land on the weapon, not its barrel.
- **Toasts** appear on the monitor you chose, not always the primary one. **Free users:** keys of Pro features no longer get eaten in other programs.
- The **Windows lock screen** no longer switches to your game profile. **Cut-off numbers** in narrow fields are fixed everywhere. A colour curve Windows refuses (HDR on) now gets one clear notice.
- Autorun per game, GPU and monitor picker, and a safer start with an older version in between.

---

## 📦 Download + install

**Download `BrightRaider.exe` and double-click it.** To uninstall, turn off “Start with Windows”, exit BrightRaider, then delete the EXE and the folders `%LOCALAPPDATA%\BrightRaider` and `%APPDATA%\BrightRaider`. The one-time Windows setting for the extended gamma range stays until you remove it (see the Manual).

If Windows Defender or SmartScreen flags it: it is an unsigned single-EXE — a **known false positive**. Choose **More info → Run anyway**. Earlier releases were submitted to Microsoft and came back clean.

```
BrightRaider.exe   8F0376C6D5AFFF3D5FD3246F8B84B5B250CBEEA1A83BDD905A252BA879DD1B72
```

Windows 10 / 11 (64-bit) · no .NET install needed · 📘 [Manual](https://github.com/BrightRaider/BrightRaider/blob/main/docs/Manual.md) · 🧹 [Autoscrapper Guide](https://github.com/BrightRaider/BrightRaider/blob/main/docs/Autoscrapper_Guide.md) · 📜 [Changelog](https://github.com/BrightRaider/BrightRaider/blob/main/docs/CHANGELOG_PUBLIC.txt) · [V1.2.1 notes](https://github.com/BrightRaider/BrightRaider/releases/tag/v1.2.1)
