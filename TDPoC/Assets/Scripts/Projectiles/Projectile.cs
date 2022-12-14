using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [field: SerializeField]
    public Tower Shooter { get; set; }

    [field: SerializeField]
    private ProjectileScriptableObject data;

    [field: SerializeField]
    public float Pierce { get { return data.pierce * Shooter.PierceModifier; } }
    [field: SerializeField]
    public float Damage { get { return data.damage * Shooter.DamageModifier; } }
    [field: SerializeField]
    public float LifeSpan { get { return data.lifeSpan; } }
    [field: SerializeField]
    public float Speed { get { return data.speed; } }

    public HashSet<BloonProperties> PropertiesCanHit;

    protected Rigidbody2D rb;

    private int pierceRemaining;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        PropertiesCanHit = new HashSet<BloonProperties>(data.canHit);
        pierceRemaining = Mathf.FloorToInt(Pierce);
        rb = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, LifeSpan);
        StartCoroutine(Travel());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        if (pierceRemaining <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public abstract IEnumerator Travel();

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Bloon b = collision.gameObject.GetComponent<Bloon>();
        if (b != null)
        {
            // hit bloon
            //Debug.Break();
            // TODO: loop over collision points -- have to turn it to OnCollision?
            if (CanHit(b) && pierceRemaining > 0)
            {
                b.SetImmunity(this);
                bool isDead = b.Damage(Mathf.FloorToInt(Damage));
                pierceRemaining--;
            }
        }
    }

    public bool CanHit(Bloon b)
    {
        return CanHit(this, b);
    }

    public static bool CanHit(Projectile p, Bloon b)
    {
        return !b.IsImmuneTo(p) && b.Properties.IsSubsetOf(p.PropertiesCanHit);
    }
}
