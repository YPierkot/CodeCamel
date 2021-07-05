using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>{
    private static T _instance;
    public static T Instance { get => _instance; }

    private void Awake(){
        if(_instance == null){
            _instance = this as T;
        }
        else if(_instance != this){
            Destroy(this.gameObject);
        }
        Init();
    }

    /// <summary>
    /// Initialise Method
    /// </summary>
    public virtual void Init() { }
}
