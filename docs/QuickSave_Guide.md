# BrightRaider — QuickSave Guide

**QuickSave** is a Pro feature that automatically drags items between your inventory slots and your Safe Pocket with a single keypress.  
Open inventory → drag item → close inventory — **one key does everything.**

---

## How It Works

When you press a QuickSave trigger key, BrightRaider performs this sequence automatically:

1. **Press Tab** — inventory opens
2. **Move cursor** to the source slot
3. **Hold Left Mouse Button** — picks up the item
4. **Move cursor** to the destination slot
5. **Release Left Mouse Button** — drops the item
6. **Press Tab** — inventory closes

The direction can be reversed per preset, and each step's timing is configurable.

---

## Requirements

- **BrightRaider Pro license** (€5.49 one-time)
- BrightRaider V9.6 or later

---

## What Can Be Moved

QuickSave can drag items between any of these positions:

| Label | In-game position |
|-------|----------|
| Slot 1–5 | Quick-deploy slots (Schnelleinsatz 1–5) |
| Augment 1–3 | Augment slots (Augment-Plätze 1–3) |
| W1–W2 | Weapon slots (Ausrüstung) |
| P1–P3 | Safe Pockets (Sicherheitstasche 1–3) |

> The **From** and **To** fields are simple dropdowns — the labels are named exactly like the in-game inventory groups, so just pick **Slot 1–5**, **Augment 1–3**, **W1/W2** or **P1–P3**.

[![QuickSave Slot Overview](https://raw.githubusercontent.com/BrightRaider/BrightRaider/main/assets/screenshots/Quicksave.jpg?v=2)](https://raw.githubusercontent.com/BrightRaider/BrightRaider/main/assets/screenshots/Quicksave.jpg?v=2)

> **Tip:** Use it to move consumables (bandages, syringes) into your Safe Pocket before an extraction, or retrieve them quickly at the start of a raid.

---

## Setup — Step by Step

1. Right-click the BrightRaider tray icon
2. Go to **Settings → QuickSave... [PRO]**
3. Check **"Enable QuickSave"**
4. For each preset you want to use:
   - Click **"Click to bind..."** and press your trigger key (keyboard key or MB3/4/5 or mouse wheel)
   - Pick **From** — the source position (Slot 1–5, Augment 1–3, W1/W2 weapons, or P1–P3 pockets)
   - Pick **To** — the destination position (Slot 1–5, Augment 1–3, W1/W2, or P1–P3)
   - Check **Active** (✓) to enable this preset
   - Check **Tab open** if you want BrightRaider to open the inventory with Tab first
   - Check **Tab close** if you want BrightRaider to close the inventory with Tab after
   - Check **⇄ Toggle** if you want the direction to reverse on alternating presses
5. Click **OK** — QuickSave is active immediately

---

## Toggle Direction (⇄)

When **Toggle** is checked for a preset:

- **First press** → drags From → To (e.g. Slot 3 → P1)
- **Second press** → drags To → From (e.g. P1 → Slot 3)

This lets you store and retrieve the same item with the same key.

---

## All Settings Explained

> **Most important for reliability: Drop Hold.** This is the single setting that decides whether a save lands. It's the window where the game actually registers the drop — the item has to dwell on the destination slot long enough for Arc Raiders to accept it before the button releases. If your saves are inconsistent, raise **Drop** first. *Open delay* only matters if the inventory is still opening when the grab starts, so on a fast-opening setup it does nothing — Drop is the one that counts.

| Setting | Description |
|---------|-------------|
| **Enable QuickSave** | Master on/off switch for all presets |
| **Inventory key (Tab)** | The key used to open/close inventory (default: Tab). Rebind if your in-game binding is different. |
| **Active (✓)** | Enable/disable each preset individually |
| **Trigger key** | The key that triggers this preset (keyboard, MB3/4/5, or mouse wheel) |
| **From** | Source position — pick Slot 1–5 / Augment 1–3 (inventory), W1/W2 (weapons) or P1–P3 (pockets) |
| **To** | Destination position — pick Slot 1–5 / Augment 1–3, W1/W2 or P1–P3 |
| **Tab open** | Press Tab before dragging (opens inventory) |
| **Tab close** | Press Tab after dragging (closes inventory) |
| **⇄ Toggle** | Reverse direction on alternating presses |
| **Open delay** | Wait after Tab before moving cursor (default: 250 ms). Increase if the inventory opens slowly. |
| **Hover** | Extra wait after cursor arrives at source before picking up (default: 0 ms) |
| **Hold** | How long LMB is held while dragging (default: 0 ms) |
| **Drop** | **The key reliability setting.** Wait after the cursor arrives at the destination before releasing — this is the window the game uses to register the drop (default: 120 ms). If saves are inconsistent, raise this first. Lower values (e.g. 20 ms) can work if **Cooldown** is set high enough to prevent back-to-back triggers before the inventory settles. |
| **Cooldown** | Minimum time between two triggers (default: 0 ms, disabled) |

---

## Toggling QuickSave On/Off

Press the **toggle key** to enable or disable QuickSave globally without opening any dialog.

| Version | Toggle Key |
|---------|-----------|
| Numpad (`BrightRaider.exe`) | `Numpad /` |
| Arrow (`BrightRaider_Arrows.exe`) | `End` |

A toast notification confirms the state: **QuickSave ON** / **QuickSave OFF**.

---

## Tips & Troubleshooting

**Q: The item lands in the wrong slot / the drag fails.**  
A: Two options: increase **Drop** delay (120 ms is safe), or keep a low Drop delay and set a **Cooldown** instead (e.g. 500 ms). The Cooldown prevents back-to-back triggers before the inventory has settled — which has the same effect as a longer Drop delay but keeps the drag itself snappy.

**Q: The item isn't being picked up.**  
A: Increase **Open delay** (try 300–400 ms). The inventory may not be fully open when the cursor arrives.

**Q: The drag fails occasionally.**  
A: This is a known behavior of the Unreal Engine Slate UI system used by Arc Raiders — the same thing that happens with normal manual drag-and-drop in the inventory. It cannot be fixed from outside the game. Just trigger again.

**Q: I want to move the item without opening/closing the inventory manually.**  
A: Make sure **Tab open** and **Tab close** are both checked, and the **Inventory key** is set to the correct key.

**Q: I want to store an item and retrieve it with the same key.**  
A: Check **⇄ Toggle** for that preset. First press stores, second press retrieves.

**Q: Can I bind a mouse button or mouse wheel?**  
A: Yes. In the preset trigger bind, press **MB3** (middle click), **MB4**, **MB5**, or **scroll the wheel** up or down.

**Q: The trigger key reaches the game even though QuickSave is enabled.**  
A: Check that the preset is marked **Active** (✓ checkbox). Inactive presets don't intercept their trigger keys.

---

## Known Limitation

Drag-and-drop in Arc Raiders uses the Unreal Engine Slate UI system, which does not always respond consistently to simulated mouse events. The same intermittent failure can occur with real manual drags in the inventory — it is a game-side behavior and cannot be fixed from outside. If a drag fails, simply press the trigger key again.

---

*BrightRaider — See in the Dark.*  
*https://github.com/BrightRaider/BrightRaider*
