using UnityEngine;
using System.Collections;

public class ContactMesh : MonoBehaviour {

	public Vector3[] newVertices;
	public Vector2[] newUV;
	public int[] newTriangles;

	[SerializeField]
	private GameObject legLeft, legRight;
	[SerializeField]
	private GameObject center;

	private Vector3 topLeft, topRight, bottomLeft, bottomRight;
	[SerializeField]
	private GameObject leftFront, leftBack, rightFront, rightBack;
	private Vector3 offsetY = new Vector3 (0f, 0.01f, 0f);
	private Vector3 offsetZ = new Vector3 (0f, 0f, 0.5f);

	void Start() {
		Mesh mesh = new Mesh();
		GetComponent<MeshFilter>().mesh = mesh;

		newVertices = new Vector3[]{new Vector3(-0.5f,0,0.5f),new Vector3(0.5f,0,0.5f),new Vector3(0.5f,0,-0.5f),new Vector3(-0.5f,0,-0.5f),
									new Vector3(-0.5f,0.1f,0.5f),new Vector3(0.5f,0.1f,0.5f),new Vector3(0.5f,0.1f,-0.5f),new Vector3(-0.5f,0.1f,-0.5f)};
		mesh.vertices = newVertices;

		newTriangles = new int[] {0,1,2,0,2,3};
		mesh.triangles = newTriangles;

		GetComponent<MeshCollider> ().sharedMesh = mesh;
		getFeetExtremes ();

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


		newVertices = new Vector3[]{topLeft, topRight, bottomLeft, bottomRight,
									  topLeft+offsetY, topRight+offsetY, bottomLeft+offsetY, bottomRight+offsetY};
		mesh.vertices = newVertices;

		newTriangles = new int[] {0,1,2,0,2,3};
		mesh.triangles = newTriangles;

		GetComponent<MeshCollider> ().sharedMesh = mesh;

		
	}

	void getFeetExtremes()
	{

	}

}
