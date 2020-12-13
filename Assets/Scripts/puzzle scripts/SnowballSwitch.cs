﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballSwitch : MonoBehaviour
{
    public SnowDoor door;
    public bool isEnabled;
    public SpriteRenderer rend;
    public Sprite on;
    public Animator anim;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Snow"){
            Debug.Log("hit");
            anim.enabled = false;
            isEnabled = true;
            rend.sprite = on;
            door.switchNum++;
        }
    }
}
