using UnityEngine;
using System.Collections;

public class FallingCheck : MonoBehaviour
{
    public bool IsFalling
    {
        get { return isFalling; }
    }

    void Start()
    {
        checkPlaneDistance = (centerPoint.transform.position - checkPlane.transform.position).magnitude;
    }

    void FixedUpdate()
    {
        Vector3 downVector = Physics.gravity.normalized;

        Vector3 startPoint = centerPoint.transform.position;

        RaycastHit[] results = Physics.RaycastAll(startPoint, downVector, checkPlaneDistance * 2f);

        if(showDebug)
            Debug.DrawLine(startPoint, startPoint + (downVector * checkPlaneDistance * 2f), isFalling ? Color.red : Color.green, Time.fixedDeltaTime, false);

        foreach(RaycastHit hit in results)
        {
            if (hit.collider != checkPlane)
                continue;

            isFalling = false;
            return;
        }

        isFalling = true;
    }

    [SerializeField]
    private Transform centerPoint;

    [SerializeField]
    private Collider checkPlane;

    [SerializeField]
    private bool showDebug;

    private float checkPlaneDistance;

    private bool isFalling;
}
