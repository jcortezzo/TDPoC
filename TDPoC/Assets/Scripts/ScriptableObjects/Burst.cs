using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Burst", menuName = "ScriptableObjects/Bloons/Rounds/Burst")]
public class Burst : ScriptableObject
{
    public Transform[] bloonPrefabSequence;
    public int[] count;
    public float[] spacing;
    public float timeTilNextWave;
}
