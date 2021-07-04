using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unit{
    [RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
    public class UnitManager : MonoBehaviour{
        public Units_SO _unitScriptable = null;
        Units_SO _lastUnitSO = null;

        /// <summary>
        /// If there is any changement to the variables
        /// </summary>
        private void OnValidate(){
            //Refresh all the data if the ScriptableObject change
            if(_unitScriptable != _lastUnitSO && _unitScriptable != null){
                _unitScriptable.RefreshUnitData(this.gameObject);
                _lastUnitSO = _unitScriptable;
            }
        }
    }
}
