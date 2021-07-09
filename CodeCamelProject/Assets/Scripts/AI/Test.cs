using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Test : MonoBehaviour
{
    private void Update(){
        if(Input.GetKeyDown(KeyCode.Z)){
            List<GameObject> neighboorList = StaticRuntime.getNeighboorListAtRange(this.gameObject, 2);
            foreach(GameObject gam in neighboorList){
                gam.GetComponent<MeshRenderer>().sharedMaterial = (Material) AssetDatabase.LoadAssetAtPath("Assets/AssetData/Materials/GoldHex.mat", typeof(Material));
            }
        }
    }
}
