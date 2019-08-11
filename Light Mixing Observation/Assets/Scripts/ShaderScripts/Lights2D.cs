using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Lights2D : MonoBehaviour {
    [HideInInspector]
    public Material lightMat;

    public Color lightColor = Color.white;

    public float noFadeDistance = 0.2f;
    public float distance = 0.5f;

    public float angle = 10;
    public float fog = 0;

    public float lightMixtureAdjustmentController = 0.4f;

    [HideInInspector]
    public Vector3 rightVector;
    [HideInInspector]
    public Vector3 leftVector;

    [HideInInspector]
    public Vector3 leftFogVector;
    [HideInInspector]
    public Vector3 rightFogVector;

    [HideInInspector]
    public Vector3 rightEndPoint;
    [HideInInspector]
    public Vector3 leftEndPoint;
    [HideInInspector]
    public Vector3 topEndPoint;

    // Update is called once per frame
    void Update () {
        lightMat = GetComponent<MeshRenderer>().sharedMaterial;


        lightMat.SetColor("_Color", lightColor);
        lightMat.SetVector("_LightPosition", transform.position);
        lightMat.SetVector("_LightDirection", transform.forward);

        //Shader.SetGlobalFloat("_Distance", distance);
        lightMat.SetFloat("_Distance", distance);
        lightMat.SetFloat("_NoFadeDistance", noFadeDistance);

        lightMat.SetFloat("_LightMixtureAdjustmentController", lightMixtureAdjustmentController);

        //set the right vector according to the angle provided

        //First, get the right guy's direction
        //transform.GetChild(0).RotateAround(transform.position, transform.up, rightAngle);

        rightVector = Quaternion.AngleAxis(angle - 90 , transform.up) * transform.forward;
        leftVector = Quaternion.AngleAxis(90 - angle, transform.up) * transform.forward;

        rightFogVector = Quaternion.AngleAxis(angle + fog - 90, transform.up) * transform.forward;
        leftFogVector = Quaternion.AngleAxis(90 - angle + (-fog), transform.up) * transform.forward;



        Vector3 actualRightVector = Quaternion.AngleAxis(angle, transform.up) * transform.forward;
        Vector3 actualLeftVector = Quaternion.AngleAxis(- angle, transform.up) * transform.forward;

        Vector3 actualRightFogvector = Quaternion.AngleAxis( angle + fog, transform.up) * transform.forward;
        Vector3 actualLeftFogVector = Quaternion.AngleAxis( -angle + (-fog), transform.up) * transform.forward;

        Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + actualLeftVector);
        Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + actualRightVector);

        Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + actualLeftFogVector);
        Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + actualRightFogvector);


        float rightVectorAngle = Vector3.Angle( transform.forward, rightFogVector);
        float leftVectorAngle = Vector3.Angle( leftFogVector, transform.forward);
        //Debug.Log("right vector angle is: " + rightVectorAngle);


        //The above vectors are not correct. So, these vectors are used to draw the mesh....good stuff....learn more about this
        Vector3 rf = Quaternion.AngleAxis(angle + fog  , transform.up) * transform.forward;
        Vector3 lf = Quaternion.AngleAxis( -angle + (-fog), transform.up) * transform.forward;

        rightEndPoint = gameObject.transform.position + rf  * distance * 2f;
        leftEndPoint = gameObject.transform.position + lf * distance * 2f;
        topEndPoint = gameObject.transform.position + gameObject.transform.forward * distance * 1.2f;
        //rightEndPoint = rightEndPoint - gameObject.transform.position;
        /*rightEndPoint = new Vector3(gameObject.transform.position.x + Mathf.Pow(distance, 43/rightVectorAngle) * Mathf.Cos(Mathf.Deg2Rad * (rightVectorAngle)),
                                    gameObject.transform.position.y + Mathf.Pow(distance, 43/rightVectorAngle) * Mathf.Sin(Mathf.Deg2Rad * (rightVectorAngle)), 
                                    gameObject.transform.position.z);*/
        //rightEndPoint = new Vector3(gameObject.transform.position.x, 0, 0) + (rightFogVector * distance);

        //rightEndPoint = gameObject.transform.position.normalized + rightFogVector;

        //leftEndPoint = gameObject.transform.position + leftFogVector * distance;
        //leftEndPoint = leftEndPoint - gameObject.transform.position;
        /*leftEndPoint = new Vector3(gameObject.transform.position.x - Mathf.Pow(distance, 43/leftVectorAngle) * Mathf.Cos(Mathf.Deg2Rad * (leftVectorAngle)),
                                    gameObject.transform.position.y + Mathf.Pow(distance, 43/leftVectorAngle) * Mathf.Sin(Mathf.Deg2Rad * (leftVectorAngle)),
                                    gameObject.transform.position.z);*/


        //Debug.DrawLine(gameObject.transform.position, rightEndPoint);
        //Debug.DrawLine(gameObject.transform.position, leftEndPoint);
        //Vector3 vec = Quaternion.Euler(0, 0, rightAngle) * transform.forward;
        //rightVector = transform.GetChild(0).position - transform.position;

        //Debug.Log(rightVector);
        lightMat.SetVector("_RightVector", rightVector);
        lightMat.SetVector("_LeftVector", leftVector);

        lightMat.SetVector("_RightFogVector", rightFogVector);
        lightMat.SetVector("_LeftFogVector", leftFogVector);

        lightMat.SetVector("_ActualRightVector", actualRightVector);
        lightMat.SetVector("_ActualLeftVector", actualLeftVector);
        lightMat.SetVector("_ActualRightFogVector", actualRightFogvector);
        lightMat.SetVector("_ActualLeftFogVector", actualLeftFogVector);



    }

    Vector3 Rotate() {
        return Vector3.zero;
    }
}
