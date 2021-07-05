using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>{
    //Delegate when an object under the mouse change
    public delegate void OnObjectUnderMouseChange();

    [Header("Mouse Informations")]
    //CYLINDER
    [SerializeField, ReadOnly] private GameObject _cylinderUnderMouse = null;
    [SerializeField] private LayerMask _cylinderLayer;
    GameObject lastCylinderUnderMouse = null;
    //Cylinder Event
    public static OnObjectUnderMouseChange cylinderChange;

    [Space]

    //UNIT
    [SerializeField, ReadOnly] private GameObject _unitUnderMouse = null;
    [SerializeField] private LayerMask _unitLayer;
    GameObject lastUnitUnderMouse = null;
    //Unit Event
    public static OnObjectUnderMouseChange unitChange;

    [Header("Drag Info")]
    [ReadOnly, SerializeField] private bool _isDraggingUnit = false;
    [SerializeField, ReadOnly] private GameObject _unitDragging = null;

    private void Update(){
        GenerateRaycastsUnderMouse();

        if(Input.GetMouseButtonDown(0)){
            StartDragging();
        }
        if(Input.GetMouseButtonUp(0)){
            StopDragging();
        }
    }

    /// <summary>
    /// Generate raycast from the mouse (Detect Cylinder/Terrain and Unit)
    /// </summary>
    void GenerateRaycastsUnderMouse(){
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit, Mathf.Infinity, _cylinderLayer)){
            _cylinderUnderMouse = hit.collider.gameObject;
            CheckCylinder();
        }
        //If there is no CYLINDER
        else{
            _cylinderUnderMouse = null;
        }

        if(Physics.Raycast(ray, out hit, Mathf.Infinity, _unitLayer)){
            _unitUnderMouse = hit.collider.gameObject;
            CheckUnit();
        }
        //If there is no UNIT
        else{
            _unitUnderMouse = null;
        }
    }


    #region DraggingUnit
    /// <summary>
    /// Let the player drag a unit
    /// </summary>
    void StartDragging(){
        if(checkForDragging()){
            _isDraggingUnit = true;
            _unitDragging = _unitUnderMouse;
            cylinderChange += MoveUnitWhenDragging;
        }
    }

    /// <summary>
    /// When the player release the mouse button when dragging
    /// </summary>
    void StopDragging(){
        if(_isDraggingUnit){
            _isDraggingUnit = false;
            _unitDragging = null;
            cylinderChange -= MoveUnitWhenDragging;
        }
    }

    /// <summary>
    /// move the unit on top of a cylinder
    /// </summary>
    void MoveUnitWhenDragging(){
        _unitDragging.transform.position = new Vector3(
            _cylinderUnderMouse.transform.position.x,
            _cylinderUnderMouse.transform.position.y + (_cylinderUnderMouse.GetComponent<MeshCollider>().bounds.size.y / 2) + (_unitDragging.GetComponent<MeshCollider>().bounds.size.y / 2), 
            _cylinderUnderMouse.transform.position.z);
    }

    #endregion DraggingUnit

    #region CheckMouse
    void CheckCylinder(){
        if(CheckCylinderUnderMouse()){
            //CALL EVENT
            if(cylinderChange != null && _cylinderUnderMouse != null) cylinderChange();
            lastCylinderUnderMouse = _cylinderUnderMouse;
        }
    }
    /// <summary>
    /// Check if the lastCylinder under the mouse is different from the current cylinder under mouse
    /// </summary>
    /// <returns></returns>
    bool CheckCylinderUnderMouse(){
        if(_cylinderUnderMouse != lastCylinderUnderMouse)return true;
        else return false;
    }

    void CheckUnit(){
        if(checkUnitUnderMouse()){
            //CALL EVENT
            if(unitChange != null) unitChange();
            lastUnitUnderMouse = _unitUnderMouse;
        }
    }
    /// <summary>
    /// Check if the lastUnit under the lmouse is different from the Unit object under mouse
    /// </summary>
    /// <returns></returns>
    bool checkUnitUnderMouse(){
        if(_unitUnderMouse != lastUnitUnderMouse) return true;
        else return false;
    }

    /// <summary>
    /// Check if the player can drag a unit
    /// </summary>
    /// <returns></returns>
    bool checkForDragging(){
        if(_unitUnderMouse != null){
            return true;
        }
        else{
            return false;
        }
    }
    #endregion CheckMouse
}
