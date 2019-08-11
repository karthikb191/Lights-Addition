using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedBackLink : MonoBehaviour {

    private GameObject feedbackButton;

    private GameObject parentCanvas;
    private Animator finishScreenAnimator;
    

	// Use this for initialization
	void Start () {
        feedbackButton = transform.GetChild(0).gameObject;
        feedbackButton.SetActive(false);

        parentCanvas = GetComponentInParent<Canvas>().gameObject;
        finishScreenAnimator = parentCanvas.GetComponent<Animator>();

        AnimatorStateInfo animatorStateInfo = finishScreenAnimator.GetCurrentAnimatorStateInfo(0);

        Invoke("DisplayFeedBackButton", animatorStateInfo.length);
    }

    void DisplayFeedBackButton() {
        feedbackButton.SetActive(true);
    }

    public void FeedBackURL() {
        Debug.Log("Clicked the URL");
        Application.OpenURL("https://www.google.com/url?q=https://docs.google.com/forms/d/e/1FAIpQLScFNtYWt0WPsFeWMIHGFh8miDL7AujNoJilYSVYfdhRi4XEjg/viewform?usp%3Dsf_link&sa=D&ust=1517639049716000&usg=AFQjCNHUQyBCm4CM9L0pB2UnMyygNLZ6wg");
    }
}
