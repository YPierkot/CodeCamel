using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

namespace Map
{
    public class HexManager : MonoBehaviour
    {
        #region Variables
        [Header("Terrain Type")]
        private List<TerrainType> _terrainType = new List<TerrainType>();
        public List<TerrainType> TerrainType { get => _terrainType; set => _terrainType = value; }

        [Header("Unit On Hex")]
        private GameObject _unitOnHex = null;
        public GameObject UnitOnHex { get => _unitOnHex; }
        #endregion Variables

        #region terrainChanges
        public void ChangeTerrainType(int id, TerrainType type){
            _terrainType[id] = type;
        }

        public void AddTerrainEffect(){
            _terrainType.Add(Map.TerrainType.Beach);
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
    }

    /// <summary>
    /// All the terrain Effect
    /// </summary>
    public enum TerrainType{
        Ground,
        Forest,
        Beach
    }

    /// <summary>
    /// Data for a effect
    /// </summary>
    public class EffectData { 
        public TerrainType _terrainEffect;
        public List<GameObject> _hexList = new List<GameObject>();
    }
}
