using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.UI;

public class GameCursor : MonoBehaviour
{
    public Dog dog;

    public GameObject cursor;

    public float speed;

    public GameObject movePoint;
    public LayerMask layer;
    [SerializeField] private int playerID = 0;
    [SerializeField] private Player player;

    public SpriteRenderer sprite;

    public Sprite nothing;
    public Sprite pressed;

    public AudioSource noise;


    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

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

        if (player.GetButton("Interact") && dog.isDistracted == false)
        {
            //dog.currTarget = dog.cursorTarget.transform;
            dog.isFollowingPlayer = false;
            RaycastHit2D ray = Physics2D.Raycast(transform.position, Vector2.down, 100f, layer);

            if (ray.collider != null){
                Debug.Log(ray.point);
                movePoint.transform.position = ray.point;
                dog.currTarget = movePoint.transform;
                
            }
        }

        if (player.GetButtonDown("Interact")){
            sprite.sprite = pressed;
        }

        if (player.GetButtonUp("Interact"))
        {
            sprite.sprite = nothing;
            noise.Play();
        }

        if (player.GetButtonDown("ComeBack"))
        {
            sprite.sprite = pressed;
        }

        if (player.GetButtonUp("ComeBack"))
        {
            sprite.sprite = nothing;
            noise.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Menu" && player.GetButton("Interact")){
            Debug.Log("it worked");
            ButtonManager butt = collision.GetComponent<ButtonManager>();
            butt.SinglePlayerbutton(butt.targetScene);
        }
    }
}
