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
    public float DamageModifier { get; protected set; } = 1;

    [field: SerializeField]
    public float PierceModifier { get; protected set; } = 1;

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

    public void Face(MonoBehaviour target)
    {
        transform.up = this.GetDirectionTowards(target);
    }

    public Projectile InstantiateProjectile(GameObject prefab)
    {
        return InstantiateProjectile(prefab, this.transform.position);
    }

    public Projectile InstantiateProjectile(GameObject prefab, Vector2 worldPosition, Vector2? direction = null)
    {
        var result = Instantiate(prefab, worldPosition, Quaternion.identity, this.transform).GetComponent<Projectile>();
        result.Shooter = this;
        // if Tower specified projectile needs to point a certain direction
        if (direction != null)
        {
            this.transform.up = (Vector2)direction;
        }
        return result;
    }

    protected void OnValidate()
    {
        if (FieldOfView != null) FieldOfView.ViewDistance = ViewDistance;
    }
}
