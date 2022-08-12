using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powers : MonoBehaviour
{
    private const float REGULAR_SPEED = 1f;
    private const float FAST_SPEED = 2f;

    // TODO: Move to some Util class
    public static float ToggleSpeed()
    {
        Time.timeScale = Time.timeScale == REGULAR_SPEED ? FAST_SPEED : REGULAR_SPEED;
        return Time.timeScale;
    }
}
