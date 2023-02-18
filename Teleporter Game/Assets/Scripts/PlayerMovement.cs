using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 180f;
    public float padding = 0f;

    private Rigidbody2D rb;
    private Camera mainCamera;

    Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        Vector2 newPosition = (Vector2)transform.position + movement * moveSpeed * Time.deltaTime;

        Vector3 lowerLeft = mainCamera.ViewportToWorldPoint(new Vector3(padding, padding, 0f));
        Vector3 upperRight = mainCamera.ViewportToWorldPoint(new Vector3(1f - padding, 1f - padding, 0f));
        float newX = Mathf.Clamp(newPosition.x, lowerLeft.x, upperRight.x);
        float newY = Mathf.Clamp(newPosition.y, lowerLeft.y, upperRight.y);
        transform.position = new Vector3(newX, newY, transform.position.z);

        if (movement != Vector2.zero)
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        }
    }
}
