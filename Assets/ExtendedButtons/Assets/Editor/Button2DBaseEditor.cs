using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

namespace ExtendedButtons.Editor
{
    [CustomEditor(typeof(Button2DBase))]
    public class Button2DBaseEditor : ButtonEditor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            Button2DBase targetButton = (Button2DBase)target;

            SerializedProperty property = serializedObject.FindProperty("onEnter");
            EditorGUILayout.PropertyField(property, new GUILayoutOption[0]);

            property = serializedObject.FindProperty("onDown");
            EditorGUILayout.PropertyField(property, new GUILayoutOption[0]);

            property = serializedObject.FindProperty("onUp");
            EditorGUILayout.PropertyField(property, new GUILayoutOption[0]);

            property = serializedObject.FindProperty("onExit");
            EditorGUILayout.PropertyField(property, new GUILayoutOption[0]);

            serializedObject.ApplyModifiedProperties();
        }
    }
}