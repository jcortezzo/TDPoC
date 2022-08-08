using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BloonSpawner : MonoBehaviour
{
    [SerializeField]
    private Round[] rounds;

    [SerializeField]
    private Path path;

    private int currentRound;

    // Start is called before the first frame update
    void Start()
    {
        currentRound = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Spawn(rounds[currentRound]);
        }
    }

    public void Spawn(Round round)
    {
        Burst[] wave = round.wave;
        if (wave == null)
        {
            Debug.LogError("Wave is null");
            return;
        }
        
        StartCoroutine(SendWave(wave));
    }


    private IEnumerator SendWave(Burst[] wave)
    {
        yield return StartCoroutine(SendWave(this, wave, this.path, 0.001f));
    }

    public static IEnumerator SendWave(MonoBehaviour mb, Burst[] wave, Path p, float distance, HashSet<Projectile> immunitySet=null)
    {
        if (wave == null)
        {
            yield break;
        }
        for (int i = 0; i < wave.Length; i++)
        {
            Burst burst = wave[i];
            yield return mb.StartCoroutine(SendBurst(burst, p, distance, immunitySet));
            yield return new WaitForSeconds(burst.timeTilNextWave);
        }
    }

    /// <summary>
    /// Currently unused
    /// </summary>
    /// <param name="burst"></param>
    /// <returns></returns>
    private IEnumerator SendBurst(Burst burst)
    {
        yield return StartCoroutine(SendBurst(burst, this.path, 0.001f));
    }

    public static IEnumerator SendBurst(Burst burst, Path p, float distance, HashSet<Projectile> immunitySet= null)
    {
        int length = burst.bloonPrefabSequence.Length;
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < burst.count[i]; j++)
            {
                Transform bloon = Instantiate(burst.bloonPrefabSequence[i], Vector3.up * 100, Quaternion.identity, null);
                Bloon b = bloon.gameObject.GetComponent<Bloon>();
                b.SetPath(p);
                b.Distance = distance;
                Debug.Log($"{immunitySet?.Count}");
                immunitySet?.ToList().ForEach(projectile => b.SetImmunity(projectile));

                // Instantiate(burst.bloonPrefabSequence[i]);
                yield return new WaitForSeconds(burst.spacing[i]);
            }
        }
    }
}
