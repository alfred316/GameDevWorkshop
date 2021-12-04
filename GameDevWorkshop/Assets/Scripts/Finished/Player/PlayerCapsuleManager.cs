using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerCapsuleManager : MonoBehaviour
{
    [Range(1, 10)]
    public float speed = 5;

    [Range(50, 80)]
    public float turnSpeed = 70;

    public GameObject leverText;

    public GameObject capsuleUI;
    public GameObject sphereUI;

    public bool nearLever;
    private Transform lever;
    // Start is called before the first frame update
    void Start()
    {
        nearLever = false;
        capsuleUI.SetActive(true);
        sphereUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if(Input.GetKeyUp(KeyCode.E) && nearLever)
        {
            if(lever != null)
            {
                lever.Rotate(new Vector3(30.0f, 0.0f, 0.0f));
                Invoke("ReleasePlatform", 1);
                
            }
        }
    }

    void Move()
    {
        float forwardMovement = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float turnMovement = Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime;

        transform.Translate(Vector3.forward * forwardMovement);
        transform.Rotate(Vector3.up * turnMovement);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("enterred trigger for: " + other.name);
        if(other.name.Contains("Lever"))
        {
            nearLever = true;
            lever = other.transform;
            if(!leverText.activeInHierarchy)
            {
                leverText.SetActive(true);
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("exitted trigger for: " + other.name);
        if (other.name.Contains("Lever"))
        {
            nearLever = false;
            if (leverText.activeInHierarchy)
            {
                leverText.SetActive(false);
            }
        }
    }

    void ReleasePlatform()
    {
        Debug.Log("Releasing platform");

        //remove the platform under the sphere
        GameObject platform = GameObject.FindGameObjectWithTag("FloorDestroy");

        platform.SetActive(false);

        //change the camera to look at the sphere
        GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");

        GameObject spherePlayer = GameObject.FindGameObjectWithTag("PlayerSphere");

        cam.GetComponent<CameraController>().player = spherePlayer;

        //enable the sphere's script
        spherePlayer.GetComponent<PlayerSphereManager>().enabled = true;
        //cam.GetComponent<CameraController>().offset = new Vector3();

        //enable sphere UI and disable capsule UI

        capsuleUI.SetActive(false);
        sphereUI.SetActive(true);
    }
}
