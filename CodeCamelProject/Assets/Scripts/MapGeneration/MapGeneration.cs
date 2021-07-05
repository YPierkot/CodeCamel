using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map {

    public class MapGeneration : MonoBehaviour{
        [Header("Map Size")]
        [SerializeField] private int _xSize = 0;
        [SerializeField] private int _ySize = 0;

        [Header("Map Gam")]
        [SerializeField] private GameObject _meshToCreate = null;
        
        /// <summary>
        /// Generate the terrain based on the width of the map
        /// </summary>
        public void GenerateMap(){
            DeleteMap();
            List<Vector3> posList = GenerateCylinderPos();

            foreach(Vector3 pos in posList){
                GameObject cyl = Instantiate(_meshToCreate, pos, Quaternion.Euler(-90,0,90), this.transform);
                cyl.name = "cylinder " + posList.IndexOf(pos).ToString();
            }
        }

        /// <summary>
        /// Delete the terrain
        /// </summary>
        public void DeleteMap(){
            if(transform.childCount == 0) return;
            for(int i = 0; i < transform.childCount; i++){
                DestroyImmediate(transform.GetChild(i).gameObject, true);
            }
        }

        List<Vector3> GenerateCylinderPos(){
            List<Vector3> cylinderPosList = new List<Vector3>();

            for(int x = 0; x < _xSize; x++){
                for(int y = 0; y < _ySize; y++){
                    cylinderPosList.Add(new Vector3(x * 1.5f , Random.Range(0, .35f), y * 1.73f + (x % 2 == 0 ? 0 : 0.865f)));
                }
            }
            return cylinderPosList;
        }
    }
}
