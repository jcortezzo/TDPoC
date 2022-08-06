using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Round", menuName = "ScriptableObjects/Bloons/Rounds/Round")]
public class Round : ScriptableObject
{
    public Burst[] wave;
}
