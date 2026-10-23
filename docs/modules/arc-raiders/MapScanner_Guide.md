# BrightRaider — Map Scanner Guide

**Map Scanner** is a Pro feature for **Arc Raiders**. Long-press <kbd>M</kbd> on the in-game map and BrightRaider scans every evacuation timer for you. A live, color-coded countdown overlay stays on your screen as you play — you always know which evac points are still safe, which are closing soon, and which are already gone.

---

## How It Works

1. You long-press <kbd>M</kbd> (default 350 ms — configurable) while looking at the in-game map
2. BrightRaider scrolls the map out, captures the timer regions, runs template matching on the digits
3. The active event (Night Raid, Hurricane, Electromagnetic Storm, ...) is detected automatically
4. An overlay appears on your screen with all evac timers, color-coded
5. Timers count down in real time. When all timers expire, the overlay auto-hides
6. An audible alarm fires when any timer drops below your configured threshold

Scanning takes ~1–2 seconds. The overlay refreshes from each fresh scan — long-press <kbd>M</kbd> again any time to refresh.

---

## Requirements

- **BrightRaider Pro license** (€5.49 one-time)
- Arc Raiders, played at the standard map zoom (BrightRaider handles the scroll-out automatically)
- BrightRaider V1.0 or later

---

## Supported Maps & Events

| Map | Detected automatically |
|-----|------------------------|
| Buried City | ✓ |
| Stella Montis | ✓ |
| Space Port | ✓ |
| Blue Gate | ✓ |
| Damm | ✓ |
| Riven Tides | ✓ (Coastal Lift, Customs Lift) |
| Pendola Pass | ✓ recognised by its name; lists its four gondola points: open ones run the raid's remaining time (the gondola has no countdown of its own), closed ones show CLOSED *(new in V1.2; name recognition for the German game UI so far)* |

| Event | Effect on overlay |
|-------|-------------------|
| Night Raid | Marks 2 evac points active, rest CLOSED. The overlay header adds "No Hatches". |
| Hurricane | All evac points stay active. |
| Electromagnetic Storm | Marks 3 active, rest CLOSED. The overlay header adds "No Hatches". |
| Harvester | Normal scan, event name displayed |
| Lush Blooms | Normal scan, event name displayed |
| Matriarch | Normal scan, event name displayed |
| Husk Graveyard | 4 evac points, Raider Hatches OPEN |
| Close Scrutiny | Normal scan, event name displayed |
| Bird City | Normal scan, event name displayed |
| Locked Gate | Normal scan, event name displayed |
| Launch Tower Loot | Normal scan, event name displayed |
| Beachcombing | Normal scan, event name displayed |
| Prospecting Probes | Normal scan, event name displayed *(new in V1.1.1)* |
| Uncovered Caches | Normal scan, event name displayed *(new in V1.1)* |
| Hidden Bunker (Space Port) | Marks 3 evac points active, rest CLOSED, hatches closed *(new in V1.1)* |
| ARC Frigate | All evac points stay active. The overlay header adds "No Hatches" *(new in V1.2)* |
| Redirection (Pendola Pass) | Event name displayed; also names the map when its title cannot be read (any game language) *(new in V1.2)* |

The event name appears below the map name in the overlay.

---

## Setup — Step by Step

1. Right-click the BrightRaider tray icon → **Settings…**
2. **Modules → Arc Raiders → Map Scanner** — the switch top right turns it on (the Arc Raiders module has to be on too). **Guide** next to it shows this in short.
3. **Colours** — the map name and the 5 timer colours; the **Preview** beside it shows them at once
4. **Position** (under the preview) — top or bottom, left / centre / right
5. **Evac alarm** — switch it on, set *When the timer drops below* (seconds) and *With sound*
6. *Advanced: overlay size, map key, alarm repeat* — **Background** (0 = no backdrop, the text floats over the game), **Font size**, **Hold the map key for** (default 600 ms; a short press opens the map as usual), **Map key** (if you rebound it in the game), **Repeat the alarm**
7. *Advanced: your own names for the exits* *(new in V1.4)* — every exit of every map, with the game's name in grey. Type what you call it (a landmark, a building) and the overlay and the evac alarm use your name; leave a field empty to keep the game's.

