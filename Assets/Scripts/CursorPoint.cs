﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorPoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.transform.parent = collision.gameObject.transform;
        /*if (collision.gameObject.tag == "Floor")
        {
            gameObject.transform.parent = collision.gameObject.transform;

        }*/
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }
}
