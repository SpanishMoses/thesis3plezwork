﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.UI;

public class MainPlayer : MonoBehaviour
{
    //good jump code from https://www.youtube.com/watch?v=7KiK0Aqtmzc&ab_channel=BoardToBitsGames
    //throwing script help from https://www.youtube.com/watch?v=3DUmpVi82q8&t=176s&ab_channel=TheGameGuy

    public Dog dog;

    public Animator topAnim;
    public Animator lowAnim;
    public SpriteRenderer topSprite;
    public SpriteRenderer lowSprite;

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

    public bool gotKey;
    public GameObject keyText;

    // Start is called before the first frame update
    void Start()
    {
        player = ReInput.players.GetPlayer(playerID);
        gotKey = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*float translationHorz = player.GetAxis("MoveHorz") * speed;
        translationHorz *= Time.deltaTime;
        transform.Translate(translationHorz, 0, 0);*/

        if (player.GetButton("Jump") && canJump == true){
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.velocity += Vector2.up * 7f;
            
        }

        if (player.GetButton("ComeBack")){
            dog.currTarget = dog.playerTarget.transform;
            dog.isFollowingPlayer = true;
        }

        if (player.GetButton("DogAction") && dog.canDig == true && dog.startDig == false){
            dog.startDig = true;
        }

        if (player.GetButton("DogAction") && dog.canBite == true && dog.startBite == false){
            dog.startBite = true;
        }

        if (player.GetAxis("MoveHorz") > 0 && canJump == true)
        {
            moveRight = true;
            topAnim.SetFloat("Blend", 1);
            lowAnim.SetFloat("Blend", 1);
            topSprite.flipX = false;
            lowSprite.flipX = false;
        }
        else if (player.GetAxis("MoveHorz") < 0 && canJump == true)
        {
            moveRight = false;
            topAnim.SetFloat("Blend", 1);
            lowAnim.SetFloat("Blend", 1);
            topSprite.flipX = true;
            lowSprite.flipX = true;
        }

        if (player.GetAxis("MoveHorz") == 0 && moveRight == true && canJump == true){
            topAnim.SetFloat("Blend", 0);
            lowAnim.SetFloat("Blend", 0);
            topSprite.flipX = false;
            lowSprite.flipX = false;
        }

        if (player.GetAxis("MoveHorz") == 0 && moveRight == false && canJump == true)
        {
            topAnim.SetFloat("Blend", 0);
            lowAnim.SetFloat("Blend", 0);
            topSprite.flipX = true;
            lowSprite.flipX = true;
        }

        if (rb.velocity.y < 0){
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        } else if (rb.velocity.y > 0 && !player.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        canJump = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);

    }

    private void FixedUpdate()
    {
        float translationHorz = player.GetAxis("MoveHorz") * speed;
        translationHorz *= Time.deltaTime;
        transform.Translate(translationHorz, 0, 0);

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
    }
}
