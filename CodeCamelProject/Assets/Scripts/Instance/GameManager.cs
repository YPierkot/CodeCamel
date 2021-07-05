using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>{
    //Delegate when an object under the mouse change
    public delegate void OnObjectUnderMouseChange();

    [Header("Mouse Informations")]
    //CYLINDER
    [SerializeField] private GameObject _cylinderUnderMouse = null;
    [SerializeField] private LayerMask _cylinderLayer;
    GameObject lastCylinderUnderMouse = null;
    //Cylinder Event
    public static OnObjectUnderMouseChange cylinderChange;

    [Space]

    //UNIT
    [SerializeField] private GameObject _unitUnderMouse = null;
    [SerializeField] private LayerMask _unitLayer;
    GameObject lastUnitUnderMouse = null;
    //Unit Event
    public static OnObjectUnderMouseChange unitChange;

    private void Update(){
        GenerateRaycastsUnderMouse();
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

    #region CheckMouse
    void CheckCylinder(){
        if(CheckCylinderUnderMouse()){
            //CALL EVENT
            if(cylinderChange != null) cylinderChange();
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
    #endregion CheckMouse
}
