using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerCapsule");
        offset = new Vector3(0, 5, -10);
    }

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
