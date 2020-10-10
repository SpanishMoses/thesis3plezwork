using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogSwitch : MonoBehaviour
{
    public bool isEnabled;

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
            isEnabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Dog")
        {
            isEnabled = false;
        }
    }
}
