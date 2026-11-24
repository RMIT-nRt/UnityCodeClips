# Known Issues & Workarounds

## Active Bugs
* **[BUG-01] Player sticks to walls when jumping:** 
  * *Cause:* Default Physics Material has friction.
  * *Planned Fix:* Assign a `PhysicsMaterial2D` / `PhysicMaterial` with `0` friction to the player collider.

## Resolved Workarounds (DO NOT REVERT)
* **[FIX-01] UI Buttons unresponsive on Level Reload:**
  * *Cause:* `Time.timeScale` was left at `0` after Game Over.
  * *Workaround:* `GameManager.RestartLevel()` explicitly resets `Time.timeScale = 1f;` before calling `SceneManager.LoadScene()`. Do not remove this line.
* **[FIX-02] Animation jitter on moving platforms:**
  * *Workaround:* `CameraFollow` must run in `LateUpdate()`, while player movement runs in `FixedUpdate()` with Rigidbody interpolation set to `Interpolate`.
