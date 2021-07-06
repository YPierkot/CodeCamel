using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Map.MapGeneration))]
public class MapGenerationEditor : Editor{
    public override void OnInspectorGUI(){
        Map.MapGeneration script = (Map.MapGeneration)target;

        GUILayout.BeginVertical("box", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
        GUILayout.Label("MAP CREATION", StaticEditor.labelStyle);

        StaticEditor.Space(10);

        script.MeshToCreate = (GameObject)EditorGUILayout.ObjectField("Prefab to Use", script.MeshToCreate, typeof(GameObject), allowSceneObjects: true);

        GUILayout.BeginHorizontal();
        GUILayout.Label("X :", StaticEditor.labelStyle);
        script.XSize = EditorGUILayout.IntField((int) Mathf.Clamp(script.XSize, 0, 15));
        StaticEditor.Space(5);
        GUILayout.Label("Y :", StaticEditor.labelStyle);
        script.YSize = EditorGUILayout.IntField((int)Mathf.Clamp(script.YSize, 0, 15));
        StaticEditor.Space(5);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal("box", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
        if(GUILayout.Button("Create Terrain", StaticEditor.buttonStyle)){
            script.GenerateMap();
        }
        if(GUILayout.Button("Delete Terrain", StaticEditor.buttonStyle)){
            script.DeleteMap();
        }
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
    }
}
