## BrightRaider V9.1 — Map Scanner Accuracy + Ultrawide Fix

**Map Scanner (Pro):**
- **OCR Upscaling** — timer regions are now upscaled 3× before OCR. This should have been in V9.0 from the start. Windows OCR needs a minimum font size to read reliably — without upscaling, small timer numbers on high-res screens were often missed or misread. With 3× upscaling the scanner is significantly more accurate and robust across all resolutions.
- Fixed: OCR coordinates now correct on ultrawide screens (21:9 — e.g. 3440×1440, 2560×1080)
- The scanner now reads from the selected monitor, not always the primary

*Thanks to @seanthespartan for reporting the ultrawide map scanner issue.*

---

> 💡 **Numpad version tip:** Keep NumLock **off** while playing. With NumLock on, Windows briefly interrupts the Shift key during profile switches — this can slow your character if Shift is your sprint key.

> ⚠️ **Existing Pro users:** No re-activation needed — your license carries over from V8.x and V9.0.
