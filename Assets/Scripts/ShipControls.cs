using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControls : MonoBehaviour
{   
    //movment parameters
    [SerializeField] float moveSpeed = 1.0f;

    float hori = 0;
    float vert = 0;
    Vector3 direction = Vector3.zero;
    Vector3 finalDirection = Vector3.zero;

    //turret parameters
    [SerializeField] Turret[] turrets;
    [SerializeField] public float fireRate = 1.0f;

    enum BulletType {red, green, blue}
    float fireCountdown = 0;
    BulletType bulletType = BulletType.red;
    bool isFireing = false;


    void Update()
    {
        //movment update(to be changed for the tutorial)
        hori = Input.GetAxis("Horizontal");
        vert = Input.GetAxis("Vertical");

        direction = new Vector3(hori, vert, 0);
        finalDirection = new Vector3(hori, vert, 2.0f);

        transform.position += direction * moveSpeed * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(finalDirection), Mathf.Deg2Rad*50.0f);

        //shooting update(finished)
        if (Input.GetButton("Fire1") || isFireing)
        {
            if (fireCountdown <= 0)
            {
                ShootTurrets();
                fireCountdown = 1.0f / fireRate;
            }
            fireCountdown -= Time.deltaTime;
        }
        
    }

    void ShootTurrets()
    {
        foreach (Turret turret in turrets)
        {
            turret.Shoot();
        }
    }

    public void Fire(bool pressed)
    {
        isFireing = pressed;
    }

    public void ChangeBulletType(bool change)//0 backward 1 forward
    {
        
    }
}
