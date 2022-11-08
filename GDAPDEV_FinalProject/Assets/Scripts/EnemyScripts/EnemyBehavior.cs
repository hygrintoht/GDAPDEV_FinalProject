using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    enum EnemyColor { red = 0, green = 1, blue = 2 };
    public enum Direction { right, left, up, down, forward, back };

    [SerializeField] Material materialR;
    [SerializeField] Material materialG;
    [SerializeField] Material materialB;
    [SerializeField] Renderer enemyRend;

    [SerializeField] float health = 30.0f;
    [SerializeField] EnemyColor enemyColor = EnemyColor.red;
    [SerializeField] float bulletDamage = 5.0f;
    [SerializeField] float moveSpeed = 5.0f;
    //[SerializeField] float timeAlive = 15.0f;

    Vector3 moveDir = Vector3.zero;

    void Start()
    {
        SetEnemyParams();
    }

    void Update()
    {
        gameObject.transform.position = gameObject.transform.position + (moveDir * moveSpeed * Time.deltaTime);

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
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
    }

    public void SetEnemyParams()
    {
        switch (Random.Range(0, 3))
        {
            case 0:
                enemyColor = EnemyColor.red;
                enemyRend.material = materialR;
                break;
            case 1:
                enemyColor = EnemyColor.green;
                enemyRend.material = materialG;
                break;
            case 2:
                enemyColor = EnemyColor.blue;
                enemyRend.material = materialB;
                break;
        }
    }

    public void MoveToDirection(Direction dir)
    {
        if (dir == Direction.down) moveDir = Vector3.down;
        if (dir == Direction.right) moveDir = Vector3.right;
        if (dir == Direction.left) moveDir = Vector3.left;
        if (dir == Direction.back) moveDir = Vector3.back;
        if (dir == Direction.up) moveDir = Vector3.up;
        if (dir == Direction.forward) moveDir = Vector3.forward;
    }
}
