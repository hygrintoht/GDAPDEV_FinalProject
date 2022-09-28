using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovment : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1.0f;

    void Update()
    {
        transform.position += (new Vector3(Input.GetAxis("Horizontal"), -Input.GetAxis("Vertical"),0)) * moveSpeed * Time.deltaTime;
    }
}
