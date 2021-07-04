using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unit{
    [RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
    public class UnitManager : MonoBehaviour{
        #region Variables
        public Units_SO _unitScriptable = null;
        Units_SO _lastUnitSO = null;

        [Header("Unit Stat")]
        //LIFE
        //life of the player 
        [SerializeField] private float _unitLife = 0f;

        //MANA
        //mana gain during party
        [SerializeField] private float _manaGain = 0f;
        //mana needed to use power
        [SerializeField] private float _manaMax = 0f;




        //PUBLIC VARIABLES
        public float ManaGain { get => _manaGain; } //ACTUAL MANA
        public float ManaMax { get => _manaMax; } //MANA FOR POWER
        public float UnitLife { get => _unitLife; } //ACTUAL LIFE
        public bool runTimeData { get; set; } //ACTUAL LIFE
        #endregion Variables

        /// <summary>
        /// Refresh all the data of the object
        /// </summary>
        public void RefreshData(){
            //Get variables of the Unit
            UnitVariables unitVar = _unitScriptable.GetStat();

            if(unitVar._basicMesh != null) GetComponent<MeshFilter>().sharedMesh = unitVar._basicMesh;
            this.name = "BaseUnit : " + unitVar._unitName;
            this._unitLife = unitVar._life;
            _manaMax = 10;
        }

        #region UnitMethods
        /// <summary>
        /// Deal damage to the player
        /// </summary>
        /// <param name="damage"></param>
        public void TakeDamage(int damage){
            _unitLife -= damage;
        }

        /// <summary>
        /// Add mana to the player
        /// </summary>
        /// <param name="mana"></param>
        public void AddMana(float mana){
            _manaGain += mana;
        }
        #endregion UnitMethods


        //RELOAD ALL THE DATA FROM THE SCRIPTABLEOBJECT
#if UNITY_EDITOR
        /// <summary>
        /// If there is any changement to the variables
        /// </summary>
        private void OnValidate(){
            //Refresh all the data if the ScriptableObject change
            if(_unitScriptable != _lastUnitSO && _unitScriptable != null){
                RefreshData();
                _lastUnitSO = _unitScriptable;
            }
        }
#endif
    }
}
