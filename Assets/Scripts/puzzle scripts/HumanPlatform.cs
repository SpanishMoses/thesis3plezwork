using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanPlatform : MonoBehaviour
{
    public float speed;
    public Transform bottomPT;
    public Transform topPT;
    public ElevatorDogSwitch dog;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dog.Move == true){
            transform.position = Vector2.MoveTowards(transform.position, topPT.position, speed * Time.deltaTime);
        }

        if (dog.Move == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, bottomPT.position, speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Dog")
        {
            speed = 0;
        }
        else
        {
            speed = 9;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Point")
        {
            collision.gameObject.transform.parent = gameObject.transform;
        }
    }

    /*private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Point")
        {
            collision.gameObject.transform.parent = null;
        }
    }*/
}
