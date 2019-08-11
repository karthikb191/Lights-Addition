using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class Buttongen : MonoBehaviour {

	public GameObject prefabButton;
	public RectTransform ParentPanel;
	private int sc_number;


    void Awake() {
        sc_number = SceneManager.sceneCountInBuildSettings;
        Debug.Log("the item is " + sc_number);
    }

	// Use this for initialization
	void Start () {

        //The scene unlocker function that runs every time a new scene is loaded
        SceneManager.sceneLoaded += SceneUnlocker;

        //If the player prefs has no key that is called LevelCounter, create it
        if (!PlayerPrefs.HasKey("LevelCounter")) {
            PlayerPrefs.SetInt("LevelCounter", 1);
            PlayerPrefs.Save();
            //first level is unlocked, every other level is locked
            CreateButtons(PlayerPrefs.GetInt("LevelCounter"));
        }
        //If it has the key, then instantiate the buttons
        else {

            CreateButtons(PlayerPrefs.GetInt("LevelCounter"));
            
        }

	}

    void SceneUnlocker(Scene scene, LoadSceneMode load) {
        Debug.Log("The scene unlocker is working.....Yaaayyyyyyy");
        if (scene.buildIndex > PlayerPrefs.GetInt("LevelCounter")) {
            PlayerPrefs.SetInt("LevelCounter", PlayerPrefs.GetInt("LevelCounter") + 1);
        }

    }

    public void CreateButtons(int num) {
        for (int i = 0; i < sc_number; i++) {
            if (i < num) {
                //These buttons are interactable and are assigned with a scene function each
                GameObject goButton = (GameObject)Instantiate(prefabButton);
                goButton.transform.SetParent(ParentPanel, false);
                goButton.transform.localScale = new Vector3(1, 1, 1);


                Button tempButton = goButton.GetComponent<Button>();
                tempButton.interactable = true;

                Debug.Log("Value of i is: " + i);
                int n = i + 1;
                goButton.GetComponent<Button>().onClick.AddListener(() => { ButtonClicked(n); });
                goButton.GetComponent<Button>().transform.GetChild(0).GetComponent<Text>().text = n.ToString();
            }
            else {
                //These buttons are not interactable
                GameObject goButton = (GameObject)Instantiate(prefabButton);
                goButton.transform.SetParent(ParentPanel, false);
                goButton.transform.localScale = new Vector3(1, 1, 1);


                Button tempButton = goButton.GetComponent<Button>();
                tempButton.interactable = false;
                tempButton.transform.GetChild(0).GetComponent<Text>().text = (i+1).ToString();

            }
        }
    }


	public void ButtonClicked(int index)
	{
        Debug.Log("button number is: " + index);
        GameManager.Instance.GoToLevel(index);
	}

}
