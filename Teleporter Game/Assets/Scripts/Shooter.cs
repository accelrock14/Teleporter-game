using UnityEngine;

public class Shooter : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    public float fireRate = 1f;
    private float lastFired = 0f;

    // Update is called once per frame
    void Update()
    {
        lastFired += Time.deltaTime;
        if (Input.GetButtonDown("Jump") && lastFired >= fireRate)
        {
            Shoot();
            lastFired = 0f;
        }
    }
    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
