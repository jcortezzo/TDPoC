using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartMonkey : Tower
{
    [SerializeField]
    private GameObject dartPrefab;
    protected override IEnumerator Shoot(Bloon target)
    {
        Face(target);
        var dir = this.GetDirectionTowards(target);
        var dart = InstantiateProjectile(dartPrefab, this.transform.position, dir).GetComponent<Dart>();
        dart.Direction = dir;
        yield return null;
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }
}
