using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(Unit.Units_SO))]
public class UnitSOEditor : Editor{
    #region Variables
    //NAME
    SerializedProperty _unitNameProperty;
    bool changeName = false;
    SerializedProperty _familyProperty;

    //ELEMENTS
    SerializedProperty _elementProperty;
    bool _elementListOpen;

    //STAT
    SerializedProperty _lifeProperty;
    SerializedProperty _atkPerSecProperty;
    SerializedProperty _CritChanceProperty;
    SerializedProperty _critValueProperty;
    SerializedProperty _dodgeValueProperty;
    SerializedProperty _attackRangeProperty;
    SerializedProperty _MoveSpeedProperty;
    SerializedProperty _meshProperty;

    //ABILITY
    SerializedProperty _styleListProperty;
    bool _styleListOpen;
    #endregion Variables

    private void OnEnable(){
        _unitNameProperty = serializedObject.FindProperty("_unitName");
        _familyProperty = serializedObject.FindProperty("_unitFamily");
        _elementProperty = serializedObject.FindProperty("_unitElement");

        _lifeProperty = serializedObject.FindProperty("_life");

        _atkPerSecProperty = serializedObject.FindProperty("_attackPerSecond");
        _attackRangeProperty = serializedObject.FindProperty("_attackRange");

        _CritChanceProperty = serializedObject.FindProperty("_critChance");
        _critValueProperty = serializedObject.FindProperty("_critValueMultiplier");

        _dodgeValueProperty = serializedObject.FindProperty("_evasionRate");

        _MoveSpeedProperty = serializedObject.FindProperty("_moveSPeed");

        _meshProperty = serializedObject.FindProperty("_basicMesh");

        _styleListProperty = serializedObject.FindProperty("_styleList");
    }

