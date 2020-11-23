using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public Box box;
    public bool disable;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (disable == true){
            box.GetComponent<Box>().hinge.enabled = false;

        }
    }
}
