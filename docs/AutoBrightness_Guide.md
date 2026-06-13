# BrightRaider — Auto-Brightness Guide

**Auto-Brightness** is a Pro feature. BrightRaider samples 5 zones on your screen every second, weighs them by your settings, and smoothly interpolates Gamma / Contrast / Vibrance between two profiles as the picture changes. Dark scene → more boost; bright scene → less.

This solves a real problem: a fixed gamma boost that makes dark caves readable also blows out bright outdoor areas. Auto-Brightness gives you the cave-readability **without** the daytime washout — you set the two endpoints, BrightRaider rides the slider between them automatically.

---

## How It Works

1. Every second, BrightRaider grabs 5 small bitmaps from your screen (one center, four corners)
2. Each bitmap is reduced to a median brightness value (0–255)
3. The 5 values are weighted (default: center = 2×, corners = 1×) and combined
4. The combined value is mapped onto a 0–100 % scale based on your **darkest** and **brightest** calibration captures
5. Gamma / Contrast / Vibrance are linearly interpolated between two profiles you assign
6. The new values are pushed to the GPU — *only* if anything actually changed (no GPU hammering on a static screen)

The whole sampling pipeline runs in a few microseconds; no measurable performance impact, no overlay flicker.

---

## Requirements

- **BrightRaider Pro license** (€5.49 one-time)
- BrightRaider V1.0 or later
- A foreground game in fullscreen or borderless (windowed can also work but sampling includes window chrome)

---

## Setup — Calibration Wizard

The Calibration Wizard captures two reference points: the **darkest typical scene** you play and the **brightest typical scene**. Pick reproducible locations in-game.

1. Right-click tray → **Settings → Auto-Brightness [PRO]**
2. Pick the two profiles to interpolate between:
   - **Dark profile** — gets used when the screen is dark (e.g. high-gamma, high-vibrance)
   - **Bright profile** — gets used when the screen is bright (e.g. low-gamma, neutral vibrance)
3. Click **Calibrate dark...**
4. In-game: walk to the **darkest spot** you regularly play (cave, basement, night map). Wait for the Wizard countdown to show, then click **Capture**. The Wizard window hides itself before the actual sample so it doesn't pollute the measurement.
5. Click **Calibrate bright...**
6. In-game: walk to the **brightest spot** (outdoors, midday, no clouds). Click **Capture**.
7. Done — the Wizard saves both calibration values to your profile

You can also calibrate the two endpoints by typing exact 0–100 % values manually if you prefer.

---

## Zone Weights

Each of the 5 zones has a weight from 0–10. Higher weight = more influence on the combined brightness value.

| Zone | Default weight |
|------|---------------|
| Center | 2 |
| Top Left | 1 |
| Top Right | 1 |
| Bottom Left | 1 |
| Bottom Right | 1 |

**When to set a zone to 0 (ignore it):**
- A persistent HUD element covers a corner (minimap, ammo counter)
- A bright UI element constantly biases one corner

**When to raise a center weight:**
- Your character or sights are dead-center — emphasize what you're aiming at
- Default `Center=2` already does this; bump to 3–4 if you want even more bias

---

## Debug Overlay

Settings → Auto-Brightness → **"Show debug overlay"** displays:

- A small panel with current brightness values per zone, the weighted total, and the mapped %
- 5 fully-transparent rectangles showing exactly where each zone samples from
- Useful for: deciding which zones to disable, sanity-checking the calibration captures

The overlay is click-through — it does not affect game input.

---

## All Settings Explained

| Setting | Description |
|---------|-------------|
| **Enable Auto-Brightness** | Master on/off switch |
| **Dark profile** | Profile used at 0 % brightness |
| **Bright profile** | Profile used at 100 % brightness |
| **Dark calibration** | The 0–255 brightness value that maps to 0 % |
| **Bright calibration** | The 0–255 brightness value that maps to 100 % |
| **Zone weights** | 5 sliders (Center / TL / TR / BL / BR), each 0–10 |
| **Show debug overlay** | Draws the 5 zone rectangles + a numeric readout |

Interpolation is linear between the two profiles for Gamma, Contrast, and Vibrance independently. Hue is not interpolated (typically you want a fixed hue).

---

## Tips & Troubleshooting

**Q: The screen pulses brightness during gameplay.**
A: Your Dark and Bright profiles are too far apart, or your two calibration captures are very close together (small range → tiny scene changes flip the whole interpolation). Try one of:
- Re-calibrate with more extreme dark/bright captures (more headroom = smoother)
- Make the two profiles closer in Gamma/Contrast (less dramatic swing)

**Q: A HUD element makes the corner always bright.**
A: Lower that corner's zone weight to 0. The center + remaining three corners will carry the sampling.

**Q: I want it to react faster / slower.**
A: The sample rate defaults to 1 Hz (configurable down to 5 Hz / 200 ms); the interpolation is direct (no smoothing curve), so a change applies on the next tick. For a faster reaction, lower the sample interval and/or raise the Center weight (the center usually changes first). For slower, lower Center back to 1.

**Q: Auto-Brightness fights my Alt-Tab Auto-Switch profile.**
A: Auto-Brightness only runs while the configured game is in the foreground. If you have a per-game color profile in Game Profiles AND Auto-Brightness enabled, Auto-Brightness wins (it pushes interpolated values every second; the per-game profile is the static base before interpolation kicks in).

**Q: How do I disable Auto-Brightness temporarily?**
A: Untick **Enable Auto-Brightness** in Settings, or use the global Hotkeys-OFF toggle (Numpad Entf by default). The sampler stops and the active profile reverts to its base values.

---

## Safety Note

Auto-Brightness is **read-only** for the screen — it uses `GetDC` + `BitBlt` to grab pixels. It does not hook into the game, does not read game memory. The 5 sample rectangles are small (32×32 pixels by default) so the screen capture is fast and CPU-light.

---

*BrightRaider — See in the Dark.*
*https://github.com/BrightRaider/BrightRaider*
