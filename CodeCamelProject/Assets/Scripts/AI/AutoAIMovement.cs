using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unit{
    public class AutoAIMovement : MonoBehaviour{
        [ReadOnly, SerializeField] private bool _autoMovement = false;
        [ReadOnly, SerializeField] private List<GameObject> _unitList = new List<GameObject>();
        [ReadOnly, SerializeField] private List<GameObject> _runtimeList = new List<GameObject>();

        private void Start(){
            _unitList.AddRange(GameManager.Instance.RedPlayerUnit);
            _unitList.AddRange(GameManager.Instance.BluePlayerUnit);
        }

        private void Update(){
            //Launch Auto Movement
            if(Input.GetKeyDown(KeyCode.R)){
                _runtimeList.Clear();
                _autoMovement = !_autoMovement;
            }

            if(_autoMovement){
                if(_runtimeList.Count != 0){
                    for(int i = 0; i < _runtimeList.Count; i++){
                        if(_runtimeList[i].GetComponent<Unit.Movement>().TargetHex != null){
                            if(Mathf.Abs(Vector3.Distance(new Vector3(_runtimeList[i].GetComponent<Unit.Movement>().TargetHex.transform.position.x, _runtimeList[i].transform.position.y,_runtimeList[i].GetComponent<Unit.Movement>().TargetHex.transform.position.z), _runtimeList[i].transform.position)) <= .1f){
                                _runtimeList.Remove(_runtimeList[i]);
                            }
                        }
                    }
                }
                else{
                    LaunchAutoMovement();
                }
            }
        }

        void LaunchAutoMovement(){
            _runtimeList.AddRange(_unitList);
            GetComponent<Unit.MovementAIManager>().ResetWorld();
            GetComponent<Unit.MovementAIManager>().SetRedUnitTarget();
            GetComponent<Unit.MovementAIManager>().SetBlueUnitTarget();
        }
    }
}