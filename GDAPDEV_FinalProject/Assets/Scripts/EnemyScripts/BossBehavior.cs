using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossBehavior : EnemyBehavior
{
    public static BossBehavior instance;

    private void InitializeSingleton()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public static BossBehavior GetInstance() { return instance; }

    [SerializeField] float bossHealth = 150;
    [SerializeField] float colorChangeTime = 5.0f;

    float colorChangeTimer = 0f;

    void Awake()
    {
        InitializeSingleton();
        InitEnemyData();
        health = bossHealth;
        colorChangeTimer = colorChangeTime;
        SetEnemyParams();
    }
    public override void Update()
    {
        colorChangeTimer -= Time.deltaTime;
        Debug.Log(colorChangeTimer);
        if (colorChangeTimer <= 0)
        {
            SetEnemyParams();
            colorChangeTimer = colorChangeTime;
        }

        if (health <= 0)
        {
            deathParticles.Play();
            GameData.Instance.UpdateScore(200);
            Destroy(this);//kasi singleton

            //add death code here
        }
    }

    public override void InitEnemyData()
    {
        Debug.Log(GameData.Instance.retrieveCurrentData() == null);
        bulletDamage = bulletDamage + (GameData.Instance.retrieveCurrentData()[3] * atkDamage.attackMultiplier);
        Debug.Log(bulletDamage);
    }

}
