using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

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
            SfxManager.sfxInstance.GetComponent<AudioSource>().PlayOneShot(SfxManager.sfxInstance.laserShoot);
        }
    }
    public void OnPointerDown()
    {
        // Start the coroutine when the button is pressed
        StartCoroutine(nameof(StartShooting));
    }

    public void OnPointerUp()
    {
        // Stop the coroutine when the button is released
        StopCoroutine(nameof(StartShooting));
    }
    private IEnumerator StartShooting()
    {
        while (true)
        {
            Shoot();
            yield return null;
        }
    }
}
