using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>{
    #region Variables
    //Delegate when an object under the mouse change
    public delegate void OnObjectUnderMouseChange();

    [Header("Mouse Informations")]
    //CYLINDER
    [SerializeField] private LayerMask _cylinderLayer;
    [SerializeField, ReadOnly] private GameObject _cylinderUnderMouse = null;
    GameObject lastCylinderUnderMouse = null;
    //Cylinder Event
    public static OnObjectUnderMouseChange cylinderChange;

    [Space]

    //UNIT
    [SerializeField] private LayerMask _unitLayer;
    [SerializeField, ReadOnly] private GameObject _unitUnderMouse = null;
    GameObject lastUnitUnderMouse = null;
    //Unit Event
    public static OnObjectUnderMouseChange unitChange;

    [Header("Drag Info")]
    [SerializeField] private float _dragSpeed = 0f;
    [ReadOnly, SerializeField] private bool _isDraggingUnit = false;
    [ReadOnly, SerializeField] private bool _hasReachTarget = false;
    [Space(5)]
    [ReadOnly, SerializeField] private GameObject _unitDragging = null;
    [ReadOnly, SerializeField] private GameObject _startHex = null;
    [ReadOnly, SerializeField] private GameObject _lastHexUnderMouse = null;
    [ReadOnly, SerializeField]  private Vector3 _targetTransform;


    [Header("Unit Info")]
    [SerializeField] private List<GameObject> _redPlayerUnit = new List<GameObject>();
    [SerializeField] private List<GameObject> _bluePlayerUnit = new List<GameObject>();

    public List<GameObject> RedPlayerUnit { get => _redPlayerUnit; set => _redPlayerUnit = value; }
    public List<GameObject> BluePlayerUnit { get => _bluePlayerUnit; set => _bluePlayerUnit = value; }
    #endregion Variables

    private void Start(){
        _hasReachTarget = true;
    }

    private void Update(){
        GenerateRaycastsUnderMouse();

        if(Input.GetMouseButtonDown(0)){
            StartDragging();
        }
        if(Input.GetMouseButtonUp(0)){
            StopDragging();
        }

        if(_isDraggingUnit) {
            if(_lastHexUnderMouse != null){
                _unitDragging.transform.position = Vector3.Lerp(_unitDragging.transform.position, _targetTransform, Time.deltaTime * _dragSpeed);
                if(Vector3.Distance(_unitDragging.transform.position, _targetTransform) < 0.01f){
                    _hasReachTarget = true;
                }
                else{
                    _hasReachTarget = false;
                }
            }
            else{
                _hasReachTarget = true;
            }
        }
        else if(!_hasReachTarget && !_isDraggingUnit){
            _unitDragging.transform.position = Vector3.Lerp(_unitDragging.transform.position, _targetTransform, Time.deltaTime * _dragSpeed);
            if(Vector3.Distance(_unitDragging.transform.position, _targetTransform) < 0.01f){
                _hasReachTarget = true;
                _unitDragging = null;
                _targetTransform = Vector3.zero;
                _lastHexUnderMouse = null;
            }
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
            _unitDragging = _unitUnderMouse;
            _isDraggingUnit = true;
            _hasReachTarget = false;
            cylinderChange += MoveUnitWhenDragging;
            if(_unitDragging.GetComponent<Unit.UnitManager>().HexUnderUnit != null) _startHex = _unitDragging.GetComponent<Unit.UnitManager>().HexUnderUnit;
        }
    }

    /// <summary>
    /// When the player release the mouse button when dragging
    /// </summary>
    void StopDragging(){
        if(_isDraggingUnit){
            _isDraggingUnit = false;
            cylinderChange -= MoveUnitWhenDragging;
            if(_lastHexUnderMouse != null) _lastHexUnderMouse.GetComponent<Map.HexManager>().AddUnitToTerrain(_unitDragging, _startHex);
            _startHex = null;

            //If goal is reach
            if(_hasReachTarget){
                _unitDragging = null;
                _targetTransform = Vector3.zero;
                _lastHexUnderMouse = null;
            }
        }
    }

    /// <summary>
    /// move the unit on top of a cylinder
    /// </summary>
    void MoveUnitWhenDragging(){
        if(_cylinderUnderMouse != null){
            if(_cylinderUnderMouse.GetComponent<Map.HexManager>().PlayerCanPose == _unitDragging.GetComponent<Unit.UnitManager>().Player){
                _lastHexUnderMouse = _cylinderUnderMouse;
            }

        }
        else{
            _lastHexUnderMouse = _unitDragging.GetComponent<Unit.UnitManager>().HexUnderUnit;
        }

        if(_lastHexUnderMouse != null){
            _targetTransform = new Vector3(_lastHexUnderMouse.transform.position.x,
            _lastHexUnderMouse.transform.position.y + (_cylinderUnderMouse.GetComponent<MeshCollider>().bounds.size.y / 2) + (_unitDragging.GetComponent<MeshCollider>().bounds.size.y / 2),
            _lastHexUnderMouse.transform.position.z);
        }
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
