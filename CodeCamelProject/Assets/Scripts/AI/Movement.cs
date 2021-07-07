using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour{
    [SerializeField] private LayerMask _layer;
    [ReadOnly, SerializeField] private GameObject _target;
    [ReadOnly, SerializeField] private GameObject _targetCylinder;

    [SerializeField] private List<GameObject> _hexPath = new List<GameObject>();

    bool canMoveUnit = false;

    private void Update(){
        if(Input.GetKeyDown(KeyCode.E)){
            if(_hexPath.Count != 0){
                cearListGam();
            }

            _hexPath.Add(this.GetComponent<Unit.UnitManager>().HexUnderUnit);
            //_hexPath[0].GetComponent<MeshRenderer>().sharedMaterial = (Material)UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/AssetData/Materials/GoldHex.mat", typeof(Material));
            GetClosestEnnemy();
        }

        if(canMoveUnit){
            MoveUnitWithPath();
        }
    }

    #region MoveUnit
    /// <summary>
    /// Move the Unit to the closest hexagone
    /// </summary>
    void MoveUnitWithPath(){
        //Get the direction of the movement
        Vector3 dir = (new Vector3(_hexPath[1].transform.position.x, this.transform.position.y, _hexPath[1].transform.position.z) - this.GetComponent<Unit.UnitManager>().HexUnderUnit.transform.position).normalized;
        this.transform.position += new Vector3(dir.x, 0f, dir.z) * Time.deltaTime * this.GetComponent<Unit.UnitManager>()._unitScriptable.GetStat()._moveSpeed;

        if(Mathf.Abs(Vector3.Distance(this.transform.position, new Vector3(_hexPath[1].transform.position.x, this.transform.position.y, _hexPath[1].transform.position.z))) <= 0.025f){
            _hexPath[1].GetComponent<Map.HexManager>().AddUnitToTerrain(this.gameObject, this.GetComponent<Unit.UnitManager>().HexUnderUnit);
            if(_hexPath.Count != 0){
                cearListGam();
            }

            if(Vector3.Distance(this.transform.position, _target.transform.position) >= this.GetComponent<Unit.UnitManager>()._unitScriptable.GetStat()._attackRange * 2){
                _hexPath.Add(this.GetComponent<Unit.UnitManager>().HexUnderUnit);
                //_hexPath[0].GetComponent<MeshRenderer>().sharedMaterial = (Material)UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/AssetData/Materials/GoldHex.mat", typeof(Material));
                GetClosestEnnemy();
            }
            else{
                canMoveUnit = false;
            }
        }
    }
    #endregion MoveUnit

    #region ChangeList
    /// <summary>
    /// Remove the gameObject in the list of HEXPATH
    /// </summary>
    void cearListGam(){
        _hexPath.Clear();
    }
    #endregion ChangeList

    #region CalculatePath
    /// <summary>
    /// Get the closest ennemy from the player
    /// </summary>
    void GetClosestEnnemy(){
        float closestDistance = Mathf.Infinity;
        GameObject closestGam = null;

        foreach(GameObject unit in GetComponent<Unit.UnitManager>().Player == EnumScript.PlayerSide.RedPlayer? GameManager.Instance.BluePlayerUnit : GameManager.Instance.RedPlayerUnit){
            if(Mathf.Abs(Vector3.Distance(this.transform.position, unit.transform.position)) < closestDistance){
                closestGam = unit;
                closestDistance = Vector3.Distance(this.transform.position, unit.transform.position);
            }
        }

        if(closestGam != null){
            _target = closestGam;
            EndCylinder();
        }
    }

    /// <summary>
    /// Get the cylinder
    /// </summary>
    void EndCylinder(){
        _targetCylinder = _target.GetComponent<Unit.UnitManager>().HexUnderUnit;
        CalculatePath();
    }

    /// <summary>
    /// Calculate the path between the two units;
    /// </summary>
    void CalculatePath(){
        while(_hexPath[_hexPath.Count - 1] != _targetCylinder){
            Vector3 dir = (_targetCylinder.transform.position - _hexPath[_hexPath.Count - 1].transform.position).normalized;

            RaycastHit hit;
            if(Physics.Raycast(_hexPath[_hexPath.Count - 1].transform.position, dir, out hit, _layer)){
                _hexPath.Add(hit.collider.gameObject);
                //hit.collider.gameObject.GetComponent<MeshRenderer>().sharedMaterial = (Material)UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/AssetData/Materials/GoldHex.mat", typeof(Material));
            }
        }
        canMoveUnit = true;
    }
    #endregion CalculatePath
}
