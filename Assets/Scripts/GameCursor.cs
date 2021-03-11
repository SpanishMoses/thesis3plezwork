using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    public bool moveScene;

    public GameObject load;
    public Transform loadingBar;

    public float currentAmount;
    public float loadSpeed;
    public string sceneName;

    public float positionX;
    public float positionY;
    public float positionZ;

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

        if (moveScene == true){
            load.SetActive(true);
            currentAmount += loadSpeed * Time.deltaTime;

            loadingBar.GetComponent<Image>().fillAmount = currentAmount / 100;

            if (currentAmount >= 100)
            {
                PlayerPrefs.SetFloat("CheckPointX", positionX);
                PlayerPrefs.SetFloat("CheckPointY", positionY);
                PlayerPrefs.SetFloat("CheckPointZ", positionZ);
                SceneManager.LoadScene(sceneName);
                //butt.SinglePlayerbutton(butt.targetScene);
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Menu")
        {
            moveScene = true;
            load.SetActive(true);
            
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        ButtonManager butt = collision.GetComponent<ButtonManager>();
            if (currentAmount >= 100)
            {
                
                butt.SinglePlayerbutton(butt.targetScene);
            }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Menu")
        {
            moveScene = false;
            load.SetActive(false);
            currentAmount = 0;
        }
    }
}
