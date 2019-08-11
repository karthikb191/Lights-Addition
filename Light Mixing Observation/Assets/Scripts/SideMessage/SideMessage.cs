using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideMessage : MonoBehaviour {

    public float timeToDisplayHint = 1.5f;

    Transform buttonCanvas;

    Animator messageAnimatorHolder;

    // Use this for initialization
    void Start () {
        buttonCanvas = gameObject.transform.parent;

        messageAnimatorHolder = buttonCanvas.GetChild(1).GetComponent<Animator>();

        gameObject.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(ActivateHint);

        //start the coroutine once when the level starts
        StartCoroutine(HintActivated());

	}

    void ActivateHint() {
        StartCoroutine(HintActivated());
    }

    IEnumerator HintActivated() {

        messageAnimatorHolder.SetBool("Activated", true);

        yield return new WaitForSeconds( messageAnimatorHolder.GetCurrentAnimatorStateInfo(0).length + timeToDisplayHint );

        messageAnimatorHolder.SetBool("Activated", false);

        yield break;
    }
	
}
