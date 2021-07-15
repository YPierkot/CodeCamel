using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Unit{
    [RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
    public class UnitManager : MonoBehaviour{
        #region Variables
        //UNIT
        [Tooltip("The ScriptableObject which contain every data of this Unit")]
        public Units_SO _unitScriptable = null;
        [Tooltip("Which player posess this Unit")]
        [SerializeField, Range(0, 1)] private EnumScript.PlayerSide _player = EnumScript.PlayerSide.None;

        //UNIT DATA
        [Tooltip("Actual life of this Unit")]
        [SerializeField] private float _unitLife = 0f;
        [Tooltip("Actual Mana of this Unit")]
        [SerializeField] private float _manaGain = 0f;
        [Tooltip("GameObject for the life of the Unit")]
        [SerializeField] private GameObject _lifeGam = null;
        [Tooltip("GameObject for the mana of the Unit")]
        [SerializeField] private GameObject _manaGam = null;

        //PUBLIC VARIABLES
        public EnumScript.PlayerSide Player => _player;
        public bool runTimeData { get; set; }
        #endregion Variables

        #region Data
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
                _manaGain = 10;
            }
            else{
                GetComponent<MeshFilter>().sharedMesh = null;
                GetComponent<MeshCollider>().sharedMesh = null;
            }
        }
        #endregion Data

        #region UnitMethods
        /// <summary>
        /// Deal damage to the player
        /// </summary>
        /// <param name="damage"></param>
        public void TakeDamage(int damage){
            _unitLife -= damage;
            _lifeGam.GetComponent<Image>().fillAmount = _unitLife / _unitScriptable.GetStat()._life;
        }

        /// <summary>
        /// Add mana to the player
        /// </summary>
        /// <param name="mana"></param>
        public void AddMana(float mana){
            _manaGain += mana;
            _manaGam.GetComponent<Image>().fillAmount = _manaGain / 10;
        }
        #endregion UnitMethods

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
