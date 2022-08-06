using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bloon", menuName = "ScriptableObjects/Bloons/Bloon")]
public class BloonScriptableObject : ScriptableObject
{
    public string bloonName;
    public int health;
    public float speed;
    public BloonProperties[] properties;
    public Burst[] toSpawn;
}

public enum BloonProperties
{
    CAMO,
    LEAD,
    PURPLE,
    REGROW,
    BLACK,
    WHITE,
    FORTIFIED,
    CERAMIC,
    MOAB,
    BOSS,
}
