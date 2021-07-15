using System;
using System.Reflection;
using System.Collections.Generic;
using AI;
using UnityEditor;
using UnityEngine;

public static class StaticRuntime
{
    /// <summary>
    /// Is the unit closer of the GameObject A than the Gam B
    /// </summary>
    /// <param name="baseC"></param>
    /// <param name="targetC"></param>
    /// <param name="unit"></param>
    /// <returns></returns>
    public static bool aCloserThanb(GameObject targetC, GameObject baseC, GameObject unit){
        if(Mathf.Abs(Vector3.Distance(unit.transform.position, new Vector3(targetC.transform.position.x, unit.transform.position.y, targetC.transform.position.z))) <= Vector3.Distance(unit.transform.position, new Vector3(baseC.transform.position.x, unit.transform.position.y, baseC.transform.position.z))){
            return true;
        }
        else{
            return false;
        }
    }

    #region Neighboor
    /// <summary>
    /// Get all the neighboor from a hex
    /// </summary>
    /// <param name="baseCylinder"></param>
    /// <returns></returns>
    public static List<GameObject> getNeighboorListAtRange(GameObject baseCylinder, int range = 1){
        int id = 0;
        if(baseCylinder != null) id = baseCylinder.GetComponent<Map.HexManager>().Id;
        else return null;

        List<int> finalList = new List<int>();
        List<int> actualList = new List<int>();
        List<int> lastListGet = new List<int>();
        List<int> rememberLastList = new List<int>();

        rememberLastList.Add(id);

        for(int i = 0; i < range; i++){
            rememberLastList.AddRange(lastListGet);
            lastListGet.Clear();
            foreach(int idH in rememberLastList){
                actualList.Clear();
                actualList = GetneighboorFromId(idH, GameManager.Instance.WolrdGam.transform.GetChild(idH).GetComponent<Map.HexManager>().Line);
                foreach(int idL in actualList){
                    if(!finalList.Contains(idL))
                        finalList.Add(idL);
                }
                lastListGet.AddRange(actualList);
            }
            rememberLastList.Clear();
        }

        List<GameObject> neighboorListGam = new List<GameObject>();
        for(int i = 0; i < finalList.Count; i++) {
            if(finalList[i] < (GameManager.Instance.WolrdGam.GetComponent<Map.MapGeneration>().XSize * GameManager.Instance.WolrdGam.GetComponent<Map.MapGeneration>().YSize) - 1 && finalList[i] >= 0){
                neighboorListGam.Add(GameManager.Instance.WolrdGam.transform.GetChild(finalList[i]).gameObject);
            }
        }

        return neighboorListGam;
    }

