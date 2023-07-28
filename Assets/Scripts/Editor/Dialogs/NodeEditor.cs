using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace Editor.Dialogs
{
    public class NodeEditor : EditorWindow
    {

        List<Rect> windows = new List<Rect>();
        private List<string> _target = new List<string>();
        List<int> windowsToAttach = new List<int>();
        List<int> attachedWindows = new List<int>();

        [MenuItem("Window/Node editor")]
        static void ShowEditor()
        {
            const int width = 600;
            const int height = 600;

            var x = (Screen.currentResolution.width - width) / 2;
            var y = (Screen.currentResolution.height - height) / 2;

            GetWindow<NodeEditor>().position = new Rect(x, y, width, height);
        }


        void OnGUI()
        {
            //Rect graphPosition = new Rect(0f, 0f, position.width, position.height);

            if (windowsToAttach.Count == 2)
            {
                attachedWindows.Add(windowsToAttach[0]);
                attachedWindows.Add(windowsToAttach[1]);
                windowsToAttach = new List<int>();
            }

            if (attachedWindows.Count >= 2)
            {
                for (int i = 0; i < attachedWindows.Count; i += 2)
                {
                    DrawNodeCurve(windows[attachedWindows[i]], windows[attachedWindows[i + 1]]);
                }
            }

            BeginWindows();

            if (GUILayout.Button("Create Node"))
            {
                windows.Add(new Rect(10, 10, 200, 100));
                _target.Add("");
            }

            for (int i = 0; i < windows.Count; i++)
            {
                windows[i] = GUI.Window(i, windows[i], DrawNodeWindow, "Window " + i);
            }

            EndWindows();
        }


        void DrawNodeWindow(int id)
        {
            
            //Create text field for node name
            GUILayout.Label("test");
            _target[id] = GUILayout.TextField(_target[id]);
            if (GUILayout.Button("Attach")) {
                windowsToAttach.Add(id);
            }
            GUI.DragWindow();
        }


        void DrawNodeCurve(Rect start, Rect end)
        {
            Vector3 startPos = new Vector3(start.x + start.width, start.y + start.height / 2, 0);
            Vector3 endPos = new Vector3(end.x, end.y + end.height / 2, 0);
            Vector3 startTan = startPos + Vector3.right * 50;
            Vector3 endTan = endPos + Vector3.left * 50;
            Color shadowCol = new Color(255, 255, 255);

            for (int i = 0; i < 3; i++)
            {// Draw a shadow
                //Handles.DrawBezier(startPos, endPos, startTan, endTan, shadowCol, null, (i + 1) * 5);
            }

            Handles.DrawBezier(startPos, endPos, startTan, endTan, Color.white, null, 5);
        }
    }
}