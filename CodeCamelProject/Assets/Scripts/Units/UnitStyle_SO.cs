using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Unit {
    [CreateAssetMenu(fileName = "UnitStyle", menuName = "ScriptableObjects/Create New Style")]
    [CanEditMultipleObjects]
    public class UnitStyle_SO : ScriptableObject {
        [Header("STYLE COST")]
        [SerializeField] private List<StyleCost> _styleCost = new List<StyleCost>();

        [Header("DAMAGE DATA")]
        [SerializeField] private List<Unit.DamageDataType> _damageDataList = new List<DamageDataType>();

        [Header("ABILITY")]
        [SerializeField] private string _abilityName = "";
        [SerializeField] private int _manaCharge = 0;

        [Header("BONUS")]
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
    }


    /// <summary>
    /// DamageType for a style
    /// </summary>
    [System.Serializable]
    public class DamageDataType {
        public Unit.UnitsElement _damageType = Unit.UnitsElement.None;
        public Vector2 _damageBorne = Vector2.zero;
    }

    [System.Serializable]
    public class StyleCost{
        public Unit.UnitsElement _elements = Unit.UnitsElement.None;
        public int _value = 0;
    }
}
