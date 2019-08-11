using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    public static bool selected;

    public static GameObject selectedLight;

    public static GameObject previousSelectedLight;

    LightDetector lightDetector;
    LevelScript levelScript;

    [HideInInspector]
    public static List<string> obtainedColors;

    public GameObject nextLevelCanvas;

    public static bool absorbing = false;

    public GameObject[] lightButtonsForLevel;

    // Use this for initialization
    void Start () {
        lightDetector = FindObjectOfType<LightDetector>();
        levelScript = GetComponent<LevelScript>();

        obtainedColors = new List<string>();

        if (nextLevelCanvas != null) {

            nextLevelCanvas.SetActive(false);

        }

	}

    Coroutine co;
	
	// Update is called once per frame
	void Update () {
        if (selectedLight != null) {
            previousSelectedLight = selectedLight;
        }

        if (obtainedColors.Count == levelScript.objectiveColors.Count) {

            //activate the panel and add the function
            nextLevelCanvas.SetActive(true);
            nextLevelCanvas.transform.GetChild(0).GetComponent<UnityEngine.UI.Button>().onClick.AddListener(GameManager.Instance.IncreaseLevel);

        }

        if (co != null && !absorbing) {
            Debug.Log("Destroying the coroutine");
            //stop the coroutine and set everything to false
            StopCoroutine(co);
            co = null;

            //lightDetector.particleSystems[0].gameObject.SetActive(false);
            lightDetector.particleSystems[1].gameObject.SetActive(false);
            lightDetector.particleSystems[2].gameObject.SetActive(false);

            
        }

        //if the lightdetector is absorbing and the color doesnt match
        LightCheck();

	}


    public void AbsorbLight() {
        bool check = false;

        for (int i = 0; i < levelScript.objectiveColors.Count; i++) {

            if (levelScript.objectiveColors[i].GetComponent<ImageName>().name == lightDetector.color) {
                if (levelScript.objectiveColors[i].GetComponent<ImageName>().colorAchieved) {
                    Debug.Log("Color achieved, so Breaking off and preventing the starting of coroutine");
                    
                    absorbing = false;
                    check = true;
                    break;
                }
            }
            
            
        }

        if (!absorbing && !check) {
            Debug.Log("color of detector is: " + lightDetector.color);
            //Debug.Log("Color of objective color is: " + levelScript.objectiveColors[i].GetComponent<ImageName>().name);

            absorbing = true;

            LightCheck();

            co = StartCoroutine(DetectLight());

        }

        

    }

    void LightCheck() {
        if (absorbing) {
            
            for (int i = 0; i < levelScript.objectiveColors.Count; i++){
                if (lightDetector.color == levelScript.objectiveColors[i].GetComponent<ImageName>().name) {

                    if (!levelScript.objectiveColors[i].GetComponent<ImageName>().colorAchieved) {
                        Debug.Log("breaking off from here");
                        break;
                    }
                    //If the color matches and the light is already achieved as an objective.
                    else {
                        absorbing = false;
                        //lightDetector.particleSystems[0].gameObject.SetActive(false);
                        lightDetector.particleSystems[1].gameObject.SetActive(false);
                        lightDetector.particleSystems[2].gameObject.SetActive(false);

                        if (co != null) {
                            
                            Debug.Log("destroooooooooooooooooyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy");
                            
                            //stop the coroutine and set everything to false
                            StopCoroutine(co);
                            co = null;
            
                        }


                    }
                }

                else
                    if (i == levelScript.objectiveColors.Count-1) {
                        absorbing = false;
                        //lightDetector.particleSystems[0].gameObject.SetActive(false);
                        lightDetector.particleSystems[1].gameObject.SetActive(false);
                        lightDetector.particleSystems[2].gameObject.SetActive(false);

                        if (co != null) {
                            
                            Debug.Log("destroooooooooooooooooyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy");
                            
                            //stop the coroutine and set everything to false
                            StopCoroutine(co);
                            co = null;
            
                        }
                    }
            }
        }
    }

    IEnumerator DetectLight() {
        if (absorbing) {
            yield return new WaitForSeconds(lightDetector.absorbTime/1.5f);

            //lightDetector.particleSystems[0].gameObject.SetActive(false);
            lightDetector.particleSystems[1].gameObject.SetActive(true);
            lightDetector.particleSystems[1].startColor = lightDetector.lightColor;
            lightDetector.particleSystems[1].playbackSpeed = 1.5f;     //Change the speed here
            float timeToWait = (lightDetector.particleSystems[1].main.duration + lightDetector.particleSystems[1].main.startLifetime.constant) / lightDetector.particleSystems[1].playbackSpeed;

            yield return new WaitForSeconds(timeToWait / 1.5f);

            //lightDetector.particleSystems[1].gameObject.SetActive(false);
            lightDetector.particleSystems[2].gameObject.SetActive(true);
            lightDetector.particleSystems[2].startColor = lightDetector.lightColor;
            lightDetector.particleSystems[2].playbackSpeed = 3;     //Change the speed here
            timeToWait = lightDetector.particleSystems[2].main.duration + lightDetector.particleSystems[2].main.startLifetime.constant / lightDetector.particleSystems[2].playbackSpeed;

            yield return new WaitForSeconds(timeToWait);

            //After waiting for a few seconds, start the change in the color
            for (int i = 0; i < levelScript.objectiveColors.Count; i++) {

                if (levelScript.objectiveColors[i].GetComponent<ImageName>().name == lightDetector.color) {
                    if (!obtainedColors.Contains(lightDetector.color)) {

                        CheckLightButtons(lightDetector.color);

                        levelScript.objectiveColors[i].GetComponent<ImageName>().colorAchieved = true;
                        obtainedColors.Add(lightDetector.color);
                        levelScript.objectiveColors[i].GetComponent<Image>().color = Color.gray;
                        Debug.LogWarning("changing color");

                        break;
                    }
                }
            }

            //set the absorbing to false again
            absorbing = false;

        }
        else {
            //Debug.Log("Exiting the coroutine");
            yield break;
        }
    }


    void CheckLightButtons(string colorToCheck) {

        for (int i = 0; i < lightDetector.lightsInScene.Count; i++) {
            Debug.Log("step 1");
            //The number of buttons on the lightpoint
            int num = lightDetector.lightsInScene[i].GetComponentInChildren<CircleButons>(true).currentButtons.Count;
            //Loop through each button and check if there's a color that matches
            for (int j = 0; j < num; j++) {
                Debug.Log("step 2");
                if (colorToCheck == lightDetector.lightsInScene[i].GetComponentInChildren<CircleButons>(true).currentButtons[j].GetComponent<ButtonScript>().myNameIs) {
                    //If the color is matched, break the loop
                    Debug.Log("color is matched, break the loop");
                    break;
                }
                
                else {
                    
                    if (j == num - 1) {
                        Debug.Log("step 3");
                        //now, this is the logic to add the button
                        for (int k = 0; k < lightButtonsForLevel.Length; k++) {
                            //loop through all the buttons for the current level and if the button is found, add it to the lightpoint
                            if (lightButtonsForLevel[k].GetComponent<ButtonScript>().myNameIs == colorToCheck) {
                                //adding the button to the light point
                                //lightDetector.lightsInScene[i].GetComponentInChildren<CircleButons>(true).buttonPrefabs.Add(lightButtonsForLevel[k]);
                                Debug.Log("Button added");
                                break;
                            }
                        }
                    }
                }
            }

        }

    }

}
