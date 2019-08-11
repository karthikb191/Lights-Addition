using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour {

    public string myNameIs;         //set this in the inspector

    public Color color;

    public int colorChangeSpeed = 5;

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(ButtonPress);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ButtonPress() {
        Debug.Log("Changing the Color");

        StartCoroutine(TransitionToColor());

    }
    IEnumerator TransitionToColor() {
        Lights2D lights2d = gameObject.transform.GetComponentInParent<Lights2D>();
        Color lightColor = lights2d.lightColor;
        
        Color32 buttonColor = color;

        Vector3 destinationColor = new Vector3(lightColor.r*255*SliderControls.opa, lightColor.g * 255 * SliderControls.opa, lightColor.b * 255 * SliderControls.opa);
        Vector3 sourceColor = new Vector3(buttonColor.r * SliderControls.opa, buttonColor.g * SliderControls.opa, buttonColor.b * SliderControls.opa);

        Debug.Log("Destination color: " + destinationColor);
        Debug.Log("Source Color: " + sourceColor);

        SliderControls.ignore = true;

        float distance = Vector3.Distance(sourceColor, destinationColor);
        while (distance > 10) {
            Debug.Log("Inside the while loop");

            Vector3 v = Vector3.Lerp(destinationColor, sourceColor, colorChangeSpeed * Time.deltaTime);
            Debug.Log("v is: " + v);

            //Assign the light to the light source
            lights2d.lightColor = new Color(v.x/255, v.y/255, v.z/255);

            //set the proper vector values
            destinationColor = v;
            distance = Vector3.Distance(sourceColor, destinationColor);

            yield return new WaitForFixedUpdate();
        }

        //finally, setting the color to the exact value
        lights2d.lightColor = new Color(color.r * SliderControls.opa, color.g * SliderControls.opa, color.b * SliderControls.opa);
        SliderControls.ignore = false;
        yield break;
    }
}
