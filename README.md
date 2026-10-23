# BrightRaider

**The gaming tool for every PC game.**

See into dark corners, cap your FPS, hear footsteps over gunfire — set once per game, on one key, switched automatically the moment your game comes to the front. One small EXE in your tray, ready a tenth of a second after you start it.

![Windows](https://img.shields.io/badge/Windows-10%2F11-blue) ![NVIDIA](https://img.shields.io/badge/NVIDIA-supported-green) ![AMD](https://img.shields.io/badge/AMD-supported-red)
[![Downloads](https://img.shields.io/github/downloads/BrightRaider/BrightRaider/total?label=Downloads&color=brightgreen)](https://github.com/BrightRaider/BrightRaider/releases)

**[⬇ Download](../../releases/latest)** · **[🌐 Website](https://brightraider.github.io/BrightRaider/)** · **[⭐ Get Pro — €5.49 once](https://brightraider.lemonsqueezy.com/checkout/buy/9b93d8c0-262f-43a4-bd41-167557efb156)**

![Normal vs Bright](assets/screenshots/comparison2.png)

> **New in V1.4:** ready in 0.11 s and 21 MB in the tray (V1.3: 0.58 s, 105 MB), modules that download and update by themselves, the crosshair remembered per game, search in the settings. — [Release notes](https://github.com/BrightRaider/BrightRaider/releases/tag/v1.4.0) · [Changelog](docs/CHANGELOG_PUBLIC.txt)

## What it does

### Free — for every game

- **Brightness on a key** — Normal / Bright / Brighter, instant, even in fullscreen.
- **Every game its own** — colours and FPS limit per game, on while the game is in front, back to normal on Alt-Tab. Replaces VibranceGUI.
- **G-SYNC / FreeSync setup** — V-SYNC, low latency and the right FPS limit in the driver, the [Blur Busters](https://blurbusters.com/gsync/gsync101-input-lag-tests-and-settings/14/) way, with an exact undo.
- **HDR per game** and **dim your other screens** while you play.
- A **setup** that walks you through, a **guide** beside every settings page, every key rebindable.
- NVIDIA, AMD and Intel · multi-monitor · one portable EXE under 6 MB, nothing to install.

### Pro — €5.49 once, for every game

- **Auto-Brightness** — follows what's on your screen.
- **Sound** — mute only the game, Footstep Booster, mute your teammates, speakers ↔ headset on one key.
- **Crosshair** — click-through overlay, 6 styles, remembered per game.
- **Autorun** — CapsLock runs for you; sprint, hold or toggle per game.
- **Profiles 4–9** with a full editor, and the Process Optimizer.

One key, no subscription, offline after activation — the Arc Raiders module included.

### Modules — tools for one game

A module adds tools for one game. Switch it on and it downloads by itself; updates come on their own; if you don't play that game, you never get it.

| Module | Tools |
|---|---|
| **[Arc Raiders](docs/modules/arc-raiders/README.md)** | Map Scanner with evac alarm · Autoscrapper · QuickSelect · QuickSave |

## Get started

1. **[Download](../../releases/latest)** `BrightRaider.exe` and run it — nothing to install.
2. The setup walks you through: keyboard, brightness, other screens, Alt-Tab, FPS, sound, crosshair — and whether you want a module.
3. On the first start Windows asks once for admin rights, for one gamma registry entry; restart your PC once. [Why](docs/Manual.md#first-launch--important)
4. **Pro:** *Settings → App & License → Activate Pro license…*, paste your key, done.

> **Coming from V9.x?** Your license key still works — re-enter it once under *Settings → App & License*. Activation limit reached? See the [known-issues post](https://github.com/BrightRaider/BrightRaider/issues/74).

<!-- TODO 1.4 release: VirusTotal link and Microsoft verdict for the V1.4.0 build; until then the V1.3.0 results stay, named as such. -->
> ⚠️ **Antivirus false positive?** Some AV tools flag BrightRaider due to its global keyboard hook (same mechanism as Logitech GHub, Razer Synapse, Discord Push-to-Talk). No data is logged or transmitted. **Verify it yourself:** [VirusTotal scan of the V1.3.0 build](https://www.virustotal.com/gui/file/80662cf00c1e420a4e5851643a283ee60a9ceb1b4613bc66eaf4da71c746dcaa) — 1 of 70 engines flags it (Bkav Pro, heuristic); Microsoft and all others show it clean. The V1.3.0 build was also submitted to **Microsoft** for analysis (18 Oct 2026); their verdict: it *"does not meet our criteria for malware or potentially unwanted applications"*. 100–200 downloads daily, zero reports — [see stats](https://github-release-stats.ghostbyte.dev/BrightRaider/BrightRaider).

<details>
<summary>📄 <b>Microsoft's analysis of the V1.3.0 build</b> (click to expand)</summary>

<br>

<img src="docs/assets/wdsi-v1.3.0.png" alt="Microsoft Security Intelligence submission result for BrightRaider V1.3.0: does not meet the criteria for malware or potentially unwanted applications" width="840">

<sub>Submitted 18 October 2026 through the <a href="https://www.microsoft.com/en-us/wdsi/filesubmission">Microsoft Security Intelligence file submission portal</a>. Analyst verdict: the file does not meet the criteria for malware or potentially unwanted applications. Submission ID and submitter address redacted. Every release before it (V1.0 to V1.1.1) was submitted the same way and came back clean.</sub>

</details>

## Docs

- 📘 **[Manual](docs/Manual.md)** — every feature, English and Deutsch
- 🧩 **[Arc Raiders module](docs/modules/arc-raiders/README.md)** — its tools, keys and guides
- 🔆 [Auto-Brightness Guide](docs/AutoBrightness_Guide.md) · 🔊 [Footstep Booster Guide](docs/FootstepBooster_Guide.md)
- 🛡️ [Anti-cheat safety](docs/Safety.md)
- 📜 [Changelog](docs/CHANGELOG_PUBLIC.txt) · ❓ [FAQ](https://brightraider.github.io/BrightRaider/#faq)
- 🇩🇪 **Deutsch:** [Website](https://brightraider.github.io/BrightRaider/) · [Handbuch](docs/Manual.md#deutsch) · [Anti-Cheat](docs/Safety.md#deutsch)

## Screenshots

<details>
<summary><b>📸 See the settings window</b> (click to expand — 8 screenshots, V1.4)</summary>

<br>

**Games & FPS — the Free killer feature.** Per-game colours + FPS limit, auto-switched on Alt-Tab; the G-SYNC / FreeSync setup folded under *Advanced*.

<img src="assets/screenshots/v1.4/settings-games-fps.png" alt="Games & FPS page" width="780">

**Colour profiles.** 9 profiles, each with Gamma / Contrast / Vibrance / Shadow Boost, its key right next to it, and dimming for your other screens.

<img src="assets/screenshots/v1.4/settings-colour-profiles.png" alt="Colour profiles page" width="780">

**A guide beside every page.** What happens, how to set it up, what to try when something is off — here for the Map Scanner.

<img src="assets/screenshots/v1.4/settings-mapscanner-guide.png" alt="Map Scanner page with its guide" width="780">

**Audio (Pro).** Mute only the game, mute teammates, Footstep Booster, output switcher — every key on the page.

<img src="assets/screenshots/v1.4/settings-audio.png" alt="Audio page" width="780">

**Crosshair (Pro).** Click-through overlay, 6 styles, colour, outline, live preview — remembered per game.

<img src="assets/screenshots/v1.4/settings-crosshair.png" alt="Crosshair page" width="780">

**Arc Raiders module.** All four tools on one page with their keys and one switch — the module downloads and updates by itself.

<img src="assets/screenshots/v1.4/settings-arc-raiders.png" alt="Arc Raiders module page" width="780">

**QuickSelect (Pro).** Your game keys once in *Setup*, then one row per item; *All columns* shows the rest.

<img src="assets/screenshots/v1.4/settings-quickselect.png" alt="QuickSelect page" width="780">

**All keys.** Every key in one place, with conflicts — each one is also on its feature's page.

<img src="assets/screenshots/v1.4/settings-all-keys.png" alt="All keys page" width="780">

</details>

<a name="anti-cheat-safety"></a>

## Anti-cheat

BrightRaider works next to your game, never inside it — nothing injected, nothing read, no game file changed. Every tool is quality of life, optional, and its page says exactly what it does. [Details](docs/Safety.md)

## Feedback

Found a bug or have an idea? [Open an issue](https://github.com/BrightRaider/BrightRaider/issues/new/choose) — or *Settings → App & License → Report a problem…*, which puts your version, system and log together for you.

> This is a solo hobby project maintained in my free time. I read everything, but response times vary — there is no support contract.

---

Made for gamers — born in the Arc Raiders community.
