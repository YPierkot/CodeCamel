using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnumScript;


namespace Map
{
    public class HexManager : MonoBehaviour{
        #region Variables
        //TERRAIN TYPE
        [Tooltip("List of terrain Effects")]
        [SerializeField] private List<EnumScript.TerrainType> _terrainType = new List<EnumScript.TerrainType>();

        [Tooltip("Which player can put his Unit on")]
        [SerializeField] private PlayerSide _playerCanPose = PlayerSide.BluePlayer;


        [Tooltip("On which line is this hex")]
        [SerializeField] private int _line = 0;
        [Tooltip("Id of the cylindder")]
        [SerializeField] private int _id = 0;

        //UNIT ON HEX
        [Tooltip("The actual Unit on this Hex")]
        private GameObject _unitOnHex = null;
        [Tooltip("The Unit who will come to this Hex")]
        private GameObject _targetedUnit = null;


        //PUBLIC VARIABLES
        public PlayerSide PlayerCanPose { get => _playerCanPose; }
        public GameObject UnitOnHex { get => _unitOnHex; }
        public GameObject TargetedUnit { get => _targetedUnit; set => _targetedUnit = value; }
        public int Line { get => _line; set => _line = value; }
        public int Id { get => _id; set => _id = value; }
        #endregion Variables

        #region terrainChanges
        /// <summary>
        /// Change the type of the terrain at a certain id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        public void ChangeTerrainType(int id, TerrainType type){
            _terrainType[id] = type;
        }

        /// <summary>
        /// Add an type to the terrain
        /// </summary>
        public void AddTerrainEffect(){
            _terrainType.Add(EnumScript.TerrainType.Beach);
        }

        /// <summary>
        /// Remove one effect on the terrain
        /// </summary>
        public void removeTerrainEffect(int id){
            _terrainType.RemoveAt(id);
        }
        #endregion terrainChanges

        #region UnitOnTerrain
        /// <summary>
        /// Add a Unit to the terrain
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="lastHex"></param>
        public void AddUnitToTerrain(GameObject unit, GameObject lastHex){
            if(lastHex != this.gameObject){
                _unitOnHex = unit;
                _targetedUnit = unit;
                if(lastHex != null) lastHex.GetComponent<HexManager>().RemoveUnit();
                unit.GetComponent<Unit.Movement>().HexUnderUnit = this.gameObject;
            }
        }

        /// <summary>
        /// Remove the actual Unit on the terrain
        /// </summary>
        public void RemoveUnit(){
            _unitOnHex = null;
        }
        #endregion UnitOnTerrain

#if UNITY_EDITOR
        /// <summary>
        /// Change the color of the hex
        /// </summary>
        public void ReloadColor(){
            switch(_playerCanPose){
                case PlayerSide.None:
                    GetComponent<MeshRenderer>().sharedMaterial = (Material)AssetDatabase.LoadAssetAtPath("Assets/AssetData/Materials/White.mat", typeof(Material));
                    break;
                case PlayerSide.RedPlayer:
                    GetComponent<MeshRenderer>().sharedMaterial = (Material)AssetDatabase.LoadAssetAtPath("Assets/AssetData/Materials/RedHex.mat", typeof(Material));
                    break;
                case PlayerSide.BluePlayer:
                    GetComponent<MeshRenderer>().sharedMaterial = (Material)AssetDatabase.LoadAssetAtPath("Assets/AssetData/Materials/BlueHex.mat", typeof(Material));
                    break;
            }
        }
#endif
    }

    /// <summary>
    /// Data for a effect
    /// </summary>
    public class EffectData { 
        public EnumScript.TerrainType _terrainEffect;
        public List<GameObject> _hexList = new List<GameObject>();
    }
}
