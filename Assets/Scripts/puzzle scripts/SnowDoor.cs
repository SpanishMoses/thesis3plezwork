using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowDoor : MonoBehaviour
{
    public int switchNum;
    public GameObject[] switches;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*for (int i = 0; i < switches.Length; i++){
            if (switches[i].GetComponent<SnowballSwitch>().isEnabled == true){
                Destroy(gameObject);
            }
        }*/
        if (switchNum == switches.Length){
            Destroy(gameObject);
        }
    }
}
