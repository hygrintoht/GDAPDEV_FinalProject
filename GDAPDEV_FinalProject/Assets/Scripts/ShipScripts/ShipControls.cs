using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipControls : MonoBehaviour
{
    //input parameters
    [SerializeField]PlayerInput playerInput;
    Vector2 input;
    float gravityValue = 2.0f;

    //ship parameters
    [SerializeField] float moveSpeed = 1.0f;

    float hori = 0;
    float vert = 0;
    Vector3 direction = Vector3.zero;
    Vector3 finalDirection = Vector3.zero;
    Vector3 relPos;

    [SerializeField] int maxHP = 5;

    [SerializeField]int HP;

    //turret parameters
    [SerializeField] Turret[] turrets;
    [SerializeField] public float fireRate = 10.0f;
    [SerializeField] Turret.BulletType currBulletType = Turret.BulletType.red;

    float fireCountdown = 0;
    bool isFireing = false;

    //sheild parameters
    [SerializeField] int shieldCount = 1;
    [SerializeField] float shieldDuration = 3.0f;

    float shieldTimer = 0;

    [SerializeField] private ParticleSystem deathExplotion;

    //unity events
    void Start()
    {
        HP = maxHP;    
    }

    void Update()
    {
        input = playerInput.actions["Move"].ReadValue<Vector2>() * gravityValue;
        //movment update(to be changed for the tutorial)
        hori = input.x;//Input.GetAxis("Horizontal");
        vert = input.y;//Input.GetAxis("Vertical");

        direction = new Vector3(hori, vert, 0);
        finalDirection = new Vector3(hori, vert, 2.0f);

        transform.position += direction * moveSpeed * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(finalDirection), Mathf.Deg2Rad*100.0f);

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
        deathExplotion.Play();
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
        Vector3 target;
        float rollDistance = 2.5f;
        if (direction) target = gameObject.transform.position + (Vector3.right * rollDistance);
        else target = gameObject.transform.position + (Vector3.left * rollDistance);

        StartCoroutine(DodgeRolling(gameObject.transform.position, target));
    }

    IEnumerator DodgeRolling(Vector3 start, Vector3 target)
    {
        float rollTimer = 0;
        float rollDuration = .35f;
        while(rollTimer < rollDuration)
        {
            gameObject.transform.position = Vector3.Lerp(start, target, rollTimer/rollDuration);
            rollTimer += Time.deltaTime;
            yield return null;
        }

    }

    public void ActivateShield()//more of invincibility rather than shields
    {
        if (shieldCount > 0)
        {
            shieldCount--;
            shieldTimer = shieldDuration;
        }
    }

    public void Bomb()//i think im going to scrap this unless i know how to implement this to the enemy manager
    {

    }
}
