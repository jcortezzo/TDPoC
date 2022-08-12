using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class BloonsMath
{
    private const float MIN_PATH_DISTANCE = 0.0001f;
    private const float MAX_PATH_DISTANCE = 1 - MIN_PATH_DISTANCE;

    public static Vector2 BezierN(float t, Vector2[] points)
    {
        int n = points.Length - 1;
        Vector2 result = Vector2.zero;

        for (int i = 0; i <= n; i++)
        {
            result += (float)Combination(n, i) *
                      Mathf.Pow(1 - t, n - i) *
                      Mathf.Pow(t, i) * points[i];
        }
        return result;
    }

    public static Vector2 DerivativeBezierN(float t, Vector2[] points)
    {
        int n = points.Length - 1;
        Vector2 result = Vector2.zero;

        for (int i = 0; i <= n; i++)
        {
            result += (float)Combination(n, i) *
                      points[i] *
                      ((i * Mathf.Pow(t, i - 1) * Mathf.Pow(1 - t, n - i)) -
                       (Mathf.Pow(t, i) * (n - i) * Mathf.Pow(1 - t, n - i - 1)));
        }
        return result;
    }

    private static System.Numerics.BigInteger Combination(int n, int r)
    {
        return Factorial(n) / ((Factorial(n - r)) * Factorial(r));
    }

    private static System.Numerics.BigInteger Factorial(System.Numerics.BigInteger n)
    {
        if (n == 0)
        {
            return 1;
        } 
        else
        {
            return n * Factorial(n - 1);
        }
    }

    public static Vector2 GetDirectionTowards(this MonoBehaviour mb, MonoBehaviour other)
    {
        Vector2 targetPos = other.transform.position;
        Vector2 currentPos = mb.transform.position;
        return (targetPos - currentPos).normalized;
    }

    public static float GetNextDistance(this Path path, float prevDistance, float speed, float ticRate, float time) {
        // t1 = (deltaTime * ticRate * speed) / dist(B(t0), B(t0 + ticRate))
        float t = time * ticRate * speed;
        t /= Vector2.Distance(path.GetPosition(prevDistance), path.GetPosition(prevDistance + ticRate));
        return t + prevDistance;
    }

    public static float BoundDistance(this float t)
    {
        return Mathf.Max(MIN_PATH_DISTANCE, Mathf.Min(MAX_PATH_DISTANCE, t));
    }
}
