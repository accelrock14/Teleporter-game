using UnityEngine;

public class DirectCollision : MonoBehaviour
{
    private GameManager gameState;
    public GameObject hitEffect;
    public float destroyTime = 0.7f;

    private void Start()
    {
        gameState = FindObjectOfType<GameManager>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameState.state = GameState.LOST;
            GameObject effect1 = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect1, 1f);
            GameObject effect2 = Instantiate(hitEffect, collision.gameObject.transform.position, Quaternion.identity);
            Destroy(effect2, 1f);
            Destroy(collision.gameObject);
            Destroy(gameObject.transform.parent.gameObject);
            gameState.Restart();
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            GameObject effect1 = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect1, destroyTime);
            GameObject effect2 = Instantiate(hitEffect, collision.gameObject.transform.position, Quaternion.identity);
            Destroy(effect2, destroyTime);
            Destroy(gameObject.transform.parent.gameObject);
            Destroy(collision.gameObject);
        }
        SfxManager.sfxInstance.GetComponent<AudioSource>().PlayOneShot(SfxManager.sfxInstance.explosion);
    }
}
