## V9.5 — QuickSelect, Evac Alarm, Break Reminder

> **Includes all features previously planned for V9.4**, which was merged into this release.

### New: QuickSelect [PRO]

Press a single key to automatically use an item from your quick-use wheel.  
BrightRaider handles the full sequence: hold Q → select slot → release Q → hold LMB → press H to free your hand.

- **5 independent action slots** — each with its own trigger key, wheel slot, LMB duration, and H-key toggle
- **6 LMB timer presets** with item names:
  - T1 · 1150ms — Adrenalin Syringe
  - T2 · 1650ms — Herb Bandage / Bandage
  - T3 · 2150ms — Shield Charger
  - T4 · 4150ms — Vita Syringe
  - T5 · 5150ms — Instant Shield Charger
  - T6 · custom (default 1000ms, up to 8000ms)
- **Q-key and H-key freely rebindable** — change to any key in the settings dialog
- **Q-Hold duration** configurable (50–2000ms, default 200ms)
- **Toggle key** — pause/resume QuickSelect instantly (Numpad: `Numpad Minus`, Arrow version: `Pos1`)
- **Per-slot on/off** — enable or disable individual slots without rebinding

📖 **[QuickSelect Setup Guide](https://github.com/BrightRaider/BrightRaider/releases/download/v9.5/QuickSelect_Guide.md)**

---

### New: Evac Alarm [PRO]

Get a red toast + sound when an evacuation timer drops below a configurable threshold.  
Set threshold in minutes + seconds. Resets automatically when you re-scan.  
Configure inside: **Map Scanner Settings → Evac Alarm**.

---

### New: Break Reminder [Free]

Configurable orange toast to remind you to take a break.  
Does not interrupt gameplay. Set interval under **Settings → Break Reminder**.

---

### Autorun improvement

Pressing **W** during autorun now stops the injected W but does **not** release it from the game.  
Your character keeps running/sprinting until you physically release W — seamless takeover.

---

### Map Scanner — 4:3 fix

**1920×1440 and other 4:3 resolutions now work correctly.**  
Arc Raiders renders in a letterboxed 16:9 area on 4:3 screens. BrightRaider now detects this and offsets the OCR scan regions accordingly.  
*(Thanks @ggvelloso for the report!)*

---

### QuickSelect improvements

- **Slot names are editable** — type a custom label for each slot directly in the settings dialog
- **Enabled by default** — no need to manually enable after install
- **New default setup:** Slot 1 = Bandage (key D4 / T2, wheel pos 3), Slot 2 = Shield Charger (key D5 / T3, wheel pos 4), Slot 3 = Adrenalin (key D6 / T1, wheel pos 5) — the way the game should have had it from the start.

---

### Performance

- Reduced CPU usage during rapid profile switching. Tray icons are now pre-rendered at startup and reused, instead of being redrawn on every keypress.

---

### Security & Antivirus

- **Improved license verification.**
- **License file moved** to `AppData\Roaming\BrightRaider\` (clean, visible path — no more hidden folder).  
  ⚠️ **V9.3/V9.4 users: one-time re-activation required** — enter your key when prompted after updating.
- **Fewer antivirus false positives** compared to previous versions.  
  BrightRaider uses a low-level keyboard hook and screen capture — the same APIs that malware uses, which is why some AV engines flag tools like this. There is no way around this without a code signing certificate (~$150/year). V9.3 was downloaded 1,400+ times without any reports of harm.  
  🔍 **[View V9.5 VirusTotal scan](https://www.virustotal.com/gui/file/179dda28e6f8ec36b48f00442f7660a0acb65386293d0fc64ae43bed5ed4e29a?nocache=1)**

---

### Also in V9.5

- Version number shown in the Exit menu item
- Toast notifications appear slightly closer to the taskbar
- Config now stored in a visible `AppData\Roaming\BrightRaider\` folder — migrated automatically from older versions

---

### Coming next: V1.0

After V9.5, the next version will be **V1.0** — a complete overhaul:

- Redesigned menus — cleaner, more intuitive, better organized
- **Alt-Tab Vibrance Auto-Switch** — automatically adjusts vibrance when you tab in/out of the game (replaces VibranceGUI)
- Better usability and in-app guidance
- Stable, polished, ready for a wider audience

---

### Files

| File | Description |
|------|-------------|
| `BrightRaider.exe` | Numpad version (Numpad 1/2/3, CapsLock autorun) |
| `BrightRaider_Arrows.exe` | Arrow keys version (TKL/laptop) |
| `QuickSelect_Guide.md` | Setup guide for the QuickSelect feature |

### System requirements

- Windows 10 / 11
- .NET Framework 4.7.2 (pre-installed on Windows 10/11)
- NVIDIA or AMD GPU recommended (Intel: gamma + contrast only)
