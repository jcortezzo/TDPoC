using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Round", menuName = "ScriptableObjects/Round", order = 1)]
public class Round : ScriptableObject
{
    public Burst[] wave;
}
