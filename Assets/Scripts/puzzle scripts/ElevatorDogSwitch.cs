using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorDogSwitch : MonoBehaviour
{
    public Sprite normal;
    public Sprite touched;
    public SpriteRenderer rend;
    public bool Move;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Move == true)
        {
            rend.sprite = touched;
        }
        if (Move == false)
        {
            rend.sprite = normal;
        }
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
