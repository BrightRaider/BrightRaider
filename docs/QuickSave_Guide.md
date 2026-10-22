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
| Backpack 1–4 | Top row of the backpack, left to right (Rucksack) |
| P1–P3 | Safe Pockets (Sicherheitstasche 1–3) |

> The **From** and **To** fields are simple dropdowns — the labels are named exactly like the in-game inventory groups, so just pick **Slot 1–5**, **Augment 1–3**, **W1/W2**, **Backpack 1–4** or **P1–P3**.

[![QuickSave Slot Overview](https://raw.githubusercontent.com/BrightRaider/BrightRaider/main/assets/screenshots/Quicksave.jpg?v=3)](https://raw.githubusercontent.com/BrightRaider/BrightRaider/main/assets/screenshots/Quicksave.jpg?v=3)

> **Tip:** Use it to move consumables (bandages, syringes) into your Safe Pocket before an extraction, or retrieve them quickly at the start of a raid.

---

## Setup — Step by Step

1. Right-click the BrightRaider tray icon → **Settings…**
2. Go to **Modules → Arc Raiders → QuickSave** and switch it on (top right). **Guide** next to it shows this in short.
3. For each preset you want to use:
   - Switch it **On**
   - Click the **Key** field and press your trigger key (keyboard key or MB3/4/5 or mouse wheel)
   - Pick **From** — the source position (Slot 1–5, Augment 1–3, W1/W2 weapons, Backpack 1–4, or P1–P3 pockets; **Slot layout** shows where each one is)
   - Pick **To** — the destination position (same choices)
   - Tick **Back and forth** if the direction should reverse on every second press
   - *All columns* also shows **Opens inventory** and **Closes it** (both on by default — BrightRaider presses your inventory key before and after)
4. Click **OK** — QuickSave is active immediately

---

## Back and forth

When **Back and forth** is ticked for a preset:

- **First press** → drags From → To (e.g. Slot 3 → P1)
- **Second press** → drags To → From (e.g. P1 → Slot 3)

This lets you store and retrieve the same item with the same key.

---

## All Settings Explained

> **Most important for reliability: Drop hold** (*Advanced: inventory key and timings*). This is the single setting that decides whether a save lands. It's the window where the game actually registers the drop — the item has to dwell on the destination slot long enough for Arc Raiders to accept it before the button releases. If your saves are inconsistent, raise **Drop** first. *Open delay* only matters if the inventory is still opening when the grab starts, so on a fast-opening setup it does nothing — Drop is the one that counts.

| Setting | Description |
|---------|-------------|
| **On / off** (top right) and **On/off key** | Master switch for all presets; the key pauses/resumes QuickSave while you play (Numpad /) |
| **On** (per preset) | Enable/disable each preset individually |
| **Key** | The key that triggers this preset (keyboard, MB3/4/5, or mouse wheel) |
| **From** | Source position — Slot 1–5 / Augment 1–3 (inventory), W1/W2 (weapons), Backpack 1–4 or P1–P3 (pockets) |
| **To** | Destination position — same choices |
| **Back and forth** | Reverse direction on every second press. A press while a move is still running is ignored and does not turn the direction *(since V1.4)* |
| **Opens inventory** *(All columns)* | Press the inventory key before dragging |
| **Closes it** *(All columns)* | Press the inventory key after dragging |
| **Inventory key** *(Advanced)* | The key used to open/close inventory (default: Tab). Rebind if your in-game binding is different. |
| **Wait after opening** *(Advanced)* | Wait after opening before moving the cursor (default: 250 ms). Increase if the inventory opens slowly. |
| **Hover before grab** *(Advanced)* | Extra wait after the cursor arrives at the source before picking up (default: 0 ms) |
| **Drag delay** *(Advanced)* | Extra time while dragging (default: 0 ms) |
| **Drop hold** *(Advanced)* | **The key reliability setting.** Wait after the cursor arrives at the destination before releasing — this is the window the game uses to register the drop (default: 120 ms). If saves are inconsistent, raise this first. Lower values (e.g. 20 ms) can work if **Cooldown** is set high enough to prevent back-to-back triggers before the inventory settles. |
| **Cooldown between presses** *(Advanced)* | Minimum time between two triggers (default: 0 ms, disabled) |

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

**Q: Which inventory does QuickSave aim at?**  
A: The one you see in a raid. On 16:9 and wider screens the hideout looks the same. On 4:3 and 16:10 the hideout inventory sits one row higher, so there QuickSave lands a row low in the hideout — use it in raids. **W1/W2** grab the middle of the weapon, not an attachment *(since V1.2.1)*.

**Q: The key does nothing on the desktop or in another game.**  
A: That is on purpose: QuickSave only acts while ARC Raiders is in front *(since V1.2)*. Everywhere else the key goes through untouched.

**Q: The item isn't being picked up.**  
A: Increase **Open delay** (try 300–400 ms). The inventory may not be fully open when the cursor arrives.

**Q: The drag fails occasionally.**  
A: This is a known behavior of the Unreal Engine Slate UI system used by Arc Raiders — the same thing that happens with normal manual drag-and-drop in the inventory. It cannot be fixed from outside the game. Just trigger again.

**Q: I want to move the item without opening/closing the inventory manually.**  
A: Make sure **Opens inventory** and **Closes it** are both ticked (*All columns*), and the **Inventory key** (*Advanced*) is set to the correct key.

**Q: I want to store an item and retrieve it with the same key.**  
A: Tick **Back and forth** for that preset. First press stores, second press retrieves.

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
