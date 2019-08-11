using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHint : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
     public void btnpressed()
    {
         if(gameObject.activeSelf == true)
        {
            gameObject.SetActive(false);
        }
         else
        {
            gameObject.SetActive(true);
        }
    }
}
