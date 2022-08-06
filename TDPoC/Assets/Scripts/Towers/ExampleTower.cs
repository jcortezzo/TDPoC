using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleTower : Tower
{
    protected override IEnumerator Shoot(Bloon target)
    {
        Debug.Log("Shoot!");
        target.Damage(1);
        yield return null;
    }
}
