using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unit{
    [RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
    public class UnitManager : MonoBehaviour{
        #region Variables
        public Units_SO _unitScriptable = null;


        [Header("Unit")]
        [SerializeField, Range(0,1)] private int _player = 0;

        [Header("Unit Stat")]
        [SerializeField] private float _unitLife = 0f; //LIFE
        [SerializeField] private float _manaGain = 0f; //MANA
        [SerializeField] private float _manaMax = 0f;  //MANA

        [Header("Hex")]
        [SerializeField] private GameObject _hexUnderUnit = null;

        //PUBLIC VARIABLES
        public float ManaGain { get => _manaGain; } //ACTUAL MANA
        public float ManaMax { get => _manaMax; } //MANA FOR POWER
        public float UnitLife { get => _unitLife; } //ACTUAL LIFE
        public bool runTimeData { get; set; } //ACTUAL LIFE
        public GameObject HexUnderUnit { get => _hexUnderUnit; } //ACTUAL HEX UNDER UNIT
        public int Player { get => _player; set => _player = value; } //WHICH PLAYER POSESS THIS UNIT
        #endregion Variables

        /// <summary>
        /// Refresh all the data of the object
        /// </summary>
        public void RefreshData(){
            //Get variables of the Unit
            if(_unitScriptable != null){
                UnitVariables unitVar = _unitScriptable.GetStat();

                if(unitVar._basicMesh != null){
                    GetComponent<MeshFilter>().sharedMesh = unitVar._basicMesh;
                    GetComponent<MeshCollider>().sharedMesh = unitVar._basicMesh;
                }

                this.gameObject.name = "BaseUnit : " + unitVar._unitName;
                _unitLife = unitVar._life;
                _manaMax = 10;
            }
            else{
                GetComponent<MeshFilter>().sharedMesh = null;
                GetComponent<MeshCollider>().sharedMesh = null;
            }
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

        #region HexTile
        public void ChangeHexUnderUnit(GameObject hex = null){
            _hexUnderUnit = hex;
        }

#endregion HexTile

        //RELOAD ALL THE DATA FROM THE SCRIPTABLEOBJECT
#if UNITY_EDITOR
        /// <summary>
        /// If there is any changement to the variables
        /// </summary>
        private void OnValidate(){
            //Refresh all the data if the ScriptableObject change
            RefreshData();
        }
#endif
    }
}