Press <kbd>M</kbd> (long-press) in-game once — overlay should appear. The key only acts while ARC Raiders is in front; in any other window <kbd>M</kbd> goes through untouched *(since V1.2)*.

---

## Timer Color Thresholds

The overlay colour reflects the remaining time on each evac timer. The steps are fixed; the colour of each is yours to pick:

| State | Default color | Default range |
|-------|---------------|---------------|
| **Safe** | Green | > 10 min remaining |
| **Warn** | Yellow | > 5 min remaining |
| **Caution** | Orange | > 1 min remaining |
| **Critical** | Red | ≤ 1 min remaining (alarm window) |
| **Closed** | Gray | Closed / unknown / expired |

> **Tip:** If you find yourself missing the last minute, set the **Evac alarm** to e.g. 120 seconds — it gives you an audible cue with more lead time before the evac point closes.

---

## Evac Alarm

A separate audio alarm fires the first time a timer drops below your threshold. Useful when you're not constantly looking at the overlay — gives you an audible cue to start moving.

- Configure on the Map Scanner page, card **Evac alarm**
- Threshold: *When the timer drops below* — 30 to 600 seconds
- *With sound*: on / off (off = silent, toast only); *Repeat the alarm* under *Advanced*
- Fires once per timer, then resets if the timer is re-scanned (long-press <kbd>M</kbd> again)
- Map Scanner must be active for the alarm to fire

---

## Overlay Toggle

If you want to hide the overlay temporarily (e.g. it covers a menu element you want to read):

- **Numpad ★** (default) — toggles the overlay on/off
- A toast confirms the new state
- A fresh scan (long-press <kbd>M</kbd>) re-enables the overlay automatically

The toggle key is rebindable at the top of the Map Scanner page (*Overlay key*), on the Arc Raiders page, or under **All keys**.

---

## Tips & Troubleshooting

**Q: The scan returns no timers / detects the wrong map.**
A: Make sure you're at the default map zoom. Don't pan or zoom before holding <kbd>M</kbd> — BrightRaider scrolls the map out itself. Reading is verified accurate across resolutions and aspect ratios (16:9, 16:10, ultrawide); if you ever do see a misread on an unusual setup, a screenshot of your map screen on a GitHub issue is the most useful thing to send.

**Q: I held M and it says *no map detected*.**
A: The map was not open yet, or a menu was in front (the XP screen, the skill tree). Since V1.4 the scanner says so instead of guessing — open the map and hold <kbd>M</kbd> again.

**Q: A timer reads `--:--` or jumps around.**
A: The template matcher couldn't read those digits. Trigger another scan — usually OCR catches them on the next pass. If a specific digit always misreads, start BrightRaider with the *debug scan* launcher from the tools download (it sets `BRIGHTRAIDER_DUMP=1`) — each scan is saved to `%LOCALAPPDATA%\BrightRaider\debug\` as a PNG plus a text file for diagnosis.

**Q: Closed timers stay visible / show the wrong state.**
A: The "Closed" state is set by event detection (Night Raid, Storm) or after a timer reaches 0. If the event auto-detect fails, you can long-press <kbd>M</kbd> again to retry. Event detection uses the map name region near the top of the in-game map.

**Q: Husk Graveyard shows wrong hatch state.**
A: V1.0 models Husk Graveyard as 4 EPs with Raider Hatches **open** — confirmed against the live game. If this changes in a future Arc Raiders update, open a GitHub issue.

**Q: Overlay covers my chat / HUD.**
A: Change the **Position** on the Map Scanner page, or temporarily toggle it off with Numpad ★. The overlay is click-through — it does not block any input even when visible.

**Q: The alarm sound is too quiet / too loud.**
A: The alarm uses Windows' standard system sound — adjust via Windows Volume Mixer → "System sounds". The toast itself respects your toast volume setting.

---

## How it works

Map Scanner reads your screen locally — it captures the screen and matches what it sees; nothing is read from the game itself. Before it reads, it moves the cursor onto the map and scrolls the mouse wheel to zoom out, so it does send a little input to the game. See the module's [anti-cheat notes](README.md#anti-cheat).

---

*BrightRaider — See in the Dark.*
*https://github.com/BrightRaider/BrightRaider*
