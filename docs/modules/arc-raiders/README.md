# BrightRaider — Arc Raiders module

**Map Scanner, Evac Alarm, Autoscrapper, QuickSelect and QuickSave for ARC Raiders.**
An optional module of [BrightRaider](../../../README.md), part of Pro.

> 🇬🇧 **English** below · 🇩🇪 **[Deutsch weiter unten](#deutsch)**

---

## English

### What it is

BrightRaider itself works with every PC game. The ARC Raiders tools are a module
on top: you only get them if you play ARC Raiders and switch them on.

- **Map Scanner** — hold M on the map: evac timers, map conditions and hatch
  states as a colour-coded overlay, plus an **Evac Alarm**.
- **Autoscrapper** — press F5 with the stash open: it reads the whole stash and
  scraps or sells by your rules, by default only after you confirm the list.
- **QuickSelect** — one key uses an item from your quick-use wheel.
- **QuickSave** — one key drags an item into your Safe Pocket or backpack and back.

### Switching it on

Switch it on in the setup, or on its own page: *Settings → Modules → Arc
Raiders*. It downloads by itself (about 8 MB) and only loads when it carries
BrightRaider's signature. Updates come on their own: when the module needs a
fix, the tray offers *Install Arc Raiders …* and only the module is downloaded —
nothing installs without your click, and never while a game is in front.

Your Pro key unlocks it — every key, including the ones bought before V1.4.
Without Pro, every page can still be opened and set up.

The module lives in `%LOCALAPPDATA%\BrightRaider\modules\`.

### Keys (defaults)

| Numpad (default) | TKL (default) | Action |
|------------------|---------------|--------|
| Numpad * | Delete | Timer overlay on/off |
| M (hold on the map) | M (hold on the map) | Scan the evac timers |
| 4 / 5 / 6 | 4 / 5 / 6 | QuickSelect: use the slot's item |
| Numpad − | Home | QuickSelect on/off |
| Numpad / | End | QuickSave on/off |
| F5 (stash open) | F5 (stash open) | Autoscrapper |

Every key is rebindable on its page, or under *Settings → All keys*. While the
module is off, its keys are not used by BrightRaider at all — they go straight
to the window in front. On Free, QuickSelect and QuickSave keys still show the
Pro notice inside the game.

<a name="anti-cheat"></a>

### Anti-cheat

The module reads your screen on your PC and sends input to the game. Nothing in
the game is modified, nothing is injected into it, nothing is read from its
memory. What each tool sends:

| Tool | What it does | Default | Risk (our estimate) |
|---|---|---|---|
| Map Scanner | Screenshot, plus cursor move and 6 mouse-wheel steps to zoom the map out | on with the module + Pro | low |
| QuickSelect | Posted keys, cursor move, mouse clicks | off (Pro) | medium |
| QuickSave | Cursor drag of an item | off (Pro) | medium |
| **Autoscrapper** | Simulated mouse clicks (marked by Windows as injected), the cursor jumping to each tile, key presses and scrolling, at fixed intervals | off (Pro) | **highest** |

"Risk" is our assessment of how input looks to behavioural anti-cheat (such as
Anybrain, used by ARC Raiders). No anti-cheat vendor has confirmed or ruled out
any of these.

**The Autoscrapper is the most exposed feature in BrightRaider.** It clicks,
moves the cursor and scrolls in the game for you, for several seconds at a time,
with machine-regular timing. Automated input like this is exactly what
behavioural anti-cheat is built to look for. It is optional and off by default —
use it at your own risk.

**QuickSave** and **QuickSelect** send a short sequence of clicks/keystrokes to
move or use an item — with no kernel driver and no injection into the game, but
automated multi-step input is something behavioural anti-cheat can in principle
flag. The strongest signal is *simulated mouse movement*, so keyboard-only
selection is a weaker signal than anything that moves the cursor for you.

**The Map Scanner** reads a screenshot on your PC, but before that it moves the
cursor onto the map and scrolls the mouse wheel 6 times to zoom out — so it does
send input, briefly.

BrightRaider's own features (display, colour, FPS, crosshair, autorun):
[Anti-cheat safety](../../Safety.md#english).

### Guides

- 🧹 **[Autoscrapper Guide](Autoscrapper_Guide.md)** — rules, the review screen, test run vs. live, naming unrecognised tiles
- 🗺️ **[Map Scanner Guide](MapScanner_Guide.md)** — supported maps, event detection, threshold colours, Evac alarm, your own exit names
- ⚡ **[QuickSelect Guide](QuickSelect_Guide.md)** — per-slot LMB hold time, modifier-key bindings, slots 7–10
- 💾 **[QuickSave Guide](QuickSave_Guide.md)** — drag presets, toggle direction, timing controls
- 🧩 **[Data packs](pack-authoring.md)** — add other games to the Map Scanner

### Map Scanner (Pro)

Hold **M** on the in-game map — BrightRaider zooms out, reads all evacuation
timers locally on your PC, and shows a live countdown overlay.

**Supported ARC Raiders maps:** Buried City, Stella Montis, Space Port, Blue
Gate, Dam, Riven Tides, Pendola Pass (gondola points; closed ones show
**CLOSED**). **Other games** can be added via data packs
(`pack-authoring.md`).

**Colour coding:**

| Colour | Meaning              |
|--------|----------------------|
| Green  | more than 10 minutes |
| Yellow | more than 5 minutes  |
| Orange | more than 1 minute   |
| Red    | less than 1 minute   |
| Gray   | closed / unknown     |

**Event detection** — the scanner recognises active map conditions and adjusts
the number of open evacuation points: **Night Raid** (2 points, hatches closed),
**Electromagnetic Storm** (3 points, hatches closed), **Husk Graveyard**,
**Locked Gate**, **Uncovered Caches**, and **Hidden Bunker** (Spaceport — 3
points, hatches closed), and the **ARC Frigate** event (all evac points open).
The condition name appears in the overlay.

**Your own names for the exits** (new in V1.4): *Advanced: your own names for
the exits* lists every exit of every map. Type what you call it — a landmark, a
building — and the overlay and the evac alarm use your name; an empty field keeps
the game's.

If the map is not actually open when you hold M (a menu, the XP screen), the
scanner says *no map detected* instead of guessing.

Configure under *Modules → Arc Raiders → Map Scanner*: the colours and the
overlay position up front; background, font size, hold time, map key and alarm
repeat under *Advanced*.

### Evac Alarm (Pro)

Alerts when a timer drops below your threshold: a red toast + a sound, once per
timer, re-armed when the timer is re-scanned. Switch it on and set the
threshold on the Map Scanner page (*Evac alarm*). Map Scanner must be active.

### QuickSelect (Pro)

Press one key to auto-use a quick-wheel item. BrightRaider runs the whole
sequence: hold Q → select slot → release Q → hold LMB for the configured time →
optionally press H to free the hand.

**Setup:** *Modules → Arc Raiders → QuickSelect* → switch it on → match your
game's keys once in the **Setup** block → per item: trigger key, slot (1–10),
**hold time in ms**, on. *Timing reference* lists the times below, *Slot
layout* shows which slot is which, *All columns* shows the after-use action.

**Recommended LMB hold times (ms):**

| Item                     | ms   |
|--------------------------|------|
| Adrenaline Syringe       | 1300 |
| Herb Bandage / Bandage   | 1800 |
| Shield Charger           | 2300 |
| Vita Syringe             | 4300 |
| Instant Shield Charger   | 5300 |

*(If an item is only partially used, add 100–200 ms.)*

**Tips:** test with a low-value item first · slots 7–10 use cursor movement,
slots 3–6 use their number key (immune to mouse movement) · H can bind to
MB3/4/5 · toggle QuickSelect off (Numpad −) when typing in chat · after pressing
H mid-use, wait a moment before re-triggering (the game holds the item in a brief
transitional state) · don't want the confirmation pop-up? Switch off *Confirm
QuickSelect and QuickSave* on the Arc Raiders page (the macros still run, just
silently).

### QuickSave (Pro)

Drags items between your inventory, Safe Pocket and **backpack** with one keypress
or wheel scroll — no manual clicking.

- **What can move:** inventory slots 1–8, weapons, Safe Pocket 1–3, and
  **Backpack 1–4** (top row) — new in V1.1. Direction is fully configurable.
- **How it works:** opens inventory (Tab) → moves cursor to source → holds LMB
  and drags to destination → releases → closes inventory (Tab).
- **Where it aims:** at the raid inventory, as in V1.0 — on 4:3 and 16:10
  screens too, where it sits in the centre. No look first, no cursor jump. In
  the hideout the inventory sits elsewhere, so QuickSave is for raids.
- **Toggle direction (⇄):** first press moves From→To, second To→From. A press
  while a move is still running is ignored and does not turn the direction.
- **Timing** (if a drag fails, *QuickSave → Advanced: inventory key and
  timings*): Open (250 ms), Hover (0 ms), Drag (0 ms), Drop (120 ms), Cooldown
  (0 ms).
- **Toggle on/off:** Numpad / (Numpad) or End (Arrow).

> **Known limitation:** occasional drag failures come from the Unreal Engine
> Slate UI that ARC Raiders uses — the same thing happens with manual drag-drop.
> It can't be fixed from outside the game; just trigger again.

### Autoscrapper (Pro)

Reads your stash from the screen and scraps or sells what your rules say —
by default after you have seen the list and confirmed it ("Run without the
review" skips that). It sends simulated clicks, cursor moves and scrolling to
the game — the most exposed input feature, see [Anti-cheat](#anti-cheat). Full walkthrough: [Autoscrapper Guide](Autoscrapper_Guide.md).

- **Rules:** *Modules → Arc Raiders → Autoscrapper* — an item (or a whole category), how
  many to keep, and what happens to the rest: leave alone, scrap or sell.
  Anything not listed is never touched.
- **Run:** open the stash in game and press **F5**. Keep your hands off the
  mouse while it scrolls (~10 s for 300 slots).
- **Review:** the list comes up in stash order. **Enter** runs it, **Esc**
  changes nothing; what you set there is saved as rules. Hold **Esc** to stop
  a running pass.
- **Test run** is the default: it goes up to the game's confirmation box and
  cancels it. Switch to **Live** once it behaves.
- **Unrecognised tiles** stay unnamed and untouched. Name them once with
  *Name unrecognised items…* and they are known from the next scan on.

<a name="launchers"></a>

### Special launchers (the tools download)

For everyday use, just run `BrightRaider.exe`. The optional **tools download**
(`BrightRaider_tools.zip` on the releases page) adds two double-click launchers
for special situations — put them in the same folder as `BrightRaider.exe` and
run the one you need:

- **`BrightRaider (debug scan).cmd`** — starts BrightRaider with Map Scanner
  diagnostics enabled. Open the in-game map and scan once; it writes a PNG +
  `.txt` into `%LocalAppData%\BrightRaider\debug`. Send me those files if a map
  timer ever reads wrong.
- **`BrightRaider (sendinput mode).cmd`** — starts QuickSave with its alternate,
  lower-level input path. Use it only if a game doesn't register QuickSave's
  default inventory drag. Off in normal use. It uses synthetic input, which
  anti-cheat may flag — use at your own risk. The launcher shows this for 8 seconds
  before it starts.

Each launcher just sets an environment variable before starting the app, so you
can open one in Notepad to see exactly what it does. You never need these for
normal use — running `BrightRaider.exe` on its own is the standard way.

### Changelog

Module updates that come without a new BrightRaider are listed here first.
Everything before: the [app's changelog](../../CHANGELOG_PUBLIC.txt).

**1.4.0** — with BrightRaider 1.4, the first version as a module
- Downloads by itself when you switch it on, updates on its own, loads only
  with BrightRaider's signature.
- Map Scanner: name the exits yourself (#80); no made-up overlay when the map
  is not actually open.
- Autoscrapper: knows the items of the 2.0 update - about 60 are recognised
  right away, the rest you name once with *Name unrecognised items…*; nearly 9
  of 10 tiles of a real stash are named right away, furniture designs and items
  with a small symbol in their corner among them; items you marked with the eye
  are never scrapped.
- QuickSave: a press while a move is still running no longer turns the
  direction around.

---

<a name="deutsch"></a>

## Deutsch

### Was es ist

BrightRaider selbst funktioniert mit jedem PC-Spiel. Die ARC-Raiders-Werkzeuge
sind ein Modul obendrauf: Du bekommst sie nur, wenn du ARC Raiders spielst und
sie einschaltest.

- **Map Scanner** — M auf der Karte halten: Evac-Timer, Kartenereignisse und
  Luken als farbiges Overlay, dazu ein **Evac-Alarm**.
- **Autoscrapper** — F5 bei offenem Lager: liest das ganze Lager und verschrottet
  oder verkauft nach deinen Regeln, standardmäßig erst, nachdem du die Liste
  bestätigt hast.
- **QuickSelect** — eine Taste benutzt ein Item aus dem Schnellrad.
- **QuickSave** — eine Taste zieht ein Item in die Sicherheitstasche oder den
  Rucksack und zurück.

### Einschalten

Im Einrichtungsassistenten einschalten, oder auf seiner eigenen Seite:
*Einstellungen → Modules → Arc Raiders*. Es lädt sich selbst herunter (etwa 8 MB)
und wird nur mit BrightRaiders Signatur geladen. Updates kommen von selbst:
Braucht das Modul einen Fix, bietet das Tray *Install Arc Raiders …* an, und nur
das Modul wird geladen — nichts installiert sich ohne deinen Klick, und nie,
während ein Spiel vorne ist.

Dein Pro-Schlüssel schaltet es frei — jeder Schlüssel, auch die vor V1.4
gekauften. Ohne Pro lässt sich jede Seite trotzdem öffnen und einrichten.

Das Modul liegt in `%LOCALAPPDATA%\BrightRaider\modules\`.

### Tasten (Standard)

| Numpad (Standard) | TKL (Standard) | Aktion |
|-------------------|----------------|--------|
| Numpad * | Entf | Timer-Overlay an/aus |
| M (halten auf der Karte) | M (halten auf der Karte) | Evac-Timer scannen |
| 4 / 5 / 6 | 4 / 5 / 6 | QuickSelect: Item des Slots benutzen |
| Numpad − | Pos1 | QuickSelect an/aus |
| Numpad / | Ende | QuickSave an/aus |
| F5 (Lager offen) | F5 (Lager offen) | Autoscrapper |

Jede Taste ist auf ihrer Seite belegbar, oder unter *Einstellungen → All keys*.
Solange das Modul aus ist, nutzt BrightRaider seine Tasten gar nicht — sie gehen
unverändert an das Fenster, das vorne ist. Ohne Pro zeigen QuickSelect- und
QuickSave-Tasten im Spiel weiter den Pro-Hinweis.

<a name="anti-cheat-de"></a>

### Anti-Cheat

Das Modul liest deinen Bildschirm auf deinem PC und sendet Eingaben ans Spiel.
Am Spiel wird nichts verändert, nichts hineininjiziert, nichts aus seinem
Speicher gelesen. Was jedes Werkzeug sendet:

| Werkzeug | Was es tut | Standard | Risiko (unsere Einschätzung) |
|---|---|---|---|
| Map Scanner | Screenshot, dazu Cursor setzen und 6× Mausrad zum Rauszoomen | an mit Modul + Pro | niedrig |
| QuickSelect | Gepostete Tasten, Cursor setzen, Mausklicks | aus (Pro) | mittel |
| QuickSave | Item per Cursor ziehen | aus (Pro) | mittel |
| **Autoscrapper** | Simulierte Mausklicks (von Windows als injiziert markiert), Cursor springt auf jede Kachel, Tastendrücke und Scrollen, in festen Abständen | aus (Pro) | **am höchsten** |

„Risiko“ ist unsere Einschätzung, wie die Eingaben für verhaltensbasiertes
Anti-Cheat (z. B. Anybrain in ARC Raiders) aussehen. Kein Anti-Cheat-Hersteller
hat eine dieser Funktionen bestätigt oder ausgeschlossen.

**Der Autoscrapper ist die am stärksten exponierte Funktion von BrightRaider:**
Er klickt, bewegt den Cursor und scrollt sekundenlang für dich, in maschinell
gleichmäßigem Takt — genau danach sucht verhaltensbasiertes Anti-Cheat.
Optional, standardmäßig aus, Nutzung auf eigenes Risiko.

**QuickSave und QuickSelect** senden eine kurze Folge von Klicks/Tastendrücken —
ohne Kernel-Treiber und ohne Injektion ins Spiel, aber automatisierte
mehrstufige Eingaben können grundsätzlich erkannt werden. Das stärkste Signal ist
*simulierte Mausbewegung*; reine Tastatur-Auswahl ist ein schwächeres Signal.

**Der Map Scanner** liest einen Screenshot lokal aus, setzt aber vorher den
Cursor auf die Karte und scrollt 6× mit dem Mausrad, um rauszuzoomen — er
sendet also kurz Eingaben.

BrightRaiders eigene Funktionen (Anzeige, Farbe, FPS, Fadenkreuz, Autorun):
[Anti-Cheat-Sicherheit](../../Safety.md#deutsch).

### Anleitungen (englisch)

- 🧹 **[Autoscrapper Guide](Autoscrapper_Guide.md)** — Regeln, die Prüfliste, Testlauf und Live, unbekannte Kacheln benennen
- 🗺️ **[Map Scanner Guide](MapScanner_Guide.md)** — Karten, Ereignisse, Farben, Evac-Alarm, eigene Namen für die Ausgänge
- ⚡ **[QuickSelect Guide](QuickSelect_Guide.md)** — Haltezeit pro Slot, Modifier-Tasten, Slots 7–10
- 💾 **[QuickSave Guide](QuickSave_Guide.md)** — Zieh-Vorlagen, Richtung umschalten, Timing
- 🧩 **[Datenpacks](pack-authoring.md)** — andere Spiele für den Map Scanner

### Map Scanner (Pro)

**M** auf der Karte halten — BrightRaider zoomt heraus, liest alle
Evakuierungs-Timer lokal auf deinem PC aus und zeigt einen Live-Countdown.

**Unterstützte ARC-Raiders-Karten:** Buried City, Stella Montis, Space Port, Blue
Gate, Dam, Riven Tides, Pendola Pass (Gondel-Punkte; geschlossene zeigen
**CLOSED**). **Andere Spiele** per Daten-Pack (`pack-authoring.md`).

**Farbkodierung:**

| Farbe  | Bedeutung             |
|--------|-----------------------|
| Grün   | mehr als 10 Minuten   |
| Gelb   | mehr als 5 Minuten    |
| Orange | mehr als 1 Minute     |
| Rot    | weniger als 1 Minute  |
| Grau   | geschlossen / unbekannt |

**Event-Erkennung** — der Scanner erkennt aktive Karten-Bedingungen und passt die
Anzahl offener Evakuierungspunkte an: **Night Raid** (2 Punkte, Hatches zu),
**Elektromagnetischer Sturm** (3 Punkte, Hatches zu), **Husk Graveyard**,
**Locked Gate**, **Uncovered Caches** und **Hidden Bunker** (Spaceport — 3 Punkte,
Hatches zu) und das Event **ARC Frigate** (alle Evakuierungspunkte offen). Der
Name erscheint im Overlay.

**Eigene Namen für die Ausgänge** (neu in V1.4): *Advanced: your own names for
the exits* listet jeden Ausgang jeder Karte. Schreib hinein, wie du ihn nennst —
ein Wahrzeichen, ein Gebäude —, und Overlay und Evac-Alarm nehmen deinen Namen;
ein leeres Feld behält den Namen aus dem Spiel.

Ist beim Halten von M gar keine Karte offen (ein Menü, der XP-Bildschirm), meldet
der Scanner *no map detected*, statt zu raten.

Konfiguration unter *Modules → Arc Raiders → Map Scanner*: Farben und
Overlay-Position vorne; Hintergrund, Schriftgröße, Haltedauer, Kartentaste und
Alarm-Wiederholung unter *Advanced*.

### Evac-Alarm (Pro)

Meldet, wenn ein Timer unter den Schwellwert fällt: roter Toast + Sound, einmal
pro Timer, neu scharf beim erneuten Scan. Einschalten und Schwellwert auf der
Map-Scanner-Seite (*Evac alarm*). Map Scanner muss aktiv sein.

### QuickSelect (Pro)

Eine Taste drücken, um ein Schnellrad-Item automatisch zu nutzen. BrightRaider
führt die Sequenz aus: Q halten → Slot wählen → Q loslassen → LMB für die
eingestellte Zeit halten → optional H für freie Hand.

**Einrichten:** *Modules → Arc Raiders → QuickSelect* → einschalten → einmal im
Block **Setup** deine Spieltasten eintragen → pro Item: Trigger-Taste, Slot
(1–10), **Haltezeit in ms**, an. *Timing reference* zeigt die Zeiten unten,
*Slot layout* zeigt, welcher Slot welcher ist, *All columns* zeigt die Aktion
nach der Nutzung.

**Empfohlene LMB-Haltezeiten (ms):**

| Item                     | ms   |
|--------------------------|------|
| Adrenalinspritze         | 1300 |
| Kräuterverband / Verband | 1800 |
| Schildauflader           | 2300 |
| Vita-Spritze             | 4300 |
| Sofort-Schildauflader    | 5300 |

*(Wird ein Item nur teilweise benutzt, 100–200 ms zugeben.)*

**Tipps:** erst mit einem günstigen Item testen · Slots 7–10 per Cursor, Slots 3–6
per Zifferntaste (immun gegen Mausbewegung) · H auch auf MB3/4/5 belegbar ·
QuickSelect beim Tippen im Chat pausieren (Numpad −) · nach H mitten in der Nutzung
kurz warten, bevor du neu triggerst (das Spiel hält das Item kurz in einem
Übergangszustand) · kein Bestätigungs-Popup gewünscht? *Confirm QuickSelect and
QuickSave* auf der Arc-Raiders-Seite ausschalten (die Makros laufen weiter, nur
lautlos).

### QuickSave (Pro)

Zieht Items zwischen Inventar, Sicherheitstasche und **Rucksack** — mit einem
Tastendruck oder Mausradscrollen.

- **Was sich verschieben lässt:** Inventar-Slots 1–8, Waffen, Sicherheitstasche
  1–3 und **Rucksack 1–4** (oberste Reihe) — neu in V1.1. Richtung frei einstellbar.
- **So funktioniert es:** Inventar öffnen (Tab) → Cursor zum Quell-Slot → LMB
  halten und zum Ziel ziehen → loslassen → Inventar schließen (Tab).
- **Wohin es zielt:** auf das Raid-Inventar, wie in V1.0 — auch bei 4:3 und
  16:10, wo es in der Mitte sitzt. Kein Nachschauen, kein Cursor-Sprung. In der
  Basis sitzt das Inventar woanders, QuickSave ist also für den Raid.
- **Richtung umkehren (⇄):** erster Druck Von→Nach, zweiter Nach→Von. Ein Druck,
  während noch ein Zug läuft, wird ignoriert und dreht die Richtung nicht um.
- **Timing** (falls Drag fehlschlägt, *QuickSave → Advanced: inventory key and
  timings*): Öffnen (250 ms), Hover (0 ms), Ziehen (0 ms), Ablegen (120 ms),
  Cooldown (0 ms).
- **An/aus:** Numpad / (Numpad) oder Ende (Pfeiltasten).

> **Bekannte Einschränkung:** gelegentliche Drag-Fehler kommen vom Unreal-Engine-
> Slate-UI, das ARC Raiders nutzt — dasselbe passiert beim manuellen Ziehen. Von
> außen nicht behebbar; einfach nochmal auslösen.

### Autoscrapper (Pro)

Liest dein Lager vom Bildschirm und verschrottet oder verkauft, was deine Regeln
sagen — standardmäßig, nachdem du die Liste gesehen und bestätigt hast („Run
without the review" überspringt das). Er sendet simulierte Klicks,
Cursorbewegungen und Scrollen ans Spiel — die am stärksten exponierte
Eingabe-Funktion, siehe [Anti-Cheat](#anti-cheat-de). Ausführlich:
[Autoscrapper Guide (EN)](Autoscrapper_Guide.md).

- **Regeln:** *Modules → Arc Raiders → Autoscrapper* — ein Gegenstand (oder eine ganze
  Kategorie), wie viele bleiben und was mit dem Rest passiert: liegen lassen,
  verschrotten oder verkaufen. Was nicht in der Liste steht, wird nie angefasst.
- **Start:** Lager im Spiel öffnen und **F5** drücken. Maus in Ruhe lassen,
  solange gescrollt wird (~10 s für 300 Plätze).
- **Prüfen:** Die Liste erscheint in Lager-Reihenfolge. **Enter** führt aus,
  **Esc** ändert nichts; was du dort einstellst, wird als Regel gespeichert.
  **Esc** gedrückt halten bricht einen laufenden Durchgang ab.
- **Testlauf** ist voreingestellt: geht bis zur Bestätigungsbox des Spiels und
  bricht dort ab. Auf **Live** umstellen, wenn alles passt.
- **Unerkannte Kacheln** bleiben unbenannt und unangetastet. Einmal über
  *Name unrecognised items…* benennen, ab dem nächsten Scan sind sie bekannt.

<a name="starter"></a>

### Sonder-Starter (das Tools-Paket)

Für den Alltag startest du einfach `BrightRaider.exe`. Das optionale
**Tools-Paket** (`BrightRaider_tools.zip` auf der Releases-Seite) bringt zwei
Doppelklick-Starter für Sonderfälle — leg sie in denselben Ordner wie
`BrightRaider.exe` und starte den, den du brauchst:

- **`BrightRaider (debug scan).cmd`** — startet BrightRaider mit
  Map-Scanner-Diagnose. Öffne die Spielkarte und scanne einmal; es schreibt ein
  PNG + `.txt` nach `%LocalAppData%\BrightRaider\debug`. Schick mir diese Dateien,
  falls ein Karten-Timer mal falsch angezeigt wird.
- **`BrightRaider (sendinput mode).cmd`** — startet QuickSave mit seinem
  alternativen, tiefer liegenden Eingabepfad. Nur nutzen, wenn ein Spiel
  QuickSaves Standard-Inventar-Drag nicht registriert. Im Normalbetrieb aus. Er
  nutzt synthetische Eingaben, die Anti-Cheat erkennen kann — Nutzung auf eigenes
  Risiko. Der Starter zeigt das 8 Sekunden lang vor dem Start.

Jeder Starter setzt nur eine Umgebungsvariable vor dem App-Start — du kannst einen
in Notepad öffnen, um genau zu sehen, was er tut. Für den Normalbetrieb brauchst
du sie nie — `BrightRaider.exe` allein ist der Standardweg.

### Changelog

Modul-Updates, die ohne neues BrightRaider kommen, stehen zuerst hier. Alles
davor: der [Changelog der App](../../CHANGELOG_PUBLIC.txt) (englisch).

**1.4.0** — mit BrightRaider 1.4, die erste Version als Modul
- Lädt sich selbst, sobald du es einschaltest, aktualisiert sich selbst, wird
  nur mit BrightRaiders Signatur geladen.
- Map Scanner: Ausgänge selbst benennen (#80); kein erfundenes Overlay, wenn die
  Karte gar nicht offen ist.
- Autoscrapper: kennt die Items aus dem Update 2.0 - rund 60 erkennt er sofort,
  die übrigen benennst du einmal über *Name unrecognised items…*; in einem echten
  Lager benennt er fast 9 von 10 Kacheln sofort, auch Möbel-Baupläne und Items
  mit kleinem Symbol in der Ecke; mit dem Auge markierte Items werden nie
  verschrottet.
- QuickSave: Ein Druck, während noch ein Zug läuft, dreht die Richtung nicht mehr
  um.
