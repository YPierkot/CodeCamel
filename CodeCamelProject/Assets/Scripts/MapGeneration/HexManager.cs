using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnumScript;
using static GameManager;

namespace Map
{
    public class HexManager : MonoBehaviour
    {
        #region Variables
        [Header("Terrain Type")]
        //Terrain effect on the hex
        [SerializeField] private List<EnumScript.TerrainType> _terrainType = new List<EnumScript.TerrainType>();
        public List<EnumScript.TerrainType> TerrainType { get => _terrainType; set => _terrainType = value; }

        //Player who can put a Unit on the Hex
        [SerializeField] private PlayerSide _playerCanPose = PlayerSide.BluePlayer;
        public PlayerSide PlayerCanPose { get => _playerCanPose; set => _playerCanPose = value; }

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

        private void OnDrawGizmos(){
            switch(_playerCanPose){
                case PlayerSide.None:
                    break;
                case PlayerSide.RedPlayer:
                    Gizmos.DrawIcon(transform.position + new Vector3(0, GetComponent<MeshCollider>().bounds.size.y / 1.8f, 0), "sv_icon_name6", true);
                    break;
                case PlayerSide.BluePlayer:
                    Gizmos.DrawIcon(transform.position + new Vector3(0, GetComponent<MeshCollider>().bounds.size.y / 1.8f, 0), "sv_icon_name1", true);
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
