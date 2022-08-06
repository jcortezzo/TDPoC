using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    private float fov = 360f;
    public float ViewDistance { get; set; }
    private Vector3 origin = Vector3.zero;

    private HashSet<Bloon> possibleTargets;
    private Dictionary<TargetTypes, Bloon> targets = new Dictionary<TargetTypes, Bloon>()
    {
        {TargetTypes.FIRST, null },
        {TargetTypes.LAST, null },
        {TargetTypes.STRONG, null },
        {TargetTypes.CLOSE, null },
    };
    private Bloon First { get { return targets[TargetTypes.FIRST]; } set { targets[TargetTypes.FIRST] = value; } }
    private Bloon Last { get { return targets[TargetTypes.LAST]; } set { targets[TargetTypes.LAST] = value; } }
    private Bloon Strongest { get { return targets[TargetTypes.STRONG]; } set { targets[TargetTypes.STRONG] = value; } }

    // TODO: Add Closest 

    private CircleCollider2D cc;
    [SerializeField] 
    private LayerMask layerMask;
    [SerializeField] 
    private LayerMask targetLayerMask;
    private MeshRenderer mr;
    private Mesh mesh;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CircleCollider2D>();

        mr = GetComponent<MeshRenderer>();
        mr.material.SetColor("_TintColor", Color.yellow);  // doesn't work
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        ViewDistance = GetComponentInParent<ExampleTower>().ViewDistance;
        cc.radius = ViewDistance;

        possibleTargets = new HashSet<Bloon>();
    }

    // Update is called once per frame
    void Update()
    {
        Render();
    }

    public Bloon GetTarget(TargetTypes targetTypes)
    {
        // TODO: Switch to Dictionary.Get but Error handle for bad input (maybe?)
        return targetTypes switch
        {
            TargetTypes.FIRST => First,
            TargetTypes.LAST => Last,
            TargetTypes.STRONG => Strongest,
            TargetTypes.CLOSE => null,  // TODO: return close;
            _ => null,
        };
    }

    private void Render()
    {
        //Vector3 origin = Vector3.zero;
        int rayCount = 180;
        float angle = 0f;
        float angleIncrease = fov / rayCount;
        Collider2D targetCollider = null;
        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;
            Physics2D.queriesHitTriggers = false;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.TransformPoint(origin), GetVectorFromAngle(angle), ViewDistance, layerMask);//, QueryTriggerInteraction.Ignore);
            RaycastHit2D raycastTarget = Physics2D.Raycast(transform.TransformPoint(origin), GetVectorFromAngle(angle), ViewDistance);

            if (raycastHit2D.collider == null)
            {
                vertex = origin + GetVectorFromAngle(angle) * ViewDistance;
            }
            else
            {
                vertex = transform.InverseTransformPoint(raycastHit2D.point);

            }
            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }
            vertexIndex++;

            angle -= angleIncrease;
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.RecalculateBounds();
        //target = targetCollider != null ? targetCollider.gameObject : null;
    }

    private static Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    private static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Bloon b = collision.GetComponent<Bloon>();
        if (b != null)
        {
            possibleTargets.Add(b);
            if (First == null || b.Distance > First.Distance)
            {
                //Debug.Log($"Reassigned first to {b}");
                First = b;
            }
            if (Last == null || b.Distance < Last.Distance)
            {
                Last = b;
            }
            // TODO: Add strongest and closest
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Bloon b = collision.GetComponent<Bloon>();
        if (b != null)
        {
            if (possibleTargets.Contains(b))
            {
                possibleTargets.Remove(b);

                // remove from targets if b is in targets map
                Dictionary<TargetTypes, Bloon> newTargets = new Dictionary<TargetTypes, Bloon>();
                foreach (var pairs in targets)
                {
                    if (b != pairs.Value)
                    {
                        newTargets.Add(pairs.Key, pairs.Value);
                    } else
                    {
                        newTargets.Add(pairs.Key, null);
                    }
                }
                targets = newTargets;
            }
        }
    }

    private void OnValidate()
    {
        if (cc != null) cc.radius = ViewDistance;
    }
}
