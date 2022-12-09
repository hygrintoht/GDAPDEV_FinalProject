using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossBehavior : EnemyBehavior
{
    public enum BossSpecialAttack
    {

    }

    public static BossBehavior instance;
    
    private void InitializeSingleton()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public static BossBehavior GetInstance() { return instance; }

    [SerializeField] float bossHealth = 150;
    [SerializeField] float colorChangeTime = 5.0f;
    [SerializeField] float maxXDistanceFromCenter = 12.5f;
    [SerializeField] float maxYDistanceFromCenter = 7.5f;
    
    float colorChangeTimer = 0f;
    Vector3 target;

    void Awake()
    {
        InitializeSingleton();
        InitEnemyData();
        health = bossHealth;
        colorChangeTimer = colorChangeTime;
        SetEnemyParams();
        target = transform.position;
    }
    public override void Update()
    {
        //changing color of boss
        colorChangeTimer -= Time.deltaTime;
        //Debug.Log(colorChangeTimer);
        if (colorChangeTimer <= 0)
        {
            SetEnemyParams();
            colorChangeTimer = colorChangeTime;
        }
        //health check
        if (health <= 0)
        {
            deathParticles.Play();
            GameData.Instance.UpdateScore(2000);
            Destroy(this);//kasi singleton

            //add death code here
        }
        //boss movement
        if(Vector3.Distance(transform.position, target) < 0.1f)//if boss is getting close to the target
        {
            //new target within bounds
            target = new Vector3(Random.Range(-maxXDistanceFromCenter, maxXDistanceFromCenter), maxYDistanceFromCenter == 0 ? transform.position.y : Random.Range(-maxYDistanceFromCenter, maxYDistanceFromCenter), transform.position.z);
            Debug.Log(target.x + " " + target.y + " " + target.z);
        }
        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);

    }

    public override void InitEnemyData()
    {
        Debug.Log(GameData.Instance.retrieveCurrentData() == null);
        bulletDamage = bulletDamage + (GameData.Instance.retrieveCurrentData()[3] * atkDamage.attackMultiplier);
        Debug.Log(bulletDamage);
    }

}
