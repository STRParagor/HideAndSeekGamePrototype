using UnityEngine;
using UnityEditor;
using UnityEditor.UI;

namespace ParagorGames.Utils.UI.Editor
{
    [CustomEditor(typeof(ScrollRectEx))]
    public class ScrollRectExEditor : ScrollRectEditor
    {
        private SerializedProperty _parentScrollRectProp;
        private GUIContent _parentScrollRectGUIContent = new ("Parent ScrollRect");
 
        protected override void OnEnable()
        {
            base.OnEnable();
            _parentScrollRectProp = serializedObject.FindProperty("_parentScroll");
        }
 
        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(_parentScrollRectProp, _parentScrollRectGUIContent);
            base.OnInspectorGUI();
            serializedObject.Update();
            serializedObject.ApplyModifiedProperties();
        }
    }
}