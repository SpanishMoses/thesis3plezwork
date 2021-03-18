using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public bool canPush;
    public Rigidbody2D rb;
    public HingeJoint2D hinge;

    // Start is called before the first frame update
    void Start()
    {
        //canPush = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canPush == false){
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        if (canPush == true){
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player"){
            
            
        }

        if (collision.gameObject.tag == "Player" && collision.gameObject.tag == "Dog") {
            canPush = true;
        }
        
        if (collision.gameObject.tag == "Floor"){
            rb.gravityScale = 1f;
        }

        if (collision.gameObject.tag == "Dog")
        {
            Debug.Log("he hit it");
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            canPush = false;
        }

        if (collision.gameObject.tag == "Platform")
        {
            gameObject.transform.parent = collision.gameObject.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            
        }

        if (collision.gameObject.tag == "Dog"){
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            canPush = true;
        }

        if (collision.gameObject.tag == "Player" && collision.gameObject.tag == "Dog")
        {
            canPush = true;
        }

        if (collision.gameObject.tag == "Platform")
        {
            gameObject.transform.parent = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Distract"){
            Destroy(collision.gameObject);
        }
    }
}
