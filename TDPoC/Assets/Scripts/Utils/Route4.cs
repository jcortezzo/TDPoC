using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Route4 : MonoBehaviour
{
    [field:SerializeField]
    public Transform[] ControlPoints { get; private set; }
    public Vector2[] ControlPointPositions
    {
        get
        {
            if (ControlPoints == null || ControlPoints.Length == 0)
            {
                return new Vector2[0];
            }
            return ControlPoints.Select(p => new Vector2(p.position.x, p.position.y)).ToArray();
        }
    }

    [SerializeField]
    private int numVisibleSegments = 20;

    [SerializeField]
    private float sphereSize = 0.25f;

    private void OnDrawGizmos()
    {
        for (float t = 0; t <= 1; t += 1f / numVisibleSegments)
        {
            Vector2 gizmosPosition = BloonsMath.BezierN(t, ControlPointPositions);

            Gizmos.DrawSphere(gizmosPosition, sphereSize);
        }

        Gizmos.DrawLine(new Vector2(ControlPoints[0].position.x, ControlPoints[0].position.y),
                        new Vector2(ControlPoints[1].position.x, ControlPoints[1].position.y));

        Gizmos.DrawLine(new Vector2(ControlPoints[2].position.x, ControlPoints[2].position.y),
                        new Vector2(ControlPoints[3].position.x, ControlPoints[3].position.y));
    }
}
