# BrightRaider — Footstep Booster Guide

**Footstep Booster** (a.k.a. **Loudness Limiter**) is a Pro feature. It caps the game's audio session at a configurable threshold, so you can crank in-game volume to make footsteps audible *without* going deaf when a gunshot fires.

It's a per-process audio compressor — only the configured game gets limited. Discord, music, browser, video all stay at their normal level.

---

## Why This Exists

Game audio has a huge dynamic range. Footsteps in Arc Raiders are quiet by design. Gunshots, explosions, and the Matriarch's screech are loud by design. To hear footsteps clearly through the noise floor, you'd normally crank the game master volume — at which point gunshots peak so hard they hurt.

A traditional fix is a hardware compressor (audio interface or VoiceMeeter). Footstep Booster does the same thing in software, on the game's audio session only, with no setup beyond three sliders.

---

## How It Works

1. BrightRaider opens the Windows audio session for the configured game (Core Audio API)
2. Polls the session's peak meter at high frequency
3. When the peak exceeds your **Threshold**, BrightRaider pulls the session volume down (attack)
4. When the peak drops back below threshold, BrightRaider lets the volume return to baseline (release)
5. Only this process's session is touched — global system volume + other apps stay at their original level

It's a feedback-based limiter, not a true lookahead compressor. Lookahead would require injecting into the audio pipeline (anti-cheat risk); this approach reads only the peak meter Windows already exposes.

---

## Requirements

- **BrightRaider Pro license** (€5.49 one-time)
- BrightRaider V1.0 or later
- Windows 10/11 (Core Audio API)
- The game must produce its own audio session (true for Arc Raiders and effectively all modern games)

---

## Setup — Step by Step

1. Right-click tray → **Settings → Audio [PRO]**
2. Scroll to **Footstep Booster / Loudness Limiter**
3. Check **Enable Footstep Booster**
4. Set **Threshold**, **Attack**, **Release** — recommended starting values below
5. Launch the game — BrightRaider auto-attaches when the game is detected as foreground

---

## Recommended Starting Values

| Setting | Starting value | Range | What it controls |
|---------|---------------|-------|------------------|
| **Threshold** | 40 % | 10–90 % | Volume above which the limiter kicks in. Lower = more aggressive compression. |
| **Attack** | 15 ms | 1–200 ms | How fast the limiter pulls volume down on a loud transient. Lower = catches sharp gunshots; too low = audible clicks. |
| **Release** | 300 ms | 50–2000 ms | How fast volume returns to baseline after the loud sound passes. Higher = smoother but slower to hear footsteps again. |

In-game: set master volume to ~80 %, voice/effects high. Adjust the threshold based on how loud you want the loudest sound to be relative to footsteps.

---

## Tuning Workflow

1. **Start with defaults.** Play a round, listen for the artifacts.
2. **Can you hear footsteps clearly?** No → lower Threshold by 5 %. Yes → continue.
3. **Are gunshots painfully loud?** Yes → lower Threshold by 5 %. No → continue.
4. **Do you hear clicks/pops on transients?** Yes → raise Attack by 5–10 ms. No → continue.
5. **Does the audio "duck and stay ducked" too long after a fight?** Yes → lower Release by 50–100 ms. No → done.

Iterate over 2–3 sessions. Most players land at Threshold 35–50 %, Attack 10–25 ms, Release 200–400 ms.

---

## All Settings Explained

| Setting | Description |
|---------|-------------|
| **Enable Footstep Booster** | Master on/off. Off = no audio processing, session volume stays at user-set level. |
| **Threshold (10–90 %)** | Peak level above which the limiter pulls volume down. Use percent of full scale. |
| **Attack (1–200 ms)** | Time for the limiter to react to a loud transient. Faster = catches sharper sounds; too fast = audible click. |
| **Release (50–2000 ms)** | Time for the volume to return to baseline after the loud sound. Faster = footsteps return quicker but with possible pumping. |

The limiter is only active while a configured game is the foreground process. Alt-Tab out → limiter stops, the game's session volume returns to its user-set level.

---

## Tips & Troubleshooting

**Q: I hear audible clicks on gunshots.**
A: Attack is too fast. Raise Attack by 5–10 ms increments until the clicks disappear.

**Q: The audio "ducks" on a gunshot and stays quiet for ages.**
A: Release is too slow. Lower Release by 50–100 ms.

**Q: Footsteps still aren't audible enough.**
A: Two paths:
- Lower the Threshold (more aggressive compression on the loud stuff)
- Raise the in-game master volume + in-game footstep-effects volume

**Q: Music / Discord get affected by the limiter.**
A: They shouldn't — Footstep Booster operates only on the configured game's session. If you see other apps affected, file a GitHub issue with the game name and the audio device.

**Q: The limiter doesn't activate at all.**
A: Check that:
- Footstep Booster is enabled in Settings
- The game has a Game Profile entry (auto-attach only fires for configured games)
- The game has been the foreground window for at least one second (initial attach delay)
- Check `%APPDATA%\BrightRaider\debug.log` for `AudioCompressor` lines

**Q: Does this work for any game, or just Arc Raiders?**
A: Any game. The compressor is process-name based — add the game in **Settings → Game Profiles** and Footstep Booster auto-attaches when that game is foreground.

---

## Safety Note

Footstep Booster uses the public Windows Core Audio API (`IAudioSessionControl2`, `ISimpleAudioVolume`). It does not inject into the game, does not hook DirectSound / XAudio2, does not touch the network. It only changes the per-session output volume — the same thing the Windows Volume Mixer does when you drag a slider.

EAC-safe; same mechanism used by streaming software for ducking game audio under voice tracks.

---

*BrightRaider — See in the Dark. No Ban.*
*https://github.com/BrightRaider/BrightRaider*
