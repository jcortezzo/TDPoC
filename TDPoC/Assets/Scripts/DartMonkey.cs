using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartMonkey : MonoBehaviour
{
    [field: SerializeField]
    public float ViewDistance { get; private set; }

    public FieldOfView FieldOfView { get; private set; }

    private bool cd;  // cooldown

    // Start is called before the first frame update
    void Start()
    {
        FieldOfView = GetComponentInChildren<FieldOfView>();

        cd = false;
    }

    // Update is called once per frame
    void Update()
    {
        Bloon target = FieldOfView?.GetTarget(TargetTypes.FIRST);
        if (target != null && !cd)
        {
            StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {
        cd = true;
        Debug.Log("Shoot!");
        yield return new WaitForSeconds(2f);
        cd = false;
    }

    private void OnValidate()
    {
        if (FieldOfView != null) FieldOfView.ViewDistance = ViewDistance;
    }
}
