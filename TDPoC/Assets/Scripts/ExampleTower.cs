using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleTower : Tower
{
    protected override IEnumerator Shoot()
    {
        cd = true;
        Debug.Log("Shoot!");
        yield return new WaitForSeconds(2f);
        cd = false;
    }
}
