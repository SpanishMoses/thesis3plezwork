using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogPlatform : MonoBehaviour
{
    public float speed;
    public Transform bottomPT;
    public Transform topPT;
    public ElevatorHumanSwitch hum;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hum.Move == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, topPT.position, speed * Time.deltaTime);
        }

        if (hum.Move == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, bottomPT.position, speed * Time.deltaTime);
        }
    }
}
