using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSwitches : MonoBehaviour
{
    public bool activated;
    public GameObject yes;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (activated == true){
            Instantiate(yes, this.gameObject.transform.position, Quaternion.identity);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Snow"){
            activated = true;
        }
    }
}
