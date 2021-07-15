using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Unit{
    public class Movement : MonoBehaviour{
        #region Variables
        [SerializeField] private LayerMask _layer;

        [Header("HEX")]
        [ReadOnly, SerializeField] private GameObject _hexUnderUnit;
        [ReadOnly, SerializeField] private GameObject _targetHex;
        [ReadOnly, SerializeField] private GameObject _nextHex;
        GameObject _startHex;

        [Header("UNIT")]
        [ReadOnly, SerializeField] private GameObject _targetUnit;

        public GameObject HexUnderUnit { get => _hexUnderUnit; }
        public GameObject TargetHex { get => _targetHex; }
        public GameObject NextHex { get => _nextHex; }
        #endregion Variables

        public void Update(){
            if(_nextHex != null){
                MoveUnitOnHex();
            }
        }

        #region Movement
        /// <summary>
        /// Move the unit on the grid
        /// </summary>
        void MoveUnitOnHex(){
            //Move Unit
            Vector3 dir = (_nextHex.transform.position - _startHex.transform.position).normalized;
            transform.position += new Vector3(dir.x, 0, dir.z) * Time.deltaTime * GetComponent<Unit.UnitManager>()._unitScriptable.GetStat()._moveSpeed;

            //Check Hexs
            CheckIfCloserToNextHex();
            CheckIfCloseToTarget();
        }

        /// <summary>
        /// Check if the Unit is closer to the nextHex than the previous Hex
        /// </summary>
        void CheckIfCloserToNextHex(){
            if(StaticRuntime.aCloserThanb(_nextHex, _startHex, this.gameObject)){
                if(_startHex.GetComponent<Map.HexManager>().UnitOnHex == this.gameObject){
                    _startHex.GetComponent<Map.HexManager>().RemoveUnit(true);
                }
                _hexUnderUnit = _nextHex;
            }
        }

        /// <summary>
        /// Check if the Unit is close to the targetHex
        /// </summary>
        void CheckIfCloseToTarget(){
            //If close to the target
            if(Mathf.Abs(Mathf.Abs(Vector3.Distance(new Vector3(_targetHex.transform.position.x, transform.position.y, _targetHex.transform.position.z), this.transform.position))) <= 0.07f){
                _targetHex.GetComponent<Map.HexManager>().AddUnitToTerrain(this.gameObject);
                if(_startHex != null) _startHex.GetComponent<Map.HexManager>().RemoveMovingUnit(this.gameObject);
                if(_nextHex != null) _nextHex.GetComponent<Map.HexManager>().RemoveMovingUnit(this.gameObject);
                _targetHex.GetComponent<Map.HexManager>().ChangeColor();
                _targetHex = null;
                _nextHex = null;
            }
            //If close to the unit
            else if(Mathf.Abs(Vector3.Distance(new Vector3(_nextHex.transform.position.x, transform.position.y, _nextHex.transform.position.z), this.transform.position)) <= 0.07f)
            {
                //Reset Target Data
                _targetHex.GetComponent<Map.HexManager>().ChangeColor();
                if(_targetHex.GetComponent<Map.HexManager>().UnitOnHex == null) _targetHex.GetComponent<Map.HexManager>().TargetedUnit = null;
                _targetHex = null;

                //Ask for a new target
                if(GetComponent<Unit.UnitManager>().Player == EnumScript.PlayerSide.RedPlayer) Unit.MovementAIManager.Instance.SetRedUnitTarget(this.gameObject);
                else Unit.MovementAIManager.Instance.SetBlueUnitTarget(this.gameObject);
            }
        }
        #endregion Movement

        #region SetHexValue
        /// <summary>
        /// Set the targeted Object of this unit
        /// </summary>
        /// <param name="targetUnit"></param>
        /// <param name="targetHex"></param>
        public void settargetData(GameObject targetUnit, GameObject targetHex, bool SearchNearestHex = false){
            //Reset TargetData
            if(_startHex != null) _startHex.GetComponent<Map.HexManager>().RemoveMovingUnit(this.gameObject);
            if(_nextHex != null) _nextHex.GetComponent<Map.HexManager>().RemoveMovingUnit(this.gameObject);
            _nextHex = null;

            //Change TargetData
            _targetUnit = targetUnit;
            _targetHex = targetHex;
            if(_targetHex != null) _targetHex.GetComponent<Map.HexManager>().TargetedUnit = this.gameObject;
            if(_targetHex != null) _targetHex.GetComponent<Map.HexManager>().ChangeColor((Material)AssetDatabase.LoadAssetAtPath("Assets/AssetData/Materials/GoldHex.mat", typeof(Material)));

            //Find the nearest Hex
            if(SearchNearestHex) SetNextHexDir();
        }

        /// <summary>
        /// Set the closest Hex on which the unit will go on
        /// </summary>
        void SetNextHexDir(){
            Vector3 dir = _targetHex.transform.position - _hexUnderUnit.transform.position;
            GameObject NextGam = null;
            RaycastHit hit;

            if(Physics.Raycast(_hexUnderUnit.transform.position, dir, out hit, Mathf.Infinity, _layer)){
                if(hit.collider.gameObject.GetComponent<Map.HexManager>().TargetedUnit == null || hit.collider.gameObject.GetComponent<Map.HexManager>().TargetedUnit == this.gameObject){
                    _nextHex = hit.collider.gameObject;
                    NextGam = hit.collider.gameObject;
                }
                else{
                    if(hit.collider.gameObject != _targetHex){
                        _nextHex = hit.collider.gameObject;
                        NextGam = hit.collider.gameObject;
                    }
                    else{
                        NextGam = hit.collider.gameObject;
                        _nextHex = null;
                    }
                }
            }

            //If can't move to the next target. The unit will get all neighboor and go to the closest one of the target hex
            if(NextGam == null) return;
            while(_nextHex == null){
                List<GameObject> _nextHexNeighboor = StaticRuntime.getNeighboorList(NextGam);

                if(HexUnderUnit != null){
                    List<GameObject> thisNeighboorList = StaticRuntime.getNeighboorList(HexUnderUnit);

                    List<GameObject> neighboorInCommonList = new List<GameObject>();

                    foreach(GameObject gam in _nextHexNeighboor){
                        if(thisNeighboorList.Contains(gam)){
                            neighboorInCommonList.Add(gam);
                        }
                    }

                    float closestNextGam = Mathf.Infinity;
                    foreach(GameObject gam in neighboorInCommonList){
                        if(gam.GetComponent<Map.HexManager>().TargetedUnit == null || gam.GetComponent<Map.HexManager>().TargetedUnit == this.gameObject){
                            _nextHex = gam;
                            break;
                        }
                        else{
                            if(Vector3.Distance(gam.transform.position, _targetHex.transform.position) <= closestNextGam){
                                NextGam = gam;
                                closestNextGam = Vector3.Distance(gam.transform.position, _targetHex.transform.position);
                            }
                        }
                    }
                }
                else{
                    _nextHex = _nextHexNeighboor[Random.Range(0, _nextHexNeighboor.Count)];
                }
               
            }

            _startHex = _hexUnderUnit;
            if(_startHex.GetComponent<Map.HexManager>().UnitOnHex == null) _startHex.GetComponent<Map.HexManager>().TargetedUnit = null;
            _startHex.GetComponent<Map.HexManager>().AddMovingUnit(this.gameObject);
            _nextHex.GetComponent<Map.HexManager>().AddMovingUnit(this.gameObject);
        }

        /// <summary>
        /// Set a new hex to the unit
        /// </summary>
        /// <param name="newHex"></param>
        public void ChangeHexUnderUnit(GameObject newHex = null){
            if(_hexUnderUnit != null)_hexUnderUnit.GetComponent<Map.HexManager>().RemoveUnit(true);
            this._hexUnderUnit = newHex;
            if(HexUnderUnit != null) this.transform.position = _hexUnderUnit.transform.position + new Vector3(
                0, 
                _hexUnderUnit.GetComponent<MeshCollider>().bounds.size.y / 2 + this.GetComponent<MeshCollider>().bounds.size.y / 2, 
                0);
        }
        #endregion SetHexValue
    }
}
