﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Dog : MonoBehaviour
{
    //from https://www.youtube.com/watch?v=jvtFUfJ6CP8&t=301s
    // go to closest target from https://www.youtube.com/watch?v=VH-bUST_w0o&ab_channel=IndieGamesDev

    public Transform myLocation;

    public GameObject playerTarget;
    //public GameObject[] testTargets;
    public GameObject cursorTarget;

    public Transform currTarget;

    public float speed;
    public float nextWaypointDistance = 5f;
    public float spawnPoopTime;

    Path path;
    int currentWaypoint = 0;
    public bool reachedEndOfPath = false;

    public Seeker seeker;
    public Rigidbody2D rb;

    public Transform groundCheckPoint;

    //public Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        //SearchTargets();
        InvokeRepeating("UpdatePath", 0f, .5f);
        
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(rb.position, currTarget.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            gameObject.transform.parent = collision.gameObject.transform;
        }
        if (collision.gameObject.tag == "Floor")
        {
            gameObject.transform.parent = collision.gameObject.transform;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (collision.gameObject.tag != "Player"){
            rb.AddForce(Vector2.up * 200f);
        }*/
        if (collision.gameObject.tag == "Floor")
        {
            rb.AddForce(Vector2.up * 200f);
        }
        if (collision.gameObject.tag == "Platform")
        {
            rb.AddForce(Vector2.up * 200f);
        }
    }
}
