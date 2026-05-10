## V9.6.1 — Hotfix: Mouse stutter & game input lag

This is a targeted hotfix for the stutter/lag issues reported while BrightRaider is running.

---

### Fixed: Mouse stutter and input lag

**Symptom:** Games stutter, mouse movement feels laggy or freezes briefly while BrightRaider is running. Turning BrightRaider off fixes it immediately.

**Root cause:** Config saves and tray menu rebuilds were running synchronously inside the Windows Low-Level Keyboard Hook callback. Windows enforces a hard ~300ms timeout on these callbacks — if they take longer, the entire input system stalls. This affected mouse movement, key presses, and game input globally.

**Fix:** Both operations are now dispatched asynchronously (off the hook thread). The hook callback returns immediately, and Windows never hits the timeout.

> This was more pronounced on high-end GPUs (RTX 5080, etc.) and 4K displays where the menu rebuild takes slightly longer.

---

### Fixed: Auto-Brightness GPU wake-ups (Optimus laptops)

**Symptom:** Micro-stutter, slight CPU/GPU spike every second while Auto-Brightness is active.

**Root cause:** `SetDeviceGammaRamp` was called on every Auto-Brightness tick (~1/sec) even when the brightness and contrast values hadn't changed. On Optimus laptops (NVIDIA dGPU + Intel iGPU), this call can wake the discrete GPU from a low-power state unnecessarily.

**Fix:** BrightRaider now caches the last applied gamma/contrast values and skips the GPU call entirely when nothing has changed.

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
