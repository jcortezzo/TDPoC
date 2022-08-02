using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bloon", menuName = "ScriptableObjects/Bloon", order = 1)]
public class BloonScriptableObject : ScriptableObject
{
    public string bloonName;
    public int health;
    public float speed;
    public BloonProperties[] properties;
}

public enum BloonProperties
{
    CAMO,
    LEAD,
    PURPLE,
    BLACK,
    WHITE,
    FORTIFIED,
    CERAMIC,
    MOAB,
    BOSS,
}
