using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unit{
    public class Attack : MonoBehaviour{
        #region Variables
        [ReadOnly, SerializeField] private bool _canAttack = false;
        [ReadOnly, SerializeField] private GameObject _unitToAttack = null;

        float timeBetweenTwoAttack = 0f;
        float actualTime = 0f;


        #endregion Variables



        private void Update(){
            if(_canAttack) {
                AttackTimer();
            }
        }

        /// <summary>
        /// Set the variable for the attack
        /// </summary>
        /// <param name="unit"></param>
        public void LaunchAttack(GameObject unit){
            _unitToAttack = unit;
            timeBetweenTwoAttack = 1 / GetComponent<Unit.UnitManager>()._unitScriptable.GetStat()._attackPerSecond;
            actualTime = 0;

            AttackEnnemy();
            _canAttack = true;
        }

        /// <summary>
        /// Stop the attack
        /// </summary>
        public void StopAttack(){
            _canAttack = false;
            _unitToAttack = null;
        }

        /// <summary>
        /// Make the timer go down to the next attack
        /// </summary>
        void AttackTimer(){
            actualTime += Time.deltaTime;

            //Check if the Targeted Unit is at range
            if(!DistanceBetweenTheTwoUnits()){
                _canAttack = false;

                if(GetComponent<Unit.UnitManager>().Player == EnumScript.PlayerSide.RedPlayer) AI.MovementAIManager.Instance.SetRedUnitTarget(this.gameObject);
                else AI.MovementAIManager.Instance.SetBlueUnitTarget(this.gameObject);
            }

            //Check if the Unit can attack
            if(actualTime >= timeBetweenTwoAttack){
                AttackEnnemy();
            }
        }

        /// <summary>
        /// Create a bullet which will go to the targeted Unit
        /// </summary>
        void AttackEnnemy(){
            actualTime = 0;
            _unitToAttack.GetComponent<Unit.UnitManager>().TakeDamage(10);
            Debug.Log($"{_unitToAttack.name} ({_unitToAttack.GetComponent<Unit.UnitManager>().Player}) take 10 damage from {this.name} ({GetComponent<Unit.UnitManager>().Player})");
        }

        #region Check
        /// <summary>
        /// Check if the distance between this Unit and his target is less than the attack range of this Unit
        /// </summary>
        /// <returns></returns>
        bool DistanceBetweenTheTwoUnits() {
            //Si l'unité a attaquer n'existe plus
            if(GameManager.Instance.RedPlayerUnit.Contains(_unitToAttack) || GameManager.Instance.BluePlayerUnit.Contains(_unitToAttack)){
                if(Vector3.Distance(this.transform.position, _unitToAttack.transform.position) <= GetComponent<Unit.UnitManager>()._unitScriptable.GetStat()._attackRange * 2){
                    return true;
                }
                else{
                    return false;
                }
            }
            else{
                return false;
            }
        }
        #endregion Check
    }
}