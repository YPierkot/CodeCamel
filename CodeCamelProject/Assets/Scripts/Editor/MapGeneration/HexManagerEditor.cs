using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Map.HexManager)), CanEditMultipleObjects]
public class HexManagerEditor : Editor{
    public override void OnInspectorGUI(){
        Repaint();
        Map.HexManager script = (Map.HexManager)target;

        //Which player can put units on this hex
        GUILayout.BeginVertical("box", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
        GUILayout.Label("WHICH PLAYER CAN PUT UNIT ON :", StaticEditor.labelStyle);
        GUILayout.BeginHorizontal();
        script.PlayerCanPose = (EnumScript.PlayerSide)EditorGUILayout.EnumPopup(script.PlayerCanPose, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
        if(GUILayout.Button("Change Player", GUILayout.Width(100))){
            script.PlayerCanPose = (int)script.PlayerCanPose == 1 ? (EnumScript.PlayerSide)2 : (EnumScript.PlayerSide)1;
        }
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();

        StaticEditor.Space(10);

        //Which effect are on this hex
        GUILayout.BeginVertical("box", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
        GUILayout.Label("TERRAIN EFFECT", StaticEditor.labelStyle);
        for(int item = 0; item < script.TerrainType.Count; item++){
            GUILayout.BeginHorizontal("box", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
            script.TerrainType[item] = (EnumScript.TerrainType) EditorGUILayout.EnumPopup($"Terrain {item}", script.TerrainType[item]);
            if(GUILayout.Button("-", StaticEditor.buttonStyle, GUILayout.Width(20))){
                script.removeTerrainEffect(item);
            }
            GUILayout.EndHorizontal();
        }
        if(GUILayout.Button("Add effect", StaticEditor.buttonStyle)){
            script.AddTerrainEffect();
        }
        GUILayout.EndVertical();

        StaticEditor.Space(10);

        //Which unit is on this hex
        GUILayout.BeginVertical("box", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
        GUILayout.Label("UNIT ON HEX", StaticEditor.labelStyle);
        GUI.enabled = false;
        EditorGUILayout.ObjectField(script.UnitOnHex, typeof(GameObject), allowSceneObjects: true);
        GUI.enabled = true;
        GUILayout.EndVertical();
    }
}
