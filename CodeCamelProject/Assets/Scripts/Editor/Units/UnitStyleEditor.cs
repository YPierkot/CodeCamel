using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Unit.UnitStyle_SO))]
public class UnitStyleEditor : Editor{
    #region Variables
    //STYLE COST VARIABLE
    SerializedProperty _styleCostProperty;
    bool _styleCostOpen;

    //DAMAGE DATA VARIABLE
    SerializedProperty _damageDataProperty;
    bool _damageDataOpen;

    //ABILITY VARIABLE
    SerializedProperty _manaChargeProperty;


    //BONUS VARIABLE
    SerializedProperty _lifeBonusProperty;
    SerializedProperty _attackSpeedProperty;
    SerializedProperty _critLuckProperty;
    SerializedProperty _critValueProperty;
    SerializedProperty _dodgeValueProperty;
    #endregion Variables

    /// <summary>
    /// Update Variable when the object is selected
    /// </summary>
    private void OnEnable(){
        _styleCostProperty = serializedObject.FindProperty("_styleCost");
        _damageDataProperty = serializedObject.FindProperty("_damageDataList");

        _manaChargeProperty = serializedObject.FindProperty("_manaCharge");

        _lifeBonusProperty = serializedObject.FindProperty("_bonusLife");
        _attackSpeedProperty = serializedObject.FindProperty("_attackSpeedMultiplier");
        _critLuckProperty = serializedObject.FindProperty("_bonusCriticalLuck");
        _critValueProperty = serializedObject.FindProperty("_bonusCriticalValue");
        _dodgeValueProperty = serializedObject.FindProperty("_bonusEvasion");
    }

