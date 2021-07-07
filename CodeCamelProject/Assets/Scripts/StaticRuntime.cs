using System.Collections;
using System.Collections.Generic;
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

    /// <summary>
    /// Get all the neighboor from a hex
    /// </summary>
    /// <param name="baseCylinder"></param>
    /// <returns></returns>
    public static List<GameObject> getNeighboorList(GameObject baseCylinder){
        return new List<GameObject>();
    }
}
