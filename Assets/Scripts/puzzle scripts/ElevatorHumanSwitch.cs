﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorHumanSwitch : MonoBehaviour
{
    public Sprite normal;
    public Sprite touched;
    public SpriteRenderer rend;
    public bool Move;

    public AudioSource noise;
    public AudioClip switchOn;
    public AudioClip switchOff;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Move == true){
            rend.sprite = touched;
        }
        if (Move == false){
            rend.sprite = normal;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player"){
            Move = true;
            noise.clip = switchOn;
            noise.Play();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Move = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Move = false;
            noise.clip = switchOff;
            noise.Play();
        }
    }
}
