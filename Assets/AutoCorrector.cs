using UnityEngine;
using System.Collections;

[RequireComponent(typeof(FallingCheck))]
public class AutoCorrector : MonoBehaviour
{

    void Start()
    {
        fallingCheck = GetComponent<FallingCheck>();
        optimalCenterPoint = body.position - transform.position;
        rigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (fallingCheck.IsFalling)
            return;

        Vector3 currentPointLocal = body.position - transform.position;

        Vector3 returnVector = (currentPointLocal - optimalCenterPoint);

        float forceValue = forceCurve.Evaluate(returnVector.magnitude / maxDistance) * forceMultiplier;
        Vector3 force = returnVector.normalized * -forceValue;
        force.y = 0;

        rigidBody.AddForceAtPosition(force, body.position);

        if (showDebug)
        {
            Debug.Log(forceValue);
            Debug.DrawLine(body.position, body.position + force, Color.magenta);
        }
    }


    [SerializeField]
    private Transform body;

    [SerializeField]
    private AnimationCurve forceCurve;

    [SerializeField]
    private float forceMultiplier = 0.1f;

    [SerializeField]
    private bool showDebug;

    private Rigidbody rigidBody;

    private float maxDistance = 2f;

    private FallingCheck fallingCheck;

    private Vector3 optimalCenterPoint;
}
