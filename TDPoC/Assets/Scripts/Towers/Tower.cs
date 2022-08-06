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

    [field: SerializeField]
    public float CooldownDuration { get; protected set; }

    [field: SerializeField]
    public float DamageModifier { get; protected set; }

    [field: SerializeField]
    public float PierceModifier { get; protected set; }

    private bool cd;  // cooldown

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
            StartCoroutine(ShootDecorator(target));
        }
    }

    protected virtual IEnumerator ShootDecorator(Bloon target)
    {
        cd = true;
        yield return StartCoroutine(Shoot(target));
        yield return new WaitForSeconds(this.CooldownDuration);
        cd = false;
    }

    protected abstract IEnumerator Shoot(Bloon target);

    protected void OnValidate()
    {
        if (FieldOfView != null) FieldOfView.ViewDistance = ViewDistance;
    }
}
