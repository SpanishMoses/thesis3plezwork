﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enablethoughts : MonoBehaviour
{

    public GameObject thoughts;
    public GameObject thoughtBubbleParticle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player"){
            thoughts.SetActive(true);
            thoughtBubbleParticle.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            thoughts.SetActive(false);
            thoughtBubbleParticle.SetActive(false);
        }
    }
}
