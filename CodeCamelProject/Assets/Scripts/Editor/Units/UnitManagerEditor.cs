using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Unit.UnitManager))]
public class UnitManagerEditor : Editor{
    SerializedProperty SOproperty;
    SerializedProperty armyProperty;

    private void OnEnable(){
        SOproperty = serializedObject.FindProperty("_unitScriptable");
        armyProperty = serializedObject.FindProperty("_player");

        //Refresh the object
        Unit.UnitManager script = (Unit.UnitManager)target;
    }

    /// <summary>
    /// Draw the inspector
    /// </summary>
    public override void OnInspectorGUI(){
        serializedObject.Update();
        Unit.UnitManager script = (Unit.UnitManager)target;

        Unit.UnitVariables unitVar = new Unit.UnitVariables();
        if(script._unitScriptable != null) unitVar = script._unitScriptable.GetStat();

        //Show the ScriptableObject Slot and a button to refresh the variable
        GUILayout.BeginVertical("box", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
        GUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(SOproperty, new GUIContent("Unit Scriptable"));
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
        GUILayout.BeginHorizontal();
        if(script._unitScriptable != null){
            if(GUILayout.Button("CHANGE ARMY")){
                armyProperty.enumValueIndex = armyProperty.enumValueIndex == 1 ? 2 : 1;
                serializedObject.ApplyModifiedProperties();
            }
            GUILayout.Label($"ARMY : {(EnumScript.PlayerSide) armyProperty.enumValueIndex}", StaticEditor.labelStyle);
        }
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();

        if(script._unitScriptable == null) return;

        StaticEditor.Space(5);
        GUILayout.BeginVertical("box", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
        if(GUILayout.Button(script.runTimeData? "CLOSE RUNTIME DATA" : "OPEN RUNTIME DATA", StaticEditor.buttonStyle,  GUILayout.ExpandWidth(true))){
            script.runTimeData = !script.runTimeData;
        }

        if(script.runTimeData){
            int iconSize = 20;

            //MANA PROGRESS BAR
            GUILayout.BeginVertical("box", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
            GUILayout.Label("UNIT STAT", StaticEditor.labelStyle);

            //LIFE PROGRESS BAR
            GUILayout.BeginHorizontal();
            GUILayout.Label(AssetDatabase.LoadAssetAtPath("Assets/AssetData/Icon/Life.png", typeof(Texture2D)) as Texture2D, GUILayout.Width(iconSize), GUILayout.Height(iconSize));
            StaticEditor.ProgressBar(script.UnitLife / unitVar._life, $"Life : {script.UnitLife} / {unitVar._life}");
            //DEAL DAMAGE
            if(GUILayout.Button("-", GUILayout.Width(20))){
                if(script.UnitLife - 1 >= 0) script.TakeDamage(1);
                else Debug.LogError("Can't deal more damage. The UnitLife is already at 0");
            }
            //GIVE LIFE
            if(GUILayout.Button("+", GUILayout.Width(20))){
                if(script.UnitLife + 1 <= unitVar._life) script.TakeDamage(-1);
                else Debug.LogError($"Can't give more life to the unit. The Unit is already at maxLife : {unitVar._life}");
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label(AssetDatabase.LoadAssetAtPath("Assets/AssetData/Icon/Mana.png", typeof(Texture2D)) as Texture2D, GUILayout.Width(iconSize), GUILayout.Height(iconSize));
            StaticEditor.ProgressBar(script.ManaGain / script.ManaMax, $"Mana : {script.ManaGain} / {script.ManaMax}");
            //REDUCE MANA
            if(GUILayout.Button("-", GUILayout.Width(20))){
                if(script.ManaGain - 1 >= 0) script.AddMana(-1);
                else Debug.LogError("Can't reduce mana anymore. The UnitMana is already at 0");
            }
            //GIVE MANA
            if(GUILayout.Button("+", GUILayout.Width(20))){
                if(script.ManaGain + 1 <= script.ManaMax) script.AddMana(1);
                else Debug.LogError($"Can't give more mana to the unit. The Unit is already at maxMana : {script.ManaMax}");
            }
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();

            GUILayout.BeginVertical("box", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
            GUILayout.Label("HEX UNDER UNIT", StaticEditor.labelStyle);
            GUI.enabled = false;
            EditorGUILayout.ObjectField(script.HexUnderUnit, typeof(GameObject), allowSceneObjects: true);
            GUI.enabled = true;
            GUILayout.EndVertical();
        }
        GUILayout.EndVertical();
    }
}
