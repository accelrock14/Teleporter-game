using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float smoothTime = 0.3f;

    private Vector3 velocity = Vector3.zero;
    private GameManager gameState;

    // Start is called before the first frame update
    void Start()
    {
        gameState = FindObjectOfType<GameManager>();
    }
    void FixedUpdate()
    {
        if (gameState.state != GameState.LOST)
        {
            // Set the target position to follow the player on the x and y axes
            Vector3 targetPosition = new Vector3(player.position.x, player.position.y, transform.position.z);

            // Smoothly move the camera towards the target position
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }
}
