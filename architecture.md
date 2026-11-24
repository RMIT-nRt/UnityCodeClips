# Technical Architecture (Unity)

## 1. Engine & Environment
* **Unity Version:** [e.g., Unity 6 (6000.0.x) / Unity 2022.3 LTS]
* **Render Pipeline:** [e.g., URP (Universal Render Pipeline)]
* **Input System:** [e.g., New Input System (`InputSystem_Actions`) / Legacy Input Manager]
* **Target Platform:** [e.g., Windows Standalone / WebGL]

## 2. Folder Structure (`Assets/`)
```text
Assets/
├── _Project/               # Keep all custom work here (separate from third-party assets)
│   ├── Art/                # Materials, Models, Sprites, Shaders
│   ├── Audio/              # Music, SFX
│   ├── Prefabs/            # Reusable GameObjects
│   ├── Scenes/             # MainMenu, Level_01, Sandbox
│   ├── ScriptableObjects/  # Data containers (.asset files)
│   └── Scripts/
│       ├── Core/           # GameManager, AudioManager, SceneLoader
│       ├── Player/         # PlayerController, PlayerHealth
│       ├── Enemies/        # EnemyAI, EnemySpawner
│       └── UI/             # HUDController, MenuUI