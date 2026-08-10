# BrightRaider V1.1.1 — Pre-release 🧪

> 🧪 **This is a pre-release for testing.** It is **not** pushed to existing users; grab it here if you'd like to help check it before it ships.
>
> **Everything carries over** — settings, profiles and your license are kept. No re-activation needed.

A bug-fix release. No new features.

---

## Fixed

- **Map conditions now show at every resolution.** Unless you played at 2560×1440, the Map Scanner never showed the condition or the hatch state — the evac timers worked, which made it look like "no event running" rather than a fault. This had been the case since V1.0. Verified at 1080p, 1440p and 4K. ([#80](https://github.com/BrightRaider/BrightRaider/issues/80))
- **"Prospecting Probes"** was missing from the condition list entirely and never showed for anyone.
- **Riven Tides:** *"Costal Lift"* → **Coastal Lift**.
- **`--selftest` and the other `--test-*` switches** printed nothing from the shipped EXE. They work now.
- **Settings → right-click the version number** now reads *"Copy system info for bug reports"* — version, Windows build, GPU and every monitor, ready to paste into an issue.

---

## 🎮 Using Map Scanner data packs?

Event fingerprints changed format. **Events trained on V1.1 or earlier need retraining** with `--pack-train-event`; old entries are skipped with a warning in the log. Maps, timers and digit templates are unaffected. See the [pack authoring guide](https://github.com/BrightRaider/BrightRaider/blob/main/docs/pack-authoring.md).

If you don't use data packs, this doesn't affect you.

---

## 🛡️ If Windows Defender or SmartScreen flags it

BrightRaider is an unsigned single-EXE, and that format can trip Defender's heuristic / SmartScreen reputation — a **known false positive**, not a real detection.

Unlike V1.1.0, **this build has not been submitted to Microsoft yet** — it's a pre-release. Choose **More info → Run anyway**, or restore it from **Windows Security → Protection history**.

## System requirements

- Windows 10 / 11 (64-bit)
- NVIDIA or AMD GPU recommended (Intel: gamma + contrast only, no vibrance / hue / FPS-limit)
- No .NET runtime install required (Native AOT — single EXE)

## SHA-256

```
BrightRaider.exe   170A5F470034E64393BBA719CF8E4D946523645C3F8F613DB065694EF8FAFABF
```

## 📦 Download + install

**Download `BrightRaider.exe` and double-click it.** On first launch the EXE unpacks its runtime into `%LOCALAPPDATA%\BrightRaider\` and starts from there. To uninstall, delete the EXE and that folder.

**Troubleshooting (optional):** `BrightRaider_tools.zip` holds two small launchers — one starts BrightRaider with Map Scanner diagnostics, the other with QuickSave's alternate input path. Drop them next to the EXE only if you're troubleshooting.

---

Thanks to @inf3rrno, whose report and debug log pinned down the resolution bug. 🙏
