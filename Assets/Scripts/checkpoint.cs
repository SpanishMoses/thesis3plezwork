using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour
{

    public float x;
    public float y;
    public float z;

    public Animator anim;

    public bool playerHit;

    public GameObject block;

    // Start is called before the first frame update
    void Start()
    {
        anim.SetFloat("Blend", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHit == true){
            anim.SetFloat("Blend", 1);
            block.SetActive(true);
        }
    }
}
