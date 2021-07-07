using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour{
    [SerializeField] private GameObject _target;
    [SerializeField] private LayerMask _layer;
    [ReadOnly, SerializeField] private GameObject _targetCylinder;
    [SerializeField] private GameObject _unit;

    [SerializeField] private List<GameObject> _hexPath = new List<GameObject>();

    private void Update(){
        if(Input.GetKeyDown(KeyCode.E)){
            EndCylinder();
        }
    }

    /// <summary>
    /// Get the cylinder
    /// </summary>
    void EndCylinder(){
        RaycastHit hit;

        if(Physics.Raycast(_target.transform.position, -Vector3.up, out hit, Mathf.Infinity, _layer)){
            _targetCylinder = hit.collider.gameObject;
            _targetCylinder.GetComponent<MeshRenderer>().sharedMaterial = (Material) UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/AssetData/Materials/GoldHex.mat", typeof(Material));
            _unit.GetComponent<Unit.UnitManager>().HexUnderUnit.GetComponent<MeshRenderer>().sharedMaterial = (Material)UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/AssetData/Materials/GoldHex.mat", typeof(Material));
        }
        else{
            _targetCylinder = null;
        }
    }

    void CalculatePath(){
        
    }
}
