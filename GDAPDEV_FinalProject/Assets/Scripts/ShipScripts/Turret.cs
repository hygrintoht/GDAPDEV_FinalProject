using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Pool;

public class Turret : MonoBehaviour
{   
    public enum BulletType { red = 0, green = 1, blue = 2 }

    [SerializeField] Rigidbody bulletR;
    [SerializeField] Rigidbody bulletG;
    [SerializeField] Rigidbody bulletB;
    
    [SerializeField] float bulletVelocity = 80.0f;
    [SerializeField] AudioSource audioSource;

    Rigidbody bullet;

    ObjectPool<Rigidbody> poolR;
    ObjectPool<Rigidbody> poolG;
    ObjectPool<Rigidbody> poolB;

    public void Start()
    {
        
    }

    public void Shoot(BulletType shipBulletType)
    {
        //to be changed for object pooling
        if (shipBulletType == BulletType.red) bullet = bulletR;
        if (shipBulletType == BulletType.green) bullet = bulletG;
        if (shipBulletType == BulletType.blue) bullet = bulletB;

        Rigidbody newBullet = Instantiate(bullet, transform.position, transform.rotation) as Rigidbody;
        newBullet.AddForce(transform.forward * bulletVelocity, ForceMode.VelocityChange);

        audioSource.Play();
    }
}
