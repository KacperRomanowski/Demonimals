using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform[] firePoint;
    public GameObject bulletPrefab;
    public AudioSource sound;
    private int firePointIndex = 0;
    
    public float bulletForce = 20f;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint[firePointIndex].position, firePoint[firePointIndex].rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint[firePointIndex].up * bulletForce, ForceMode2D.Impulse);
        firePointIndex ^= 1;
        sound.Play();
    }
}
