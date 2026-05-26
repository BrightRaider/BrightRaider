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

| Event | Effect on overlay |
|-------|-------------------|
| Night Raid | Marks 2 evac points active, rest CLOSED. Shows "⚠ Raider Hatches closed" warning. |
| Hurricane | All evac points stay active. |
| Electromagnetic Storm | Marks 3 active, rest CLOSED. Shows the hatch warning. |
| Harvester | Normal scan, event name displayed |
| Lush Blooms | Normal scan, event name displayed |
| Matriarch | Normal scan, event name displayed |
| Husk Graveyard | 4 evac points, Raider Hatches OPEN |
| Close Scrutiny | Normal scan, event name displayed |
| Bird City | Normal scan, event name displayed |
| Locked Gate | Normal scan, event name displayed |
| Launch Tower Loot | Normal scan, event name displayed |
| Beachcombing | Normal scan, event name displayed |

The event name appears below the map name in the overlay.

---

## Setup — Step by Step

1. Right-click the BrightRaider tray icon
2. **Settings → Map Scanner [PRO]**
3. Set the **Hold duration** — how long to hold <kbd>M</kbd> before the scan triggers (default 350 ms)
4. Pick an **Overlay position** — 6 screen corners + center top / center bottom
5. Adjust **Font size** and **Map name color** if you want
6. Drag the **Background opacity** slider — fully transparent up to fully opaque
7. Open the **Timer colors** expander to set the 5 threshold colors (see below)
8. Set the **Evac Alarm** threshold (minutes + seconds) and enable the sound

Press <kbd>M</kbd> (long-press) in-game once — overlay should appear.

---

## Timer Color Thresholds

The overlay color reflects the remaining time on each evac timer. You can tune each threshold + color independently:

| State | Default color | Default range |
|-------|---------------|---------------|
| **Safe** | Green | > 10 min remaining |
| **Warn** | Yellow | > 5 min remaining |
| **Caution** | Orange | > 1 min remaining |
| **Critical** | Red | ≤ 1 min remaining (alarm window) |
| **Closed** | Gray | Closed / unknown / expired |

> **Tip:** If you find yourself missing the 1-minute warning, raise the **Critical** threshold to e.g. 90 seconds — gives you more lead time before the evac point closes.

---

## Evac Alarm

A separate audio alarm fires the first time a timer drops below your threshold. Useful when you're not constantly looking at the overlay — gives you an audible cue to start moving.

- Configure in: **Settings → Map Scanner → Evac Alarm section**
- Threshold: separate **minutes** (0–59) and **seconds** (0–59) fields
- Sound toggle: on / off (off = silent, toast only)
- Fires once per timer, then resets if the timer is re-scanned (long-press <kbd>M</kbd> again)
- Map Scanner must be active for the alarm to fire

---

## Overlay Toggle

If you want to hide the overlay temporarily (e.g. it covers a menu element you want to read):

- **Numpad ★** (default) — toggles the overlay on/off
- A toast confirms the new state
- A fresh scan (long-press <kbd>M</kbd>) re-enables the overlay automatically

The toggle key is rebindable in **Settings → Hotkeys**.

---

## Tips & Troubleshooting

**Q: The scan returns no timers / detects the wrong map.**
A: Make sure you're at the default map zoom. Don't pan or zoom before holding <kbd>M</kbd> — BrightRaider scrolls the map out itself. If your monitor has a non-standard aspect ratio, the timer rectangles may need adjustment (open a GitHub issue with a screenshot).

**Q: A timer reads `--:--` or jumps around.**
A: The template matcher couldn't read those digits. Trigger another scan — usually OCR catches them on the next pass. If a specific digit always misreads, set the environment variable `BRIGHTRAIDER_DUMP=1` before launching BrightRaider — failed regions are dumped to `%APPDATA%\BrightRaider\debug\` as PNGs for diagnosis.

**Q: Closed timers stay visible / show the wrong state.**
A: The "Closed" state is set by event detection (Night Raid, Storm) or after a timer reaches 0. If the event auto-detect fails, you can long-press <kbd>M</kbd> again to retry. Event detection uses the map name region near the top of the in-game map.

**Q: Husk Graveyard shows wrong hatch state.**
A: V1.0 models Husk Graveyard as 4 EPs with Raider Hatches **open** — confirmed against the live game. If this changes in a future Arc Raiders update, open a GitHub issue.

**Q: Overlay covers my chat / HUD.**
A: Change the overlay position in Settings, or temporarily toggle it off with Numpad ★. The overlay is click-through — it does not block any input even when visible.

**Q: The alarm sound is too quiet / too loud.**
A: The alarm uses Windows' standard system sound — adjust via Windows Volume Mixer → "System sounds". The toast itself respects your toast volume setting.

---

## Safety Note

Map Scanner is **read-only** — it captures screen pixels and runs template matching. It does not inject into the game, does not read game memory, does not touch the network connection. EAC-safe; uses the same approach as Discord overlays and OBS screen capture.

---

*BrightRaider — See in the Dark. No Ban.*
*https://github.com/BrightRaider/BrightRaider*
