using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManipulator : MonoBehaviour {

    public bool iAmBeingHeld;

    public LightManager lm;

    private bool rotating;
	// Use this for initialization
	void Start () {
        lm = FindObjectOfType<LightManager>();
	}
	
	// Update is called once per frame
	void Update () {
        

        if (iAmBeingHeld)
            CaptureLight();

        //if the light is selcted and the canvas is not active
        if (LevelManager.selectedLight != null) {

            if (LevelManager.selectedLight == this.gameObject) {
                StartCoroutine(PanelToggle(1));
            }

            else {
                StartCoroutine(PanelToggle(0));
            }

        }

        if (LevelManager.selectedLight == null) {

            if (gameObject.transform.GetChild(0).gameObject.activeSelf)
                StartCoroutine(PanelToggle(0));

        }

        //Rotation logic for lights
        if (Input.GetMouseButton(0)) {
            Vector3 posOnScreen = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            float distance = Vector3.Distance(new Vector3(Input.mousePosition.x, Input.mousePosition.y, lm.zpos),
                                                new Vector3(posOnScreen.x, posOnScreen.y, lm.zpos));

            if (LevelManager.selectedLight != null) {
                if (LevelManager.selectedLight == this.gameObject) {
                    Debug.Log("Radius is: " + transform.GetComponentInChildren<CircleButons>().radius);
                    if((distance > transform.GetComponentInChildren<CircleButons>().radius && 
                            distance < transform.GetComponentInChildren<CircleButons>().radius + 100f) || rotating)
                        RotationLogic();
                }
            }
        }

        //If the rotating variable is set to true, then reset it to false when the mouse button is released
        if (Input.GetMouseButtonUp(0)) {
            if (rotating)
                rotating = false;
        }

        //If the light is currently under selection and delete button is pressed, the light must be deleted
        if (LevelManager.selectedLight == this.gameObject) {
            if (Input.GetKeyDown(KeyCode.Delete)) {
                lm.DeleteLight(this.gameObject);
            }
        }

	}

    void CaptureLight() {

        Vector3 mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 mousePos = new Vector3(mp.x,
                                        mp.y, lm.zpos);

        

        Vector3 lightPos = new Vector3(gameObject.transform.position.x,
                                       gameObject.transform.position.y,
                                        lm.zpos);


        Vector3 direction = (mousePos - lightPos).normalized;

        float distance = Vector3.Distance(mousePos, lightPos);
        if(distance > 3f) {
            Vector3 position = gameObject.transform.position;
            position += direction * GameManager.Instance.speed/2 * Time.deltaTime;

            position.x = Mathf.Clamp(position.x, -60, 60);
            position.y = Mathf.Clamp(position.y, -30, 30);

            gameObject.transform.position = position;
        }

    }

    IEnumerator PanelToggle(int p) {

        if (p == 1) {
            transform.GetChild(0).gameObject.SetActive(true);
            Animator anim = transform.GetChild(0).GetChild(0).GetComponent<Animator>();
            yield break;
        }

        if (p == 0) {
            Animator anim = transform.GetChild(0).GetChild(0).GetComponent<Animator>();
            AnimatorStateInfo s = anim.GetCurrentAnimatorStateInfo(0);
            anim.SetBool("Activated", false);

            yield return new WaitForSeconds(s.length);

            transform.GetChild(0).gameObject.SetActive(false);

            yield break;
        }
    }

    void RotationLogic() {
        rotating = true;
        Vector3 lookAtVector = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        transform.LookAt(new Vector3(lookAtVector.x, lookAtVector.y, transform.position.z), transform.up);

    }
}
