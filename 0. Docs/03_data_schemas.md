# Data Schemas & ScriptableObjects

## 1. Weapon Data (`WeaponDataSO.cs`)
Used to define weapon stats in the Inspector without hardcoding values.
* `string weaponName` — Display name in UI
* `Sprite icon` — HUD icon
* `float damage` — Base damage per hit
* `float fireRate` — Shots per second
* `GameObject projectilePrefab` — Prefab spawned on fire

## 2. Enemy Stats (`EnemyStatsSO.cs`)
* `string enemyID` — Unique identifier (e.g., `"enemy_grunt_01"`)
* `float maxHealth` — Starting HP
* `float moveSpeed` — NavMeshAgent / Rigidbody speed
* `int scoreValue` — Points awarded on death

## 3. Save File Structure (`save_data.json`)
Saved via `JsonUtility` to `Application.persistentDataPath + "/save.json"`.
```json
{
  "highestLevelUnlocked": 1,
  "totalCoins": 0,
  "masterVolume": 0.8,
  "unlockedWeaponIDs": ["wpn_pistol"]
}
