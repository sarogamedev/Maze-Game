 using System.IO;
 using Assets.Scripts;
 using UnityEditor;
 using UnityEngine;

 public class DebugConsole : EditorWindow
{
    [MenuItem("Window/Debug Console")]
    public static void ShowWindow()
    {
        GetWindow<DebugConsole>("Debug");
    }
    private void OnGUI()
    {
        GUILayout.Label("Debug Console", EditorStyles.boldLabel);

        if (GUILayout.Button("Delete Save Data"))
        {
            DeleteSaveData();
        }
    }

    private static void DeleteSaveData()
    {
        File.Delete(SaveSystem.path);
        Debug.Log("Save Data Deleted! restart into play mode to take effect");
    }
}
