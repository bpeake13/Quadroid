using UnityEngine;
using System.Collections;

public class ContactMesh : MonoBehaviour {

	public Vector3[] newVertices;
	public Vector2[] newUV;
	public int[] newTriangles;

	[SerializeField]
	private GameObject legLeft, legRight;

	private Vector3 topLeft, topRight, bottomLeft, bottomRight;
	private GameObject leftFront, leftBack, rightFront, rightBack;
	private Vector3 offsetY = new Vector3 (0f, 0.01f, 0f);



	void Start() {
		Mesh mesh = new Mesh();
		GetComponent<MeshFilter>().mesh = mesh;

		newVertices = new Vector3[]{new Vector3(-0.5f,0,0.5f),new Vector3(0.5f,0,0.5f),new Vector3(0.5f,0,-0.5f),new Vector3(-0.5f,0,-0.5f),
									new Vector3(-0.5f,0.1f,0.5f),new Vector3(0.5f,0.1f,0.5f),new Vector3(0.5f,0.1f,-0.5f),new Vector3(-0.5f,0.1f,-0.5f)};
		mesh.vertices = newVertices;

		newTriangles = new int[] {0,1,2,0,2,3};
		mesh.triangles = newTriangles;

		GetComponent<MeshCollider> ().sharedMesh = mesh;

		leftBack = new GameObject();
		leftBack.transform.position = legLeft.transform.position + new Vector3 (legLeft.GetComponent<Collider>().bounds.extents.x, 0f, legLeft.GetComponent<Collider>().bounds.extents.z);
		leftBack.transform.SetParent (legLeft.transform);

		rightBack = new GameObject();
		rightBack.transform.position = legRight.transform.position + new Vector3 (-legRight.GetComponent<Collider>().bounds.extents.x, 0f, legRight.GetComponent<Collider>().bounds.extents.z);
		rightBack.transform.SetParent (legRight.transform);

		leftFront = new GameObject();
		leftFront.transform.position = legLeft.transform.position + new Vector3 (legLeft.GetComponent<Collider>().bounds.extents.x, 0f, -legLeft.GetComponent<Collider>().bounds.extents.z);
		leftFront.transform.SetParent (legLeft.transform);

		rightFront = new GameObject();
		rightFront.transform.position = legRight.transform.position + new Vector3 (-legRight.GetComponent<Collider>().bounds.extents.x, 0f, -legRight.GetComponent<Collider>().bounds.extents.z);
		rightFront.transform.SetParent (legRight.transform);


		//getFeetExtremes ();

	}
	
	// Update is called once per frame
	void Update () {
			
		Mesh mesh = new Mesh ();
		GetComponent<MeshFilter>().mesh = mesh;
		mesh.Clear();

	
		topLeft = transform.InverseTransformVector(leftFront.transform.position - new Vector3(0f, leftFront.transform.position.y, 0f)); 
		bottomLeft = transform.InverseTransformVector(leftBack.transform.position - new Vector3(0f, leftBack.transform.position.y, 0f));
		topRight = transform.InverseTransformVector(rightFront.transform.position - new Vector3(0f, rightFront.transform.position.y, 0f));
		bottomRight = transform.InverseTransformVector(rightBack.transform.position - new Vector3(0f, rightBack.transform.position.y, 0f));


		//getFeetExtremes();

		newVertices = new Vector3[]{topLeft, topRight, bottomLeft, bottomRight,
									  topLeft+offsetY, topRight+offsetY, bottomLeft+offsetY, bottomRight+offsetY};
		mesh.vertices = newVertices;

		newTriangles = new int[] {0,1,2,0,2,3};
		mesh.triangles = newTriangles;

		GetComponent<MeshCollider> ().sharedMesh = mesh;

		
	}

	void getFeetExtremes()
	{
		topLeft = legLeft.transform.position + new Vector3 (0f, -legLeft.transform.position.y, legLeft.GetComponent<Collider>().bounds.extents.z);
		Debug.Log (topLeft.ToString ());
		bottomLeft = legLeft.transform.position + new Vector3 (0f, -legLeft.transform.position.y, -legLeft.GetComponent<Collider>().bounds.extents.z);

		topRight = legRight.transform.position + new Vector3 (0f, -legRight.transform.position.y, legRight.GetComponent<Collider>().bounds.extents.z);
		bottomRight= legRight.transform.position + new Vector3 (0f, -legRight.transform.position.y, -legLeft.GetComponent<Collider>().bounds.extents.z);

		topLeft = transform.InverseTransformVector (topLeft);
		bottomLeft = transform.InverseTransformVector (bottomLeft);
		
		topRight = transform.InverseTransformVector (topRight);
		bottomRight = transform.InverseTransformVector (bottomRight);
	}

}
