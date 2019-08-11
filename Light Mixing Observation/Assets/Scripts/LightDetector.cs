using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDetector : MonoBehaviour
{

    [HideInInspector]
    public List<GameObject> lightsInScene;

    public LayerMask whatIsTheDetector;

    public ParticleSystem[] particleSystems;    //Assign in the inspector

    [HideInInspector]
    public Color32 lightColor;


    public string color;

    public float absorbTime = 5;

    private LevelManager levelManager;

    private List<GameObject> lightsInContact;

    // Use this for initialization
    void Start()
    {
        lightsInScene = new List<GameObject>();
        lightsInContact = new List<GameObject>();

        levelManager = FindObjectOfType<LevelManager>();

        for (int i = 0; i < particleSystems.Length; i++)
        {
            particleSystems[i].gameObject.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {

        //Get the lights in the scene and store them in a list;
        GameObject[] lights;
        lights = GameObject.FindGameObjectsWithTag("tag_light");

        lightsInScene.Clear();
        foreach (GameObject g in lights)
        {
            lightsInScene.Add(g);
        }

        //make each light emmit a ray
        lightColor = Color.black;
        for (int i = 0; i < lightsInScene.Count; i++)
        {
            float distance = lightsInScene[i].GetComponent<Lights2D>().distance;
            float noFadeDistance = lightsInScene[i].GetComponent<Lights2D>().noFadeDistance;

            //Remove this later. This is just for the purpose of testing
            gameObject.GetComponent<SpriteRenderer>().color = new Color32(lightColor.r, lightColor.g, lightColor.b, 255);

            RaycastHit2D[] hit;
            hit = Physics2D.RaycastAll(lightsInScene[i].transform.position, lightsInScene[i].transform.forward, (Mathf.Clamp(noFadeDistance, 0, Mathf.Infinity)), whatIsTheDetector);

            for (int j = 0; j < hit.Length; j++)
            {

                if (j == 1 && hit[j].collider.gameObject == this.gameObject)
                {

                    lightColor += hit[0].collider.GetComponent<Lights2D>().lightColor;

                    //The light detector is immediately named so this name can be used in the level manager's script
                    NamingColor();

                    //If the current light is not inside the lightsincontact array, add it
                    if (!lightsInContact.Contains(lightsInScene[i]))
                    {
                        lightsInContact.Add(lightsInScene[i]);
                    }

                    //Debug.Log("Collider hitting is: " + hit[j].collider);
                    //Debug.Log("Color of light: " + i + " is " + lightColor);

                    //Debug.Log("The light is striking the detector");

                    //Remove this later. This is just for the purpose of testing. Replace the holder with some sprite and add some particles to look attractive
                    gameObject.GetComponent<SpriteRenderer>().color = new Color32(lightColor.r, lightColor.g, lightColor.b, 255);

                    //Assign the particle color and play the first particle
                    Color32 c = new Color32(lightColor.r, lightColor.g, lightColor.b, 255);
                    particleSystems[0].startColor = c;

                    if (!particleSystems[0].gameObject.activeSelf)
                        particleSystems[0].gameObject.SetActive(true);

                    //If the levelmanager's absorbing variable is set to false, execute the function
                    if (LevelManager.absorbing == false)
                    {
                        Debug.Log("absorbing light");
                        levelManager.AbsorbLight();
                    }

                }
                else if (j == 1 || hit.Length == 1)
                {
                    if (lightsInContact.Count == 0)
                    {
                        //Debug.Log("Light is not striking the detector");
                        particleSystems[0].gameObject.SetActive(false);

                        //set absorbing of the level manager to false
                        LevelManager.absorbing = false;
                    }

                    if (lightsInContact.Contains(lightsInScene[i]))
                    {
                        lightsInContact.Remove(lightsInScene[i]);
                    }

                }
            }

            //Debug.DrawLine(lightsInScene[i].transform.position, lightsInScene[i].transform.position + lightsInScene[i].transform.forward*(Mathf.Clamp(noFadeDistance, 0, Mathf.Infinity)));
            Debug.DrawRay(lightsInScene[i].transform.position, lightsInScene[i].transform.forward * (Mathf.Clamp(noFadeDistance, 0, Mathf.Infinity)));
        }

        //check if the light color is matching with one of the goal colors.... Give it a little vaiance

        NamingColor();
    }

    void NamingColor()
    {
        if (lightColor.r == 255 && lightColor.g == 255 && lightColor.b == 255)
        {
            color = "White";
            return;
        }
        if (lightColor.r == 0 && lightColor.g == 0 && lightColor.b == 255)
        {
            color = "Blue";
            return;
        }
        if (lightColor.r == 255 && lightColor.g == 0 && lightColor.b == 0)
        {
            color = "Red";
            return;
        }
        if (lightColor.r == 0 && lightColor.g == 255 && lightColor.b == 0)
        {
            color = "Green";
            return;
        }
        if (lightColor.r == 255 && lightColor.g == 255 && lightColor.b == 0)
        {
            color = "Yellow";
            return;
        }
        if (lightColor.r == 0 && lightColor.g == 255 && lightColor.b == 255)
        {
            color = "Cyan";
            return;
        }
        if (lightColor.r == 255 && lightColor.g == 0 && lightColor.b == 255)
        {
            color = "Magenta";
            return;
        }
        if (lightColor.r == 255 && lightColor.g <= 165 && lightColor.b == 0 && lightColor.g > 5)
        {
            color = "Orange";
            return;
        }
        if (lightColor.r == 255 && lightColor.g <= 165 && lightColor.b <= 165 && lightColor.b > 5 && lightColor.g > 5)
        {
            color = "Pink";
            return;
        }
        //Debug.Log(lightColor);
    }
}


