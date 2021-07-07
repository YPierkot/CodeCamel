using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Map {

    public class MapGeneration : MonoBehaviour{
        #region Variables
        //MAP INFORMATION
        [SerializeField] private int _xSize = 0;
        [SerializeField] private int _ySize = 0;

        //MAP HEX GAMEOBJECT
        [SerializeField] private GameObject _meshToCreate = null;

        //PUBLIC VARIABLES
        public int XSize { get => _xSize; set => _xSize = value; }
        public int YSize { get => _ySize; set => _ySize = value; }
        public GameObject MeshToCreate { get => _meshToCreate; set => _meshToCreate = value; }
        #endregion Variables

#if UNITY_EDITOR
        /// <summary>
        /// Generate the terrain based on the width of the map
        /// </summary>
        public void GenerateMap(){
            DeleteMap();
            List<Vector3> posList = GenerateCylinderPos();

            foreach(Vector3 pos in posList){
                Object cyl = PrefabUtility.InstantiatePrefab(_meshToCreate, this.transform);
                GameObject cylGam = cyl as GameObject;
                cylGam.transform.position = pos;
                cylGam.transform.rotation = Quaternion.Euler(-90, 0, 90);
                cyl.name = "cylinder " + posList.IndexOf(pos).ToString();
            }
        }

        /// <summary>
        /// Delete the terrain
        /// </summary>
        public void DeleteMap(){
            if(transform.childCount == 0) return;
            for(int i = 0; i < transform.childCount; i++){
                DestroyImmediate(transform.GetChild(0).gameObject, true);
            }
        }

        /// <summary>
        /// Get all the position of the hex
        /// </summary>
        /// <returns></returns>
        List<Vector3> GenerateCylinderPos(){
            List<Vector3> cylinderPosList = new List<Vector3>();

            for(int x = 0; x < _xSize; x++){
                for(int y = 0; y < _ySize; y++){
                    cylinderPosList.Add(new Vector3(x * 1.5f , Random.Range(0, .35f), y * 1.73f + (x % 2 == 0 ? 0 : 0.865f)));
                }
            }
            return cylinderPosList;
        }
#endif
    }
}
