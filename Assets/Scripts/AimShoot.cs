﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.UI;

public class AimShoot : MonoBehaviour
{
    public MainPlayer main;
    public GameObject cursor;
    public GameObject throwPt;
    public GameObject snowball;
    public float launchForce;
    //public GameObject point;
    //public GameObject[] points;
    public int numberOfPoints;
    public float force;
    Vector2 Direction;
    public Animator topAnim;
    //public Animator lowAnim;
    public bool windUp;

    [SerializeField] private int playerID = 0;
    [SerializeField] private Player player;

    public int numSnow;
    public int maxSnow;
    public GameObject snow1;
    public GameObject snow2;
    public GameObject snow3;

    // Start is called before the first frame update
    void Start()
    {
        player = ReInput.players.GetPlayer(playerID);
        /*points = new GameObject[numberOfPoints];
        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i] = Instantiate(point, throwPt.transform.position, Quaternion.identity);
        }*/
    }

// Update is called once per frame
void Update()
    {
    Vector2 cursorPos = cursor.transform.position;
    Vector2 myPos = throwPt.transform.position;
    Direction = cursorPos - myPos;
    transform.right = Direction;

    if (numSnow >= 3){
            snow1.SetActive(true);
            snow2.SetActive(true);
            snow3.SetActive(true);
    }

        if (numSnow <= 2)
        {
            snow1.SetActive(false);
            snow2.SetActive(true);
            snow3.SetActive(true);
        }

        if (numSnow <= 1)
        {
            snow1.SetActive(false);
            snow2.SetActive(false);
            snow3.SetActive(true);
        }

        if (numSnow <= 0)
        {
            snow1.SetActive(false);
            snow2.SetActive(false);
            snow3.SetActive(false);
        }

        if (numSnow > maxSnow){
            numSnow = maxSnow;
        }

        if (player.GetButtonDown("Aim&Shoot"))
    {
            main.isPetting = true;
        /*for (int i = 0; i < points.Length; i++)
        {
            points[i].transform.position = PointPosition(i * 0.1f);
        }*/
    }

    if (player.GetButtonUp("Aim&Shoot"))
    {
            /*for (int i = 0; i < points.Length; i++)
            {
                Destroy(points[i]);
            }*/
            //topAnim.SetFloat("Blend", 3);
            main.isPetting = false;
            if (numSnow > 0)
            {
                GameObject snowballIns = Instantiate(snowball, throwPt.transform.position, throwPt.transform.rotation);
                snowballIns.GetComponent<Rigidbody2D>().velocity = transform.right * launchForce;
                numSnow--;
            }
    }
}


    Vector2 PointPosition(float t)
    {
        Vector2 currentPointPos = (Vector2)throwPt.transform.position + (Direction.normalized * force * t) + 0.5f * Physics2D.gravity * (t * t);
        return currentPointPos;
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SnowPile"){
            numSnow = 3;
        }
    }*/
}
