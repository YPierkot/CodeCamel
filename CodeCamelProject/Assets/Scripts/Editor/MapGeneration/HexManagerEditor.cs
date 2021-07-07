using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using static UnityEditor.Progress;

[CustomEditor(typeof(Map.HexManager)), CanEditMultipleObjects]
public class HexManagerEditor : Editor{
    SerializedProperty playerProperty;
    SerializedProperty effectProperty;

    private void OnEnable(){
        playerProperty = serializedObject.FindProperty("_playerCanPose");
        effectProperty = serializedObject.FindProperty("_terrainType");
    }

    public override void OnInspectorGUI(){
        serializedObject.Update();
        Map.HexManager script = (Map.HexManager)target;

        //Which player can put units on this hex
        GUILayout.BeginVertical("box", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
        GUILayout.Label("WHICH PLAYER CAN PUT UNIT ON :", StaticEditor.labelStyle);
        GUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(playerProperty, GUIContent.none);
        serializedObject.ApplyModifiedProperties();
        if(GUILayout.Button("Change Player", GUILayout.Width(100))){
            playerProperty.enumValueIndex = (int)playerProperty.enumValueIndex == 1 ? 2 : 1;
            serializedObject.ApplyModifiedProperties();
        }
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();

        StaticEditor.Space(10);

        //Which effect are on this hex
        GUILayout.BeginVertical("box", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
        GUILayout.Label("TERRAIN EFFECT", StaticEditor.labelStyle);

        int lastindex = 0;
        for(int effect = 0; effect < effectProperty.arraySize; effect++){
            GUILayout.BeginHorizontal("box", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
            var terEffect = effectProperty.GetArrayElementAtIndex(effect);
            EditorGUILayout.PropertyField(terEffect, GUIContent.none);
            serializedObject.ApplyModifiedProperties();
            if(GUILayout.Button("-", StaticEditor.buttonStyle, GUILayout.Width(20))){
                effectProperty.DeleteArrayElementAtIndex(effect);
                serializedObject.ApplyModifiedProperties();
            }
            GUILayout.EndHorizontal();
            lastindex = effect;
        }

        if(GUILayout.Button("Add effect", StaticEditor.buttonStyle)){
            effectProperty.InsertArrayElementAtIndex(lastindex == 0? 0 : lastindex + 1);
            serializedObject.ApplyModifiedProperties();
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
