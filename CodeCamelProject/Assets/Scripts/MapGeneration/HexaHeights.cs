using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexaHeights : MonoBehaviour{
    [SerializeField] private float gameHeight;
    [SerializeField] private float speed;
    [SerializeField] private List<GameObject> listHexa;
    [SerializeField] private List<float> startHexa;
    [SerializeField] private float valeurImportante;


    private bool allow; 

    private void Awake()
    {
        allow = false;
        if(listHexa.Count != 0){
            for(int i = 0; i < listHexa.Count; i++){
                startHexa.Add(listHexa[i].transform.position.y);
            }
        }
    }

    void Update(){
        if(listHexa.Count != 0){
            if(Input.GetKeyDown(KeyCode.Return)){
                allow =! allow;
            }


            if(allow && Vector3.Distance(listHexa[0].transform.position, new Vector3(listHexa[0].transform.position.x, gameHeight, listHexa[0].transform.position.z)) >= valeurImportante){
                for(int i = 0; i < listHexa.Count; i++){
                    Vector3 vector3 = new Vector3(listHexa[i].transform.position.x, gameHeight, listHexa[i].transform.position.z);
                    listHexa[i].transform.position = Vector3.Lerp(listHexa[i].transform.position, vector3, speed * Time.deltaTime);

                    if(listHexa[i].GetComponent<Map.HexManager>().UnitOnHex != null){
                        listHexa[i].GetComponent<Map.HexManager>().UnitOnHex.transform.position = new Vector3(
                        listHexa[i].GetComponent<Map.HexManager>().UnitOnHex.transform.position.x,
                        listHexa[i].transform.position.y + (listHexa[i].GetComponent<MeshCollider>().bounds.size.y / 2) + (listHexa[i].GetComponent<Map.HexManager>().UnitOnHex.GetComponent<MeshCollider>().bounds.size.y / 2),
                        listHexa[i].GetComponent<Map.HexManager>().UnitOnHex.transform.position.z);
                    }
                }
            }
            else if(!allow && Vector3.Distance(listHexa[0].transform.position, new Vector3(listHexa[0].transform.position.x, startHexa[0], listHexa[0].transform.position.z)) >= valeurImportante){
                for(int i = 0; i < listHexa.Count; i++){
                    Vector3 vector3 = new Vector3(listHexa[i].transform.position.x, startHexa[i], listHexa[i].transform.position.z);
                    listHexa[i].transform.position = Vector3.Lerp(listHexa[i].transform.position, vector3, speed * Time.deltaTime);

                    if(listHexa[i].GetComponent<Map.HexManager>().UnitOnHex != null){
                        listHexa[i].GetComponent<Map.HexManager>().UnitOnHex.transform.position = new Vector3(listHexa[i].transform.position.x,
                        listHexa[i].transform.position.y + (listHexa[i].GetComponent<MeshCollider>().bounds.size.y / 2) + (listHexa[i].GetComponent<Map.HexManager>().UnitOnHex.GetComponent<MeshCollider>().bounds.size.y / 2),
                        listHexa[i].transform.position.z);
                    }
                }
            }
        }
        else { }
    }
}
