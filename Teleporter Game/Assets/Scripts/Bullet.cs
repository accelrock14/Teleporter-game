using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;

    public float bulletForce = 20f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * bulletForce, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x > 4 || transform.position.x < -4 || transform.position.y >6 || transform.position.y < -6)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        Destroy(collision.gameObject);
        Destroy(gameObject);
    }
}
