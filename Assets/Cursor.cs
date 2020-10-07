using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class Cursor : MonoBehaviour
{
    public Dog dog;

    public GameObject cursor;

    public float speed;

    public GameObject movePoint;

    [SerializeField] private int playerID = 0;
    [SerializeField] private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = ReInput.players.GetPlayer(playerID);
    }

    // Update is called once per frame
    void Update()
    {
        float translationHorz = player.GetAxis("CursorHorz") * speed;
        translationHorz *= Time.deltaTime;
        transform.Translate(translationHorz, 0, 0);

        float translationVert = player.GetAxis("CursorVert") * speed;
        translationVert *= Time.deltaTime;
        transform.Translate(0, translationVert, 0);

        if (player.GetButton("Interact"))
        {
            //dog.currTarget = dog.cursorTarget.transform;
            RaycastHit2D ray = Physics2D.Raycast(transform.position, -Vector2.up);

            if (ray.collider != null){
                
                movePoint.transform.position = ray.point;
                dog.currTarget = movePoint.transform;
            }
        }
    }

    
}
