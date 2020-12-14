using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.UI;

public class Dog : MonoBehaviour
{
    //from https://www.youtube.com/watch?v=jvtFUfJ6CP8&t=301s
    // go to closest target from https://www.youtube.com/watch?v=VH-bUST_w0o&ab_channel=IndieGamesDev

    //public GameObject cursorPoint;

    public Transform myLocation;

    public bool isFollowingPlayer;

    public GameObject playerTarget;
    //public GameObject[] testTargets;
    public GameObject cursorTarget;

    public Transform currTarget;

    public Animator anim;
    public SpriteRenderer sprite;
    public bool moveRight;

    public float speed;
    public float nextWaypointDistance = 5f;
    public float spawnPoopTime;

    Path path;
    int currentWaypoint = 0;
    public bool reachedEndOfPath = false;

    public Seeker seeker;
    public Rigidbody2D rb;

    public bool isGrounded;
    public Transform groundCheckPoint;
    public float groundCheckRadius;
    public LayerMask groundLayer;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    public CircleCollider2D circle;

    int playerLayer, platformLayer;

    public LayerMask jumpLayer;

    public float rayDist;
    public LayerMask layer;

    public GameObject exclamation;
    public bool canDig;
    public bool startDig;

    public bool canBite;
    public bool startBite;

    public bool gotKey;
    public GameObject keyText;

    public AudioSource noise;
    public AudioClip keyNoise;

    //public Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        //SearchTargets();
        InvokeRepeating("UpdatePath", 0f, .5f);
        gotKey = false;
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(rb.position, currTarget.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if (rb.velocity.x < 0.1f)
        {
            moveRight = true;
            anim.SetFloat("Blend", 1);
            sprite.flipX = false;
        }
        else if (rb.velocity.x > 0.1f)
        {
            moveRight = false;
            anim.SetFloat("Blend", 1);
            sprite.flipX = true;
        }

        if (rb.velocity.x >= 0 && moveRight == true)
        {
            anim.SetFloat("Blend", 0);
            sprite.flipX = true;
        }

        if (rb.velocity.x <= 0 && moveRight == false)
        {
            anim.SetFloat("Blend", 0);
            sprite.flipX = false;
        }

        if (rb.velocity.y < 0 || currTarget.position.x > 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        if (rb.velocity.x > 0 && isGrounded == true){
            RaycastHit2D ray3 = Physics2D.Raycast(transform.position, -Vector2.left, rayDist, jumpLayer);
            if (ray3.collider != null)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.velocity += Vector2.up * 14f;
                anim.SetFloat("Blend", 3);
                Debug.Log("erp");
            }
        } else if (rb.velocity.x < 0 && isGrounded == true){
            RaycastHit2D ray2 = Physics2D.Raycast(transform.position, Vector2.left, rayDist, jumpLayer);
            if (ray2.collider != null)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.velocity += Vector2.up * 14f;
                anim.SetFloat("Blend", 3);
                Debug.Log("erp");
            }
        }

        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);
        /*if (isGrounded == false){
            rb.AddForce(Vector2.up * 400f);
        }*/

        if (isGrounded == false){
            anim.SetFloat("Blend", 3);
        }

        if (isGrounded == true)
        {
            RaycastHit2D ray = Physics2D.Raycast(transform.position, -Vector2.up, rayDist, layer);
            if (ray.collider == null)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.velocity += Vector2.up * 7f;
                anim.SetFloat("Blend", 3);
                Debug.Log("erp");
            }
            /*RaycastHit2D ray2 = Physics2D.Raycast(transform.position, Vector2.left, rayDist, jumpLayer);
            if (ray2.collider != null)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.velocity += Vector2.up * 14f;
                Debug.Log("erp");
            }
            RaycastHit2D ray3 = Physics2D.Raycast(transform.position, -Vector2.left, rayDist, jumpLayer);
            if (ray3.collider != null)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.velocity += Vector2.up * 14f;
                Debug.Log("erp");
            }*/
        }
        Debug.DrawRay(transform.position, -Vector2.up, Color.red, rayDist);
        Debug.DrawRay(transform.position, Vector2.left, Color.red, rayDist);
        Debug.DrawRay(transform.position, -Vector2.left, Color.red, rayDist);

        if (startDig == true){
            canDig = false;
            StartCoroutine(begindig());
        }

        if (startBite == true){
            canBite = false;
            StartCoroutine(beginbite());
        }

        if (gotKey == false){
            keyText.SetActive(false);
        }
    }

    IEnumerator begindig(){
        anim.SetFloat("Blend", 2);
        yield return new WaitForSeconds(1);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f);

        foreach (Collider2D near in colliders){
            DigSpot dig = near.GetComponent<DigSpot>();
            if (dig != null && dig.dug == false){
                //Instantiate(dig.obj, dig.transform.position, Quaternion.identity);
                dig.spawn();
                dig.dug = true;
                currTarget = transform;
            }
        }
        startDig = false;
    }

    IEnumerator beginbite(){
        yield return new WaitForSeconds(1);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f);
        foreach (Collider2D near in colliders)
        {
            Rope rope = near.GetComponent<Rope>();
            if (rope != null)
            {
                //Instantiate(dig.obj, dig.transform.position, Quaternion.identity);
                rope.disable = true;
                currTarget = transform;
                //Destroy(near.gameObject);
            }
        }
        startBite = false;
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            gameObject.transform.parent = collision.gameObject.transform;
            //currTarget = transform;
        }
        /*if (collision.gameObject.tag == "Floor")
        {
            gameObject.transform.parent = collision.gameObject.transform;

        }*/
        if (collision.gameObject.tag == "Key"){
            gotKey = true;
            keyText.SetActive(true);
            noise.clip = keyNoise;
            noise.Play();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Floor")
        {
            gameObject.transform.parent = collision.gameObject.transform;
        }
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
        /*if (collision.gameObject.tag != "Player"){
            rb.AddForce(Vector2.up * 200f);
        }
        if (collision.gameObject.tag == "Floor")
        {
            rb.AddForce(Vector2.up * 70f);
        }
        if (collision.gameObject.tag == "Platform")
        {
            rb.AddForce(Vector2.up * 70f);
        }*/

        if (collision.gameObject.tag == "Dig"){
            exclamation.SetActive(true);
            canDig = true;
        }

        if (collision.gameObject.tag == "Rope"){
            exclamation.SetActive(true);
            canBite = true;
        }
        /*if (collision.gameObject.tag == "Point"){
            currTarget = transform;
        }*/
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Dig")
        {
            exclamation.SetActive(false);
            canDig = false;
        }
        if (collision.gameObject.tag == "Rope")
        {
            exclamation.SetActive(false);
            canBite = false;
        }

    }
}
