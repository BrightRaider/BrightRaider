# BrightRaider — QuickSelect Guide

**QuickSelect** is a Pro feature that lets you use items from your quick-use wheel with a single keypress.  
Instead of manually opening the wheel, selecting an item, and holding Left Mouse Button —  
**one key does everything.**

---

## How It Works

When you press a QuickSelect trigger key, BrightRaider automatically performs this sequence:

1. **Hold Q** — your quick-use wheel opens
2. **Press slot key** — the correct item is selected
3. **Release Q** — item is now in your hand
4. **Hold Left Mouse Button** — item is used for the configured duration
5. **Press H** (optional, per slot) — hand is freed after use

All timings are configurable. Everything happens in the background while you stay in control of your mouse and view.

---

## Requirements

- **BrightRaider Pro license** (€5.49 one-time)
- BrightRaider V9.5 or later

---

## Setup — Step by Step

1. Right-click the BrightRaider tray icon  
2. Go to **Settings → QuickSelect... [PRO]**  
3. Check **"Enable QuickSelect"**  
4. For each slot you want to use:
   - Click **"Click to bind..."** and press your trigger key (e.g. `3`, `4`, `5`)
   - Set the **Slot** number — this must match your in-game quick-use wheel slot
   - Pick an **LMB Timer** that matches your item type (see table below)
   - Check the **Active** box (✓) to enable this slot
   - Check **H** if you want your hand freed after item use
5. Click **OK** — QuickSelect is active immediately

---

## LMB Timer Presets

Each item type requires a different hold duration to be fully used.  
Pick the timer that matches your item:

| Timer | Duration | Item |
|-------|----------|------|
| T1 | 1150 ms | Adrenalin Syringe / Adrenalinspritze |
| T2 | 1650 ms | Herb Bandage / Bandage (Kräuterverband / Verband) |
| T3 | 2150 ms | Shield Charger / Schildauflader |
| T4 | 4150 ms | Vita Syringe / Vita-Spritze |
| T5 | 5150 ms | Instant Shield Charger / Sofort-Schildauflader |
| T6 | custom | Free timer — set your own value (100–8000 ms) |

> **Tip:** If an item is only partially used, try the next timer up (e.g. T2 instead of T1).

---

## All Settings Explained

| Setting | Description |
|---------|-------------|
| **Enable QuickSelect** | Master on/off switch |
| **Toggle Key** | Pause/resume QuickSelect without opening the dialog (Numpad: Numpad Minus, Arrow: Pos1/Home) |
| **Q-Key** | The key that opens your quick-use wheel (default: Q). Rebind if your in-game binding is different. |
| **H-Key** | The key that frees your hand after item use (default: H). |
| **Q-Hold** | How long Q is held before the slot key is pressed (50–2000 ms, default 200 ms). Increase if the wheel doesn't open reliably. |
| **Active (✓)** | Enable/disable each slot individually without rebinding |
| **Trigger key** | The key you press to trigger this slot |
| **Slot** | Which wheel slot to select (1–9) |
| **Timer** | Which LMB duration preset to use |
| **H (per slot)** | Whether to press H after item use for this specific slot |

---

## Default Configuration

| Slot | Trigger Key | Default Slot | Default Timer |
|------|-------------|--------------|---------------|
| 1 | 3 | 3 | T1 — Adrenalin Syringe |
| 2 | 4 | 4 | T2 — Herb Bandage |
| 3 | 5 | 5 | T3 — Shield Charger |
| 4 | (unbound) | 6 | T4 — Vita Syringe |
| 5 | (unbound) | 7 | T5 — Instant Shield Charger |

Slots 4 and 5 are disabled by default. Bind a key and check Active to use them.

---

## Toggling QuickSelect On/Off

Press the **Toggle Key** (default: `Numpad Minus` / `Pos1`) to pause or resume QuickSelect.  
A toast notification confirms the state change: **QuickSelect ON** / **QuickSelect OFF**.

Use this when:
- You're in the lobby and don't want 3/4/5 intercepted
- You're typing in chat
- You need the keys for something else temporarily

---

## Tips & Troubleshooting

**Q: The item is only partially used.**  
A: Increase the LMB timer. Try the next preset up, or use T6 with a custom value.

**Q: The wheel doesn't open / closes too quickly.**  
A: Increase Q-Hold (try +50ms). Default is 200ms; some systems need 250–350ms.

**Q: My trigger key (3/4/5) still reaches the game.**  
A: Make sure QuickSelect is enabled and the slot is active (✓ checked). The key is suppressed only when active.

**Q: The wrong slot is being selected.**  
A: Check the Slot number — it must match your in-game quick-wheel assignment. Slots are numbered 1–9.

**Q: I don't want to press H every time.**  
A: Uncheck the H box for that slot in the settings dialog.

**Q: Can I use a different key than Q to open the wheel?**  
A: Yes. In the dialog, click the **Q-Key** bind button and press your preferred key.

---

## In-Game Wheel Slot Numbers

In Arc Raiders, your quick-use wheel slots are numbered 1–9.  
The number corresponds to the position on the wheel. Check your in-game keybindings  
to see which number maps to which item slot, then set the Slot field accordingly.

---

## Safety Note

QuickSelect only intercepts the configured trigger keys **while it is enabled and active**.  
It does **not** intercept keys in menus, the desktop, or other applications —  
only when the trigger key is pressed and a game window is in focus.

The H key and Q key are also only sent to the **currently focused window** (the game).

---

*BrightRaider — See in the Dark. No Ban.*  
*https://github.com/BrightRaider/BrightRaider*
