using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Unit {
    [CreateAssetMenu(fileName = "UnitStyle", menuName = "ScriptableObjects/Create New Style"), CanEditMultipleObjects]
    public class UnitStyle_SO : ScriptableObject {
        #region Variables
        //STYLE COST
        [Tooltip("Cost of the style")]
        [SerializeField] private List<StyleCost> _styleCost = new List<StyleCost>();

        //DAMAGE DATA
        [Tooltip("List of the different damage possible")]
        [SerializeField] private List<Unit.DamageDataType> _damageDataList = new List<DamageDataType>();

        //ABILITY
        [Tooltip("Name of the ability")]
        [SerializeField] private string _abilityName = "";
        [Tooltip("Mana to get to active the abitlity")]
        [SerializeField] private int _manaCharge = 0;

        //BONUS
        [Tooltip("Addition to the base life")]
        [SerializeField] private float _bonusLife = 0;
        [Space(4)]
        [Tooltip("Mulitply to the attack speed")]
        [SerializeField] private float _attackSpeedMultiplier = 0;
        [Space(4)]
        [Tooltip("Addition to the base of the critLuck")]
        [SerializeField] private float _bonusCriticalLuck = 0;
        [Tooltip("Addition to the base of the critValue")]
        [SerializeField] private float _bonusCriticalValue = 0;
        [Space(4)]
        [Tooltip("Addition to the base of the evasionValue")]
        [SerializeField] private float _bonusEvasion = 0;
        #endregion Variables
    }

    #region CustomClass
    /// <summary>
    /// DamageType for a style
    /// </summary>
    [System.Serializable]
    public class DamageDataType {
        public EnumScript.UnitsElement _damageType = EnumScript.UnitsElement.None;
        public Vector2 _damageBorne = Vector2.zero;
    }

    /// <summary>
    /// Cost of a style
    /// </summary>
    [System.Serializable]
    public class StyleCost{
        public EnumScript.UnitsElement _elements = EnumScript.UnitsElement.None;
        public int _value = 0;
    }
    #endregion CustomClass
}
