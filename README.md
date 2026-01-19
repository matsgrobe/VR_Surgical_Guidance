# Surgical AR Guidance

This project implements an Augmented Reality guidance system for a 3-point sequential biopsy simulation using Unity and Vuforia.

## Key Features

* **Sequential Targeting:** A logic-driven guidance system that requires the surgeon to reach three tumor sites in a specific order.
* **Dwell-Time Success Logic:** To ensure surgical stability, the system requires the instrument tip to stay within a 2cm threshold for **1.5 seconds** before a target is marked as successful.
* **Visual Guidance:** A **Line Renderer (Laser Guide)** connecting the scalpel tip to the active target.
    * **Ghost Skin Visualization:** A semi-transparent shader effect on the patient's skin for organ visibility.
* **Real-time HUD:** A TextMeshPro display showing Target ID, Distance (cm), and Success Progress (%).


## Note
* **Excluded Files:** Large raw 3D meshes (>50MB) and the `Library/` folder were excluded via `.gitignore` to comply with GitHub repository standards. 
* **Core Logic:** Found in `Assets/guidance.cs`.
