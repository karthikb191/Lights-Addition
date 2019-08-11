using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleLight : MonoBehaviour {

    Material m;

    public float noFadeDistance = 0.2f;
    public float distance = 0.5f;

    // Use this for initialization
    void Start () {
        m = GetComponent<MeshRenderer>().material;

        m.SetFloat("_Distance", distance);
        m.SetFloat("_NoFadeDistance", noFadeDistance);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
