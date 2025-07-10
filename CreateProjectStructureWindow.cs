using UnityEditor;
using UnityEngine;
using System.IO;

public class CreateProjectStructureWindow : EditorWindow
{
    private string _defaultProjectPath = "MyNewProject";

    private const string ROOT = "Assets";

    private static readonly string[] DefaultFolders = {
        "Materials",
        "Models",
        "Prefabs",
        "Scenes",
        "Scripts",
        "Textures"
    };

    [MenuItem("Tools/Create Project Folder Structure")]
    public static void ShowWindow()
    {
        var window = GetWindow<CreateProjectStructureWindow>("Create Project Structure");
        window.minSize = new Vector2(320, 120);
    }

    private void OnGUI()
    {
        GUILayout.Label("Create New Project Structure", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        _defaultProjectPath = EditorGUILayout.TextField("Project Path", _defaultProjectPath);

        if (GUILayout.Button("Create Structure"))
        {
            CreateStructure();
        }
    }

    private void CreateStructure()
    {
        if (string.IsNullOrEmpty(_defaultProjectPath))
        {
            EditorUtility.DisplayDialog("Invalid Input", "Please enter a valid project path.", "OK");
            return;
        }

        string fullPath = Path.Combine(ROOT, _defaultProjectPath);

        if (AssetDatabase.IsValidFolder(fullPath))
        {
            EditorUtility.DisplayDialog("Folder Exists", $"The folder \"{fullPath}\" already exists.", "OK");
            return;
        }

        // create folders under the path
        string[] subFolders = _defaultProjectPath.Split('/');
        string currentPath = ROOT;

        foreach (var folder in subFolders)
        {
            string nextPath = Path.Combine(currentPath, folder);
            if (!AssetDatabase.IsValidFolder(nextPath))
            {
                AssetDatabase.CreateFolder(currentPath, folder);
            }
            currentPath = nextPath;
        }

        // create sub-folders using DefaultFolders
        foreach (var folder in DefaultFolders)
        {
            string subPath = Path.Combine(currentPath, folder);
            if (!AssetDatabase.IsValidFolder(subPath))
            {
                AssetDatabase.CreateFolder(currentPath, folder);
            }
        }

        AssetDatabase.Refresh();

        EditorUtility.DisplayDialog("Success", $"Project folder structure created at:\n{fullPath}", "OK");
    }
}