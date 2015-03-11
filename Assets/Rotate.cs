using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

	// Update is called once per frame
	void Update () 
    {
        Vector3 current = transform.eulerAngles;
        current += rotationRate * Time.deltaTime;
        transform.eulerAngles = current;
	}

    [SerializeField]
    private Vector3 rotationRate;
}
