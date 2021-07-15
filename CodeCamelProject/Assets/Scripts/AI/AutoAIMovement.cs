using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace AI{
    public class AutoAIMovement : MonoBehaviour{
#if UNITY_EDITOR
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
                _autoMovement = !_autoMovement;
                LaunchAutoMovement();
            }

            //Ai for the movement
            /*
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
                    _runtimeList.Clear();
                    LaunchAutoMovement();
                    _runtimeList.AddRange(_unitList);
                }
            }
*/

            //Ai for the Attack
            if(_autoMovement){
                if(GameManager.Instance.RedPlayerUnit.Count == 0 || GameManager.Instance.BluePlayerUnit.Count == 0){
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
            foreach(GameObject unit in _unitList){
                if(unit.GetComponent<Unit.UnitManager>().Player == EnumScript.PlayerSide.RedPlayer){
                    if(!GameManager.Instance.RedPlayerUnit.Contains(unit)){
                        GameManager.Instance.RedPlayerUnit.Add(unit);
                    }
                }
                else{
                    if(!GameManager.Instance.BluePlayerUnit.Contains(unit)){
                        GameManager.Instance.BluePlayerUnit.Add(unit);
                    }
                }
            }

            GetComponent<AI.MovementAIManager>().ResetWorld();
            GetComponent<AI.MovementAIManager>().SetRedUnitTarget();
            GetComponent<AI.MovementAIManager>().SetBlueUnitTarget();
        }
    }
#endif
}