using UnityEngine;

public class ParallaxScroll : MonoBehaviour
{
    public float scrollSpeed = 0.1f;
    private Transform playerTransform;
    private GameManager gameState;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        gameState = FindObjectOfType<GameManager>();
    }

    void FixedUpdate()
    {
        if (gameState.state != GameState.LOST)
        {
            float xPos = playerTransform.position.x * (1 - scrollSpeed);
            float yPos = playerTransform.position.y * (1 - scrollSpeed);
            float xOffset = (transform.position.x - xPos) * scrollSpeed;
            float yOffset = (transform.position.y - yPos) * scrollSpeed;
            transform.position = new Vector3(xPos + xOffset, yPos + yOffset, transform.position.z);
        }
    }
}
