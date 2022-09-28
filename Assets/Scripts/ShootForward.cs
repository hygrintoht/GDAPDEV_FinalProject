using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootForward : MonoBehaviour
{
    [SerializeField] Rigidbody bullet;
    [SerializeField] float velocity = 80.0f;
    [SerializeField] public float fireRate = 1.0f;

    float fireCountdown = 0;

    void Update()
    {
        if(Input.GetButton("Fire1"))
        {
            if (fireCountdown <= 0)
            {
                Shoot();
                fireCountdown = 1.0f / fireRate;
            }
            fireCountdown -= Time.deltaTime;
        }
    }
    private void Shoot()
    {
        Rigidbody newBullet = Instantiate(bullet, transform.position, transform.rotation) as Rigidbody;
        newBullet.AddForce(transform.forward * velocity, ForceMode.VelocityChange);
    }
}
