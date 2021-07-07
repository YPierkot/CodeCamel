using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnumScript;


namespace Map
{
    public class HexManager : MonoBehaviour{
        #region Variables
        [Header("Terrain Type")]
        //Terrain effect on the hex
        [SerializeField] private List<EnumScript.TerrainType> _terrainType = new List<EnumScript.TerrainType>();

        //Player who can put a Unit on the Hex
        [SerializeField] private PlayerSide _playerCanPose = PlayerSide.BluePlayer;
        public PlayerSide PlayerCanPose { get => _playerCanPose; }

        [Header("Unit On Hex")]
        //Which Unit is on the hex
        private GameObject _unitOnHex = null;
        public GameObject UnitOnHex { get => _unitOnHex; }
        #endregion Variables

        #region terrainChanges
        public void ChangeTerrainType(int id, TerrainType type){
            _terrainType[id] = type;
        }

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
        public void AddUnitToTerrain(GameObject unit, GameObject lastHex){
            if(lastHex != this.gameObject){
                _unitOnHex = unit;
                if(lastHex != null) lastHex.GetComponent<HexManager>().RemoveUnit();
                unit.GetComponent<Unit.UnitManager>().ChangeHexUnderUnit(this.gameObject);
            }
        }

        public void RemoveUnit(){
            _unitOnHex = null;
        }
        #endregion UnitOnTerrain

        /// <summary>
        /// Change the color of the hex
        /// </summary>
        public void ChangeColor(){
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
    }

    /// <summary>
    /// Data for a effect
    /// </summary>
    public class EffectData { 
        public EnumScript.TerrainType _terrainEffect;
        public List<GameObject> _hexList = new List<GameObject>();
    }
}
