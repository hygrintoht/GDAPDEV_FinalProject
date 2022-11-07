using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ShipControls : MonoBehaviour
{   

    //ship parameters
    [SerializeField] float moveSpeed = 1.0f;

    float hori = 0;
    float vert = 0;
    Vector3 direction = Vector3.zero;
    Vector3 finalDirection = Vector3.zero;
    Vector3 relPos;

    [SerializeField] int maxHP = 5;

    int HP;

    //turret parameters
    [SerializeField] Turret[] turrets;
    [SerializeField] public float fireRate = 1.0f;
    [SerializeField] Turret.BulletType currBulletType = Turret.BulletType.red;

    float fireCountdown = 0;
    bool isFireing = false;

    //sheild parameters
    [SerializeField] int shieldCount = 1;
    [SerializeField] float shieldDruration = 3.0f;

    float shieldTimer = 0;
    
    //unity events
    void Start()
    {
        HP = maxHP;    
    }

    void Update()
    {
        //movment update(to be changed for the tutorial)
        hori = Input.GetAxis("Horizontal");
        vert = Input.GetAxis("Vertical");

        direction = new Vector3(hori, vert, 0);
        finalDirection = new Vector3(hori, vert, 2.0f);

        transform.position += direction * moveSpeed * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(finalDirection), Mathf.Deg2Rad*50.0f);

        relPos = Camera.main.WorldToViewportPoint(transform.position);
        relPos.x = Mathf.Clamp01(relPos.x);
        relPos.y = Mathf.Clamp01(relPos.y);
        transform.position = Camera.main.ViewportToWorldPoint(relPos);

        //shooting update(finished)
        if (/*Input.GetButton("Fire1") ||*/ isFireing)
        {
            if (fireCountdown <= 0)
            {
                ShootTurrets();
                fireCountdown = 1.0f / fireRate;
            }
            fireCountdown -= Time.deltaTime;
        }

        //shield timer update
        if (shieldTimer > 0)
        {
            shieldTimer -= Time.deltaTime;
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if (shieldTimer <= 0)//if sheild timer is not counting down
        {
            HP--;
        }
        if (HP <= 0)//if hp is lower than 1
        {
            ShipDeath();
        }
    }

    void ShootTurrets()//loops through turrets that needs to shoot a bullet type
    {
        foreach (Turret turret in turrets)
        {
            turret.Shoot(currBulletType);
        }
    }

    void ShipDeath()
    {

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

    public void DodgeRoll(bool direction)//0 left 1 right (dodge roll(no iframes because of shield))
    {

    }

    public void ActivateShield()//more of invincibility rather than shields
    {
        if (shieldCount > 0)
        {
            shieldCount--;
            shieldTimer = shieldDruration;
        }
    }

    public void Bomb()//i think im going to scrap this unless i know how to implement this to the enemy manager
    {

    }
}