    /// <summary>
    /// Redraw the inspector with a custom one
    /// </summary>
    public override void OnInspectorGUI(){
        serializedObject.Update();

        #region NameAndFamily
        //NAME
        StaticEditor.VerticalBox();
        GUILayout.BeginHorizontal();
        if(!changeName){
            GUILayout.Label(_unitNameProperty.stringValue, StaticEditor.labelTitleStyle);
        }
        else{
            EditorGUILayout.PropertyField(_unitNameProperty, GUIContent.none);
        }
        if(GUILayout.Button(new GUIContent(EditorGUIUtility.IconContent("d_CollabEdit Icon", "Edit the scriptable of this Unit")), GUILayout.Width(30), GUILayout.Height(20))){
            changeName = !changeName;
        }
        GUILayout.EndHorizontal();
        
        //FAMILY
        string[] enumList = Enum.GetNames(typeof(EnumScript.UnitsFamily));
        _familyProperty.enumValueIndex = GUILayout.Toolbar(_familyProperty.enumValueIndex, enumList);
        serializedObject.ApplyModifiedProperties();
        GUILayout.EndHorizontal();
        #endregion NameAndFamily

        StaticEditor.Space(5);

        #region Element
        StaticEditor.VerticalBox();
        if(GUILayout.Button("UNIT ELEMENTS", StaticEditor.buttonTitleStyle)){
            _elementListOpen = !_elementListOpen;
        }

        if(_elementListOpen)
        {
            int lastIndex = 0;
            for(int cost = 0; cost < _elementProperty.arraySize; cost++){
                StaticEditor.HorizontalBox();
                GUILayout.Label($"Element 00{cost + 1} :", StaticEditor.labelStyle);
                EditorGUILayout.PropertyField(_elementProperty.GetArrayElementAtIndex(cost), GUIContent.none);
                serializedObject.ApplyModifiedProperties();

                if(GUILayout.Button("-", StaticEditor.buttonTitleStyle, GUILayout.Width(20), GUILayout.Height(19))){
                    _elementProperty.DeleteArrayElementAtIndex(cost);
                    serializedObject.ApplyModifiedProperties();
                }
                GUILayout.EndHorizontal();
                lastIndex = cost;
            }

            //Button to add a cost
            if(GUILayout.Button("Add effect", StaticEditor.buttonStyle))
            {
                _elementProperty.InsertArrayElementAtIndex(lastIndex == 0 ? 0 : lastIndex + 1);
                serializedObject.ApplyModifiedProperties();
            }
        }
        GUILayout.EndVertical();
        #endregion Element

        StaticEditor.Space(5);

        #region Stat
        StaticEditor.VerticalBox();
        GUILayout.Label("UNIT DATA", StaticEditor.labelTitleStyle);

        //LIFE
        StaticEditor.HorizontalBox();
        GUILayout.Label("unit Life :", StaticEditor.labelStyle, GUILayout.Width( 100));
        EditorGUILayout.PropertyField(_lifeProperty, GUIContent.none);
        serializedObject.ApplyModifiedProperties();
        GUILayout.EndHorizontal();


        //ATTACK
        StaticEditor.VerticalBox();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Attack Speed :", StaticEditor.labelStyle, GUILayout.Width(100));
        EditorGUILayout.PropertyField(_atkPerSecProperty, GUIContent.none);
        serializedObject.ApplyModifiedProperties();
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Attack Range :", StaticEditor.labelStyle, GUILayout.Width(100));
        EditorGUILayout.PropertyField(_attackRangeProperty, GUIContent.none);
        serializedObject.ApplyModifiedProperties();
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();


        //CRIT DAMAGE
        StaticEditor.VerticalBox();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Crit Luck :", StaticEditor.labelStyle, GUILayout.Width(100));
        EditorGUILayout.PropertyField(_CritChanceProperty, GUIContent.none);
        serializedObject.ApplyModifiedProperties();
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Crit Value :", StaticEditor.labelStyle, GUILayout.Width(100));
        EditorGUILayout.PropertyField(_critValueProperty, GUIContent.none);
        serializedObject.ApplyModifiedProperties();
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();


        //DODGE
        StaticEditor.HorizontalBox();
        GUILayout.Label("Dodge Rate :", StaticEditor.labelStyle, GUILayout.Width(100));
        EditorGUILayout.PropertyField(_dodgeValueProperty, GUIContent.none);
        serializedObject.ApplyModifiedProperties();
        GUILayout.EndHorizontal();


        //MOVESPEED
        StaticEditor.HorizontalBox();
        GUILayout.Label("Move Speed :", StaticEditor.labelStyle, GUILayout.Width(100));
        EditorGUILayout.PropertyField(_MoveSpeedProperty, GUIContent.none);
        serializedObject.ApplyModifiedProperties();
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
        #endregion Stat

        StaticEditor.Space(5);

        #region Mesh
        StaticEditor.VerticalBox();
        GUILayout.Label("UNIT MESH", StaticEditor.labelTitleStyle);
        EditorGUILayout.PropertyField(_meshProperty, GUIContent.none);
        serializedObject.ApplyModifiedProperties();
        GUILayout.EndVertical();
        #endregion Mesh

        StaticEditor.Space(5);

        #region Style
        StaticEditor.VerticalBox();
        GUILayout.Label("UNIT STYLE", StaticEditor.labelTitleStyle);

        int lastIndex2 = 0;
        for(int cost = 0; cost < _styleListProperty.arraySize; cost++){
            StaticEditor.HorizontalBox();
            GUILayout.Label($"Style 00{cost + 1} :", StaticEditor.labelStyle, GUILayout.Width(100));
            EditorGUILayout.PropertyField(_styleListProperty.GetArrayElementAtIndex(cost), GUIContent.none);
            serializedObject.ApplyModifiedProperties();

            if(GUILayout.Button("-", StaticEditor.buttonTitleStyle, GUILayout.Width(20), GUILayout.Height(19))) {
                _styleListProperty.DeleteArrayElementAtIndex(cost);
                serializedObject.ApplyModifiedProperties();
            }
            if(GUILayout.Button(new GUIContent(EditorGUIUtility.IconContent("d_CollabEdit Icon", "Edit the scriptable of this Unit")), GUILayout.Width(30), GUILayout.Height(20))) {
                Selection.activeObject = AssetDatabase.LoadMainAssetAtPath(AssetDatabase.GetAssetPath(_styleListProperty.GetArrayElementAtIndex(cost).objectReferenceValue));
                EditorApplication.ExecuteMenuItem("Assets/Properties...");
                Selection.activeObject = AssetDatabase.LoadMainAssetAtPath(AssetDatabase.GetAssetPath(target));
            }
            GUILayout.EndHorizontal();
            lastIndex2 = cost;
        }

        //Button to add a cost
        if(GUILayout.Button("Add Style", StaticEditor.buttonStyle)) {
            _styleListProperty.InsertArrayElementAtIndex(lastIndex2 == 0 ? 0 : lastIndex2 + 1);
            serializedObject.ApplyModifiedProperties();
        }
        GUILayout.EndVertical();
        #endregion Style
    }
}
