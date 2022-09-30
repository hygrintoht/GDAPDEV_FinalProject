using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollTexture : MonoBehaviour
{
    [SerializeField] Renderer rend;

    [SerializeField] float scrollX = 0;
    [SerializeField] float scrollY = 0;
    
    float offsetX = 0;
    float offsetY = 0;

    void Start()
    {
        rend = GetComponent<Renderer>();    
    }

    void Update()
    {
        offsetX = Time.time * scrollX;
        offsetY = Time.time * scrollY;
        rend.material.mainTextureOffset = new Vector2(offsetX, offsetY);
    }
}
