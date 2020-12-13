﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    
    public Rigidbody2D rb;
    public CircleCollider2D circle;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.up * 5f;
        StartCoroutine(enable());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    IEnumerator enable(){
        yield return new WaitForSeconds(0.5f);
        circle.enabled = true;
    }
}
