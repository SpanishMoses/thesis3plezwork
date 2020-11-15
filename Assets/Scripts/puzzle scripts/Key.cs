using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    
    public Rigidbody2D rb;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * 10f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
