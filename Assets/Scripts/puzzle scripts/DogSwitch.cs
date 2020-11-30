using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogSwitch : MonoBehaviour
{
    public bool isEnabled;
    public GameObject good;
    public GameObject bad;

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
            good.SetActive(true);
        }
        if (collision.gameObject.tag == "Player" && isEnabled == false)
        {
            bad.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Dog")
        {
            isEnabled = false;
            good.SetActive(false);
        }
        if (collision.gameObject.tag == "Player" && isEnabled == false)
        {
            bad.SetActive(false);
        }
    }
}
