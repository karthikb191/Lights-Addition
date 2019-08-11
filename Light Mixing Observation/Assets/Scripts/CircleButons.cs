using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleButons : MonoBehaviour {

    //public GameObject buttonPrefab;

    //public int numberOfButtons = 4;

    public float radius = 10;

    private float angle;

    public List<GameObject> buttonPrefabs;

    [HideInInspector]
    public List<GameObject> currentButtons;

	// Use this for initialization
	void Start () {
        currentButtons = new List<GameObject>();

        
        CalculatePoints();
        gameObject.transform.parent.gameObject.SetActive(false);

	}
	
	// Update is called once per frame
	void Update () {

        //If the button is added at the runtime, the change affects immediately
        if (currentButtons.Count != buttonPrefabs.Count) {    

            //First, destroy the current buttons and then add the new ones
            DestroyButtons();
            currentButtons.Clear();

            CalculatePoints();

        }

	}

    void DestroyButtons() {
        foreach (GameObject g in currentButtons) {
            Destroy(g);
        }
    }

    void CalculatePoints() {

        angle = (270/ buttonPrefabs.Count);
        
        currentButtons.Clear();
        for (int i = 1; i <= buttonPrefabs.Count; i++) {

            //float xpos = radius * Mathf.Cos(i * Mathf.Deg2Rad * angle );
            //float ypos = radius * Mathf.Sin(i * Mathf.Deg2Rad * angle );

            float xpos =  radius * Mathf.Cos(Mathf.Deg2Rad *( i * angle ));
            float ypos =  radius * Mathf.Sin(Mathf.Deg2Rad *( i * angle ));

            Debug.Log("angle is: " + i*angle);
            
            Debug.Log("xpos is: " + xpos );
            Debug.Log("ypos is: " + ypos );

            GameObject button = Instantiate(buttonPrefabs[i-1], Vector3.one, Quaternion.Euler(0,0,60 * (i-1)), gameObject.transform);

            button.GetComponent<RectTransform>().localPosition = new Vector3(xpos, ypos, 0);

            currentButtons.Add(button);

        }
    }
}
