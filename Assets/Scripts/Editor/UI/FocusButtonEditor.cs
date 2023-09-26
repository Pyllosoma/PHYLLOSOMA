using Runtime.UI.Components.Buttons;
using UnityEditor;

namespace Editor.UI
{
    [CustomEditor(typeof(FocusButton))]
    public class FocusButtonEditor : UnityEditor.UI.ButtonEditor
    {
        private SerializedProperty _focusOnStart;
        private SerializedProperty _onFocusIn;
        private SerializedProperty _onFocusOut;
        protected override void OnEnable()
        {
            base.OnEnable();
            _focusOnStart = serializedObject.FindProperty("_focusOnStart");
            _onFocusIn = serializedObject.FindProperty("_onFocusIn");
            _onFocusOut = serializedObject.FindProperty("_onFocusOut");
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.Space();
            serializedObject.Update();
            EditorGUILayout.PropertyField(_focusOnStart);
            EditorGUILayout.PropertyField(_onFocusIn);
            EditorGUILayout.PropertyField(_onFocusOut);
            serializedObject.ApplyModifiedProperties();
        }
    }
}