# BrightRaider — Autoscrapper Guide

The **Autoscrapper** reads your ARC Raiders stash from the screen, works out what every tile is, and scraps or sells what you told it to — after you have seen the list and said yes.

**Nothing happens until you confirm.** Every run ends in a review screen that cannot be skipped, and an item that is not on your list is never touched — including anything BrightRaider misreads.

---

## Requirements

- **BrightRaider Pro license**
- BrightRaider V1.2 or later
- ARC Raiders running, with the **stash open** when you press the trigger key

---

## Quick start

1. Right-click the BrightRaider tray icon → **Settings** → tab **♻ Autoscrapper**.
2. Tick **Autoscrapper**. While it is off, the trigger key does nothing.
3. Leave **A run may:** on **Test run — nothing is destroyed** for now.
4. Add a rule, for example *Metal Parts — keep 200 — scrap*. (More on rules below.)
5. Click **OK**, switch to the game and open your stash.
6. Press **F5**. Keep your hands off the mouse — BrightRaider scrolls through the whole stash (about 10 seconds for 300 slots).
7. The review screen opens. Check it, then press **Enter** to run or **Esc** to cancel.

When the test run behaves, switch **A run may:** to **Live — really scrap and sell**.

---

## Rules: what may go

The table in the Autoscrapper tab is the list of everything BrightRaider is allowed to touch.

| Column | Meaning |
|---|---|
| **Item** | One item, or a whole category — type "All" to see them, e.g. *All Recyclable* |
| **Keep this many** | How many stay. Everything above that number is what the action applies to |
| **With the rest** | *leave alone*, *scrap* or *sell* |

**"Keep 200 / scrap"** means 200 stay and everything above that is scrapped.

- **Only whole stacks are touched.** The game cannot act on part of a stack, so when the number cannot be hit exactly, one stack too many stays — never one too few.
- **A category row counts per item**, not across the category: *All Recyclable — keep 10* keeps ten of each.
- **A row for a single item always wins** over the category it belongs to.
- **Quest items are protected** and are never scrapped or sold, whatever the rules say.
- **While the list is empty, a scan does nothing.**

---

## The review screen

After the scan you get your stash as a list, **in the same order as in the game**, so you can follow it alongside the stash.

| Column | Meaning |
|---|---|
| **Item** | What the scanner read |
| **Count** | Items on that row, all stacks together |
| **Value** | What *one* is worth, from the item data |
| **Keep this many** | Edit it right here |
| **With the rest** | Click to cycle: leave alone → scrap → sell |
| **Worth of the rest** | What everything above your keep count is worth — on a scrap row, that is what you are giving up |

- **Enter / Run it** carries out the list. **Esc / Cancel** changes nothing.
- **Keep everything** sets every row to *leave alone* in one click.
- **What you set here is saved as rules.** The second run needs no editing at all.
- To **stop a run** once it has started, hold **Esc**.

---

## Test run and Live

| Mode | What it does |
|---|---|
| **Test run** *(default)* | Goes all the way — selects the tiles, presses the scrap/sell key and lets the game's confirmation box appear — then presses **Escape instead of confirming**. Nothing is destroyed. |
| **Live** | Does what the review said. **Scrapping cannot be undone.** |

The test run is the only way to see that BrightRaider finds the game's confirm button before a live run depends on it. **Run it once after changing your resolution.**

---

## When it cannot name something

**If it is not sure, it says nothing.** A tile it cannot name with confidence gets no name — and a tile with no name is never scrapped or sold.

You can teach it:

1. In the review screen, click **Name this…** on a row — or in the tab, click **Name unrecognised items…**.
2. The **Teach the scanner** window shows the tile exactly as it came off your screen. Type part of the name and click it. *Not an item — discard* and *Skip for now* are there too.
3. Names take effect **from the next scan on**. They are compared against your own screen, which is why this is the best way to make it better for you.

Your names are kept in `items_named.txt`. **No update ever overwrites that file.**

### Items that look identical

Some items share their picture. The two Looting Mk. 3 blueprints are pixel-for-pixel identical, and a Modification blueprint shows neither a tier numeral nor a rarity crescent. No picture can tell those apart, so BrightRaider does not guess.

Instead the row shows **both** names and asks: do your rules say the same thing about both? If they do, the name does not matter and it acts. If they differ, the row is left alone and tells you why.

---

## Resolutions

Every resolution works, including 16:10, 4:3 and 21:9 ultrawide.

| Checked in game | |
|---|---|
| 1920×1080, 2560×1440, 3840×2160, 5120×2880 | ✓ |
| 1920×1200 (16:10), 1920×1440 (4:3) | ✓ |
| 21:9 ultrawide | supported, not yet checked on a real ultrawide |

On every resolution checked, every slot was found, nothing was named wrong and no stack count was off. Well over 90 % of tiles are named on a fresh install; the rest stay unnamed until you name them.

After changing resolution, the first scan takes a few seconds longer while the reference pictures are prepared for the new size.

---

## Setup — keys

Open **Setup — trigger key and the game's own keys** at the bottom of the tab. The trigger key is always there; the two game keys show with *Advanced* on.

| Setting | Default | Meaning |
|---|---|---|
| **Trigger key** | F5 | Press it with the stash open. With the stash closed, nothing happens. |
| **Scrap (in game)** | R | Sent to the game after a selection is right-clicked |
| **Sell (in game)** | S | Same, for selling |

Only change the last two if you rebound them in ARC Raiders.

---

## Troubleshooting

| Problem | What to do |
|---|---|
| F5 does nothing | Is the Autoscrapper ticked, and is the stash open? Is your rule list empty? |
| A scan stops early or misses rows | Don't touch the mouse while it scrolls, and keep the game in front |
| Clicks land in the wrong window | The game has to be in front — if it does not come back by itself after the review, click into it |
| Many tiles unnamed | Name them once with **Name unrecognised items…** — it learns from your own screen |
| Something went wrong | The log is at `%APPDATA%\BrightRaider\debug.log` — please attach it to a GitHub issue |
