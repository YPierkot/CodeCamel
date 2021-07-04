using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Unit.UnitManager))]
public class UnitManagerEditor : Editor{
    SerializedObject mySerObj;
    SerializedProperty SOproperty;

    private void OnEnable(){
        mySerObj = new SerializedObject(target);
        SOproperty = mySerObj.FindProperty("_unitScriptable");

        //Refresh the object
        Unit.UnitManager script = (Unit.UnitManager)target;
        if(script._unitScriptable != null) script._unitScriptable.RefreshUnitData(script.gameObject);
    }
    public override void OnInspectorGUI(){
        //Show the ScriptableObject Slot and a button to refresh the variable
        GUILayout.BeginHorizontal("box", GUILayout.ExpandWidth(true), GUILayout.Height(25));
        EditorGUILayout.PropertyField(SOproperty, new GUIContent("Unit Scriptable"));
        mySerObj.ApplyModifiedProperties();
        if(GUILayout.Button(new GUIContent(EditorGUIUtility.IconContent("d_Refresh")))){
            Unit.UnitManager script = (Unit.UnitManager)target;
            if(script._unitScriptable != null) script._unitScriptable.RefreshUnitData(script.gameObject);
        }
        GUILayout.EndHorizontal();
    }
}
