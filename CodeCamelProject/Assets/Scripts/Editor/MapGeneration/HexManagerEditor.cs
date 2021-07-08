using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using static UnityEditor.Progress;

[CustomEditor(typeof(Map.HexManager)), CanEditMultipleObjects]
public class HexManagerEditor : Editor{
    #region Variables
    SerializedProperty _playerProperty;
    SerializedProperty _effectProperty;
    #endregion Variables

    private void OnEnable(){
        _playerProperty = serializedObject.FindProperty("_playerCanPose");
        _effectProperty = serializedObject.FindProperty("_terrainType");
    }

    public override void OnInspectorGUI(){
        serializedObject.Update();
        Map.HexManager script = (Map.HexManager)target;

        #region PlayerHex
        StaticEditor.VerticalBox();
        GUILayout.Label("WHICH PLAYER CAN PUT UNIT ON :", StaticEditor.labelTitleStyle); 
        string[] enumList = { "None", "Red Player", "Blue Player" };
        EditorGUI.BeginChangeCheck();
        _playerProperty.enumValueIndex = GUILayout.Toolbar(_playerProperty.enumValueIndex, enumList);
        serializedObject.ApplyModifiedProperties();
        if(EditorGUI.EndChangeCheck()){
            script.ReloadColor();
        }
        GUILayout.EndVertical();
        #endregion PlayerHex

        StaticEditor.Space(10);

        #region TerrainEffect
        StaticEditor.VerticalBox();
        GUILayout.Label("TERRAIN EFFECT", StaticEditor.labelTitleStyle);

        int lastindex = 0;
        for(int effect = 0; effect < _effectProperty.arraySize; effect++){
            GUILayout.BeginHorizontal("box", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
            var terEffect = _effectProperty.GetArrayElementAtIndex(effect);
            EditorGUILayout.PropertyField(terEffect, GUIContent.none);
            serializedObject.ApplyModifiedProperties();
            if(GUILayout.Button("-", StaticEditor.buttonTitleStyle, GUILayout.Width(20), GUILayout.Height(18))){
                _effectProperty.DeleteArrayElementAtIndex(effect);
                serializedObject.ApplyModifiedProperties();
            }
            GUILayout.EndHorizontal();
            lastindex = effect;
        }

        if(GUILayout.Button("Add effect", StaticEditor.buttonTitleStyle)){
            _effectProperty.InsertArrayElementAtIndex(lastindex == 0? 0 : lastindex + 1);
            serializedObject.ApplyModifiedProperties();
        }

        GUILayout.EndVertical();
        #endregion TerrainEffect

        StaticEditor.Space(10);

        #region UnitInfo
        StaticEditor.VerticalBox();
        GUILayout.Label("UNIT ON HEX", StaticEditor.labelTitleStyle);
        GUI.enabled = false;
        GUILayout.BeginHorizontal();
        GUILayout.Label("actual :", StaticEditor.labelStyle, GUILayout.Width(100));
        EditorGUILayout.ObjectField(script.UnitOnHex, typeof(GameObject), allowSceneObjects: true);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("next :", StaticEditor.labelStyle, GUILayout.Width(100));
        EditorGUILayout.ObjectField(script.TargetedUnit, typeof(GameObject), allowSceneObjects: true);
        GUILayout.EndHorizontal();
        GUI.enabled = true;
        GUILayout.EndVertical();
        #endregion UnitInfo
    }
}
