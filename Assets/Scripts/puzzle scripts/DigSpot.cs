using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigSpot : MonoBehaviour
{
    public bool dug;
    public GameObject obj;
    public SpriteRenderer rend;
    public Sprite sprite;

    // Start is called before the first frame update
    void Start()
    {
        dug = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (dug == true){
            rend.sprite = sprite;
        }
    }

    public void spawn(){
        Instantiate(obj, transform.position, Quaternion.identity);
    }
}
