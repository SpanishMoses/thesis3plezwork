using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Point")
        {
            collision.gameObject.transform.parent = gameObject.transform;
        }
    }
}
