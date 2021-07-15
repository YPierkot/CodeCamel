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
        [Tooltip("The actual Unit on this Hex")]
        [SerializeField] private List<GameObject> _transitionUnitOnHex = new List<GameObject>();

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
        public void AddUnitToTerrain(GameObject unit){
                _unitOnHex = unit;
                _targetedUnit = unit;
        }

        /// <summary>
        /// Remove the actual Unit on the terrain
        /// </summary>
        public void RemoveUnit(bool targetHex = false){
            _unitOnHex = null;
            if(targetHex) _targetedUnit = null;
        }

        /// <summary>
        /// Add a moving unit on this tile
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="lastMovingCylinder"></param>
        public void AddMovingUnit(GameObject unit){
            _transitionUnitOnHex.Add(unit);
        }

        /// <summary>
        /// Remove a moving unit
        /// </summary>
        /// <param name="unit"></param>
        public void RemoveMovingUnit(GameObject unit){
            if(_transitionUnitOnHex.Contains(unit)) _transitionUnitOnHex.Remove(unit);
        }
        #endregion UnitOnTerrain

#if UNITY_EDITOR
        /// <summary>
        /// Change the color of the hex
        /// </summary>
        public void ChangeColor(Material mat = null){
            if(mat == null){
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
            else{
                GetComponent<MeshRenderer>().sharedMaterial = mat;
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
