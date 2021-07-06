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
}
