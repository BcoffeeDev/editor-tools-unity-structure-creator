# Unity Project Structure Creator

This is a lightweight Unity editor tool that helps you quickly generate a standardized and customizable folder structure for your Unity project.

## ✨ Features

- One-click creation of commonly used Unity folder structures
- Customizable structure presets
- Highly flexible — select only the folders you need within the chosen structure

## 📁 Folder Structure

The tool offers three predefined folder structures to choose from:
- `DefaultStructure`: A balanced folder layout suitable for general-purpose Unity projects. Inspired by community conventions.
- `ScriptingStructure`: A focused layout tailored for script-heavy projects, including Editor, Runtime, and Tests folders.
- `ArtStructure`: A visually-oriented structure designed for asset-intensive projects with folders for Animations, Shaders, and more.

The `DefaultStructure` is based on [themorfeus/unity_project_structure](https://github.com/themorfeus/unity_project_structure).

> 💡 You can extend the tool by creating your own `ProjectStructure` subclass to define custom folder presets.

Depending on your input, the tool creates the following structure:

#### 🔹 If you enter a name (e.g., `MyGame`):

```
Assets/
└── MyGame/
    ├── Materials/
    ├── Models/
    ├── Prefabs/
    ├── Scenes/
    ├── Scripts/
    └── Textures/
```

#### 🔹 If you enter a relative path (e.g., `SubProjects/MyGame`):

```
Assets/
└── SubProjects/
    └── MyGame/
        ├── Materials/
        ├── Models/
        ├── Prefabs/
        ├── Scenes/
        ├── Scripts/
        └── Textures/
```

## 🛠️ How to Use

1. Place the script in your Unity project under any `Editor/` folder.
2. Open the tool via `Tools → Create Project Structure`.
3. Choose one of the predefined structures.
4. Check the subfolders you want to include.
5. Enter a name or a relative path.
6. Click the **Create Folders** button.

> ℹ️ The folder structure will be created relative to `Assets/`, based on the name or path you enter.

## 📦 Installation

Download the `ProjectStructureCreator.cs` file and place it anywhere inside an `Editor/` folder in your Unity project.

## 🤝 Contributing

Feel free to open issues or submit pull requests if you have ideas to improve or expand the tool.

## 🪪 License

MIT License. See [`LICENSE`](LICENSE) for details.
