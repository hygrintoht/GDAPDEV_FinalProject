using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Pool;

public class Turret : MonoBehaviour
{
    public enum BulletType { red = 0, green = 1, blue = 2, none = 3}

    [SerializeField] Bullet bulletPrefab;
    [SerializeField] float bulletVelocity = 60.0f;
    [SerializeField] AudioSource audioSource;
    [SerializeField] private ParticleSystem muzzleParticles;

    ObjectPool<Bullet> pool;

    void Start()
    {
        pool = new ObjectPool<Bullet>(
            () => { return Instantiate(bulletPrefab); },
            bullet => { bullet.gameObject.SetActive(true); },
            bullet => { bullet.gameObject.SetActive(false); },
            bullet => { Destroy(bullet.gameObject); },
            false,
            15,
            30
            );
    }

    public void Shoot(BulletType shipBulletType)
    {
        Bullet bullet = pool.Get();
        bullet.SetBulletType(shipBulletType);
        bullet.transform.SetLocalPositionAndRotation(transform.position, transform.rotation);
        bullet.rb.velocity = Vector3.zero;
        bullet.rb.angularVelocity = Vector3.zero;
        bullet.rb.AddForce(transform.forward * bulletVelocity, ForceMode.VelocityChange);
        bullet.Init(KillBullet);
        audioSource.Play();
        muzzleParticles.Play();
    }

    private void KillBullet(Bullet bullet)
    {
        pool.Release(bullet);
    }
}
