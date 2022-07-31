using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ExtensionMethods
{
    public static Vector2 Bezier4(this float t, Vector2 p0, Vector2 p1,
                                                Vector2 p2, Vector2 p3)
    {
        return Mathf.Pow(1 - t, 3) * p0 +
               3 * Mathf.Pow(1 - t, 2) * t * p1 +
               3 * Mathf.Pow(1 - t, 2) * Mathf.Pow(t, 2) * p2 +
               Mathf.Pow(t, 3) * p3;
    }

    public static Vector2 BezierN(this float t, Vector2[] points)
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
}
