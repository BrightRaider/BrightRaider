## BrightRaider V9.4

### Break Reminder (Free)

Never lose track of time mid-session. BrightRaider now shows an orange toast notification after a configurable interval to remind you to take a break.

- Default: every 45 minutes
- Configure under **Settings → Break Reminder**
- Set 0 to disable
- Toast appears in the bottom-right corner, does **not** steal focus or pull you out of the game

---

### Evac Alarm (Pro)

Get an alert when an evacuation timer is running out. When a scanned timer drops below your configured threshold, BrightRaider plays a sound and shows a red toast notification — without interrupting your game.

- Configure inside **Settings → Map Scanner → Settings** (Evac Alarm section at the bottom)
- Set threshold in minutes and seconds (e.g. 2 min 30 sec)
- Alert fires once per timer, resets automatically if the timer is re-scanned
- Requires Map Scanner to be active

---

### Seamless Upgrade from V9.3

No re-activation required. BrightRaider V9.4 automatically finds and migrates your existing license from V9.3.

---

### Other

- Version number now visible in the Exit menu item
- Toast notifications no longer steal focus when a game is in the foreground
- Reduced antivirus false positives compared to V9.3

---

### ⚠️ Antivirus Note

Some AV engines will always flag BrightRaider regardless of version. This is a **false positive**, not malware.

BrightRaider uses a **low-level keyboard hook** (to read Numpad/Arrow keys globally) and **screen capture** (for Auto-Brightness and Map Scanner). These are the same OS APIs that keyloggers and screenshot malware use — so AV heuristics flag them by design, no matter how clean the code is.

The only real fix is an Authenticode **code signing certificate** from a trusted CA (~$150/year). That's not economical for a €5.49 tool. Every detection on VirusTotal traces back to one of these two APIs or the obfuscation layer that protects the license system — nothing else.
