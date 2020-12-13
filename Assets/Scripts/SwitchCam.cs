using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCam : MonoBehaviour
{
    public CameraManager mainCam;
    public GameObject Cursor;
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
        if (playerCrossed == true && dogCrossed == true){
            playerCam.SetActive(false);
            puzzleCam.SetActive(true);
            Cursor.transform.parent = puzzleCam.gameObject.transform;
            mainCam.cam = puzzleCam;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerCrossed = true;
        }

        if (collision.gameObject.tag == "Dog"){
            dogCrossed = true;
        }
    }
}
