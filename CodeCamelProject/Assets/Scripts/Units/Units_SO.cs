using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Unit{
    [CreateAssetMenu(fileName = "Unit", menuName = "ScriptableObjects/Create New Unit"), CanEditMultipleObjects]
    public class Units_SO : ScriptableObject{
        #region Variables
        //UNIT INFO
        [Tooltip("Name of the Unit")]
        [SerializeField] private string _unitName = "";
        [Tooltip("Family of the Unit")]
        [SerializeField] private EnumScript.UnitsFamily _unitFamily = EnumScript.UnitsFamily.Family1;
        [Tooltip("Element of the Unit")]
        [SerializeField] private List<EnumScript.UnitsElement> _unitElement = new List<EnumScript.UnitsElement>();

        //UNIT BASIC STAT
        [Tooltip("MaxLife (and StartLife) of the Unit")]
        [SerializeField] private int _life = 0;
        [Tooltip("How much attack does the unity make per second")]
        [SerializeField] private float _attackPerSecond = 0;
        [Tooltip("Chance to make a critical attack")]
        [SerializeField] private float _critChance = 0f;
        [Tooltip("Pourcent of creat value")]
        [SerializeField] private float _critValueMultiplier = 0f;
        [Tooltip("Pourcent of dodging")]
        [SerializeField] private float _evasionRate = 0f;
        [Tooltip("Pourcent of dodging")]
        [SerializeField] private float _attackRange = 0f;
        [Tooltip("Pourcent of dodging")]
        [SerializeField] private float _moveSPeed = 0f;

        //UNIT ABILITY
        [SerializeField] private List<Unit.UnitStyle_SO> _styleList = new List<UnitStyle_SO>();

        //UNIT ASSET
        [Tooltip("the basic mesh of the unit. This mesh will be updated when the scriptable change")]
        [SerializeField] private Mesh _basicMesh = null;
        #endregion Variables

        #region Methods
        /// <summary>
        /// Get the stat of this ScriptableObject
        /// </summary>
        /// <param name="baseObject"></param>
        public UnitVariables GetStat(){
            UnitVariables unitV = new UnitVariables();
            unitV._unitName = this._unitName;
            unitV._unitFamily = this._unitFamily;
            unitV._unitElement = this._unitElement;
            unitV._life = this._life;
            unitV._attackPerSecond = this._attackPerSecond;
            unitV._critChance = this._critChance;
            unitV._critValueMultiplier = this._critValueMultiplier;
            unitV._evasionRate = this._evasionRate;
            unitV._attackRange = this._attackRange;
            unitV._moveSpeed = this._moveSPeed;
            unitV._basicMesh = this._basicMesh;
            return unitV;
        }
        #endregion Methods
    }

    /// <summary>
    /// Variables of the scriptableObject
    /// </summary>
    public class UnitVariables{
        public string _unitName;
        public EnumScript.UnitsFamily _unitFamily;
        public List<EnumScript.UnitsElement> _unitElement;

        public int _life;
        public float _attackPerSecond;
        public float _critChance;
        public float _critValueMultiplier;
        public float _evasionRate;
        public float _attackRange;
        public float _moveSpeed;

        public Mesh _basicMesh;
    }
}