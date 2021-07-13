using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unit{
    public class MovementAIManager : MonoSingleton<MovementAIManager>{
        private void Update(){
            if(Input.GetKeyDown(KeyCode.E)){
                SetRedUnitTarget();
                SetBlueUnitTarget();
            }

            if(Input.GetKeyDown(KeyCode.A)){
                ResetWorld();
            }
        }


        /// <summary>
        /// Reset all the world data related to the movement
        /// </summary>
        public void ResetWorld(){
            for(int i = 0; i < GameManager.Instance.WolrdGam.GetComponent<Map.MapGeneration>().GamList.Count; i++){
                GameManager.Instance.WolrdGam.GetComponent<Map.MapGeneration>().GamList[i].GetComponent<Map.HexManager>().RemoveUnit();
                GameManager.Instance.WolrdGam.GetComponent<Map.MapGeneration>().GamList[i].GetComponent<Map.HexManager>().TargetedUnit = null;
                GameManager.Instance.WolrdGam.GetComponent<Map.MapGeneration>().GamList[i].GetComponent<Map.HexManager>().ChangeColor();
            }

            for(int i = 0; i < GameManager.Instance.RedPlayerUnit.Count; i++){
                GameManager.Instance.RedPlayerUnit[i].GetComponent<Unit.Movement>().StopAllCoroutines();
                GameManager.Instance.RedPlayerUnit[i].GetComponent<Unit.Movement>().ChangeHexUnderUnit();
                GameManager.Instance.RedPlayerUnit[i].GetComponent<Unit.Movement>().settargetData(null, null);

                int randomValue = Random.Range(0, GameManager.Instance.WolrdGam.GetComponent<Map.MapGeneration>().GamList.Count);
                while(GameManager.Instance.WolrdGam.transform.GetChild(randomValue).GetComponent<Map.HexManager>().PlayerCanPose != EnumScript.PlayerSide.RedPlayer ||
                    GameManager.Instance.WolrdGam.transform.GetChild(randomValue).GetComponent<Map.HexManager>().UnitOnHex != null){
                    randomValue = Random.Range(0, GameManager.Instance.WolrdGam.transform.childCount);
                }

                GameManager.Instance.WolrdGam.transform.GetChild(randomValue).GetComponent<Map.HexManager>().AddUnitToTerrain(GameManager.Instance.RedPlayerUnit[i]);
                GameManager.Instance.RedPlayerUnit[i].GetComponent<Unit.Movement>().ChangeHexUnderUnit(GameManager.Instance.WolrdGam.transform.GetChild(randomValue).gameObject);
                GameManager.Instance.RedPlayerUnit[i].transform.position = 
                    GameManager.Instance.RedPlayerUnit[i].GetComponent<Unit.Movement>().HexUnderUnit.transform.position + 
                    new Vector3(0, GameManager.Instance.RedPlayerUnit[i].GetComponent<MeshCollider>().bounds.size.y / 2, 0) +
                    new Vector3(0, GameManager.Instance.RedPlayerUnit[i].GetComponent<Unit.Movement>().HexUnderUnit.GetComponent<MeshCollider>().bounds.size.y / 2, 0);
            }

            for(int i = 0; i < GameManager.Instance.BluePlayerUnit.Count; i++){
                GameManager.Instance.BluePlayerUnit[i].GetComponent<Unit.Movement>().StopAllCoroutines();
                GameManager.Instance.BluePlayerUnit[i].GetComponent<Unit.Movement>().ChangeHexUnderUnit();
                GameManager.Instance.RedPlayerUnit[i].GetComponent<Unit.Movement>().settargetData(null, null);

                int randomValue = Random.Range(0, GameManager.Instance.WolrdGam.transform.childCount);
                while(GameManager.Instance.WolrdGam.transform.GetChild(randomValue).GetComponent<Map.HexManager>().PlayerCanPose != EnumScript.PlayerSide.BluePlayer ||
                    GameManager.Instance.WolrdGam.transform.GetChild(randomValue).GetComponent<Map.HexManager>().UnitOnHex != null) { 
                    randomValue = Random.Range(0, GameManager.Instance.WolrdGam.transform.childCount);
                }

                GameManager.Instance.WolrdGam.transform.GetChild(randomValue).GetComponent<Map.HexManager>().AddUnitToTerrain(GameManager.Instance.BluePlayerUnit[i]);
                GameManager.Instance.BluePlayerUnit[i].GetComponent<Unit.Movement>().ChangeHexUnderUnit(GameManager.Instance.WolrdGam.transform.GetChild(randomValue).gameObject);
                GameManager.Instance.BluePlayerUnit[i].transform.position =
                    GameManager.Instance.BluePlayerUnit[i].GetComponent<Unit.Movement>().HexUnderUnit.transform.position +
                    new Vector3(0, GameManager.Instance.BluePlayerUnit[i].GetComponent<MeshCollider>().bounds.size.y / 2, 0) +
                    new Vector3(0, GameManager.Instance.BluePlayerUnit[i].GetComponent<Unit.Movement>().HexUnderUnit.GetComponent<MeshCollider>().bounds.size.y / 2, 0);
            }
        }

        #region MovementAI
        /// <summary>
        /// Get the target for the unit
        /// </summary>
        public void SetRedUnitTarget(GameObject gam = null){
            List<GameObject> redPlayerList = ArrangeGamList(GameManager.Instance.RedPlayerUnit);

            StartCoroutine(SetTargetToUnit(gam != null ? new List<GameObject>() { gam } : redPlayerList, GameManager.Instance.BluePlayerUnit));
        }

        /// <summary>
        /// get all the target for the blue player
        /// </summary>
        /// <param name="gam"></param>
        public void SetBlueUnitTarget(GameObject gam = null){
            List<GameObject> bluePlayerList = ArrangeGamList(GameManager.Instance.BluePlayerUnit);
            StartCoroutine(SetTargetToUnit(gam != null ? new List<GameObject>() { gam } : bluePlayerList, GameManager.Instance.RedPlayerUnit));
        }

        /// <summary>
        /// Set all the target to the Unit
        /// </summary>
        /// <param name="gamList"></param>
        /// <param name="ennemyList"></param>
        /// <returns></returns>
        IEnumerator SetTargetToUnit(List<GameObject> gamList, List<GameObject> ennemyList){
            foreach(GameObject gam in gamList){
                //get the closest Unit
                ClosestGam closestUnit = StaticRuntime.getClosestGameObject(ennemyList, gam);
                //Get the closest Hex
                ClosestGam closestHex = StaticRuntime.getClosestFreeHex(StaticRuntime.getNeighboorListAtRange(closestUnit.closestGameObject, (int) gam.GetComponent<Unit.UnitManager>()._unitScriptable.GetStat()._attackRange), gam);

                List<GameObject> ennemyUnit = new List<GameObject>();
                ennemyUnit.AddRange(ennemyList);

                while(closestHex.closestGameObject == null){
                    ennemyUnit.Remove(closestUnit.closestGameObject);
                    if(ennemyUnit.Count == 0){
                        StartCoroutine(SetTargetToUnit(new List<GameObject> { gam }, ennemyList));
                        break;
                    }
                    closestUnit = StaticRuntime.getClosestGameObject(ennemyUnit, gam);
                    closestHex = StaticRuntime.getClosestFreeHex(StaticRuntime.getNeighboorListAtRange(closestUnit.closestGameObject, (int)gam.GetComponent<Unit.UnitManager>()._unitScriptable.GetStat()._attackRange), gam);
                }

                if(closestHex.closestGameObject != null){
                    gam.GetComponent<Unit.Movement>().settargetData(closestUnit.closestGameObject, closestHex.closestGameObject, true);
                }
                else{
                    StartCoroutine(SetTargetToUnit(new List<GameObject> { gam }, ennemyList));
                    Debug.LogError("There is no place for this Unit");
                }

                yield return new WaitForSeconds(.01f);
            }
        }

        /// <summary>
        /// Order a list by the range of the Unit. Smaller the range of the unit is smaller will be the index
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        List<GameObject> ArrangeGamList(List<GameObject> list){
            List<GameObject> playerList = new List<GameObject>();

            foreach(GameObject gam in list){
                int index = 0;
                if(playerList.Count != 0){
                    foreach(GameObject gamL in playerList){
                        if(gamL.GetComponent<Unit.UnitManager>()._unitScriptable.GetStat()._attackRange < gam.GetComponent<Unit.UnitManager>()._unitScriptable.GetStat()._attackRange){
                            index++;
                        }
                        else{
                            break;
                        }
                    }
                }

                playerList.Insert(index, gam);
            }
            return playerList;
        }
        #endregion MovementAI
    }

    public class ClosestGam{
        public float closestDistance;
        public GameObject closestGameObject;
    }
}
