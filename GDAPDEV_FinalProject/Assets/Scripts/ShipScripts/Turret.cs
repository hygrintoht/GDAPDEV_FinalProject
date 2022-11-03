using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public enum BulletType { red = 0, green = 1, blue = 2 }

    [SerializeField] Rigidbody bulletR;
    [SerializeField] Rigidbody bulletG;
    [SerializeField] Rigidbody bulletB;

    [SerializeField] float velocity = 80.0f;
    [SerializeField] AudioSource audioSource;

    Rigidbody bullet;

    public void Shoot(BulletType shipBulletType)
    {
        //to be changed for object pooling
        if (shipBulletType == BulletType.red) bullet = bulletR;
        if (shipBulletType == BulletType.green) bullet = bulletG;
        if (shipBulletType == BulletType.blue) bullet = bulletB;

        Rigidbody newBullet = Instantiate(bullet, transform.position, transform.rotation) as Rigidbody;
        newBullet.AddForce(transform.forward * velocity, ForceMode.VelocityChange);

        audioSource.Play();
    }
}
