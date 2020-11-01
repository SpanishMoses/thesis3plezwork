using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigSpot : MonoBehaviour
{
    public bool dug;
    public GameObject obj;

    // Start is called before the first frame update
    void Start()
    {
        dug = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawn(){
        Instantiate(obj, transform.position, Quaternion.identity);
    }
}
