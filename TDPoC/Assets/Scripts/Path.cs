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
        //Debug.Log(t);
        t *= Size;
        var index = Mathf.FloorToInt(t);
        t -= index;
        //Debug.Log($"t: {t}, index: {index}");
        return t.BezierN(routes[index].ControlPointPositions);
    }

    public Vector2 GetRotation(float t)
    {
        t *= Size;
        var index = Mathf.FloorToInt(t);
        t -= index;
        return t.DerivativeBezierN(routes[index].ControlPointPositions);
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
