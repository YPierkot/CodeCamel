using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Unit.UnitManager))]
public class UnitManagerEditor : Editor{
    SerializedObject mySerObj;
    SerializedProperty SOproperty;

    int dataNumber = 2;
    static int boxSize = 24;

    private void OnEnable(){
        mySerObj = new SerializedObject(target);
        SOproperty = mySerObj.FindProperty("_unitScriptable");

        //Refresh the object
        Unit.UnitManager script = (Unit.UnitManager)target;
    }

    /// <summary>
    /// Draw the inspector
    /// </summary>
    public override void OnInspectorGUI(){
        mySerObj.Update();
        Unit.UnitManager script = (Unit.UnitManager)target;
        Unit.UnitVariables unitVar = script._unitScriptable.GetStat();

        //Show the ScriptableObject Slot and a button to refresh the variable
        GUILayout.BeginHorizontal("box", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
        EditorGUILayout.PropertyField(SOproperty, new GUIContent("Unit Scriptable"));
        mySerObj.ApplyModifiedProperties();
        if(GUILayout.Button(new GUIContent(EditorGUIUtility.IconContent("d_Refresh", "Refresh all the data of this unit")), GUILayout.Width(30))){
            if(script._unitScriptable != null) script.RefreshData();
        }
        if(GUILayout.Button(new GUIContent(EditorGUIUtility.IconContent("d_CollabEdit Icon", "Edit the scriptable of this Unit")), GUILayout.Width(30), GUILayout.Height(20))){
            GameObject activObj = Selection.activeGameObject;
            Selection.activeObject = AssetDatabase.LoadMainAssetAtPath(AssetDatabase.GetAssetPath(script._unitScriptable));
            EditorApplication.ExecuteMenuItem("Assets/Properties...");
            Selection.activeObject = activObj;
        }
        GUILayout.EndHorizontal();

        Space(5);
        GUILayout.BeginVertical("box", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
        var style = new GUIStyle(GUI.skin.button) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold, fontSize = 12};
        if(GUILayout.Button(script.runTimeData? "CLOSE RUNTIME DATA" : "OPEN RUNTIME DATA", style,  GUILayout.ExpandWidth(true))){
            script.runTimeData = !script.runTimeData;
        }

        if(script.runTimeData){
            Space(5);
            int iconSize = 20;

            //MANA PROGRESS BAR
            GUILayout.BeginHorizontal();
            GUILayout.Label(AssetDatabase.LoadAssetAtPath("Assets/AssetData/Icon/Mana.png", typeof(Texture2D)) as Texture2D, GUILayout.Width(iconSize), GUILayout.Height(iconSize));
            ProgressBar(script.ManaGain / script.ManaMax, $"Mana : {script.ManaGain} / {script.ManaMax}");
            //Reduce Mana
            if(GUILayout.Button("-", GUILayout.Width(20))){
                if(script.ManaGain - 1 >= 0) script.AddMana(-1);
                else Debug.LogError("Can't reduce mana anymore. The UnitMana is already at 0");
            }
            //Give Mana
            if(GUILayout.Button("+", GUILayout.Width(20))){
                if(script.ManaGain + 1 <= script.ManaMax) script.AddMana(1);
                else Debug.LogError($"Can't give more mana to the unit. The Unit is already at maxMana : {script.ManaMax}");
            }
            GUILayout.EndHorizontal();

            //LIFE PROGRESS BAR
            GUILayout.BeginHorizontal();
            GUILayout.Label(AssetDatabase.LoadAssetAtPath("Assets/AssetData/Icon/Life.png", typeof(Texture2D)) as Texture2D, GUILayout.Width(iconSize), GUILayout.Height(iconSize));
            ProgressBar(script.UnitLife / unitVar._life, $"Life : {script.UnitLife} / {unitVar._life}");
            //Deal damage
            if(GUILayout.Button("-", GUILayout.Width(20))) {
                if(script.UnitLife - 1 >= 0) script.TakeDamage(1);
                else Debug.LogError("Can't deal more damage. The UnitLife is already at 0");
            }
            //Give life
            if(GUILayout.Button("+", GUILayout.Width(20))) {
                if(script.UnitLife + 1 <= unitVar._life) script.TakeDamage(-1);
                else Debug.LogError($"Can't give more life to the unit. The Unit is already at maxLife : {unitVar._life}");
            }
            GUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();
    }

    /// <summary>
    /// Create a progressBar
    /// </summary>
    /// <param name="value"></param>
    /// <param name="label"></param>
    void ProgressBar(float value, string label){
        // Get a rect for the progress bar using the same margins as a textfield:
        Rect rect = GUILayoutUtility.GetRect(18, 18, "TextField");
        EditorGUI.ProgressBar(rect, value, label);
        Space(2);
    }

    /// <summary>
    /// Make a space in the editor
    /// </summary>
    /// <param name="value"></param>
    void Space(int value){
        GUILayout.Space(value);
    }
}
