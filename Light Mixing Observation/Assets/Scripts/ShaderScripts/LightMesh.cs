using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LightMesh : MonoBehaviour {

    [HideInInspector]
    public MeshFilter meshFilter;
    [HideInInspector]
    public MeshRenderer meshRenderer;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();

        Lights2D light = GetComponent<Lights2D>();

        //Mesh Generation
        Vector3[] vertices = new Vector3[4];
        Vector3[] normals = new Vector3[4];
        Vector2[] uvs = new Vector2[4];
        int[] tris = new int[6];


        Mesh mesh = new Mesh();

        Vector3 zero = gameObject.transform.position;
        vertices[0] = transform.InverseTransformPoint(zero);
        normals[0] = gameObject.transform.up;

        //Vector3 leftDir = transform.InverseTransformDirection( light.leftFogVector);
        Vector3 one = transform.InverseTransformPoint(light.leftEndPoint);
        vertices[1] = one;
        normals[1] = gameObject.transform.up;

        //Vector3 rightDir = transform.InverseTransformDirection(light.rightFogVector);
        Vector3 two = transform.InverseTransformPoint(light.rightEndPoint);
        vertices[2] = two;
        normals[2] = gameObject.transform.up;

        Vector3 three = transform.InverseTransformPoint(light.topEndPoint);
        vertices[3] = three;
        normals[3] = gameObject.transform.up;

        uvs[0] = new Vector2(0, 1);
        uvs[1] = new Vector2(1, 0);
        uvs[2] = new Vector2(1, 1);
        uvs[3] = new Vector2(0, 0);

        tris = new int[] { 0, 1, 2, 1, 3 ,2 };

        mesh.Clear();

        mesh.vertices = vertices;
        mesh.normals = normals;
        mesh.uv = uvs;
        mesh.triangles = tris;

        
        //mesh.RecalculateNormals();

        meshFilter.mesh = mesh;
        meshRenderer.material = light.lightMat;
    }
}
