using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovment : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1.0f;//up down left right
    //[SerializeField] float forwardMoveSpeed = 1.0f;

    float hori = 0;
    float vert = 0;
    Vector3 direction = Vector3.zero;
    Vector3 finalDirection = Vector3.zero;

    void Update()
    {
        hori = Input.GetAxis("Horizontal");
        vert = Input.GetAxis("Vertical");

        direction = new Vector3(hori, vert, 0);
        finalDirection = new Vector3(hori, vert, 2.0f);

        transform.position += direction * moveSpeed * Time.deltaTime; //+ transform.forward * forwardMoveSpeed * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(finalDirection), Mathf.Deg2Rad*50.0f);
    }
}
