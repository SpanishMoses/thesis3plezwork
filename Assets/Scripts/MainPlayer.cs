using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainPlayer : MonoBehaviour
{
    //good jump code from https://www.youtube.com/watch?v=7KiK0Aqtmzc&ab_channel=BoardToBitsGames
    //throwing script help from https://www.youtube.com/watch?v=3DUmpVi82q8&t=176s&ab_channel=TheGameGuy

    public Dog dog;
    public AimShoot shoot;

    public Animator topAnim;
    public Animator lowAnim;
    public SpriteRenderer topSprite;
    public SpriteRenderer lowSprite;

    public float speed;

    public Rigidbody2D rb;

    public bool moveRight;

    public bool pushing;

    public bool isPetting;

    public bool follow;

    //things for jump
    public bool canJump;
    public float timeToHoldJump;
    public float maxTimeForJump;
    public Transform groundCheckPoint;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    [SerializeField] private int playerID = 0;
    [SerializeField] private Player player;

    public bool gotKey;
    public GameObject keyText;

    public float petDistance;

    public GameObject cursor;

    //code for respawning
    public float pointX;
    public float pointY;
    public float pointZ;

    public AudioSource noise;
    public AudioClip commandSound;
    public AudioClip jumpSound;

    public Scene curScene;
    public string sceneName;

    public GameObject load;
    public Transform loadingBar;

    public float currentAmount;
    public float loadSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        player = ReInput.players.GetPlayer(playerID);
        gotKey = false;
        pointX = PlayerPrefs.GetFloat("CheckPointX");
        pointY = PlayerPrefs.GetFloat("CheckPointY");
        pointZ = PlayerPrefs.GetFloat("CheckPointZ");
        this.transform.position = new Vector2(pointX, pointY);
        dog.transform.position = new Vector2(dog.pointX, dog.pointY);
        ResetPos();
        Physics.SyncTransforms();
        //Physics.SyncTransforms();
        curScene = SceneManager.GetActiveScene();
        sceneName = curScene.name;
    }

    // Update is called once per frame
    void Update()
    {
        /*float translationHorz = player.GetAxis("MoveHorz") * speed;
        translationHorz *= Time.deltaTime;
        transform.Translate(translationHorz, 0, 0);*/

        if (player.GetButton("Jump") && canJump == true && isPetting == false && shoot.windUp == false)
        {
            noise.clip = jumpSound;
            noise.Play();
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.velocity += Vector2.up * 7f;
        }

        if (player.GetButton("Jump") && canJump == false)
        {

            topAnim.SetFloat("Blend", 7);
            lowAnim.SetFloat("Blend", 7);
        }

        if (canJump == false && player.GetAxis("MoveHorz") > 0)
        {
            topAnim.SetFloat("Blend", 7);
            lowAnim.SetFloat("Blend", 7);
            topSprite.flipX = false;
            lowSprite.flipX = false;
        }

        if (canJump == false && player.GetAxis("MoveHorz") < 0)
        {
            topAnim.SetFloat("Blend", 7);
            lowAnim.SetFloat("Blend", 7);
            topSprite.flipX = true;
            lowSprite.flipX = true;
        }

        if (player.GetButton("ComeBack") && dog.isDistracted == false){
            dog.currTarget = dog.playerTarget.transform;
            dog.isFollowingPlayer = true;
            topAnim.SetFloat("Blend", 5);
            lowAnim.SetFloat("Blend", 5);
        }

        //All the petting script...AHHHHHH
        //petting looking right idle
        if (player.GetButtonDown("Pet") && player.GetAxis("MoveHorz") == 0 && moveRight == true && canJump == true && pushing == false && shoot.windUp == false){
            isPetting = true;
            topAnim.SetFloat("Blend", 9);
            lowAnim.SetFloat("Blend", 9);
            topSprite.flipX = false;
            lowSprite.flipX = false;
        } else if (player.GetButtonUp("Pet") && player.GetAxis("MoveHorz") == 0 && moveRight == true && canJump == true && pushing == false && shoot.windUp == false)
        {
            isPetting = false;
            topAnim.SetFloat("Blend", 0);
            lowAnim.SetFloat("Blend", 0);
            topSprite.flipX = false;
            lowSprite.flipX = false;
        }

        //petting looking left idle
        if (player.GetButtonDown("Pet") && player.GetAxis("MoveHorz") == 0 && moveRight == false && canJump == true && pushing == false && shoot.windUp == false)
        {
            isPetting = true;
            topAnim.SetFloat("Blend", 9);
            lowAnim.SetFloat("Blend", 9);
            topSprite.flipX = true;
            lowSprite.flipX = true;
        }
        else if (player.GetButtonUp("Pet") && player.GetAxis("MoveHorz") == 0 && moveRight == false && canJump == true && pushing == false && shoot.windUp == false)
        {
            isPetting = false;
            topAnim.SetFloat("Blend", 0);
            lowAnim.SetFloat("Blend", 0);
            topSprite.flipX = true;
            lowSprite.flipX = true;
        }

        //petting while running right
        if (player.GetButtonDown("Pet") && player.GetAxis("MoveHorz") > 0 && moveRight == true && canJump == true && pushing == false && shoot.windUp == false)
        {
            isPetting = true;
            topAnim.SetFloat("Blend", 8);
            lowAnim.SetFloat("Blend", 8);
            topSprite.flipX = false;
            lowSprite.flipX = false;
        }
        else if (player.GetButtonUp("Pet") && player.GetAxis("MoveHorz") > 0 && moveRight == true && canJump == true && pushing == false && shoot.windUp == false)
        {
            isPetting = false;
            topAnim.SetFloat("Blend", 1);
            lowAnim.SetFloat("Blend", 1);
            topSprite.flipX = false;
            lowSprite.flipX = false;
        }

        //petting while running left
        if (player.GetButtonDown("Pet") && player.GetAxis("MoveHorz") < 0 && moveRight == false && canJump == true && pushing == false && shoot.windUp == false)
        {
            isPetting = true;
            topAnim.SetFloat("Blend", 8);
            lowAnim.SetFloat("Blend", 8);
            topSprite.flipX = true;
            lowSprite.flipX = true;
        }
        else if (player.GetButtonUp("Pet") && player.GetAxis("MoveHorz") < 0 && moveRight == false && canJump == true && pushing == false && shoot.windUp == false)
        {
            isPetting = false;
            topAnim.SetFloat("Blend", 1);
            lowAnim.SetFloat("Blend", 1);
            topSprite.flipX = true;
            lowSprite.flipX = true;
        }

        if (Vector2.Distance(transform.position, dog.transform.position) < petDistance && isPetting == true && shoot.windUp == false)
        {
            dog.beingPet = true;
            dog.anim.SetFloat("Blend", 4);
        } else if (isPetting == false){
            dog.beingPet = false;
        }

        //PETTING SCRIPT ENDS WHY THIS SO LONG!?!?

        if (player.GetButton("DogAction") && dog.canDig == true && dog.startDig == false){
            dog.startDig = true;
        }

        if (player.GetButton("DogAction") && dog.canBite == true && dog.startBite == false){
            dog.startBite = true;
        }

        if (player.GetAxis("MoveHorz") > 0 && canJump == true && pushing == false && isPetting == false)
        {
            moveRight = true;
            topAnim.SetFloat("Blend", 1);
            lowAnim.SetFloat("Blend", 1);
            topSprite.flipX = false;
            lowSprite.flipX = false;
        }
        else if (player.GetAxis("MoveHorz") < 0 && canJump == true && pushing == false && isPetting == false)
        {
            moveRight = false;
            topAnim.SetFloat("Blend", 1);
            lowAnim.SetFloat("Blend", 1);
            topSprite.flipX = true;
            lowSprite.flipX = true;
        }

        if (player.GetAxis("MoveHorz") == 0 && moveRight == true && canJump == true && pushing == false && isPetting == false)
        {
            topAnim.SetFloat("Blend", 0);
            lowAnim.SetFloat("Blend", 0);
            topSprite.flipX = false;
            lowSprite.flipX = false;
        }

        if (player.GetAxis("MoveHorz") == 0 && moveRight == false && canJump == true && pushing == false && isPetting == false)
        {
            topAnim.SetFloat("Blend", 0);
            lowAnim.SetFloat("Blend", 0);
            topSprite.flipX = true;
            lowSprite.flipX = true;
        }

        if (player.GetButton("Aim&Shoot") && player.GetAxis("MoveHorz") == 0 && pushing == false && isPetting == false)
        {
            topAnim.SetFloat("Blend", 2);
            lowAnim.SetFloat("Blend", 2);
        }

        if (player.GetButton("Aim&Shoot") && player.GetAxis("MoveHorz") < 0 && pushing == false || player.GetButton("Aim&Shoot") && player.GetAxis("MoveHorz") > 0 && pushing == false && isPetting == false)
        {
            topAnim.SetFloat("Blend", 3);
            lowAnim.SetFloat("Blend", 3);
        }

        if (rb.velocity.y < 0){
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        } else if (rb.velocity.y > 0 && !player.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        canJump = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);

        if (pushing == true && canJump == true && isPetting == false)
        {
            topAnim.SetFloat("Blend", 4);
            lowAnim.SetFloat("Blend", 4);
        }

        if (player.GetButton("Interact") && isPetting == false)
        {
            follow = false;
        }

        if (player.GetButton("ComeBack") && isPetting == false)
        {
            follow = true;
        }

        if (player.GetButton("Interact") && player.GetAxis("MoveHorz") == 0 && pushing == false && isPetting == false)
        {
            topAnim.SetFloat("Blend", 5);
            lowAnim.SetFloat("Blend", 5);
        }

        if (player.GetButton("Interact") && player.GetAxis("MoveHorz") < 0 && pushing == false && isPetting == false || player.GetButton("Interact") && player.GetAxis("MoveHorz") > 0 && pushing == false && isPetting == false)
        {
            topAnim.SetFloat("Blend", 6);
            lowAnim.SetFloat("Blend", 6);
        }

        if (player.GetButton("ComeBack") && player.GetAxis("MoveHorz") == 0 && pushing == false && isPetting == false)
        {
            topAnim.SetFloat("Blend", 5);
            lowAnim.SetFloat("Blend", 5);
        }

        if (player.GetButton("ComeBack") && player.GetAxis("MoveHorz") < 0 && pushing == false && isPetting == false || player.GetButton("ComeBack") && player.GetAxis("MoveHorz") > 0 && pushing == false && isPetting == false)
        {
            topAnim.SetFloat("Blend", 6);
            lowAnim.SetFloat("Blend", 6);
        }

        if (player.GetButton("Aim&Shoot"))
        {
            if (cursor.transform.position.x > transform.position.x)
            {
                Debug.Log("ahead");
                topSprite.flipX = false;
                lowSprite.flipX = false;
                moveRight = true;
            }
            if (cursor.transform.position.x < transform.position.x)
            {
                Debug.Log("back");
                topSprite.flipX = true;
                lowSprite.flipX = true;
                moveRight = false;
            }
        }

        if (gotKey == false)
        {
            keyText.SetActive(false);
        }

        if (player.GetButton("Restart"))
        {
            load.SetActive(true);
            currentAmount += loadSpeed * Time.deltaTime;
        }
        else
        {
            load.SetActive(false);
            currentAmount = 0;
        }
            loadingBar.GetComponent<Image>().fillAmount = currentAmount / 100;
            if (currentAmount >= 100)
            {
                SceneManager.LoadScene(sceneName);
                ResetPos();
            }

            if (Input.GetKey(KeyCode.M)){
            PlayerPrefs.SetFloat("CheckPointX", 192.61f);
            PlayerPrefs.SetFloat("CheckPointY", 8.25f);
            PlayerPrefs.SetFloat("CheckPointZ", 0);
        }
    }


    private void FixedUpdate()
    {
        if (isPetting == false)
        {
            float translationHorz = player.GetAxis("MoveHorz") * speed;
            translationHorz *= Time.deltaTime;
            transform.Translate(translationHorz, 0, 0);
        }
        /*if (player.GetAxis("MoveHorz") > 0){
            anim.SetFloat("Blend", 1);
            sprite.flipX = false;
        } else
        {
            anim.SetFloat("Blend", 1);
            sprite.flipX = true;
        }*/
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform"){
            gameObject.transform.parent = collision.gameObject.transform;
        }
        if (collision.gameObject.tag == "Floor")
        {
            gameObject.transform.parent = collision.gameObject.transform;
        }
        if (collision.gameObject.tag == "Key")
        {
            gotKey = true;
            keyText.SetActive(true);
            Destroy(collision.gameObject);
        }
        /*if (collision.gameObject.tag == "Block"){
            Debug.Log("yes");
            topAnim.SetFloat("Blend", 4);
            lowAnim.SetFloat("Blend", 4);
        }*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Block")
        {
            pushing = true;
            Debug.Log("yes");
            
        }

        if (collision.gameObject.tag == "SnowPile")
        {
            shoot.numSnow = 3;
        }

        if (collision.gameObject.tag == "Checkpoint")
        {
            //checkpoint = other.gameObject;           
            checkpoint check = collision.gameObject.GetComponent<checkpoint>();
            check.playerHit = true;
            pointX = check.x;
            pointY = check.y;
            pointZ = check.z;
            float positionX = pointX;
            float positionY = pointY;
            float positionZ = pointZ;
            PlayerPrefs.SetFloat("CheckPointX", positionX);
            PlayerPrefs.SetFloat("CheckPointY", positionY);
            PlayerPrefs.SetFloat("CheckPointZ", positionZ);
            //dog.transform.position = collision.transform.position;
            /*CheckPoints check = other.transform.GetComponent<CheckPoints>();
            check.Save();*/
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Block")
        {
            pushing = false;
            Debug.Log("yes");

        }
    }

    void ResetPos()
    {

        pointX = PlayerPrefs.GetFloat("CheckPointX");
        pointY = PlayerPrefs.GetFloat("CheckPointY");
        pointZ = PlayerPrefs.GetFloat("CheckPointZ");
        /*pointX = PlayerPrefs.GetFloat("CheckPointX");
        pointY = PlayerPrefs.GetFloat("CheckPointY");
        pointZ = PlayerPrefs.GetFloat("CheckPointZ");*/
        this.transform.position = new Vector2(pointX, pointY);
        dog.transform.position = new Vector3(dog.pointX, dog.pointY);
        //Physics.SyncTransforms();
    }
}
