# BrightRaider V1.2.2

> 🧪 **Pre-release.** Settings, profiles and license carry over — no re-activation needed. These notes cover **everything since V1.1.1**: V1.2.0, V1.2.1 and V1.2.2 in one place.

**The Frozen Trail update.** The **Autoscrapper** reads your stash and does the scrapping for you, the **Map Scanner** knows Pendola Pass and the ARC Frigate, and your second screen finally stops blinding you.

> **Using the Autoscrapper?** The Frozen Trail update brought a long list of new items that BrightRaider does not know yet. Please read the note in the Autoscrapper section below — in short: nothing it cannot name is ever touched, and you can teach it the new items yourself.

## 🌙 Dim other displays — the one you will feel at once

A bright second screen — a browser, a map, a stream — sits in the corner of your eye and costs you contrast on the game. **Dim other displays** darkens every screen except the one you play on, **only while the game is in front**. Alt-Tab out and they are back to normal that instant; nothing stays dark on your desktop.

- **Set it so you can still read the other screen** (chat, a guide) but it is clearly calmer: you get **more focus and more contrast** on the game, and a white web page next to a dark raid no longer blinds you.
- It works on your display's **own calibrated curve**, so nothing turns grey or washed out. It goes down to **a tenth** of the normal brightness with the one-time Windows permission for the extended gamma range (asked once; a button in Settings → Display asks again). Without it the slider stops at about 60 %.
- **Settings → Display → Dim other displays**, free. *Test on screen* flashes it together with a profile, no game needed. 0 = off; with "All Monitors" selected there is no other screen to dim.
- **Watching a film on the other screen?** Bind **Settings → Hotkeys → Dim on/off**: one press brings the other screens back, the next press dims them again. It only lasts for the session; your slider stays as it is.

## What's new since V1.1.1

| | |
|---|---|
| **Autoscrapper** — reads your stash and scraps or sells by your rules; nothing happens until you press Run | 🔓 Pro |
| **Dim other displays** — darkens the screens you don't play on, only in the game | ⚡ Free |
| **Map Scanner: Pendola Pass** — the four gondola points, open or closed, with the raid time | 🔓 Pro |
| **Map Scanner: ARC Frigate** — all evac points open, no Raider Hatches | 🔓 Pro |
| **Much faster Alt-Tab** — switching into and out of the game is far quicker | ⚡ Free |
| **Mute teammates** — one key mutes your voice chat (Discord, TeamSpeak, …), apps picked from a list | 🔓 Pro |
| **Mute icons** — a speaker or a headset in the corner while the game or your voice chat is muted | 🔓 Pro |
| **QuickSave on 4:3 and 16:10** — finds where the inventory sits | 🔓 Pro |
| **Windows lock screen** no longer switches to your game profile | ⚡ Free |

---

## 🧹 Autoscrapper

New in V1.2.0, better in V1.2.1 and V1.2.2.

- **Reads the stash off the screen**, works out what every tile is and shows a list of what it would scrap or sell: around 300 slots in about ten seconds, at every resolution from 1080p to 5K, 16:10, 4:3 and 21:9.
- **Nothing happens until you press Run.** An item that is not on your list is never touched.
- **The review screen is the rule list.** Set "keep 200, scrap the rest" while looking at the real item — its picture, how many you have, what one is worth. Run writes your choices back as rules, so the second run needs no editing. "Worth of the rest" shows what the rest is worth before you decide.
- **Test run** goes the whole way and closes the game's confirmation with Escape: everything except the last click. Only **Live** acts.
- Items the game draws identically are not guessed. Anything BrightRaider cannot name goes into a naming queue and is recognised from then on.
- **V1.2.1:** optional run without the review once your rules are settled (off by default; unnamed tiles stay untouched, and an incomplete scan brings the review up anyway). It works upward from the end of the list, damaged items are no longer mistaken for their blueprint, and stack counts read right.
- **V1.2.2:** **about twice as fast** — it scans only as far as your stash is filled and stops as soon as nothing more fits. **Exact amounts:** "keep 40" over stacks of 10, 10, 10, 10, 10, 10, 7 and 5 now leaves exactly 40, not 42.

### A word from me, about the Frozen Trail update

I have put easily 100 hours into the Autoscrapper — thousands of tiles checked against known answers, at every resolution, until it named what it saw. Then the Frozen Trail update arrived with a long list of new items, and part of what worked the day before does not work for them yet: BrightRaider has never seen them, so it cannot name them. I would rather tell you that plainly than hide it.

- **For the items it already knew, nothing has changed.** The everyday items — everything that is not a weapon — are recognised as reliably as before, and those are usually exactly the ones you want handled automatically.
- **Nothing breaks and nothing is lost.** A tile BrightRaider cannot name is never touched, whatever your rules say. Everything it already knew is still recognised, and your rules still apply to it.
- **It can learn the new items — every one it can see.** Unnamed tiles go into the naming queue with their picture (**Settings → Autoscrapper → Name unrecognised items…**). Name an item once and it is recognised from the next scan on. Your names are kept in your own file and no update ever overwrites them; if an item is one the built-in list has never heard of, you can simply type its name.
- **I will teach it the rest step by step**, the way I did with the Map Scanner: collect the pictures, name them, release. For the items you care about you do not have to wait for me.
- **Weapons are the hard part.** The weapon picture gets smaller the more attachments sit on the weapon, and everybody runs different attachments, so a weapon has no single picture to learn. As far as I can tell that is how the game draws it, but I am not sure it is meant to be that way. Naming every look one by one does not scale, so weapons need a different approach — recognising the weapon itself, whatever is attached to it — and that is the next big piece of work on the Autoscrapper. Until then a weapon BrightRaider cannot name stays untouched, which is the safe side, and weapons are rarely in anyone's rules anyway.

