using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Unit.UnitManager))]
public class UnitManagerEditor : Editor{
    #region Variables
    //UNIT
    SerializedProperty _scriptableProperty;
    SerializedProperty _armyProperty;

    //UNIT DATA
    SerializedProperty _actualLifeProperty;
    SerializedProperty _actualManaProperty;
    #endregion Variables

    /// <summary>
    /// Update the variable when the object is selected
    /// </summary>
    private void OnEnable(){
        //UNIT
        _scriptableProperty = serializedObject.FindProperty("_unitScriptable");
        _armyProperty = serializedObject.FindProperty("_player");

        //UNIT DATA
        _actualLifeProperty = serializedObject.FindProperty("_unitLife");
        _actualManaProperty = serializedObject.FindProperty("_manaGain");

    }

    /// <summary>
    /// Redraw the inspector with a custom one
    /// </summary>
    public override void OnInspectorGUI(){
        serializedObject.Update();

        //ScriptableData
        Unit.UnitManager script = (Unit.UnitManager)target;
        Unit.UnitVariables unitVar = new Unit.UnitVariables();
        if(script._unitScriptable != null) unitVar = script._unitScriptable.GetStat();

        #region ScriptableObject
        StaticEditor.VerticalBox();
        GUILayout.Label("SCRIPTABLE DATA", StaticEditor.labelTitleStyle);
        GUILayout.BeginHorizontal();
        GUILayout.Label("Unit Data :", StaticEditor.labelStyle);
        EditorGUILayout.PropertyField(_scriptableProperty, GUIContent.none);
        serializedObject.ApplyModifiedProperties();

        if(script._unitScriptable != null){
            if(GUILayout.Button(new GUIContent(EditorGUIUtility.IconContent("d_Refresh", "Refresh all the data of this unit")), GUILayout.Width(30))){
                if(script._unitScriptable != null) script.RefreshData();
            }
            if(GUILayout.Button(new GUIContent(EditorGUIUtility.IconContent("d_CollabEdit Icon", "Edit the scriptable of this Unit")), GUILayout.Width(30), GUILayout.Height(20))){
                GameObject activObj = Selection.activeGameObject;
                Selection.activeObject = AssetDatabase.LoadMainAssetAtPath(AssetDatabase.GetAssetPath(script._unitScriptable));
                EditorApplication.ExecuteMenuItem("Assets/Properties...");
                Selection.activeObject = activObj;
            }
        }
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
        #endregion ScriptableObject

        StaticEditor.Space(5);

        #region Army
        StaticEditor.VerticalBox();
        GUILayout.Label("PLAYER ARMY", StaticEditor.labelTitleStyle);
        string[] enumList = { "None", "Red Player", "Blue Player" };

        _armyProperty.enumValueIndex = GUILayout.Toolbar(_armyProperty.enumValueIndex, enumList);
        serializedObject.ApplyModifiedProperties();

        GUILayout.EndVertical();
        #endregion Army

        StaticEditor.Space(5);

        #region RuntimeData
        StaticEditor.VerticalBox();
        if(GUILayout.Button(script.runTimeData ? "CLOSE RUNTIME DATA" : "OPEN RUNTIME DATA", StaticEditor.buttonTitleStyle)){
            script.runTimeData = !script.runTimeData;
        }

        if(script.runTimeData){
            int iconSize = 20;
            StaticEditor.VerticalBox();
            GUILayout.Label("UNIT STAT", StaticEditor.labelTitleStyle);

            //LIFE PROGRESS BAR
            GUILayout.BeginHorizontal();
            GUILayout.Label(AssetDatabase.LoadAssetAtPath("Assets/AssetData/Icon/Life.png", typeof(Texture2D)) as Texture2D, GUILayout.Width(iconSize), GUILayout.Height(iconSize));
            StaticEditor.ProgressBar(_actualLifeProperty.floatValue / unitVar._life, $"Life : {_actualLifeProperty.floatValue} / {unitVar._life}");

            //DEAL DAMAGE
            if(GUILayout.Button("-", GUILayout.Width(20))){
                if(_actualLifeProperty.floatValue - 1 >= 0) script.TakeDamage(1);
                else Debug.LogError("Can't deal more damage. The UnitLife is already at 0");
            }
            //GIVE LIFE
            if(GUILayout.Button("+", GUILayout.Width(20))){
                if(_actualLifeProperty.floatValue + 1 <= unitVar._life) script.TakeDamage(-1);
                else Debug.LogError($"Can't give more life to the unit. The Unit is already at maxLife : {unitVar._life}");
            }
            GUILayout.EndHorizontal();

            //MANA
            GUILayout.BeginHorizontal();
            GUILayout.Label(AssetDatabase.LoadAssetAtPath("Assets/AssetData/Icon/Mana.png", typeof(Texture2D)) as Texture2D, GUILayout.Width(iconSize), GUILayout.Height(iconSize));
            StaticEditor.ProgressBar(_actualManaProperty.floatValue / 10, $"Mana : {_actualManaProperty.floatValue} / {10}");
            //REDUCE MANA
            if(GUILayout.Button("-", GUILayout.Width(20))){
                if(_actualManaProperty.floatValue - 1 >= 0) script.AddMana(-1);
                else Debug.LogError("Can't reduce mana anymore. The UnitMana is already at 0");
            }
            //GIVE MANA
            if(GUILayout.Button("+", GUILayout.Width(20))){
                if(_actualManaProperty.floatValue + 1 <= 10) script.AddMana(1);
                else Debug.LogError($"Can't give more mana to the unit. The Unit is already at maxMana : {10}");
            }
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
            GUILayout.EndVertical();
        }
        GUILayout.EndVertical();
        #endregion RuntimeData
    }
}
