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

        public GameObject HexUnderUnit { get => _hexUnderUnit; set => _hexUnderUnit = value; }
        public GameObject TargetHex { get => _targetHex; set => _targetHex = value; }
        public GameObject NextHex { get => _nextHex; set => _nextHex = value; }
        public GameObject TargetUnit { get => _targetUnit; set => _targetUnit = value; }

        public Vector3 pos;

        #endregion Variables

        public void Update(){
            if(_nextHex != null){
                MoveUnit();

            }
            else if(_targetHex != null){
                getNextHex();
            }
        }

        #region Movement
        /// <summary>
        /// Get the nextHexagone to go
        /// </summary>
        void getNextHex(){
            Vector3 dir = _targetHex.transform.position - _hexUnderUnit.transform.position;
            RaycastHit hit;

            if(Physics.Raycast(_hexUnderUnit.transform.position, dir, out hit, Mathf.Infinity, _layer)){
                NextHex = hit.collider.gameObject;
            }

            _startHex = _hexUnderUnit;
            _startHex.GetComponent<Map.HexManager>().TargetedUnit = null;
        }

        void MoveUnit(){
            Vector3 dir = (_nextHex.transform.position - _startHex.transform.position).normalized;
            transform.position += new Vector3(dir.x, 0, dir.z) * Time.deltaTime * GetComponent<Unit.UnitManager>()._unitScriptable.GetStat()._moveSpeed;

            if(Mathf.Abs(Vector3.Distance(new Vector3(_nextHex.transform.position.x, transform.position.y, _nextHex.transform.position.z), this.transform.position)) <=
               Mathf.Abs(Vector3.Distance(new Vector3(_hexUnderUnit.transform.position.x, transform.position.y, _hexUnderUnit.transform.position.z), this.transform.position))){
                _nextHex.GetComponent<Map.HexManager>().AddUnitToTerrain(this.gameObject, _hexUnderUnit);
                _hexUnderUnit = _nextHex;
            }

            if(Mathf.Abs(Mathf.Abs(Vector3.Distance(new Vector3(_targetHex.transform.position.x, transform.position.y, _nextHex.transform.position.z), this.transform.position))) <= 0.1f){
                _targetHex.GetComponent<Map.HexManager>().ReloadColor();
                _targetHex = null;
                _nextHex = null;
            }
            else{
                if(Mathf.Abs(Mathf.Abs(Vector3.Distance(new Vector3(_nextHex.transform.position.x, transform.position.y, _nextHex.transform.position.z), this.transform.position))) <= 0.1f){
                    _targetHex.GetComponent<Map.HexManager>().ReloadColor();
                    _targetHex.GetComponent<Map.HexManager>().TargetedUnit = null;
                    Unit.MovementAIManager.Instance.GetRedPlayerTarget(this.gameObject);
                }
            }


        }
        #endregion Movement
        public int z = 0;
        public IEnumerator checkPos(){
            yield return new WaitForSeconds(.25f);
            if(pos != transform.position){
                pos = transform.position;
                z++;

                if(z >= 2){
                    Unit.MovementAIManager.Instance.ResetWorld();
                    Unit.MovementAIManager.Instance.GetRedPlayerTarget();
                }
                StartCoroutine(checkPos());
            }
            else{
                Debug.LogError(gameObject.name + " / " + transform.position + " / " + pos);
                foreach(GameObject gamL in GameManager.Instance.RedPlayerUnit){
                    gamL.GetComponent<Unit.Movement>().StopAllCoroutines();
                }
            }
        }
    }
}
