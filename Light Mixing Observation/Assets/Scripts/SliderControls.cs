using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderControls : MonoBehaviour {
    private GameObject angle;
    private GameObject fog;
    private GameObject distance;
    private GameObject opacity;

    private Lights2D lightPoint;

    public static bool ignore;
    public static float opa;
	// Use this for initialization
	void Start () {
        

        angle = gameObject.transform.Find("Angle").gameObject;
        fog = gameObject.transform.Find("Fog").gameObject;
        distance = gameObject.transform.Find("Distance").gameObject;
        opacity = gameObject.transform.Find("Opacity").gameObject;
        lightPoint = gameObject.transform.GetComponentInParent<Lights2D>();

        SetLightParameters();

        Debug.Log("slider controls script is working");
    }

    /*private void OnEnable() {
        angle = gameObject.transform.Find("Angle").gameObject;
        fog = gameObject.transform.Find("Fog").gameObject;
        distance = gameObject.transform.Find("Distance").gameObject;

        lightPoint = gameObject.transform.GetComponentInParent<Lights2D>();

        SetLightParameters();

        Debug.Log("slider controls script is working");
    }*/

    // Update is called once per frame
    void Update () {
        //Debug.Log("slider controls script is working");
        SetLightParameters();
	}

    void SetLightParameters() {
        lightPoint.angle = angle.GetComponent<UnityEngine.UI.Slider>().value;
        lightPoint.fog = fog.GetComponent<UnityEngine.UI.Slider>().value;
        lightPoint.distance = distance.GetComponent<UnityEngine.UI.Slider>().value;
        opa = opacity.GetComponent<UnityEngine.UI.Slider>().value;
        if(lightPoint.lightColor.r > 0 && !ignore)
        {
            lightPoint.lightColor.r = opa;
        }
        if (lightPoint.lightColor.g > 0 && !ignore)
        {
            lightPoint.lightColor.g = opa;
        }
        if (lightPoint.lightColor.b > 0 && !ignore)
        {
            lightPoint.lightColor.b = opa;
        }

        lightPoint.noFadeDistance = lightPoint.distance - 30;

        //Debug.Log("light point angle is: " + lightPoint.angle);
    }
}
