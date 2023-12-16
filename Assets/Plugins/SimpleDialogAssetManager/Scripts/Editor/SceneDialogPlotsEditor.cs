using System;
using System.Collections.Generic;
using DialogSystem.Structure.ScriptableObjects;
using UnityEditor;

namespace DialogSystem.Editor
{
    [CustomEditor(typeof(SceneDialogPlots))]
    public class SceneDialogPlotsEditor : UnityEditor.Editor
    {
        private SceneDialogPlots _target;
        private void OnEnable() {
            _target = (SceneDialogPlots)serializedObject.targetObject;
        }
        public override void OnInspectorGUI()
        {
            List<string> excludeMember = new List<string>(){"m_Script"};
            serializedObject.Update();
            EditorGUI.BeginChangeCheck();
            if (!_target.UseStartUpPlot) {
                excludeMember.Add("_startUpPlotId");
            }
            DrawPropertiesExcluding(serializedObject, excludeMember.ToArray());
            serializedObject.ApplyModifiedProperties();
        }
        
    }
}