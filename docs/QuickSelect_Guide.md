# BrightRaider — QuickSelect Guide

**QuickSelect** is a Pro feature that lets you use items from your quick-use wheel with a single keypress.  
Instead of manually opening the wheel, selecting an item, and holding Left Mouse Button —  
**one key does everything.**

[![QuickSelect wheel slot numbers](https://raw.githubusercontent.com/BrightRaider/BrightRaider/main/assets/screenshots/Quickselect.jpg)](https://raw.githubusercontent.com/BrightRaider/BrightRaider/main/assets/screenshots/Quickselect.jpg)

> The **Wheel** number you set per slot matches these in-game wheel positions:
> **1–2** weapons · **3–6** number-key slots · **7–10** mouse-only. More detail at the bottom.

---

## How It Works

When you press a QuickSelect trigger key, BrightRaider automatically performs this sequence:

1. **Hold Q** — your quick-use wheel opens *(optional, per slot)*
2. **Press slot key** — the correct item is selected *(optional, per slot)*
3. **Release Q** — item is now in your hand *(optional, per slot)*
4. **Hold Left Mouse Button** — item is used for the configured duration *(optional, per slot)*
5. **Press H** (optional, per slot) — hand is freed after use

Each step is independently controllable. Everything happens in the background while you stay in control of your mouse and view.

---

## Requirements

- **BrightRaider Pro license** (€5.49 one-time)
- BrightRaider V1.3 or later for the steps below (the settings window changed in V1.3)

---

## Setup — Step by Step

1. Right-click the BrightRaider tray icon → **Settings…**
2. Go to **Modules → Arc Raiders → QuickSelect** and switch it on (top right). **Guide** next to it shows this in short.
3. In **Setup — match your keys in Arc Raiders**, check once that *Wheel*, *Holster*, *Weapon 1* and *Weapon 2* match your in-game keys
4. Under **Your items**, for each item you want:
   - Click the **Key** field and press your trigger key (e.g. `4`, `5`, `6`)
   - Type the **Item** name (just a label for you)
   - Set the **Slot** — this must match your in-game quick-use wheel slot (**Slot layout** shows which is which)
   - Type **Hold ms** — how long LMB is held to use the item (see recommended values below; **Timing reference** lists them too)
   - Switch it **On**
   - *All columns* also shows **Opens wheel** (uncheck if the item is already in hand), **Use (LMB)** (uncheck for select-only) and **After use** (holster or back to a weapon)
5. Click **Apply** to save without closing, or **OK** to save and close

---

## Recommended LMB hold times

Each item type needs a different hold duration to be fully used. Type the value
directly into the item's **Hold ms** field — these are solid starting points
(the same list is behind the **Timing reference** button on the page):

| Item | Hold ms |
|------|-----------|
| Adrenaline Syringe | 1300 |
| Herb Bandage | 1800 |
| Shield Charger | 2300 |
| Vita Syringe | 4300 |
| Instant Shield Charger | 5300 |

> **Tip:** If an item is only partially used, add 100–200 ms. Arc Raiders timing varies even on strong hardware.

---

## All Settings Explained

| Setting | Description |
|---------|-------------|
| **On / off** (top right) | Master switch |
| **On/off key** (top) | Pause/resume QuickSelect while you play (Numpad: Numpad Minus) |
| **Wheel** (Setup) | The key that opens your quick-use wheel (default: Q). Rebind if your in-game binding is different. |
| **Holster** (Setup) | The key that frees your hand after item use (default: H). Can also be bound to MB3, MB4, or MB5. |
| **Weapon 1 / 2** (Setup) | Your weapon keys, for *After use → back to a weapon* |
| **Hold wheel (ms)** (Setup) | How long the wheel key is held before the slot is picked (50–2000 ms, default 200 ms). Increase if the wheel doesn't open reliably. |
| **Equip (ms)** (Setup) | Wait after picking before using (default 300 ms) |
| **On** (per item) | Enable/disable each item individually without rebinding |
| **Key** | The key you press to trigger this item |
| **Slot** | Which in-game wheel slot to select (1–2 = weapons, 3–6 = number-key wheel, 7–10 = mouse-only) |
| **Hold ms** | LMB hold time in milliseconds — typed directly per item |
| **Opens wheel** *(All columns)* | Whether to open the wheel first. Uncheck if the item is already in hand. |
| **Use (LMB)** *(All columns)* | Whether to auto-use the item. Uncheck for select-only — picks the item without triggering use. |
| **After use** *(All columns)* | Nothing, holster, or back to weapon 1 / 2 |

---

## Default Configuration

| Item | Name | Key | Slot | Hold ms |
|------|------|-------------|-------|-----------|
| 1 | Herb Bandage | key 4 | 3 | 1800 |
| 2 | Shield Charger | key 5 | 4 | 2300 |
| 3 | Adrenaline | key 6 | 5 | 1300 |
| 4 | Vita Syringe | (unbound) | 6 | 4300 |
| 5 | Instant Shield Charger | (unbound) | 7 | 5300 |
| 6–8 | (empty) | (unbound) | 8–10 | 1000–1300 |

Slots 4–8 are disabled by default — they come pre-filled so you only need to bind a key and check Active.

> **Note:** Slots 7–10 use mouse movement to select the wheel position while Q is held — no additional key press is required for slot selection. Slots 3–6 are selected by their in-game number key (mouse-immune), so moving the mouse during the select can't pull them off-target.

---

## Modifier Bindings (Ctrl / Alt / Shift)

Every Key field accepts modifier + key combinations:

1. Click the Key field — it shows **"Press a key… (Esc cancels)"**
2. Hold <kbd>Ctrl</kbd>, <kbd>Alt</kbd>, and/or <kbd>Shift</kbd>
3. Press the trigger key — e.g. `Shift+5`, `Ctrl+Numpad 5`, `Alt+F2`
4. The field shows the full binding (`Shift+5`); hover for the tooltip if the column truncates the label

**Bindable:**
- Letters A–Z, digits 0–9, F1–F24, Numpad keys, OEM punctuation
- Arrow keys, nav keys (Home, End, PageUp, PageDown, Insert, Delete)
- Mouse: <kbd>MB3</kbd> (middle), <kbd>MB4</kbd>, <kbd>MB5</kbd>
- Wheel: scroll up, scroll down

**Not bindable (modifier-only / UI-reserved):**
- Bare <kbd>Shift</kbd>, <kbd>Ctrl</kbd>, <kbd>Alt</kbd>, <kbd>Win</kbd> (use them as modifiers, not as the main key)
- <kbd>Esc</kbd> (cancels the rebind), <kbd>Tab</kbd> (focus traversal), <kbd>Enter</kbd> (default button)

**OS-reserved combos** — you can pick them, but Windows handles them before BrightRaider sees the press. A warning toast appears in these cases: <kbd>Win+L</kbd>, <kbd>Win+D</kbd>, <kbd>Win+E</kbd>, <kbd>Win+R</kbd>, <kbd>Win+S</kbd>, <kbd>Win+I</kbd>, <kbd>Win+Tab</kbd>, <kbd>Alt+F4</kbd>, <kbd>Alt+Tab</kbd>, <kbd>Ctrl+Alt+Del</kbd>, <kbd>Ctrl+Shift+Esc</kbd>.

### Sprint Conflict (read this if you use Shift+ bindings)

If you bind a Shift+ combo (e.g. `Shift+5`) **and** Sprint is bound to Shift (the default for many shooters incl. Arc Raiders), the binding **will fire every time you sprint in-game**. BrightRaider shows a 10-second warning toast when you create such a binding.

Two ways out:
- Use `Ctrl+` or `Alt+` instead of `Shift+`
- Rebind Sprint to a non-Shift key under **Settings → Movement → Your game's keys**

---

## Toggling QuickSelect On/Off

Press the **Toggle Key** (default: `Numpad Minus` / `Pos1`) to pause or resume QuickSelect.  
A toast notification confirms the state change: **QuickSelect ON** / **QuickSelect OFF**.

Use this when:
- You're in the lobby and don't want 4/5/6 intercepted
- You're typing in chat
- You need the keys for something else temporarily

---

## Tips & Troubleshooting

**Q: The item is only partially used.**  
A: Increase the slot's **Timer (ms)** by 100–200 ms until the item is fully used.

**Q: The wheel doesn't open / closes too quickly.**  
A: Increase Q-Hold (try +50ms). Default is 200ms; some systems need 250–350ms.

**Q: My trigger key (4/5/6) still reaches the game.**  
A: Make sure QuickSelect is enabled and the slot is active (✓ checked). The key is suppressed only when active.

**Q: The wrong slot is being selected.**  
A: Check the Wheel number — it must match your in-game quick-wheel assignment. Slots are numbered 1–10 (1–2 = weapons, 3–6 = number-key wheel slots, 7–10 = mouse-only).

**Q: I don't want to press H every time.**  
A: Uncheck the H box for that slot in the settings dialog.

**Q: I just want to select the item without using it.**  
A: Uncheck the **Use** box for that slot. BrightRaider will open the wheel and select the item, but won't press LMB.

**Q: The item is already in my hand — I just want to use it without going through the wheel.**  
A: Uncheck the **Q** box for that slot. BrightRaider will skip wheel selection and go straight to LMB + H.

**Q: Can I use a different key than Q to open the wheel?**  
A: Yes. In the dialog, click the **Q-Key** bind button and press your preferred key.

**Q: After pressing H to cancel, QuickSelect just pulls the item out without using it.**  
A: The game keeps the item in a transitional state briefly after an H-cancel. Wait a moment before triggering QuickSelect again — this is game-side behavior, not a BrightRaider issue.

---

## In-Game Wheel Slot Numbers

In Arc Raiders the slot numbers map like this:

- **1–2** — weapons (not on the quick-use wheel)
- **3–6** — wheel slots that have an in-game number key
- **7–10** — wheel positions reachable by mouse only (no number key, console-style layout)

The number corresponds to the position on the wheel. Check your in-game keybindings
to see which number maps to which item slot, then set the Wheel field accordingly.
(The wheel image is at the top of this guide.)

---

## Anti-Cheat & Detection

QuickSelect sends input to the game, so unlike BrightRaider's display features it isn't purely passive — worth knowing if you play a title with kernel-level / behavioural anti-cheat (Arc Raiders now runs EAC + Denuvo Anti-Cheat + Anybrain). BrightRaider uses **no kernel driver and no injection**, but automated input is something behavioural detection can in principle flag, like any input-automation tool.

The slot type matters here:

- **Number-key slots (3–6)** are selected by their in-game digit — **no cursor movement at all**. Simulated mouse movement is the strongest thing behavioural detection keys on, so a keyboard-only selection is a weaker signal.
- **Mouse slots (7–10)** position the cursor on the wheel for you — that *is* simulated mouse movement, the more exposed path. (AZERTY layouts fall back to the mouse path for 3–6 too, since the digits need Shift in-game.)

If you want to keep your exposure as low as possible in an anti-cheat title, prefer the number-key slots (3–6). As always these features are optional — the display, FPS and overlay features send no input to the game. Use input automation at your own discretion.

---

## Safety Note

QuickSelect only intercepts the configured trigger keys **while it is enabled and active**.  
It does **not** intercept keys in menus, the desktop, or other applications —  
it only acts while ARC Raiders is in front *(since V1.2)*; in any other window the key goes through untouched.

The H key and Q key are also only sent to the **currently focused window** (the game).

---

*BrightRaider — See in the Dark.*  
*https://github.com/BrightRaider/BrightRaider*
