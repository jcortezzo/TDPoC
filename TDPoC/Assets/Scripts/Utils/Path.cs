using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField]
    private Route4[] routes;

    public int Size { get { return routes.Length; } }

    public Vector2 GetPosition(float t)
    {
        return GetFromBezierCurve(t, BloonsMath.BezierN);
    }

    public Vector2 GetRotation(float t)
    {
        return GetFromBezierCurve(t, BloonsMath.DerivativeBezierN);
    }

    private Vector2 GetFromBezierCurve(float t, Func<float, Vector2[], Vector2> f)
    {
        t = t.BoundDistance();
        t *= Size;
        var index = Mathf.FloorToInt(t);
        t -= index;
        return f(t, routes[index].ControlPointPositions);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
