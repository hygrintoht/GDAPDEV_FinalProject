using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    enum EnemyColor { red = 0, blue = 1, green = 2 };

    [SerializeField] float health = 30.0f;
    [SerializeField] EnemyColor enemyColor = EnemyColor.red;
    [SerializeField] float bulletDamage = 10.0f;

    void Start()
    {
        
    }

    void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 7 && enemyColor == EnemyColor.red)
        {
            health -= bulletDamage * 3;
        }
        else if(collision.gameObject.layer == 8 && enemyColor == EnemyColor.green)
        {
            health -= bulletDamage * 3;
        }
        else if(collision.gameObject.layer == 9 && enemyColor == EnemyColor.blue)
        {
            health -= bulletDamage * 3;
        }
        else
        {
            health -= bulletDamage;
        }
        Destroy(collision.gameObject);
    }
}
