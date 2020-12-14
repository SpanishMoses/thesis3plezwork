using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevertCam : MonoBehaviour
{
    public GameObject cursor;
    public GameObject playerCam;
    public GameObject puzzleCam;

    public bool playerCrossed;
    public bool dogCrossed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (playerCrossed == true && dogCrossed == true)
        {
            playerCam.SetActive(true);
            puzzleCam.SetActive(false);
            cursor.transform.parent = playerCam.gameObject.transform;
            Destroy(gameObject, 10f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerCrossed = true;
        }

        if (collision.gameObject.tag == "Dog")
        {
            dogCrossed = true;
        }
    }
}
