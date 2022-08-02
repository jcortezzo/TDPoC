using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloon : MonoBehaviour
{
    [SerializeField]
    private Path path;

    [SerializeField]
    private float speed;

    [field: SerializeField]
    public float Distance { get; private set; }  // normalized to Path length

    // TODO: Add Health

    private bool coroutineAllowed = true;

    // Start is called before the first frame update
    void Start()
    {
        coroutineAllowed = true;
        Distance = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (coroutineAllowed)
        {
            StartCoroutine(FollowPath());
        }
    }

    public void SetPath(Path p)
    {
        this.path = p;
    }

    private IEnumerator FollowPath()
    {
        coroutineAllowed = false;

        float modifier = 1f;
        float ticRate = 0.01f;
        // if we don't want to normalize speed, just put Time.deltaTime * speed as your for loop condition
        for (Distance = 0; Distance <= 1; Distance += Time.deltaTime * ticRate * speed * modifier)
        {
            transform.position = path.GetPosition(Distance);

            // normalize over points to get constant speed
            var nextPos = path.GetPosition(Mathf.Min(0.999f, Distance + ticRate));
            var dist = Vector2.Distance(transform.position, nextPos);
            modifier = 1f / dist;
            yield return new WaitForEndOfFrame();
        }

        coroutineAllowed = true;
    }
}
