# BrightRaider — Auto-Brightness Guide

**Auto-Brightness** is a Pro feature. BrightRaider samples 5 zones on your screen every second, weighs them by your settings, and picks the profile that fits how bright the scene is — blending smoothly between neighbouring profiles as the picture changes. Dark scene → more boost; bright scene → less.

This solves a real problem: a fixed gamma boost that makes dark caves readable also blows out bright outdoor areas. Auto-Brightness gives you the cave-readability **without** the daytime washout — you show it a dark and a bright scene once, BrightRaider spreads your profiles over that range and rides between them automatically.

---

## How It Works

1. Every second, BrightRaider grabs 5 small bitmaps from your screen (one center, four corners)
2. Each bitmap is reduced to a median brightness value (0–255)
3. The 5 values are weighted (default: center = 2×, corners = 1×) and combined
4. Every enabled profile has a brightness range (set for you by the calibration); BrightRaider finds the two profiles whose ranges surround the measured value
5. Gamma / Contrast / Vibrance are blended linearly between those two (darker than every range → the darkest profile, brighter → the brightest)
6. The new values are pushed to the GPU — *only* if anything actually changed (no GPU hammering on a static screen)

The whole sampling pipeline runs in a few microseconds; no measurable performance impact, no overlay flicker.

---

## Requirements

- **BrightRaider Pro license** (€5.49 one-time)
- BrightRaider V1.3 or later for the steps below (the settings window changed in V1.3)
- A foreground game in fullscreen or borderless (windowed can also work but sampling includes window chrome)

---

## Setup — Set up the ranges

The calibration captures two reference points: the **darkest typical scene** you play and the **brightest typical scene**. Pick reproducible locations in-game.

1. Right-click tray → **Settings… → Auto-Brightness** and switch it on (top right). The **Guide** button next to it shows this page in short.
2. Under **Set up the ranges**, click **Start…** — the calibration window opens (*Step 1 / 2*).
3. In-game: go to the **darkest spot** you regularly play (cave, basement, night map), then press **Enter** or click **Capture**. The window steps out of the way before it samples, so it doesn't skew the measurement.
4. *Step 2 / 2:* go to the **brightest spot** (outdoors, midday, no clouds) and capture again.
5. Done — BrightRaider spreads your profiles over that range: **Profile 1 takes the brightest part, your last profile the darkest.** A message confirms it.

To fine-tune one profile's range by hand: *Colour profiles → (pick the profile) → Advanced: hue and Auto-Brightness range* — **From** and **To** are scene brightness on a 0–255 scale; −1 leaves the profile out of Auto-Brightness.

---

## Zone Weights

*Auto-Brightness → Advanced: screen zones and debug view.* Each of the 5 zones has a weight from 0–10. Higher weight = more influence on the combined brightness value.

| Zone | Default weight |
|------|---------------|
| Centre | 2 |
| Top left | 1 |
| Top right | 1 |
| Bottom left | 1 |
| Bottom right | 1 |

**When to set a zone to 0 (ignore it):**
- A persistent HUD element covers a corner (minimap, ammo counter)
- A bright UI element constantly biases one corner

**When to raise the centre weight:**
- Your character or sights are dead-centre — emphasise what you're aiming at
- The default `Centre = 2` already does this; bump to 3–4 if you want even more bias

---

## Live values on screen

*Auto-Brightness → Advanced → **Show live values on screen*** displays:

- A small panel with current brightness values per zone, the weighted total and the profile it maps to
- 5 rectangles showing exactly where each zone samples from
- Useful for: deciding which zones to switch off, sanity-checking the calibration

The overlay is click-through — it does not affect game input.

---

## All Settings Explained

| Setting | Where | Description |
|---------|-------|-------------|
| **On / off** | Auto-Brightness, top right (also in the tray) | Master switch |
| **Set up the ranges → Start…** | Auto-Brightness | Two-step calibration; spreads your profiles over dark → bright |
| **Check every** | Auto-Brightness | How often it measures, in ms (default 1000; lower reacts faster and costs a few FPS) |
| **Zone weights** | Auto-Brightness → Advanced | 5 sliders (Centre / corners), each 0–10 |
| **Show live values on screen** | Auto-Brightness → Advanced | Draws the 5 zone rectangles + a numeric readout |
| **From / To** per profile | Colour profiles → Advanced | That profile's brightness range (0–255; −1 = not used) |

Blending is linear between the two neighbouring profiles for Gamma, Contrast and Vibrance independently. Hue is not blended (typically you want a fixed hue).

---

## Tips & Troubleshooting

**Q: The screen pulses brightness during gameplay.**
A: Neighbouring profiles are too different, or your two calibration captures were very close together (small range → tiny scene changes move you across profiles). Try one of:
- Set up the ranges again with more extreme dark/bright captures (more headroom = smoother)
- Make neighbouring profiles closer in Gamma/Contrast (less dramatic swing)

**Q: A HUD element makes the corner always bright.**
A: Lower that corner's zone weight to 0. The center + remaining three corners will carry the sampling.

**Q: I want it to react faster / slower.**
A: It measures once a second by default (*Check every*, down to 100 ms); the blend is direct (no smoothing curve), so a change applies on the next measurement. For a faster reaction, lower *Check every* and/or raise the Centre weight (the centre usually changes first). For slower, lower Centre back to 1.

**Q: Auto-Brightness fights my Alt-Tab Auto-Switch profile.**
A: Auto-Brightness only runs while the configured game is in the foreground. If you have a per-game colour profile on Games & FPS AND Auto-Brightness enabled, Auto-Brightness wins (it pushes interpolated values every second; the per-game profile is the static base before interpolation kicks in).

**Q: How do I disable Auto-Brightness temporarily?**
A: Switch it off on the Auto-Brightness page or in the tray, or use the global Hotkeys-OFF toggle (Numpad Del by default). The sampler stops and the active profile reverts to its base values.

---

## Safety Note

Auto-Brightness is **read-only** for the screen — it uses `GetDC` + `BitBlt` to grab pixels. It does not hook into the game, does not read game memory. The 5 sample rectangles are small (32×32 pixels by default) so the screen capture is fast and CPU-light.

---

*BrightRaider — See in the Dark.*
*https://github.com/BrightRaider/BrightRaider*
