using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    //code from https://www.youtube.com/watch?v=ailbszpt_AI&feature=emb_title&ab_channel=PressStart

    public Camera mainCamera; 
    private Vector2 screenBoundaries;
    private float objectWidth;
    private float objectHeight;

    // Start is called before the first frame update
    void Start()
    {
        screenBoundaries = mainCamera.ScreenToWorldPoint(new Vector3 (Screen.width, Screen.height, mainCamera.transform.position.z));
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        
    }

    private void Update()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBoundaries.x + objectWidth, screenBoundaries.x - objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBoundaries.y + objectHeight, screenBoundaries.y - objectHeight);
        transform.position = viewPos;
    }
}
