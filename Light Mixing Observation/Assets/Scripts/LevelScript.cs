using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelScript : MonoBehaviour {

    public GameObject fadePanel;    //Instantiate the fade panel and assign that to this variable in the inspector

    public GameObject objectiveLightsPanel;
    public Image[] targetColors;

    [HideInInspector]
    public List<Image> objectiveColors; //use this instead of targetcolors

    void Awake() {
        objectiveColors = new List<Image>();
        StartCoroutine(FadeOut());
    }

	// Use this for initialization
	void Start () {

        for (int i = 0; i < targetColors.Length; i++) {
           objectiveColors.Add(Instantiate(targetColors[i], objectiveLightsPanel.transform));
        }
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator FadeOut() {

        fadePanel.GetComponent<Animator>().SetBool("FadeIn", false);
        AnimatorStateInfo ass = fadePanel.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(ass.length);
        fadePanel.SetActive(false);

        yield break;
    }
}
