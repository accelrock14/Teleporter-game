using UnityEngine;

public class DirectCollision : MonoBehaviour
{
    private GameManager gameState;

    private void Start()
    {
        gameState = FindObjectOfType<GameManager>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameState.state = GameState.LOST;
            Destroy(collision.gameObject);
            Destroy(gameObject.transform.parent.gameObject);
            gameState.Restart();
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject.transform.parent.gameObject);
            Destroy(collision.gameObject);
        }
    }
}
