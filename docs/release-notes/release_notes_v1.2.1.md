# BrightRaider V1.2.1

> 🧪 **Pre-release.** Settings, profiles and license carry over — no re-activation needed.

**A fix-up for V1.2.** Autoscrapper and QuickSave are more accurate, and the Autoscrapper can run your saved rules without the review.

| What's new | |
|---|---|
| **Run without the review** — optional, your saved rules run straight away | 🔓 Pro |
| **QuickSave on 4:3, 16:10 and stretched** — and the weapon slots grab the weapon, not its barrel | 🔓 Pro |
| **Settings survive a downgrade** — run an older version in between and your V1.2 settings come back | ⚡ Free |

---

## 🧹 Autoscrapper

- **Run without the review** *(off by default)*: the run follows your saved rules straight away — no list, no Enter. Items without a rule and unnamed tiles are left alone as always; the review still comes up if the scan did not see the whole stash.
- **Works upward from the end of the list** after the scan, instead of scrolling all the way back to the top first.
- **Damaged items** are no longer mistaken for their blueprint.
- **Stack counts:** a 9 no longer reads as 8, and a 4 no longer as 0 or 9.
- **No more false "not all of your stash" warning** on a stash less than three-quarters full.

📖 **[Autoscrapper Guide](https://github.com/BrightRaider/BrightRaider/blob/main/docs/Autoscrapper_Guide.md)**

## 🔧 Fixed

- **QuickSave on tall and stretched resolutions ([#83](https://github.com/BrightRaider/BrightRaider/issues/83))** — the inventory is anchored at the top, not centred; the targets were one slot too low.
- **QuickSave W1/W2** grabbed the first attachment (the barrel) on weapons with four attachments; all drops now land on the slot centre.

---

## 📦 Download + install

**Download `BrightRaider.exe` and double-click it.** To uninstall, delete the EXE and `%LOCALAPPDATA%\BrightRaider\`.

If Windows Defender or SmartScreen flags it: it is an unsigned single-EXE — a **known false positive**. Choose **More info → Run anyway**. Every release so far has been submitted to Microsoft and came back clean.

```
BrightRaider.exe   F399BE4EE232C209DA8C4047BEEB69A8C068305C2AA328AC5FEA8AFC6C7DFE0B
```

Windows 10 / 11 (64-bit) · no .NET install needed · 📘 [Manual](https://github.com/BrightRaider/BrightRaider/blob/main/docs/Manual.md) · 📜 [Changelog](https://github.com/BrightRaider/BrightRaider/blob/main/docs/CHANGELOG_PUBLIC.txt) · [V1.2.0 notes](https://github.com/BrightRaider/BrightRaider/releases/tag/v1.2.0)
