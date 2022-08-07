using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Projectile", menuName = "ScriptableObjects/Towers/Projectile")]

public class ProjectileScriptableObject : ScriptableObject
{
    public string projectileName;
    public int pierce;
    public float damage;
    public float lifeSpan;
    public float speed;

    public BloonProperties[] canHit = 
            (BloonProperties[]) System.Enum.GetValues(typeof(BloonProperties));
}
