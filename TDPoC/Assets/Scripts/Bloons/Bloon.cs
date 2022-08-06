using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloon : MonoBehaviour
{
    [SerializeField]
    private BloonScriptableObject data;

    [SerializeField]
    private Path path;

    [SerializeField]
    public float Speed { get { return data.speed; } }

    [field: SerializeField]
    public float Distance { get; set; }  // normalized to Path length

    [field: SerializeField]
    public HashSet<BloonProperties> Properties { get; private set; }

    [field: SerializeField]
    public int Health { get; private set; }

    // TODO: Add Health

    private bool coroutineAllowed = true;

    void Awake()
    {
        Health = 1;
        Distance = 0.001f;
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("made bloon!");
        coroutineAllowed = true;
        Properties = new HashSet<BloonProperties>(data.properties);
        Health = data.health;
    }

    // Update is called once per frame
    void Update()
    {
        if (coroutineAllowed)
        {
            StartCoroutine(FollowPath());
        }
    }

    void LateUpdate()
    {
        if (!IsAlive())
        {
            Destroy(this.gameObject);
        }
    }

    public void SetPath(Path p)
    {
        this.path = p;
    }

    public bool Damage(int d)
    {
        Health -= d;
        return !IsAlive();
    }

    public bool IsAlive()
    {
        return Health > 0;
    }

    public void OnDestroy()
    {
        //new List<Burst>(data.toSpawn).ForEach(Debug.Log);
        BloonCoroutineManager.Instance.StartCoroutine(BloonSpawner.SendWave(BloonCoroutineManager.Instance, data.toSpawn, this.path, this.Distance));
        //StartCoroutine(BloonSpawner.SendWave(this, data.toSpawn, this.path, this.Distance));
    }

    private IEnumerator FollowPath()
    {
        Debug.Log("starting on path");
        coroutineAllowed = false;

        float modifier = 1f;
        float ticRate = 0.01f;
        // if we don't want to normalize speed, just put Time.deltaTime * speed as your for loop condition
        for (; Distance <= 1; Distance += Time.deltaTime * ticRate * Speed * modifier)
        {
            transform.position = path.GetPosition(Distance);
            transform.up = path.GetRotation(Distance);

            // normalize over points to get constant speed
            var nextPos = path.GetPosition(Mathf.Min(0.999f, Distance + ticRate));
            var dist = Vector2.Distance(transform.position, nextPos);
            modifier = 1f / dist;
            yield return new WaitForEndOfFrame();
        }

        Destroy(this.gameObject);

        coroutineAllowed = true;
    }
}
