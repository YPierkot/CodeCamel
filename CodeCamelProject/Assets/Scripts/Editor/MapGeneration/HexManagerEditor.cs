using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Map.HexManager))]
public class HexManagerEditor : Editor{
    public override void OnInspectorGUI(){
        Repaint();
        Map.HexManager script = (Map.HexManager)target;
        
        GUILayout.BeginVertical("box", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
        GUILayout.Label("TERRAIN EFFECT", StaticEditor.labelStyle);
        int i = 0;


        for(int item = 0; item < script.TerrainType.Count; item++){
            GUILayout.BeginHorizontal("box", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
            script.TerrainType[item] = (Map.TerrainType) EditorGUILayout.EnumPopup($"Terrain {item}", script.TerrainType[item]);
            if(GUILayout.Button("-", StaticEditor.buttonStyle, GUILayout.Width(20))){
                script.removeTerrainEffect(item);
            }
            GUILayout.EndHorizontal();
        }

        if(GUILayout.Button("Add effect", StaticEditor.buttonStyle)){
            script.AddTerrainEffect();
        }
        GUILayout.EndVertical();

        StaticEditor.Space(5);

        GUILayout.BeginVertical("box", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
        GUILayout.Label("UNIT ON HEX", StaticEditor.labelStyle);
        GUI.enabled = false;
        EditorGUILayout.ObjectField(script.UnitOnHex, typeof(GameObject), allowSceneObjects: true);
        GUI.enabled = true;
        GUILayout.EndVertical();
    }
}
