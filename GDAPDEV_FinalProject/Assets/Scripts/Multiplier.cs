using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multiplier : MonoBehaviour
{
    //Multiplier will be use as reference for passing data for upgrades.

    [Header("Ship Multiplier")]
    [SerializeField] public int healthMultiplier = 1;
    [SerializeField] public float attackMultiplier = 0.5f;
    [SerializeField] public float attackSpdMultiplier = 0.25f ;
    

}
