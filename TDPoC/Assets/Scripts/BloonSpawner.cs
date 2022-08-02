using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        for (int i = 0; i < wave.Length; i++)
        {
            Burst burst = wave[i];
            StartCoroutine(SendBurst(burst));
            yield return new WaitForSeconds(burst.timeTilNextWave);
        }
    }

    private IEnumerator SendBurst(Burst burst)
    {
        int length = burst.bloonPrefabSequence.Length;
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < burst.count[i]; j++)
            {
                Transform bloon = Instantiate(burst.bloonPrefabSequence[i], Vector3.zero, Quaternion.identity, this.transform);
                bloon.gameObject.GetComponent<Bloon>().SetPath(path);
                // Instantiate(burst.bloonPrefabSequence[i]);
                yield return new WaitForSeconds(burst.spacing[i]);
            }
        }
    }
}
