using UnityEditor;
using UnityEngine;

namespace Editor.Dialogs
{
    public class DialogEditor : EditorWindow
    {
        //Create new Dialog Window
        [MenuItem("Window/Dialog Editor")]
        public static void ShowWindow()
        {
            EditorWindow window = GetWindow<DialogEditor>();
            window.titleContent = new GUIContent("Dialog Editor");
        }
    }
}