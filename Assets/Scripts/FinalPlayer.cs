using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class FinalPlayer : MonoBehaviour
{
    public Animator anim;
    public SpriteRenderer sprite;
    public float speed;

    public Rigidbody2D rb;

    public bool moveRight;

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

    public float petDistance;

    public GameObject cursor;

    //code for respawning
    public float pointX;
    public float pointY;
    public float pointZ;

    public AudioSource noise;
    public AudioClip commandSound;
    public AudioClip jumpSound;

    public GameObject fade;

    private void Awake()
    {
        player = ReInput.players.GetPlayer(playerID);
        PlayerPrefs.SetFloat("CheckPointX", 567.8f);
        PlayerPrefs.SetFloat("CheckPointY", 142.63f);
        PlayerPrefs.SetFloat("CheckPointZ", 0);
        pointX = PlayerPrefs.GetFloat("CheckPointX");
        pointY = PlayerPrefs.GetFloat("CheckPointY");
        pointZ = PlayerPrefs.GetFloat("CheckPointZ");
        this.transform.position = new Vector2(pointX, pointY);
        ResetPos();
        Physics.SyncTransforms();
        //Physics.SyncTransforms();
        fade.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetButton("Jump") && canJump == true)
        {
            noise.clip = jumpSound;
            noise.Play();
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.velocity += Vector2.up * 7f;
        }

        if (player.GetButton("Jump") && canJump == false)
        {

            anim.SetFloat("Blend", 2);
        }

        if (canJump == false && player.GetAxis("MoveHorz") > 0)
        {
            anim.SetFloat("Blend", 2);
            sprite.flipX = false;
        }

        if (canJump == false && player.GetAxis("MoveHorz") < 0)
        {
            anim.SetFloat("Blend", 2);
            sprite.flipX = true;
        }

        if (player.GetAxis("MoveHorz") > 0 && canJump == true)
        {
            moveRight = true;
            anim.SetFloat("Blend", 1);
            sprite.flipX = false;
        }
        else if (player.GetAxis("MoveHorz") < 0 && canJump == true)
        {
            moveRight = false;
            anim.SetFloat("Blend", 1);
            sprite.flipX = true;
        }

        if (player.GetAxis("MoveHorz") == 0 && moveRight == true && canJump == true)
        {
            anim.SetFloat("Blend", 0);
            sprite.flipX = false;
        }

        if (player.GetAxis("MoveHorz") == 0 && moveRight == false && canJump == true)
        {
            anim.SetFloat("Blend", 0);
            sprite.flipX = true;
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !player.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        canJump = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);

        if (Input.GetKey(KeyCode.M))
        {
            PlayerPrefs.SetFloat("CheckPointX", 567.8f);
            PlayerPrefs.SetFloat("CheckPointY", 142.63f);
            PlayerPrefs.SetFloat("CheckPointZ", 0);
        }
    }

    private void FixedUpdate()
    {
        float translationHorz = player.GetAxis("MoveHorz") * speed;
        translationHorz *= Time.deltaTime;
        transform.Translate(translationHorz, 0, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            gameObject.transform.parent = collision.gameObject.transform;
        }
        if (collision.gameObject.tag == "Floor")
        {
            gameObject.transform.parent = collision.gameObject.transform;
        }
        /*if (collision.gameObject.tag == "Block"){
            Debug.Log("yes");
            topAnim.SetFloat("Blend", 4);
            lowAnim.SetFloat("Blend", 4);
        }*/
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            gameObject.transform.parent = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
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

    void ResetPos()
    {

        pointX = PlayerPrefs.GetFloat("CheckPointX");
        pointY = PlayerPrefs.GetFloat("CheckPointY");
        pointZ = PlayerPrefs.GetFloat("CheckPointZ");
        /*pointX = PlayerPrefs.GetFloat("CheckPointX");
        pointY = PlayerPrefs.GetFloat("CheckPointY");
        pointZ = PlayerPrefs.GetFloat("CheckPointZ");*/
        this.transform.position = new Vector2(pointX, pointY);
        //Physics.SyncTransforms();
    }
}
