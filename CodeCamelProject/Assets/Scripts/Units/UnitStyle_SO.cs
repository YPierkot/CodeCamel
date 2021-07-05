using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Unit {
    [CreateAssetMenu(fileName = "UnitStyle", menuName = "ScriptableObjects/Create New Style")]
    [CanEditMultipleObjects]
    public class UnitStyle_SO : ScriptableObject {
        [Header("Damage Data")]
        [SerializeField] private List<Unit.DamageDataType> _damageDataList = new List<DamageDataType>();

        [Header("Ability")]
        [SerializeField] private string _abilityName = "";
        [SerializeField] private int _manaCharge = 0;

        [Header("Bonus")]
        [Tooltip("Addition to the base life")]
        [SerializeField] private int _bonusLife = 0;
        [Space(2)]
        [Tooltip("Mulitply to the attack speed")]
        [SerializeField] private int _attackSpeedMultiplier = 0;
        [Space(2)]
        [Tooltip("Addition to the base of the critLuck")]
        [SerializeField] private int _bonusCriticalLuck = 0;
        [Tooltip("Addition to the base of the critValue")]
        [SerializeField] private int _bonusCriticalValue = 0;
        [Space(2)]
        [Tooltip("Addition to the base of the evasionValue")]
        [SerializeField] private int _bonusEvasion = 0;
    }


    /// <summary>
    /// DamageType for a style
    /// </summary>
    [System.Serializable]
    public class DamageDataType {
        public Unit.UnitsElement _damageType = Unit.UnitsElement.None;
        public Vector2 _damageBorne = Vector2.zero;
    }
}