    /// <summary>
    /// Get all the nieghboorID in a list
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static List<int> GetneighboorFromId(int id, int line){
        List<int> neighboorIdList = new List<int>();

        int ySize = GameManager.Instance.WolrdGam.GetComponent<Map.MapGeneration>().YSize;
        int xSize = GameManager.Instance.WolrdGam.GetComponent<Map.MapGeneration>().XSize;

        if(line % 2 == 0){
            if(id % ySize == 0){
                if(id == 0){
                    neighboorIdList.Add(id + 1); //GAUCHE
                    neighboorIdList.Add(id + 9); //HAUT GAUCHE
                }
                else if(id == (xSize * ySize) - xSize){
                    neighboorIdList.Add(id + 1); //GAUCHE
                    neighboorIdList.Add(id - 9); //BAS GAUCHE
                }
                else{
                    neighboorIdList.Add(id + 1); //GAUCHE
                    neighboorIdList.Add(id + 9); //HAUT GAUCHE
                    neighboorIdList.Add(id - 9); //BAS GAUCHE
                }
            }
            else if(id % ySize == ySize - 1){
                if(id == xSize - 1){
                    neighboorIdList.Add(id + 9); //HAUT GAUCHE
                    neighboorIdList.Add(id + 8); //HAUT DROITE
                    neighboorIdList.Add(id - 1); //DROITE
                }
                else if(id == (xSize * ySize) - 1){
                    neighboorIdList.Add(id - 1); //DROITE
                    neighboorIdList.Add(id - 10); //BAS DROITE
                    neighboorIdList.Add(id - 9); //BAS GAUCHE
                }
                else{
                    neighboorIdList.Add(id + 9); //HAUT GAUCHE
                    neighboorIdList.Add(id + 8); //HAUT DROITE
                    neighboorIdList.Add(id - 1); //DROITE
                    neighboorIdList.Add(id - 10); //BAS DROITE
                    neighboorIdList.Add(id - 9); //BAS GAUCHE
                }
            }
            else{
                if(line == 0){
                    neighboorIdList.Add(id + 1); //GAUCHE
                    neighboorIdList.Add(id + 9); //HAUT GAUCHE
                    neighboorIdList.Add(id + 8); //HAUT DROITE
                    neighboorIdList.Add(id - 1); //DROITE
                }
                else if(line == ySize - 1){
                    neighboorIdList.Add(id + 1); //GAUCHE
                    neighboorIdList.Add(id - 1); //DROITE
                    neighboorIdList.Add(id - 10); //BAS DROITE
                    neighboorIdList.Add(id - 9); //BAS GAUCHE
                }
                else{
                    neighboorIdList.Add(id + 1); //GAUCHE
                    neighboorIdList.Add(id + 9); //HAUT GAUCHE
                    neighboorIdList.Add(id + 8); //HAUT DROITE
                    neighboorIdList.Add(id - 1); //DROITE
                    neighboorIdList.Add(id - 10); //BAS DROITE
                    neighboorIdList.Add(id - 9); //BAS GAUCHE
                }
            }
        }
        else{
            if(id % ySize == 0){
                if(id == (xSize * ySize) - xSize){
                    neighboorIdList.Add(id + 1); //GAUCHE
                    neighboorIdList.Add(id - 9); //BAS DROITE
                    neighboorIdList.Add(id - 8); //BAS GAUCHE
                }
                else{
                    neighboorIdList.Add(id + 1); //GAUCHE
                    neighboorIdList.Add(id + 10); //HAUT GAUCHE
                    neighboorIdList.Add(id + 9); //HAUT DROITE
                    neighboorIdList.Add(id - 9); //BAS DROITE
                    neighboorIdList.Add(id - 8); //BAS GAUCHE
                }
            }
            else if(id % ySize == ySize - 1){
                if(id == (xSize * ySize) - 1){
                    neighboorIdList.Add(id - 1); //DROITE
                    neighboorIdList.Add(id - 9); //BAS DROITE
                }
                else{
                    neighboorIdList.Add(id + 9); //HAUT DROITE
                    neighboorIdList.Add(id - 1); //DROITE
                    neighboorIdList.Add(id - 9); //BAS DROITE
                }
            }
            else{
                if(line == 0){
                    neighboorIdList.Add(id + 1); //GAUCHE
                    neighboorIdList.Add(id + 10); //HAUT GAUCHE
                    neighboorIdList.Add(id + 9); //HAUT DROITE
                    neighboorIdList.Add(id - 1); //DROITE
                }
                else if(line == ySize - 1){
                    neighboorIdList.Add(id + 1); //GAUCHE
                    neighboorIdList.Add(id - 1); //DROITE
                    neighboorIdList.Add(id - 9); //BAS DROITE
                    neighboorIdList.Add(id - 8); //BAS GAUCHE
                }
                else{
                    neighboorIdList.Add(id + 1); //GAUCHE
                    neighboorIdList.Add(id + 10); //HAUT GAUCHE
                    neighboorIdList.Add(id + 9); //HAUT DROITE
                    neighboorIdList.Add(id - 1); //DROITE
                    neighboorIdList.Add(id - 9); //BAS DROITE
                    neighboorIdList.Add(id - 8); //BAS GAUCHE
                }
            }
        }


        return neighboorIdList;
    }

    /// <summary>
    /// Get the closest GameObject from an Object
    /// </summary>
    /// <param name="gamToCheck"></param>
    /// <param name="baseGam"></param>
    /// <returns></returns>
    public static ClosestGam getClosestGameObject(List<GameObject> gamToCheck, GameObject baseGam){
        ClosestGam closeGam = new ClosestGam();
        closeGam.closestDistance = Mathf.Infinity;

        foreach(GameObject gam in gamToCheck){
            if(Vector3.Distance(baseGam.transform.position, gam.transform.position) <= closeGam.closestDistance){
                closeGam.closestDistance = Vector3.Distance(baseGam.transform.position, gam.transform.position);
                closeGam.closestGameObject = gam;
            }
        }

        return closeGam;
    }

    /// <summary>
    /// Get the closest Hexagone from a Unit
    /// </summary>
    /// <param name="gamToCheck"></param>
    /// <param name="baseGam"></param>
    /// <returns></returns>
    public static ClosestGam getClosestFreeHex(List<GameObject> gamToCheck, GameObject baseGam){
        ClosestGam closeGam = new ClosestGam();
        closeGam.closestDistance = Mathf.Infinity;
        //Debug.Log(" ");
        foreach(GameObject gam in gamToCheck){
            //Debug.Log(baseGam.name + " : " + gam.name + " / " + Mathf.Abs(Vector3.Distance(baseGam.transform.position, gam.transform.position)) + " / " + gam.GetComponent<Map.HexManager>().TargetedUnit);
            if(Mathf.Abs(Vector3.Distance(baseGam.transform.position, gam.transform.position)) <= closeGam.closestDistance && gam.GetComponent<Map.HexManager>().TargetedUnit == null){
                closeGam.closestDistance = Vector3.Distance(baseGam.transform.position, gam.transform.position);
                closeGam.closestGameObject = gam;
            }
        }

        return closeGam;
    }
    #endregion Neighboor
}
