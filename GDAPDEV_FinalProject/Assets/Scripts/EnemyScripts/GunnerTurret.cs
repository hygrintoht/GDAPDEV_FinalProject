using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnerTurret : MonoBehaviour
{
    [SerializeField] Turret turret;
    [SerializeField] GameObject player;

    [SerializeField] float barrageDuration = 3.0f;
    [SerializeField] float restDuration = 3.0f;
    [SerializeField] float attackSpeed = 2.0f;

    float barrageTimer;
    float restTimer;
    float attackTimer;

    void Start()
    {
        barrageTimer = barrageDuration;
        restTimer = restDuration;
        attackTimer = 1 / attackSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        barrageTimer -= Time.deltaTime;
        attackTimer -= Time.deltaTime;

        if(barrageTimer <= 0)
        {
            if (attackTimer <= 0)
            {
                transform.LookAt(player.transform);
                turret.Shoot(Turret.BulletType.none);
                attackTimer = 1 / attackSpeed;
            }
        }
        else
        {
            restTimer -= Time.deltaTime;
            
            if(restTimer <= 0)
            {
                barrageTimer = barrageDuration;
                attackTimer = 1 / attackSpeed;
                restTimer = restDuration;
            }
        }
    }
}
