using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject playerRb;
    private GameManager state;

    public float bulletForce = 20f;
    private float destoryDistance = 20f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerRb = GameObject.FindWithTag("Player");
        rb.AddForce(transform.up * bulletForce, ForceMode2D.Impulse);
        state = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerRb != null && Vector2.Distance(transform.position, playerRb.transform.position) > destoryDistance)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            state.state = GameState.LOST;
            Destroy(collision.gameObject);
            state.Restart();
        }
        else if(collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }
        Destroy(gameObject);
    }
}
