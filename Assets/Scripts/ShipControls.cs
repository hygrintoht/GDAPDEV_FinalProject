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
    [SerializeField] Turret.BulletType currBulletType = Turret.BulletType.red;
    
    float fireCountdown = 0;
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

    void ShootTurrets()//loops through turrets that needs to shoot a bullet type
    {
        foreach (Turret turret in turrets)
        {
            turret.Shoot(currBulletType);
        }
    }

    public void Fire(bool pressed)//changes the ship state if firing
    {
        isFireing = pressed;
    }

    public void ChangeBulletType(bool change)//shifts to another bullet type (0 backward 1 forward)
    {
        int numChange;
        if (change) numChange = (int)currBulletType + 1;
        else numChange = (int)currBulletType - 1;

        if (numChange == -1) numChange = 2;
        if (numChange == 3) numChange = 0;

        if (numChange == 0) currBulletType = Turret.BulletType.red;
        if (numChange == 1) currBulletType = Turret.BulletType.green;
        if (numChange == 2) currBulletType = Turret.BulletType.blue;
        //not confusing at all (i mean i can use unisigned int and modulo but some times it is unreadable)
    }
}
