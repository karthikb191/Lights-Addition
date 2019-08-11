using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    private static GameManager instance;
    public static GameManager Instance {
        get {
            return instance;
        }
        
    }

    public bool isPausePanelAcive;
    

    public GameObject finishScreen;

    public GameObject globalScreen;
    [HideInInspector]
    public GameObject globalCanvas;

    public GameObject FadeCanvas;
    [HideInInspector]
    public GameObject finishCanvas; //use this instead of finish screen
    

    public const int maxLights = 3;

    public float speed = 2;

    void Awake() {

        Debug.Log("Instantiated.....well, lets count");

        GameManager[] objs = FindObjectsOfType<GameManager>();
        foreach (GameManager gm in objs) {
            
            if (gm.gameObject != this.gameObject) {
                Debug.Log("destroying");
                Destroy(this.gameObject);
            }

        }

        
        

    }

    private void OnSceneLoad(Scene scene, LoadSceneMode mode) {
        Debug.Log( "scene is:" + scene);
        Debug.Log("mode is: " + mode);

        Debug.Log("inside the scene mode");
        instance = this;
        //instance = this;
        //setting the canvas here makes sure that this is called whenever the screen is loaded


        /*finishCanvas = Instantiate(finishScreen);

            finishCanvas.transform.position = new Vector3(finishScreen.transform.position.x,
                                                       finishScreen.transform.position.y + 0.5f,
                                                       finishScreen.transform.position.z);*/


        
            //adding the button functions in runtime
            if (SceneManager.GetActiveScene().buildIndex > 0 && SceneManager.GetActiveScene().name != "FinishScreen") { 
            globalCanvas = Instantiate(globalScreen);

            globalCanvas.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(GlobalCanvasAvtivate);
            globalCanvas.transform.GetChild(1).gameObject.SetActive(false);
            globalCanvas.transform.GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener(GlobalCanvasDeactivate);
            globalCanvas.transform.GetChild(1).GetChild(1).GetComponent<Button>().onClick.AddListener(RestartLevel);
            globalCanvas.transform.GetChild(1).GetChild(2).GetComponent<Button>().onClick.AddListener(GoToMainMenu);
        }

    }

    // Use this for initialization
    void Start () {
        Debug.Log("Game manager started");

        //Assigning this here so that the awake and scene loaded function can execute without giving problems
        DontDestroyOnLoad(this.gameObject);

        SceneManager.sceneLoaded += OnSceneLoad;



        Debug.Log("inside the scene mode");
        instance = this;
        //instance = this;
        //setting the canvas here makes sure that this is called whenever the screen is loaded


        ///This is just for a simple level finish
        /*finishCanvas = Instantiate(finishScreen);

        finishCanvas.transform.position = new Vector3(finishScreen.transform.position.x,
                                                   finishScreen.transform.position.y + 0.5f,
                                                   finishScreen.transform.position.z);*/


        //adding the button functions in runtime
        if (SceneManager.GetActiveScene().buildIndex > 0 && SceneManager.GetActiveScene().name != "FinishScreen") { 
            globalCanvas = Instantiate(globalScreen);

        globalCanvas.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(GlobalCanvasAvtivate);

        globalCanvas.transform.GetChild(1).gameObject.SetActive(false);
        globalCanvas.transform.GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener(GlobalCanvasDeactivate);
        globalCanvas.transform.GetChild(1).GetChild(1).GetComponent<Button>().onClick.AddListener(RestartLevel);
        globalCanvas.transform.GetChild(1).GetChild(2).GetComponent<Button>().onClick.AddListener(GoToMainMenu);

    }
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void IncreaseLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        isPausePanelAcive = false;
    }

    public void GoToLevel(int index)
    {
        if (FadeCanvas == null)
            SceneManager.LoadScene(index);
        //If the fade canvas exists, play the animation and then increase the level
        else
        {
            //StartCoroutine(GoToLevelWithFade(index));
        }
    }
    public void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        isPausePanelAcive = false;
    }

    public void GoToMainMenu() {
        SceneManager.LoadScene(0);
        isPausePanelAcive = false;
    }

    public void GlobalCanvasAvtivate() {
        globalCanvas.transform.GetChild(1).gameObject.SetActive(true);
        isPausePanelAcive = true;
    }

    public void GlobalCanvasDeactivate() {
        globalCanvas.transform.GetChild(1).gameObject.SetActive(false);
        isPausePanelAcive = false;
    }

}
