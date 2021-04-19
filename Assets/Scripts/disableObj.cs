using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disableObj : MonoBehaviour
{

    public GameObject disable;
    public GameObject enable;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void doIt(){
        disable.SetActive(false);
        enable.SetActive(true);
    }
}
