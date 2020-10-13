﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorDogSwitch : MonoBehaviour
{
    public bool Move;

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
        if (collision.gameObject.tag == "Dog")
        {
            Move = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Dog")
        {
            Move = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Dog")
        {
            Move = false;
        }
    }
}
