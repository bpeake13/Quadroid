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
		//getOptimalPosition ();
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


	void getOptimalPosition()
	{
		Vector3 downVector = Physics.gravity.normalized;
		Vector3 pointLeft = new Vector3(), pointRight = new Vector3();
		bool leftOnGround = false, rightOnGround = false;

		RaycastHit[] results = Physics.RaycastAll(footLeft.transform.position, downVector, 1f);

		foreach (RaycastHit hit in results) {
			if (hit.collider == checkPlane)
			{	
				leftOnGround = true;
				pointLeft = hit.point;
			}
		}

		results = Physics.RaycastAll(footRight.transform.position, downVector, 1f);
		
		foreach (RaycastHit hit in results) {
			if (hit.collider == checkPlane)
			{	
				rightOnGround = true;
				pointRight = hit.point;
			}
		}

		if (leftOnGround && !rightOnGround) {
			pointRight = pointLeft;
			optimalCenterPoint = Vector3.Lerp (pointLeft, pointRight, 0.5f);
		} else if (!leftOnGround && rightOnGround) {
			pointLeft = pointRight;
			optimalCenterPoint = Vector3.Lerp (pointLeft, pointRight, 0.5f);
		}

			optimalCenterPoint = Vector3.Lerp (pointLeft, pointRight, 0.5f);
			optimalCenterPoint.y = body.position.y - transform.position.y;

	}



    [SerializeField]
    private Transform body;

	[SerializeField]
	private Transform footLeft, footRight;

	[SerializeField]
	private Collider checkPlane;

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
