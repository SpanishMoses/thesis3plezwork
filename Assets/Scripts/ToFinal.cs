using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToFinal : MonoBehaviour
{
    public bool playerCrossed;
    public GameObject fade;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCrossed == true){
            fade.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerCrossed = true;
        }
    }
}
