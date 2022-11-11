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
        SetEnemyParams();
        health = bossHealth;
        colorChangeTimer = colorChangeTime;
    }
    public override void Update()
    {
        colorChangeTimer -= Time.deltaTime;

        if (colorChangeTimer <= 0)
        {
            SetEnemyParams();
            colorChangeTimer = colorChangeTime;
        }

        if (health <= 0)
        {
            deathParticles.Play();
            StartCoroutine(Death());
            Destroy(this);
        }
    }

    public override void InitEnemyData()
    {
        List<int> currData = new List<int>();
        currData = GameData.Instance.retrieveCurrentData();
        Debug.Log(currData == null);
        //Bullet Damage Increased
        bulletDamage = bulletDamage + (currData[3] * atkDamage.attackMultiplier);
    }

}
