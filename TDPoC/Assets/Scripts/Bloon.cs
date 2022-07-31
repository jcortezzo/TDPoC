using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloon : MonoBehaviour
{
    [SerializeField]
    private Path path;

    [SerializeField]
    private float speed;

    private bool coroutineAllowed = true;

    // Start is called before the first frame update
    void Start()
    {
        coroutineAllowed = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (coroutineAllowed)
        {
            StartCoroutine(FollowPath());
        }
    }

    private IEnumerator FollowPath()
    {
        coroutineAllowed = false;

        float modifier = 1f;
        float ticRate = 0.01f;
        // if we don't want to normalize speed, just put Time.deltaTime * speed as your for loop condition
        for (float t = 0; t <= 1; t += Time.deltaTime * ticRate * speed * modifier)
        {
            transform.position = path.GetPosition(t);

            // normalize over points to get constant speed
            var nextPos = path.GetPosition(Mathf.Min(0.999f, t + ticRate));
            var dist = Vector2.Distance(transform.position, nextPos);
            modifier = 1f / dist;
            yield return new WaitForEndOfFrame();
        }

        coroutineAllowed = true;
    }
}
