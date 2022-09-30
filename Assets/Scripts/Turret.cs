using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] Rigidbody bullet;
    [SerializeField] float velocity = 80.0f;
    [SerializeField] AudioSource audioSource;
    public void Shoot()
    {
        //to be changed for object pooling
        Rigidbody newBullet = Instantiate(bullet, transform.position, transform.rotation) as Rigidbody;
        newBullet.AddForce(transform.forward * velocity, ForceMode.VelocityChange);

        audioSource.Play();
    }
}
