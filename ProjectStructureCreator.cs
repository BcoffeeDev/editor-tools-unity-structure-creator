using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace BCF.Editor
{
    public class ProjectStructureCreator : EditorWindow
    {
        private const string ROOT = "Assets";

        private readonly List<ProjectStructure> _structureOptions = new List<ProjectStructure>
        {
            new DefaultStructure(),
            new ScriptingStructure(),
            new ArtStructure()
        };

        private string[] _structureDisplayNames;
        private int _selectedStructureIndex = 0;
        private Dictionary<string, bool> _folderToggles = new Dictionary<string, bool>();

        private void OnEnable()
        {
            InitializeStructureOptions();
            LoadFolderToggles();
        }

        private void InitializeStructureOptions()
        {
            _structureDisplayNames = _structureOptions.Select(s => s.DisplayName).ToArray();
        }

        private ProjectStructure GetCurrentStructure() => _structureOptions[_selectedStructureIndex];

        private void LoadFolderToggles()
        {
            _folderToggles.Clear();
            foreach (var folder in GetCurrentStructure().Folders)
            {
                _folderToggles[folder] = true;
            }
        }

        [MenuItem("Tools/Create Project Folder Structure")]
        public static void ShowWindow()
        {
            var window = GetWindow<ProjectStructureCreator>("Project Structure Creator");
            window.minSize = new Vector2(320, 120);
        }

        private void OnGUI()
        {
            GUILayout.Label("Create New Project Structure", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            int newSelectedIndex = EditorGUILayout.Popup("Select Structure", _selectedStructureIndex, _structureDisplayNames);
            if (newSelectedIndex != _selectedStructureIndex)
            {
                _selectedStructureIndex = newSelectedIndex;
                LoadFolderToggles();
            }

            var current = GetCurrentStructure();
            current.ProjectPath = EditorGUILayout.TextField("Project Path", current.ProjectPath);

            GUILayout.Label("Select folders to Create:", EditorStyles.label);
            foreach (var folder in current.Folders)
            {
                if (!_folderToggles.TryGetValue(folder, out bool state))
                {
                    state = true;
                    _folderToggles[folder] = true;
                }

                _folderToggles[folder] = EditorGUILayout.ToggleLeft(folder, state);
            }
            EditorGUILayout.Space();

            if (GUILayout.Button("Create Structure"))
            {
                CreateStructure(current);
            }
        }

        private void CreateStructure(ProjectStructure structure)
        {
            if (string.IsNullOrEmpty(structure.ProjectPath))
            {
                EditorUtility.DisplayDialog("Invalid Input", "Please enter a valid project path.", "OK");
                return;
            }

            string fullPath = Path.Combine(ROOT, structure.ProjectPath);

            if (AssetDatabase.IsValidFolder(fullPath))
            {
                EditorUtility.DisplayDialog("Folder Exists", $"The folder \"{fullPath}\" already exists.", "OK");
                return;
            }

            string currentPath = ROOT;
            foreach (var folder in structure.ProjectPath.Split('/'))
            {
                string nextPath = Path.Combine(currentPath, folder);
                if (!AssetDatabase.IsValidFolder(nextPath))
                {
                    AssetDatabase.CreateFolder(currentPath, folder);
                }
                currentPath = nextPath;
            }

            foreach (var (folder, isEnabled) in _folderToggles)
            {
                if (!isEnabled) continue;
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

    [System.Serializable]
    public class ProjectStructure
    {
        public string DisplayName;
        public string ProjectPath;
        public List<string> Folders;
    }

    [System.Serializable]
    public class DefaultStructure : ProjectStructure
    {
        public DefaultStructure()
        {
            DisplayName = "Default";
            ProjectPath = "NewProject";
            Folders = new List<string> {
                "Materials",
                "Models",
                "Prefabs",
                "Scenes",
                "Scripts",
                "Textures"
            };
        }
    }

    [System.Serializable]
    public class ScriptingStructure : ProjectStructure
    {
        public ScriptingStructure()
        {
            DisplayName = "Scripting";
            ProjectPath = "NewScripts";
            Folders = new List<string>
            {
                "Editor",
                "Runtime",
                "Tests"
            };
        }
    }

    [System.Serializable]
    public class ArtStructure : ProjectStructure
    {
        public ArtStructure()
        {
            DisplayName = "Art";
            ProjectPath = "NewArt";
            Folders = new List<string>
            {
                "Animations",
                "Materials",
                "Prefabs",
                "Shaders",
                "Textures",
            };
        }
    }
}