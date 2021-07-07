using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unit
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private LayerMask _layer;

        [Header("Hex Info")]
        [ReadOnly, SerializeField] private GameObject _targetUnit;
        [ReadOnly, SerializeField] private GameObject _targetUnitCylinder;
        [Space]
        [ReadOnly, SerializeField] private GameObject _basedCylinder = null;
        [ReadOnly, SerializeField] private GameObject _targetCylinder = null;

        bool canMoveUnit = false;

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.E))
            {

                _basedCylinder = this.GetComponent<Unit.UnitManager>().HexUnderUnit;
                GetClosestEnnemy();
            }

            if(canMoveUnit)
            {
                MoveUnitWithPath();
            }
        }

        #region MoveUnit
        /// <summary>
        /// Move the Unit to the closest hexagone
        /// </summary>
        void MoveUnitWithPath()
        {
            //Get the direction of the movement
            Vector3 dir = (new Vector3(_targetCylinder.transform.position.x, this.transform.position.y, _targetCylinder.transform.position.z) - _basedCylinder.transform.position).normalized;
            this.transform.position += new Vector3(dir.x, 0f, dir.z) * Time.deltaTime * this.GetComponent<Unit.UnitManager>()._unitScriptable.GetStat()._moveSpeed;

            //If the unit is closer to the target hex than the original hex
            if(StaticRuntime.aCloserThanb(_targetCylinder, _basedCylinder, this.gameObject))
            {
                _basedCylinder.GetComponent<Map.HexManager>().RemoveUnit();
                _targetCylinder.GetComponent<Map.HexManager>().AddUnitToTerrain(this.gameObject, this.GetComponent<Unit.UnitManager>().HexUnderUnit);
                _targetCylinder.GetComponent<Map.HexManager>().RemoveNextUnit();
            }

            //If the unit is close to the target position (value < 0.025f)
            if(CheckDistanceBetweenUnit())
            {
                _targetCylinder.GetComponent<Map.HexManager>().AddUnitToTerrain(this.gameObject, _basedCylinder);

                if(Vector3.Distance(this.transform.position, _targetUnit.transform.position) >= this.GetComponent<Unit.UnitManager>()._unitScriptable.GetStat()._attackRange * 2)
                {
                    _basedCylinder = this.GetComponent<Unit.UnitManager>().HexUnderUnit;
                    GetClosestEnnemy();
                }
                else
                {
                    canMoveUnit = false;
                }
            }
        }
        #endregion MoveUnit


        #region CalculatePath
        /// <summary>
        /// Get the closest ennemy from the player
        /// </summary>
        void GetClosestEnnemy()
        {
            float closestDistance = Mathf.Infinity;
            GameObject closestGam = null;

            foreach(GameObject unit in GetComponent<Unit.UnitManager>().Player == EnumScript.PlayerSide.RedPlayer ? GameManager.Instance.BluePlayerUnit : GameManager.Instance.RedPlayerUnit)
            {
                if(Mathf.Abs(Vector3.Distance(this.transform.position, unit.transform.position)) < closestDistance)
                {
                    closestGam = unit;
                    closestDistance = Vector3.Distance(this.transform.position, unit.transform.position);
                }
            }

            if(closestGam != null)
            {
                _targetUnit = closestGam;
                CalculatePath();
            }
        }

        /// <summary>
        /// Calculate the path between the two units;
        /// </summary>
        void CalculatePath()
        {
            _targetUnitCylinder = _targetUnit.GetComponent<Unit.UnitManager>().HexUnderUnit;
            Vector3 dir = (_targetUnitCylinder.transform.position - this.transform.position).normalized;

            RaycastHit hit;
            if(Physics.Raycast(_basedCylinder.transform.position, dir, out hit, _layer))
            {
                _targetCylinder = hit.collider.gameObject;
            }

            if(_targetCylinder.GetComponent<Map.HexManager>().TargetedUnit == null && _targetCylinder.GetComponent<Map.HexManager>().UnitOnHex == null)
            {
                canMoveUnit = true;
                _targetCylinder.GetComponent<Map.HexManager>().AddNextUnitOnHex(this.gameObject);
            }
            else
            {
                canMoveUnit = false;
                _targetCylinder = null;
                _basedCylinder = null;
            }
        }
        #endregion CalculatePath

        #region Check
        /// <summary>
        /// Check if the distance bewteen two units is less than 0.025f
        /// </summary>
        /// <returns></returns>
        bool CheckDistanceBetweenUnit()
        {
            if(Mathf.Abs(Vector3.Distance(
                this.transform.position,
                new Vector3(_targetCylinder.transform.position.x, this.transform.position.y, _targetCylinder.transform.position.z))) <= 0.025f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion Check
    }
}
