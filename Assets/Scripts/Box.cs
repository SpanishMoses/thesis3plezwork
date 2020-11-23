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
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player"){
            canPush = true;
        }

        if (collision.gameObject.tag == "Player" && collision.gameObject.tag == "Dog"){
            canPush = false;
        }
        if (collision.gameObject.tag == "Floor"){
            rb.gravityScale = 1f;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canPush = false;
        }

        if (collision.gameObject.tag == "Player" && collision.gameObject.tag == "Dog")
        {
            canPush = false;
        }
    }
}
