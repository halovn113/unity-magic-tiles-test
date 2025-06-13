# Unity Developer Test – Magic Tiles 3 (Simplified)

This is my submission for the Unity Developer Test inspired by **Magic Tiles 3**, created using Unity **2021.3.31f1 LTS**.  
The project implements a simplified rhythm game with core gameplay features and a few bonus enhancements.

---

## How to Run the Project

1. Clone or download this repository.
2. Open the project in **Unity 2021.3.31f1 LTS**.
3. Open the scene: `Assets/Scenes/Game.unity`.
4. Press **Play** in the Unity Editor to start the game.

No additional packages or SDKs are required to run.

---

## Core Features Implemented

- **Tile Spawning Synchronized to Music Beat**  
  Tiles fall in sync with the background music using a pre-defined beatmap (JSON-based).

- **Tap Detection & Scoring**  
  Players tap on tiles as they reach the target zone. Score is awarded based on accuracy:
  - `Perfect`, `Good`, `Miss`

- **Game Over Condition**  
  When a tile reaches the bottom without being tapped.

- **Beat-Driven Movement**  
  Tiles move at dynamically calculated speed based on BPM and scroll time for better rhythm sync.

- **Combo System**  
  A combo counter is incremented with each successful tap (`Perfect` or `Great`), and resets on a `Miss`.  
  The current combo is displayed in the UI and updates in real time.  

- Note: Only one song available for testing. 
---

## Additional Features (Stretch Goals)

- **Combo System**: Consecutive successful taps increase combo count. Displayed on UI.
- **Observer Pattern Architecture**: Decoupled input-scoring-UI using a clean observer event system.
- **Tween-based UI Effects**: PrimeTween used for some lightweight UI transitions (e.g., combo popups).
- **Beatmap via JSON**: All note times are read from a custom `.json` file for accuracy and easy editing.

---

## ⚙Technical Design Decisions

### Architecture
- Using **Observer Pattern** 

### Performance
- **Object Pooling** for tiles to avoid instantiating/destroying during gameplay.
---

## Assets & External Tools Used

-  **PrimeTween v1.3.2**
  - Used for lightweight UI animations (combo popup, score feedback).
  - [Asset Store Link](https://assetstore.unity.com/packages/tools/animation/primetween-high-performance-animations-and-sequences-252960)

-  **Audio + Visual Assets**
  - Only used the assets provided in the test materials.
  - No other Asset Store content or external licensed assets used.
  - Music used: Yesterday Once More - The Carpenters (is cut shot for testing)

---

## 🧪 Tested On

- Unity Editor (PC)


Feel free to reach out for any clarification!

---

🛠️ **Developed by:** [TChau Kien Hung]  
📧 Email: swathung113@gmail.com
