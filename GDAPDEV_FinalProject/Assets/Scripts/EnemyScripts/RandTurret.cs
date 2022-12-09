using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class RandTurret : MonoBehaviour
{
    [SerializeField] Turret turret;
    //[SerializeField] GameObject EnemyShootArea;

    [SerializeField]private float fireCountDown;
    [SerializeField] private float decreaseCooldown = 0;
    Vector3 shootAreaCenter;
    Vector3 shootAreaSize;
    //Vector3 result;

    void Start()
    {
        fireCountDown = Random.Range(3.0f - decreaseCooldown, 7.5f - (decreaseCooldown * 1.5f));
        shootAreaCenter = Vector3.zero;
        shootAreaSize = new Vector3(13.0f,7.0f,0);
    }

    void Update()
    {
        fireCountDown -= Time.deltaTime;
        if(fireCountDown <= 0)
        {
            //result = RandomPositionInShootArea();
            //Debug.Log($"{result}");
            transform.LookAt(RandomPositionInShootArea());
            turret.Shoot(Turret.BulletType.none);
            fireCountDown = Random.Range(3.0f - decreaseCooldown, 7.5f - (decreaseCooldown * 1.5f));
        }
    }

    Vector3 RandomPositionInShootArea()
    {
        return shootAreaCenter + new Vector3(Random.Range(-shootAreaSize.x / 2, shootAreaSize.x / 2), Random.Range(-shootAreaSize.y / 2, shootAreaSize.y / 2), Random.Range(-shootAreaSize.z / 2, shootAreaSize.z / 2));
    }
}
