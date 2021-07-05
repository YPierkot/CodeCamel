using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Unit{
    [CreateAssetMenu(fileName = "Unit", menuName = "ScriptableObjects/Create New Unit")]
    [CanEditMultipleObjects]
    public class Units_SO : ScriptableObject{
        [Header("Unit Info")]
        [Tooltip("Name of the Unit")]
        [SerializeField] private string _unitName = "";
        [Tooltip("Family of the Unit")]
        [SerializeField] private UnitsFamily _unitFamily = UnitsFamily.Family1;
        [Tooltip("Element of the Unit")]
        [SerializeField] private List<UnitsElement> _unitElement = new List<UnitsElement>();

        [Header("Unit Basic Stat")]
        [Tooltip("MaxLife (and StartLife) of the Unit")]
        [SerializeField] private int _life = 0;
        [Tooltip("Damage of the Unit for each attack")]
        [SerializeField] private int _damage = 0;
        [Tooltip("Time between two attacks")]
        [SerializeField] private float _fireRate = 0f;
        [Tooltip("Speed of the Unit when need to Move")]
        [SerializeField] private float _moveSpeed;

        [Header("Unit Asset")]
        [Tooltip("the basic mesh of the unit. This mesh will be updated when the scriptable change")]
        [SerializeField] private Mesh _basicMesh = null;

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
            unitV._damage = this._damage;
            unitV._fireRate = this._fireRate;
            unitV._moveSpeed = this._moveSpeed;
            unitV._basicMesh = this._basicMesh;
            return unitV;
        }
    }

    /// <summary>
    /// Family of the Unit
    /// </summary>
    public enum UnitsFamily
    {
        Family1,
        Family2,
        Family3,
        Family4,
        Family5
    }

    /// <summary>
    /// Elements for the Unit
    /// </summary>
    public enum UnitsElement{
        None,
        Fire,
        Water,
        Ground,
        Wind
    }

    /// <summary>
    /// Variables of the scriptableObject
    /// </summary>
    public class UnitVariables{
        public string _unitName;
        public UnitsFamily _unitFamily;
        public List<UnitsElement> _unitElement;

        public int _life;
        public int _damage;
        public float _fireRate;
        public float _moveSpeed;

        public Mesh _basicMesh;
    }
}