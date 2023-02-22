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
    }
    public void Shoot()
    {
        if (lastFired >= fireRate)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            lastFired = 0f;
        }
    }
}
