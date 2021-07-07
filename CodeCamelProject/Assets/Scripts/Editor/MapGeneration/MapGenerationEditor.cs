using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Map.MapGeneration))]
public class MapGenerationEditor : Editor{
    SerializedProperty xSizeProperty;
    SerializedProperty ySizeProperty;
    SerializedProperty objectProperty;

    private void OnEnable(){
        xSizeProperty = serializedObject.FindProperty("_xSize");
        ySizeProperty = serializedObject.FindProperty("_ySize");
        objectProperty = serializedObject.FindProperty("_meshToCreate");
    }

    public override void OnInspectorGUI(){
        Map.MapGeneration script = (Map.MapGeneration)target;

        GUILayout.BeginVertical("box", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
        GUILayout.Label("MAP CREATION", StaticEditor.labelTitleStyle);

        StaticEditor.Space(10);

        GUILayout.BeginHorizontal();
        GUILayout.Label("OBJECT TO SPAWN :", StaticEditor.labelTitleStyle);
        EditorGUILayout.PropertyField(objectProperty, GUIContent.none);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("X :", StaticEditor.labelTitleStyle);
        EditorGUILayout.PropertyField(xSizeProperty, GUIContent.none);
        xSizeProperty.intValue = Mathf.Clamp(xSizeProperty.intValue, 0, 15);
        serializedObject.ApplyModifiedProperties();

        StaticEditor.Space(5);

        GUILayout.Label("Y :", StaticEditor.labelTitleStyle);
        EditorGUILayout.PropertyField(ySizeProperty, GUIContent.none);
        ySizeProperty.intValue = Mathf.Clamp(ySizeProperty.intValue, 0, 15);
        serializedObject.ApplyModifiedProperties();
        StaticEditor.Space(5);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal("box", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
        if(GUILayout.Button("Create Terrain", StaticEditor.buttonTitleStyle)){
            script.GenerateMap();
        }
        if(GUILayout.Button("Delete Terrain", StaticEditor.buttonTitleStyle)){
            script.DeleteMap();
        }
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
    }
}
