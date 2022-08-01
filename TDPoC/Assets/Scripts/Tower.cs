using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    [field: SerializeField]
    public float ViewDistance { get; protected set; }

    [field: SerializeField]
    public TargetTypes Targeting { get; protected set; }

    public FieldOfView FieldOfView { get; protected set; }

    protected bool cd;  // cooldown

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Targeting = TargetTypes.FIRST;
        FieldOfView = GetComponentInChildren<FieldOfView>();
        cd = false;
    }

    protected virtual void Update()
    {
        Bloon target = FieldOfView?.GetTarget(Targeting);
        if (target != null && !cd)
        {
            StartCoroutine(Shoot());
        }
    }

    protected abstract IEnumerator Shoot();

    protected void OnValidate()
    {
        if (FieldOfView != null) FieldOfView.ViewDistance = ViewDistance;
    }
}
