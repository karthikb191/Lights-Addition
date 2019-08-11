using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour {

    List<GameObject> lights;

    //The distance is measured in the screen space(not the world coordinates)
    public float distanceForCapture = 10;

    public float zpos = 0;

    public GameObject lightPrefab;
    public Material lightMaterial;

    private bool caughtNothing;

    //The button for deleting the light.....For mobile games
    public GameObject deleteButton;

    public LevelManager levelManager;

    

	// Use this for initialization
	void Start () {
        levelManager = FindObjectOfType<LevelManager>();
        lights = new List<GameObject>();
        GameObject[] l;

        lights.Clear();

        l = GameObject.FindGameObjectsWithTag("tag_light");

        foreach (GameObject g in l) {
            lights.Add(g);
        }

        caughtNothing = true;

        Debug.Log("total lights in the scene are: " + lights.Count);

        Debug.Log("Light position is: " + lights[0].transform.position);
        Debug.Log("Light position in screen space is: " + Camera.main.WorldToScreenPoint(lights[0].transform.position));
	}
	
	// Update is called once per frame
	void Update () {
        float rangeDistance;

        if (Input.GetMouseButtonDown(0)) {
            float distance;

            Debug.Log("Mouse position in the screen space is: " + Input.mousePosition);
            Debug.Log("mouse position in world is: " + Camera.main.ScreenToWorldPoint(Input.mousePosition));

            for (int i = 0; i < lights.Count; i++) {

                Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, zpos);

                Vector3 lightPos = new Vector3(Camera.main.WorldToScreenPoint(lights[i].transform.position).x,
                                                Camera.main.WorldToScreenPoint(lights[i].transform.position).y,
                                                zpos);


                Debug.Log("Mouse pos is: " + mousePos);
                Debug.Log("Light pos is: " + lightPos);


                distance = Vector3.Distance(mousePos, lightPos);

                Debug.Log("Distance between light and the mouse pointer is: " + distance);

                if (distance < distanceForCapture) {
                    lights[i].GetComponent<LightManipulator>().iAmBeingHeld = true;
                    LevelManager.selected = true;
                    LevelManager.selectedLight = lights[i];
                    caughtNothing = false;
                    break;
                    //If a light is caught, then assign the proper flags and break the loop
                }
                //If a light is not caught, either spawn the light if no light is active or deactivate the light if anything is selected
                else {
                    if (i == lights.Count - 1) {
                        //check the distance to the options so that there is no error for selecting the options
                        //The distance check is only done if the selected light is not null and no other lights are selected
                        if (LevelManager.selectedLight != null) {
                            Vector3 lightpos = Camera.main.WorldToScreenPoint(LevelManager.selectedLight.transform.position);
                            rangeDistance = Vector3.Distance(mousePos, new Vector3(lightpos.x,
                                                                                lightpos.y,
                                                                                zpos));
                            if(rangeDistance > LevelManager.selectedLight.transform.GetChild(0).GetChild(0).GetComponent<CircleButons>().radius+100)
                                caughtNothing = true;
                        }
                        else {
                            caughtNothing = true;
                        }
                    }
                }

                //rotation logic
            }

            //Check if nothing is selected and spawn the light if nothing is selected
            if (!LevelManager.selected) {
                Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                //Set the minimum and maximum limits for the spawning of the lights
                if(worldMousePos.x >= -60 && worldMousePos.x <=60 &&
                    worldMousePos.y >= -30 && worldMousePos.y <= 30 && !GameManager.Instance.isPausePanelAcive) {

                    SpawnLight();
                }
            }


            if (caughtNothing) {
                LevelManager.selected = false;
                LevelManager.selectedLight = null;
            }

        }

        if (Input.GetMouseButtonUp(0)) {
            if (LevelManager.selected) {
                LevelManager.selectedLight.GetComponent<LightManipulator>().iAmBeingHeld = false;

                //LevelManager.selected = false;
                //LevelManager.selectedLight = null;
            }
        }

	}

    void SpawnLight() {
        if (lights.Count < GameManager.maxLights) {
            Debug.Log("Inside the lights spawner");

            Vector3 pos = new Vector3 (Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                                        Camera.main.ScreenToWorldPoint(Input.mousePosition).y,
                                        zpos);
            GameObject light = Instantiate(lightPrefab, pos, Quaternion.Euler(0, 90, -90));
            Material mat = new Material(lightMaterial);                    //Creating a new material from the prefab
            light.GetComponent<MeshRenderer>().material = mat;          //Assigning the material to the light
            lights.Add(light);

            //Add the button for the obtained colors
            for (int i = 0; i < LevelManager.obtainedColors.Count; i++) {
                for (int j = 0; j < levelManager.lightButtonsForLevel.Length; j++) {
                    bool check = true;
                    if (LevelManager.obtainedColors[i] == levelManager.lightButtonsForLevel[j].GetComponent<ButtonScript>().myNameIs) {

                        for (int k = 0; k < light.GetComponentInChildren<CircleButons>().buttonPrefabs.Count; k++) {
                            if (LevelManager.obtainedColors[i] == light.GetComponentInChildren<CircleButons>().buttonPrefabs[k].GetComponent<ButtonScript>().myNameIs)
                                check = false;
                        }

                        if(check)
                            light.GetComponentInChildren<CircleButons>().buttonPrefabs.Add( Instantiate(levelManager.lightButtonsForLevel[j]));
                    }

                }
            }

        }
    }

    public void DeleteLight(GameObject light) {
        lights.Remove(light);

        GameObject.Destroy(light);

        LevelManager.selected = false;
        LevelManager.selectedLight = null;
    }

    public void DeleteButtonPressed() {
        Debug.Log("Inside the delete buton press script");
        if (LevelManager.previousSelectedLight != null) {
            Debug.Log("deleting the light");
            DeleteLight(LevelManager.previousSelectedLight);
        }

    }

}
