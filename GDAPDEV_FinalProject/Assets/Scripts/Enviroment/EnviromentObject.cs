using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentObject : MonoBehaviour
{
    public Action<EnviromentObject> action;

    public virtual void Update()
    {
        gameObject.transform.position = gameObject.transform.position + (Vector3.back * 5.0f * Time.deltaTime);
        if (transform.position.z < -10) action(this);
    }

    public void Init(Action<EnviromentObject> act)
    {
        action = act;
    }
}
