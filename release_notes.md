## V9.6 — QuickSave, Events, Crosshair Styles

### New: QuickSave [PRO]

One keypress automatically drags an item between your inventory and your Safe Pocket.

- **5 independent presets** — each with trigger key, source slot, destination slot
- **Source/destination:** Fast Swap Slots 1–8 or Safe Pocket 1–3
- **Toggle direction (⇄):** first press = slot → pocket, second press = pocket → slot
- **Open/close inventory** automatically via configurable Tab key
- **All timing configurable:** open delay, hover, drag hold, drop delay, cooldown
- **Global on/off toggle:**
  - Numpad version: `Numpad /`
  - Arrow version: `End`
- Configure under: **Settings → QuickSave... [PRO]**

📖 **[QuickSave Setup Guide](docs/QuickSave_Guide.md)** — full walkthrough, all settings explained, tips & troubleshooting.

> ⚠️ Occasional drag failures can occur — this is a known behavior of the Unreal Engine Slate UI system used by Arc Raiders, the same thing that happens with manual drag-and-drop. Just trigger again if it happens.

---

### New: Map Scanner — Event Detection

The overlay now detects and displays active world events:

| Event | Active evac points |
|---|---|
| Night | 2 |
| Electromagnetic Storm | 3 |
| Hurricane | All standard |

- Event name shown in the overlay header next to the map name
- Excess evac points shown as **CLOSED** immediately (no timer scan needed)
- **⚠ Raider Hatches closed** warning shown during Night and Storm events

---

### New: Crosshair — Outline

All crosshair styles now support an optional **outline** for better visibility on bright backgrounds.
Configure outline color independently. Enable/disable per checkbox.

---

### New: Crosshair Styles

Two new styles added:

- **Ring** — circle only, no dot
- **Cross with gap** — CS:GO-style cross with adjustable center gap

---

### Crosshair fixes

- **Dot size** is now fully adjustable via the size slider (was previously fixed at 7×7px)
- **Minimum size** reduced from 10 to 4 for all styles

---

### QuickSelect improvements

- **Apply button** — apply settings without closing the dialog
- **Mouse buttons MB3, MB4, MB5** can now be bound as the H-key (holster)
- **3 additional action slots** (total: 8) — slots 7–10 use mouse wheel position selection
- **Wheel equip delay** — configurable post-Q delay (default 300ms) before the item is used

---

### Bug Fix

- **Profile 1 settings reset on restart** — vibrance (and other values) set for Profile 1 were overwritten with the hardware default on every launch. Profiles 2–9 were not affected. (Thanks to the GitHub reporter!)

---

### Files

| File | Description |
|------|-------------|
| `BrightRaider.exe` | Numpad version (Numpad 1–9, CapsLock autorun) |
| `BrightRaider_Arrows.exe` | Arrow keys version (TKL / laptop keyboards) |

### System requirements

- Windows 10 / 11
- .NET Framework 4.7.2 (pre-installed on Windows 10/11)
- NVIDIA or AMD GPU recommended (Intel: gamma + contrast only)
