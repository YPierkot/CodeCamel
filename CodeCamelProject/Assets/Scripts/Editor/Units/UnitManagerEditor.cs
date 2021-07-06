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

        Unit.UnitVariables unitVar = new Unit.UnitVariables();
        if(script._unitScriptable != null) unitVar = script._unitScriptable.GetStat();

        //Show the ScriptableObject Slot and a button to refresh the variable
        GUILayout.BeginVertical("box", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
        GUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(SOproperty, new GUIContent("Unit Scriptable"));
        mySerObj.ApplyModifiedProperties();
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
                script.Player = script.Player == 1? 2 : 1;
            }
            GUILayout.Label($"THIS IS A UNIT OF THE PLAYER : {script.Player}", StaticEditor.labelStyle);
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
            //Deal damage
            if(GUILayout.Button("-", GUILayout.Width(20))){
                if(script.UnitLife - 1 >= 0) script.TakeDamage(1);
                else Debug.LogError("Can't deal more damage. The UnitLife is already at 0");
            }
            //Give life
            if(GUILayout.Button("+", GUILayout.Width(20))){
                if(script.UnitLife + 1 <= unitVar._life) script.TakeDamage(-1);
                else Debug.LogError($"Can't give more life to the unit. The Unit is already at maxLife : {unitVar._life}");
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label(AssetDatabase.LoadAssetAtPath("Assets/AssetData/Icon/Mana.png", typeof(Texture2D)) as Texture2D, GUILayout.Width(iconSize), GUILayout.Height(iconSize));
            StaticEditor.ProgressBar(script.ManaGain / script.ManaMax, $"Mana : {script.ManaGain} / {script.ManaMax}");
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
            GUILayout.EndVertical();

            GUILayout.BeginVertical("box", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
            GUILayout.Label("HEX UNDER UNIT", StaticEditor.labelStyle);
            GUI.enabled = false;
            EditorGUILayout.ObjectField(script.HexUnderUnit, typeof(GameObject), allowSceneObjects: true);
            GUI.enabled = true;
            GUILayout.EndVertical();
        }

        //Close the box Runtime
        GUILayout.EndVertical();
    }


    private Texture2D MakeTex(int width, int height, Color col)
    {
        Color[] pix = new Color[width * height];
        for(int i = 0; i < pix.Length; ++i)
        {
            pix[i] = col;
        }
        Texture2D result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();
        return result;
    }
}
