using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [field: SerializeField]
    public Tower Shooter { get; private set; }

    [field: SerializeField]
    private ProjectileScriptableObject data;

    public float Pierce { get { return data.pierce * Shooter.PierceModifier; } }
    public float Damage { get { return data.damage * Shooter.DamageModifier; } }

    public HashSet<BloonProperties> CanHit;

    // Start is called before the first frame update
    void Start()
    {
        CanHit = new HashSet<BloonProperties>(data.canHit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Bloon b = collision.gameObject.GetComponent<Bloon>();
        if (b == null)
        {
            // hit bloon
            bool canHit = b.Properties.IsSubsetOf(this.CanHit);
            if (canHit)
            {
                bool isDead = b.Damage(Mathf.FloorToInt(Damage));
            }
        }
    }
}
