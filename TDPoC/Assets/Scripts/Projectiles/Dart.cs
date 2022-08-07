using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dart : Projectile
{
    [field: SerializeField]
    public Vector2 Direction { get; set; }

    protected override void Start()
    {
        if (Direction == null) Direction = Vector2.zero;
        base.Start();
    }

    public override IEnumerator Travel()
    {
        rb.velocity = Direction.normalized * Speed;
        Debug.Log($"Dart go! @ speed={Speed} direction={rb.velocity.normalized}");
        yield return null;
    }
}
