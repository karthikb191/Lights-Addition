using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour {

    public GameObject animatedParticlePrefab;   //Assign this in the button

    private GameObject ps;

    public GameObject fadePanel;

    public bool startPressed;

	// Use this for initialization
	void Start () {
        //GameManager gm = FindObjectOfType<GameManager>() as GameManager;
        gameObject.GetComponent<Button>().onClick.AddListener(ButtonPress);

        ps = GameObject.FindGameObjectWithTag("tag_particle");

        fadePanel.SetActive(false);
	}

    void ButtonPress() {
        StartCoroutine(IncreaseLevel());
    }

    IEnumerator IncreaseLevel() {
        //ps.GetComponent<ParticleSystem>().emissionRate = 0;

        if (!startPressed) {
            startPressed = true;
            GameObject ps1 = Instantiate(animatedParticlePrefab, Vector3.zero, Quaternion.identity);

            AnimatorStateInfo ass = ps1.transform.GetChild(0).GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);

            yield return new WaitForSeconds(ass.length*5);
            fadePanel.SetActive(true);
            fadePanel.GetComponent<Animator>().SetBool("FadeIn", true);
            yield return new WaitForSeconds( fadePanel.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length+1.5f);

            GameManager.Instance.IncreaseLevel();
            yield break;
        }
    }

}
