using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LightScript : MonoBehaviour {

    public Color lightColor = Color.red;

    public float angle = 10;

    private Vector3 points;



    public Material targetMat;
    public Material mat;
	
	// Update is called once per frame
	void Update () {
        points = mat.GetVector("_PixelCoordinates");
        Debug.Log(points);
        targetMat.SetVector("_TargetPoint", points);
	}
}
