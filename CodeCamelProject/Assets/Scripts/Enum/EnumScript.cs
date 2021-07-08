using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumScript : MonoBehaviour{
    /// <summary>
    /// Know which player
    /// </summary>
    public enum PlayerSide{
        None,
        RedPlayer,
        BluePlayer
    }

    /// <summary>
    /// All the terrain Effect
    /// </summary>
    public enum TerrainType
    {
        Ground,
        Forest,
        Beach
    }

    /// <summary>
    /// Family of the Unit
    /// </summary>
    public enum UnitsFamily
    {
        Family1,
        Family2,
        Family3,
        Family4,
        Family5
    }

    /// <summary>
    /// Elements for the Unit
    /// </summary>
    public enum UnitsElement
    {
        None,
        Fire,
        Water,
        Ground,
        Wind
    }
}
