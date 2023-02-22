using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 180f;
    public float padding = 0f;

    public Transform circle;
    public Transform outerCircle;

    float horizontalInput;
    public float recoil = 0.1f;

    private Rigidbody2D rb;
    private Camera mainCamera;
    private Joystick joystick;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
        joystick = FindObjectOfType<Joystick>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        // Rotate the player based on the input
        transform.Rotate(Vector3.forward * -joystick.Horizontal * rotateSpeed * Time.deltaTime);

        // Apply a force in the direction of rotation
        rb.velocity = transform.up * moveSpeed * Time.deltaTime;

        if (joystick.Horizontal != 0)
        {
            rb.AddForce(transform.right * recoil * -Mathf.Sign(joystick.Horizontal), ForceMode2D.Impulse);
        }
        //horizontalInput = Input.GetAxis("Horizontal");
    }
    /*
    private void FixedUpdate()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        // Rotate the player based on the input
        transform.Rotate(Vector3.forward * -horizontalInput * rotateSpeed * Time.deltaTime);

        // Apply a force in the direction of rotation
        rb.velocity = transform.up * moveSpeed * Time.deltaTime;
        
        if(horizontalInput != 0)
        {
            rb.AddForce(transform.right * recoil * -Mathf.Sign(horizontalInput), ForceMode2D.Impulse);
        }   
    }
    */
}
