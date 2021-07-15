using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unit{
    public class AutoAIMovement : MonoBehaviour{
        [ReadOnly, SerializeField] private bool _autoMovement = false;
        [ReadOnly, SerializeField] private List<GameObject> _unitList = new List<GameObject>();
        [ReadOnly, SerializeField] private List<GameObject> _runtimeList = new List<GameObject>();

        bool falseT = false;

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


            if(Input.GetKey(KeyCode.T)){
                falseT = true;
            }

            if(falseT){
                int id = Random.Range(0, GameManager.Instance.WolrdGam.transform.childCount);
                List<int> neighboorIdList = StaticRuntime.GetneighboorFromId(id, GameManager.Instance.WolrdGam.transform.GetChild(id).GetComponent<Map.HexManager>().Line);
                foreach(int i in neighboorIdList){
                    if(i < 0 || i > 80){
                        Debug.LogError(id + " / " + i);
                    }
                    else{
                        Debug.Log(id + " / " + i);
                    }
                }
            }
        }


        /// <summary>
        /// Launch the Automatic Movement for all the units
        /// </summary>
        void LaunchAutoMovement(){
            _runtimeList.AddRange(_unitList);
            GetComponent<Unit.MovementAIManager>().ResetWorld();
            GetComponent<Unit.MovementAIManager>().SetRedUnitTarget();
            GetComponent<Unit.MovementAIManager>().SetBlueUnitTarget();
        }
    }
}