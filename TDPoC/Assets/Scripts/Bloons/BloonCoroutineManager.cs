using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloonCoroutineManager : MonoBehaviour
{
    public static BloonCoroutineManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
