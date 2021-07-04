using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unit{
    [CreateAssetMenu(fileName = "Unit", menuName = "ScriptableObjects/Create New Unit")]
    public class Units_SO : ScriptableObject{
        [Header("Unit Info")]
        [Tooltip("Name of the Unit")]
        [SerializeField] private string _unitName = "";
        [Tooltip("Family of the Unit")]
        [SerializeField] private UnitsFamily _unitFamily = UnitsFamily.Family1;
        [Tooltip("Element of the Unit")]
        [SerializeField] private UnitsElement _unitElement = UnitsElement.None;

        [Header("Unit Basic Stat")]
        [Tooltip("MaxLife (and StartLife) of the Unit")]
        [SerializeField] private int _life = 0;
        [Tooltip("Damage of the Unit for each attack")]
        [SerializeField] private int _damage = 0;
        [Tooltip("Time between two attacks")]
        [SerializeField] private float _fireRate = 0f;
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
}