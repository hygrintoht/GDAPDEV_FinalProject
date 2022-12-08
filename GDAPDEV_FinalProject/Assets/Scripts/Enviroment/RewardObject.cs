using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardObject : EnviromentObject
{
    private void OnCollisionEnter(Collision collision)
    {
        action(this);
    }
}
