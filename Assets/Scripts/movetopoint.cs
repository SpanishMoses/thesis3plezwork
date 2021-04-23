using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movetopoint : MonoBehaviour
{
    public GameObject oldBackground;
    public GameObject newBackground;
    public GameObject newPoint;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        player.transform.position = newPoint.transform.position;
        Physics.SyncTransforms();
        oldBackground.SetActive(false);
        newBackground.SetActive(true);
    }
}