    /// <summary>
    /// Redraw the inspector with a custom one
    /// </summary>
    public override void OnInspectorGUI(){
        serializedObject.Update();

        #region StyleCost
        StaticEditor.VerticalBox();
        if(GUILayout.Button("STYLE COST", StaticEditor.buttonTitleStyle)){
            _styleCostOpen = !_styleCostOpen;
        }

        if(_styleCostOpen){
            int lastIndex = 0;
            for(int cost = 0; cost < _styleCostProperty.arraySize; cost++){
                StaticEditor.HorizontalBox();
                var styleCost = _styleCostProperty.GetArrayElementAtIndex(cost);
                GUILayout.BeginVertical();

                GUILayout.BeginHorizontal();
                GUILayout.Label("Element :", StaticEditor.labelStyle);
                EditorGUILayout.PropertyField(styleCost.FindPropertyRelative("_elements"), GUIContent.none);
                serializedObject.ApplyModifiedProperties();

                GUILayout.Label(" X ", StaticEditor.labelStyle);
                EditorGUILayout.PropertyField(styleCost.FindPropertyRelative("_value"), GUIContent.none, GUILayout.Width(40));
                serializedObject.ApplyModifiedProperties();
                GUILayout.EndHorizontal();


                GUILayout.EndVertical();
                if(GUILayout.Button("-", StaticEditor.buttonTitleStyle, GUILayout.Width(20), GUILayout.ExpandHeight(true))){
                    _styleCostProperty.DeleteArrayElementAtIndex(cost);
                    serializedObject.ApplyModifiedProperties();
                }
                GUILayout.EndHorizontal();
                lastIndex = cost;

                StaticEditor.Space(4);
            }

            //Button to add a cost
            if(GUILayout.Button("Add effect", StaticEditor.buttonStyle)){
                _styleCostProperty.InsertArrayElementAtIndex(lastIndex == 0 ? 0 : lastIndex + 1);
                serializedObject.ApplyModifiedProperties();
            }
        }
        GUILayout.EndVertical();
        #endregion StyleCost

        StaticEditor.Space(5);

        #region Damage
        StaticEditor.VerticalBox();
        if(GUILayout.Button("DAMAGE DATA", StaticEditor.buttonTitleStyle)){
            _damageDataOpen = !_damageDataOpen;
        }

        if(_damageDataOpen){
            int lastIndex = 0;
            for(int cost = 0; cost < _damageDataProperty.arraySize; cost++){
                StaticEditor.HorizontalBox();
                var damageList = _damageDataProperty.GetArrayElementAtIndex(cost);
                GUILayout.BeginVertical();

                GUILayout.BeginHorizontal();
                GUILayout.Label("Type :", StaticEditor.labelStyle);
                EditorGUILayout.PropertyField(damageList.FindPropertyRelative("_damageType"), GUIContent.none);
                serializedObject.ApplyModifiedProperties();
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Label(new GUIContent("Borne Damage :", "MIN and MAX value of the damage"), StaticEditor.labelStyle);
                EditorGUILayout.PropertyField(damageList.FindPropertyRelative("_damageBorne"), GUIContent.none);
                serializedObject.ApplyModifiedProperties();
                GUILayout.EndHorizontal();

                GUILayout.EndVertical();
                if(GUILayout.Button("-", StaticEditor.buttonTitleStyle, GUILayout.Width(20), GUILayout.ExpandHeight(true))){
                    _damageDataProperty.DeleteArrayElementAtIndex(cost);
                    serializedObject.ApplyModifiedProperties();
                }
                GUILayout.EndHorizontal();
                lastIndex = cost;
                StaticEditor.Space(4);
            }

            //Button to add a cost
            if(GUILayout.Button("Add Damage Type", StaticEditor.buttonStyle)){
                _damageDataProperty.InsertArrayElementAtIndex(lastIndex == 0 ? 0 : lastIndex + 1);
                serializedObject.ApplyModifiedProperties();
                Repaint();
            }
        }
        GUILayout.EndVertical();
        #endregion Damage

        StaticEditor.Space(5);

        #region Ability
        StaticEditor.VerticalBox();
        GUILayout.Label("ABILITY DATA", StaticEditor.labelTitleStyle);

        GUILayout.BeginHorizontal();
        GUILayout.Label(new GUIContent("Mana Value :","Value of Mana needed to activate the ability"), StaticEditor.labelStyle, GUILayout.Width(150));
        EditorGUILayout.PropertyField(_manaChargeProperty, GUIContent.none);
        GUILayout.EndHorizontal();

        GUILayout.EndVertical();
        #endregion Ability

        StaticEditor.Space(5);

        #region BonusData
        StaticEditor.VerticalBox();
        GUILayout.Label("BONUS DATA", StaticEditor.labelTitleStyle);

        StaticEditor.HorizontalBox();
        GUILayout.Label(new GUIContent("Life Bonus :", "Bonus of life with this style (Addition)"), StaticEditor.labelStyle, GUILayout.Width(150));
        EditorGUILayout.PropertyField(_lifeBonusProperty, GUIContent.none);
        GUILayout.EndHorizontal();

        StaticEditor.Space(4);

        StaticEditor.HorizontalBox();
        GUILayout.Label(new GUIContent("AttackSpeed Bonus :", "Bonus of attackSpeed with this style (Multiplication)"), StaticEditor.labelStyle, GUILayout.Width(150));
        EditorGUILayout.PropertyField(_attackSpeedProperty, GUIContent.none);
        GUILayout.EndHorizontal();

        StaticEditor.Space(4);

        GUILayout.BeginVertical();
        StaticEditor.HorizontalBox();
        GUILayout.Label(new GUIContent("CritDamage Luck :", "Bonus of Chance to the criticalDamage (Addition)"), StaticEditor.labelStyle, GUILayout.Width(150));
        EditorGUILayout.PropertyField(_critLuckProperty, GUIContent.none);
        GUILayout.EndHorizontal();
        StaticEditor.HorizontalBox();
        GUILayout.Label(new GUIContent("CritDamage Value :", "Bonus of critical Damage (Addiion)"), StaticEditor.labelStyle, GUILayout.Width(150));
        EditorGUILayout.PropertyField(_critValueProperty, GUIContent.none);
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();

        StaticEditor.Space(4);

        StaticEditor.HorizontalBox();
        GUILayout.Label(new GUIContent("Dodge Bonus :", "Bonus of dodging (Addition)"), StaticEditor.labelStyle, GUILayout.Width(150));
        EditorGUILayout.PropertyField(_dodgeValueProperty, GUIContent.none);
        GUILayout.EndHorizontal();

        GUILayout.EndVertical();
        #endregion BonusData
    }
}
