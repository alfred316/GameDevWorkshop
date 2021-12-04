using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerSphereManager : MonoBehaviour
{
    public float speed;
    private TMP_Text countText;
    private TMP_Text winText;
    private Rigidbody rb;
    private int count;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        countText = GameObject.FindGameObjectWithTag("CountText").GetComponent<TMP_Text>();
        winText = GameObject.FindGameObjectWithTag("WinText").GetComponent<TMP_Text>();
        count = 0;
        SetCountText();
        winText.text = "";
    }

    private void OnEnable()
    {
        GameObject playerCapsule = GameObject.FindGameObjectWithTag("PlayerCapsule");
        playerCapsule.GetComponent<PlayerCapsuleManager>().enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            count += 1;
            SetCountText();
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 10)
        {
            winText.text = "You win!";
        }
    }
}
