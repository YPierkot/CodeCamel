using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Map.MapGeneration))]
public class MapGenerationEditor : Editor{
    #region Variables
    SerializedProperty xSizeProperty;
    SerializedProperty ySizeProperty;
    SerializedProperty objectProperty;
    #endregion Variables

    private void OnEnable(){
        xSizeProperty = serializedObject.FindProperty("_xSize");
        ySizeProperty = serializedObject.FindProperty("_ySize");
        objectProperty = serializedObject.FindProperty("_meshToCreate");
    }

    public override void OnInspectorGUI(){
        Map.MapGeneration script = (Map.MapGeneration)target;

        #region GridInfo
        StaticEditor.VerticalBox();
        GUILayout.Label("MAP CREATION", StaticEditor.labelTitleStyle);

        GUILayout.BeginHorizontal();
        GUILayout.Label("OBJECT TO SPAWN :", StaticEditor.labelStyle);
        EditorGUILayout.PropertyField(objectProperty, GUIContent.none);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("X :", StaticEditor.labelStyle);
        EditorGUILayout.PropertyField(xSizeProperty, GUIContent.none);
        xSizeProperty.intValue = Mathf.Clamp(xSizeProperty.intValue, 0, 15);
        serializedObject.ApplyModifiedProperties();

        StaticEditor.Space(5);

        GUILayout.Label("Y :", StaticEditor.labelStyle);
        EditorGUILayout.PropertyField(ySizeProperty, GUIContent.none);
        ySizeProperty.intValue = Mathf.Clamp(ySizeProperty.intValue, 0, 15);
        serializedObject.ApplyModifiedProperties();
        StaticEditor.Space(5);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        if(GUILayout.Button("Create Terrain", StaticEditor.buttonStyle)){
            script.GenerateMap();
        }
        if(GUILayout.Button("Delete Terrain", StaticEditor.buttonStyle)){
            script.DeleteMap();
        }
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
        #endregion GridInfo
    }
}
