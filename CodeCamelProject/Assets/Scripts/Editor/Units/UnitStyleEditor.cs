using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Unit.UnitStyle_SO))]
public class UnitStyleEditor : Editor{
    #region Variables
    SerializedProperty _styleCostProperty;
    #endregion Variables

    private void OnEnable(){
        _styleCostProperty = serializedObject.FindProperty("_styleCost");
    }


    public override void OnInspectorGUI(){
        base.OnInspectorGUI();

        //Cost of the style
        GUILayout.BeginVertical("box", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
        GUILayout.Label("STYLE COST", StaticEditor.labelStyle);

        int lastIndex = 0;
        for(int cost = 0; cost < _styleCostProperty.arraySize; cost++){
            GUILayout.BeginHorizontal("box", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
            var styleCost = _styleCostProperty.GetArrayElementAtIndex(cost);
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Elemement :", StaticEditor.labelStyle);
            EditorGUILayout.PropertyField(styleCost.FindPropertyRelative("_elements"), GUIContent.none);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Value :", StaticEditor.labelStyle);
            EditorGUILayout.PropertyField(styleCost.FindPropertyRelative("_value"), GUIContent.none);
            GUILayout.EndHorizontal();
            serializedObject.ApplyModifiedProperties();
            GUILayout.EndVertical();
            if(GUILayout.Button("-", StaticEditor.buttonStyle, GUILayout.Width(20))){
                _styleCostProperty.DeleteArrayElementAtIndex(cost);
                serializedObject.ApplyModifiedProperties();
            }
            GUILayout.EndHorizontal();
            lastIndex = cost;
        }

        //Button to add a cost
        if(GUILayout.Button("Add effect", StaticEditor.buttonStyle)){
            _styleCostProperty.InsertArrayElementAtIndex(lastIndex == 0 ? 0 : lastIndex + 1);
            serializedObject.ApplyModifiedProperties();
        }
        GUILayout.EndVertical();


    }
}
