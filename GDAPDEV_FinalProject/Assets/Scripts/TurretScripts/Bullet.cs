using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Material materialR;
    [SerializeField] Material materialG;
    [SerializeField] Material materialB;

    [SerializeField]public Rigidbody rb;
    [SerializeField]Renderer bulletRend;

    Action<Bullet> action;

    void Update()
    {
        if(transform.position.z > 75 || transform.position.z < -25)
        {
            action(this);
        }    
    }

    private void OnCollisionEnter(Collision collision)
    {
        action(this);
    }

    public void Init(Action<Bullet> act)
    {
        action = act;
    }

    public void SetBulletType(Turret.BulletType bulletType)
    {
        if (bulletType == Turret.BulletType.red)
        {
            bulletRend.material = materialR;
            gameObject.layer = LayerMask.NameToLayer("BulletR");
        }
        if (bulletType == Turret.BulletType.green)
        {
            bulletRend.material = materialG;
            gameObject.layer = LayerMask.NameToLayer("BulletG");
        }
        if (bulletType == Turret.BulletType.blue)
        {
            bulletRend.material = materialB;
            gameObject.layer = LayerMask.NameToLayer("BulletB");
        }
    }
}
