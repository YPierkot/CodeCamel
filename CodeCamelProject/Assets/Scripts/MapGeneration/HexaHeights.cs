using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexaHeights : MonoBehaviour
{
    [SerializeField] private float gameHeight;
    [SerializeField] private float speed;
    [SerializeField] private List<GameObject> listHexa;
    [SerializeField] private List<float> startHexa;


    private bool allow; 

    private void Awake()
    {
        allow = false;

        for (int i = 0; i < listHexa.Count; i++)
        {
            startHexa.Add(listHexa[i].transform.position.y);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            allow = true;
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            allow = false;
        }


        if (allow && listHexa[0].transform.position.y != gameHeight)
        {
            for (int i = 0; i < listHexa.Count; i++)
            {
                Vector3 vector3 = new Vector3(listHexa[i].transform.position.x, gameHeight, listHexa[i].transform.position.z);
                listHexa[i].transform.position = Vector3.Lerp(listHexa[i].transform.position, vector3, speed);

                if (listHexa[i].GetComponent<Map.HexManager>().UnitOnHex != null)
                {
                    listHexa[i].GetComponent<Map.HexManager>().UnitOnHex.transform.position = new Vector3(listHexa[i].transform.position.x,
                    listHexa[i].transform.position.y + (listHexa[i].GetComponent<MeshCollider>().bounds.size.y / 2) + (listHexa[i].GetComponent<Map.HexManager>().UnitOnHex.GetComponent<MeshCollider>().bounds.size.y / 2),
                    listHexa[i].transform.position.z);
                }
            }
        }
        else if (!allow && listHexa[0].transform.position.y != startHexa[0])
        {
            for (int i = 0; i < listHexa.Count; i++)
            {
                Vector3 vector3 = new Vector3(listHexa[i].transform.position.x, startHexa[i], listHexa[i].transform.position.z);
                listHexa[i].transform.position = Vector3.Lerp(listHexa[i].transform.position, vector3, speed);

                if (listHexa[i].GetComponent<Map.HexManager>().UnitOnHex != null)
                {
                    listHexa[i].GetComponent<Map.HexManager>().UnitOnHex.transform.position = new Vector3(listHexa[i].transform.position.x,
                    listHexa[i].transform.position.y + (listHexa[i].GetComponent<MeshCollider>().bounds.size.y / 2) + (listHexa[i].GetComponent<Map.HexManager>().UnitOnHex.GetComponent<MeshCollider>().bounds.size.y / 2),
                    listHexa[i].transform.position.z);
                }
            }
        }
    }
}
