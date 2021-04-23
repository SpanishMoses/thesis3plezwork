using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadespawn : MonoBehaviour
{
    public GameObject moveNow;
    public GameObject mainObj;
    public float newX;
    public float newY;
    public float newZ;
    public GameObject fadeIn;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawnMe(){
        //player.transform.position = new Vector2(newX, newY);
        

        Debug.Log("is spawned");
    }

    void restartNow(){
        fadeIn.SetActive(true);
        mainObj.SetActive(false);
        moveNow.SetActive(false);
    }

    void startMove(){
        moveNow.SetActive(true);
    }
}
