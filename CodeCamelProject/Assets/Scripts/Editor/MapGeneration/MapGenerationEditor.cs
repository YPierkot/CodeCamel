using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Map.MapGeneration))]
public class MapGenerationEditor : Editor{
    public override void OnInspectorGUI(){
        base.OnInspectorGUI();

        Map.MapGeneration script = (Map.MapGeneration)target;

        GUILayout.BeginHorizontal("box", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
        if(GUILayout.Button("Create Terrain")){
            script.GenerateMap();
        }
        if(GUILayout.Button("Delete Terrain")){
            script.DeleteMap();
        }
        GUILayout.EndHorizontal();
    }
}
