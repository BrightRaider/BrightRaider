# BrightRaider V1.1.1 — Pre-release 🧪

> 🧪 **This is a pre-release for testing.** It is **not** pushed to existing users; grab it here if you'd like to help check it before it ships.
>
> **Everything carries over** — settings, profiles and your license are kept. No re-activation needed.

A bug-fix release. No new features.

---

## 🗺️ The Map Scanner only recognised conditions at 2560×1440

If you play at **1920×1080, 4K, ultrawide** — anything other than 1440p — the Map Scanner never showed the map condition, and never showed whether the hatches were open. Evac timers worked fine, which is what made it look like "no event is running" rather than a fault.

It has been this way **since V1.0**, on every resolution except the one BrightRaider is developed on. That's why it took six weeks and a good bug report ([#80](https://github.com/BrightRaider/BrightRaider/issues/80)) to surface.

The condition is identified by fingerprinting the event icon, and that fingerprint was resolution-dependent. It now works at any resolution — verified against 53 captures taken at 1920×1080, 2560×1440 and 3840×2160.

## 🔍 "Prospecting Probes" was never recognised

The condition was missing from the lookup table entirely, so it showed as no event at all — on every resolution, including 1440p. It's in now.

## Smaller fixes

- **Riven Tides:** the evac point read *"Costal Lift"*; it's **Coastal Lift**.
- **Diagnostics work from the shipped EXE.** `BrightRaider.exe --selftest` and the other `--test-*` switches printed nothing at all before — useful when a bug report needs more than a log.
- **Settings → right-click the version number** now reads *"Copy system info for bug reports"*, which is what it does: version, Windows build, GPU and every monitor with its resolution and scaling, ready to paste into an issue.

---

## 🎮 Map Scanner data packs — action needed

Event fingerprints changed format. **Packs with events trained on V1.1 or earlier need those events retrained** with `--pack-train-event`; the old entries are skipped with a warning in the log. Maps, timers and digit templates are unaffected. See the [pack authoring guide](https://github.com/BrightRaider/BrightRaider/blob/main/docs/pack-authoring.md).

If you don't use data packs, this doesn't affect you.

---

## 🛡️ If Windows Defender or SmartScreen flags it

BrightRaider is an unsigned single-EXE, and that format can trip Defender's heuristic / SmartScreen reputation — a **known false positive**, not a real detection.

Unlike the V1.1.0 build, **this one has not been submitted to Microsoft for analysis yet** — it's a pre-release. Expect a warning; choose **More info → Run anyway**, or restore it from **Windows Security → Protection history**.

## System requirements

- Windows 10 / 11 (64-bit)
- NVIDIA or AMD GPU recommended (Intel: gamma + contrast only, no vibrance / hue / FPS-limit)
- No .NET runtime install required (Native AOT — single EXE)

## SHA-256

```
BrightRaider.exe   170A5F470034E64393BBA719CF8E4D946523645C3F8F613DB065694EF8FAFABF
```

Verify on Windows: `Get-FileHash BrightRaider.exe -Algorithm SHA256`

## 📦 Download + install

**Download `BrightRaider.exe` and double-click it.** On first launch the EXE unpacks its runtime into `%LOCALAPPDATA%\BrightRaider\` and starts from there. To uninstall, delete the EXE and that folder.

**Troubleshooting (optional):** `BrightRaider_tools.zip` holds two small launchers — one starts BrightRaider with Map Scanner diagnostics, the other with QuickSave's alternate input path. Drop them next to the EXE only if you're troubleshooting.

---

Thanks to @inf3rrno, whose report and debug log pinned down the resolution bug. 🙏
