# BrightRaider Game Packs — Authoring Guide

Since V1.1, BrightRaider's **Map Scanner** can learn new games through **data
packs** — a folder of plain data files. No coding, no waiting for an app
update: measure your game's map screen once, train the digit font from two
screenshots, drop the folder in place, restart BrightRaider.

> Packs cover the **Map Scanner** (evac/objective timers + event detection).
> The display features (color profiles, FPS limits, Auto-HDR, crosshair …)
> already work with every game out of the box — just add your game under
> *Game Profiles*.

## Folder layout

```
%APPDATA%\BrightRaider\packs\my_game\
├── pack.ini              manifest (see below)
├── templates\            digit templates you train (digit_0.bin … digit_colon.bin)
└── events.txt            optional: event-icon hash table you train
```

BrightRaider loads every valid pack at startup — check `%APPDATA%\BrightRaider\debug.log`
for `loaded pack '…'`. A broken pack is skipped with a reason in the log; it can
never break the app.

## pack.ini

```ini
PackFormat=1
Id=my_game                    ; must equal the folder name
Name=My Game
ProcessNames=MyGame-Win64-Shipping;MyGameAlt
SourceW=2560                  ; the resolution ALL coordinates below were measured at
SourceH=1440
Threshold=180                 ; optional: OCR brightness cutoff (bright text on dark map)
EventIconRect=1980,230,95,40  ; optional: x,y,w,h search box for the event icon
                              ; (required if you ship events.txt)

Map1.Name=Harbor
Map1.Timer1=North,1283,530,1353,550     ; Name, left, top, right, bottom
Map1.Timer2=South,1311,1013,1384,1032
Map2.Name=Foundry
Map2.Timer1=...

; optional: events that close evac points ("N points stay open[,hatches closed]")
EventRule.Night Raid=2,hatches
```

Coordinates are pixel positions **on a screenshot taken at SourceW×SourceH**
(any 16:9 resolution works; BrightRaider rescales to the player's screen at
runtime, including letterbox/pillarbox handling for 16:10 and ultrawide).

## Workflow

1. **Screenshot the in-game map** at your chosen SourceW×SourceH, with timers
   visible. PNG or JPG.
2. **Measure the timer boxes** in any image editor: note the left/top and
   right/bottom pixel corners of each timer's digits (a few pixels of padding
   is fine). Enter them as `MapN.TimerM` lines.
3. **Train the digit font** — run from the BrightRaider folder:
   ```
   BrightRaider.exe --pack-train-digit <packDir> <screenshot> <map> <timer> "28:58"
   ```
   where `"28:58"` is exactly what that timer shows in the screenshot. Repeat
   with a second screenshot until every digit 0–9 (plus the colon) has been
   seen once. Templates land in `templates\`.
4. **Train events** (optional) — with an event active in the screenshot:
   ```
   BrightRaider.exe --pack-train-event <packDir> "Night Raid" <screenshot>
   ```
   > **Changed in V1.2:** event hashes are now 256-bit (64 hex characters) and
   > are recognised at any screen resolution — the old 64-bit hashes only ever
   > matched on the exact resolution they were trained at. Entries written by
   > V1.1 or earlier are ignored with a warning in the log; re-run
   > `--pack-train-event` once per event to update them.
5. **Verify** — the same scan the app runs live, printed for your screenshot:
   ```
   BrightRaider.exe --pack-verify <packDir> <screenshot>
   ```
   You want your map recognized with most timers read and the right event
   name. Adjust rects / retrain until it's clean.
6. Restart BrightRaider. Long-press the Map-Scan key in game — done.

## Tips

- The scanner assumes **bright timer digits on a darker map** (that's what
  `Threshold` cuts at). If your game's timers are dark-on-bright, invert your
  expectations — or lower/raise `Threshold` and check with `--pack-verify`.
- One timer rect must contain exactly one `M:SS` / `MM:SS` style countdown.
- The event icon must be a distinct little symbol at a fixed screen position;
  `EventIconRect` is the search box around it.
- Share your pack as a zip of the folder — receivers unzip it into
  `%APPDATA%\BrightRaider\packs\` and restart. Packs are pure data and cannot
  execute anything.