## 🗺 Map Scanner

- **Pendola Pass** is recognised by its name, and the overlay lists its four gondola points — open ones run the raid's remaining time (the same on each), closed ones show **CLOSED**; at night only two are open. The gondola has no countdown of its own, so the evac alarm follows the raid timer. Recognised on its own in the German game UI so far; in other languages it still shows up under the Redirection condition.
- **ARC Frigate** (any map) is recognised: all evac points stay open, and the overlay header adds "No Hatches".
- Map events are sorted into major and minor ones, and the triangle in front of the major events (Matriarch, Hurricane, Night Raid, Electromagnetic Storm) is gone from the overlay header.

## 🔊 Audio and voice chat

- **Mute teammates** (Settings → Hotkeys): one key mutes or unmutes your voice chat apps. Pick them from a list — Discord, TeamSpeak, Mumble, Teams and Zoom are already in it, **+ Add app…** takes any running program. Unbound by default. A mute left behind by an exit or crash is undone at the next start.
- **Mute icons** (Settings → Audio): a small speaker with a cross in the top-left corner of the game's screen while the Mute hotkey has the game silenced, and a headset struck through while Mute teammates has your voice chat silenced. They never take clicks or focus; switch them off in the Audio tab.
- **Game mute needed two presses** to bring the sound back when something else had muted the game (Alt-Tab background mute, the Windows mixer, an earlier run). The key now looks at the game's real mute state, and the toast and icon show what the audio actually is afterwards.
- **Headsets with several playback devices** (an Astro A50 shows up as both "Game" and "Voice"): mute, ducking and the Footstep Booster now cover every output the game uses instead of only the first one.
- **A game no longer stays silent** if BrightRaider is closed, killed or crashes while it is muted or ducked: the change is undone at the next start, or when the game comes to the front. **Settings → Audio → Reset game audio** undoes it by hand, and BrightRaider tells you when a game comes to the front muted or at your ducking level.

## ⚡ Alt-Tab, keys and macros

- **Much faster Alt-Tab:** switching into and out of the game is far quicker — the work BrightRaider does on every focus change got much lighter.
- **QuickSave on 4:3 and 16:10 screens** (#83, #85): the game puts the inventory at the top of such a screen for some setups and in the centre for others. QuickSave now looks at the open inventory and aims where it really is. On every screen the targets sit in the middle of each slot, and W1/W2 land on the weapon instead of grabbing its barrel.
- **Keys no longer swallowed in other programs:** on Free, the keys of Pro features (Num4–9, mute, crosshair, audio device, …) and the Arc toggle keys with Arc off now reach the window in front. The Global ON/OFF key follows "only while a game is focused" unless it sits on the numpad.
- **QuickSelect, QuickSave and the Map Scanner key** only act while the game is in front; elsewhere the key goes through untouched.
- **Autorun per game:** the sprint key, hold or toggle, and the tap timings can differ per game profile. Autorun stops when hotkeys are switched off or the game is left.
- **GPU picker and monitor index:** choose which adapter and which monitor BrightRaider drives.
- **Notifications on the monitor you chose** (#85): toasts always appeared on the primary monitor. They now follow the monitor you picked in Settings, or the one the game runs on.
- **A colour curve that did nothing:** when Windows does not take a profile (HDR on, extended gamma range missing), BrightRaider now says so once, with the likely cause, instead of toasting "Profile 2".

## 🔧 Fixed

- **Cut-off numbers:** the value in number fields with up/down buttons was clipped when the field was narrow ("72" showed as "7"), and the tap-timing fields under Movement cut off the last digit. They keep room for the digits now, in every window.
- **Bright lock screen:** BrightRaider took the Windows lock screen for a game and switched to the game profile.
- **Incomplete scans:** a row skipped while scrolling could leave the scan incomplete when the stash was not full.
- **Clicks next to the target** when the list was still moving after a scroll, and an item tooltip that could cover the stash and stop a test run.
- **A game profile without an FPS value** no longer writes 0 into the driver.
- **Running an older version in between** no longer loses your newer settings: they are restored on the next start.
- **Under the hood:** sturdier hooks and startup, and safer loading of graphics-driver libraries.

---

## 📦 Download + install

**Download `BrightRaider.exe` and double-click it.** To uninstall, turn off “Start with Windows”, exit BrightRaider, then delete the EXE and the folders `%LOCALAPPDATA%\BrightRaider` and `%APPDATA%\BrightRaider`. The one-time Windows setting for the extended gamma range stays until you remove it (see the Manual).

If Windows Defender or SmartScreen flags it: it is an unsigned single-EXE — a **known false positive**. Choose **More info → Run anyway**. Earlier releases were submitted to Microsoft and came back clean.

```
BrightRaider.exe   8F0376C6D5AFFF3D5FD3246F8B84B5B250CBEEA1A83BDD905A252BA879DD1B72
```

Windows 10 / 11 (64-bit) · no .NET install needed · 📘 [Manual](https://github.com/BrightRaider/BrightRaider/blob/main/docs/Manual.md) · 📜 [Changelog](https://github.com/BrightRaider/BrightRaider/blob/main/docs/CHANGELOG_PUBLIC.txt) · [V1.2.1 notes](https://github.com/BrightRaider/BrightRaider/releases/tag/v1.2.1)
