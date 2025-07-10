# Unity Project Structure Creator

This is a lightweight Unity editor tool that helps you quickly generate a standardized folder structure for your Unity project — inspired by [themorfeus/unity_project_structure](https://github.com/themorfeus/unity_project_structure).

## ✨ Features

- One-click creation of commonly used folders for Unity projects
- Follows scalable folder structure practices
- Helps teams and solo devs maintain consistent project layout

## 📁 Default Folder Structure

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
3. Enter a name or a relative path.
4. Click the **Create Folders** button.
> ℹ️ The folder structure will be created relative to `Assets/`, based on the name or path you enter.

## 📦 Installation

Download the `CreateProjectStructureWindow.cs` file and place it anywhere inside an `Editor/` folder in your Unity project.

## 🤝 Contributing

Feel free to open issues or submit pull requests if you have ideas to improve or expand the tool.

## 🪪 License

MIT License. See [`LICENSE`](LICENSE) for details.
