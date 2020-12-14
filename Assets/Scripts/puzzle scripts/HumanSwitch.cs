using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanSwitch : MonoBehaviour
{
    public bool isEnabled;
    public GameObject good;
    public GameObject bad;
    public Sprite normal;
    public Sprite touched;
    public SpriteRenderer rend;

    public AudioSource noise;
    public AudioClip switchOn;
    public AudioClip switchOff;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnabled == false)
        {
            rend.sprite = touched;
        }
        if (isEnabled == true)
        {
            rend.sprite = normal;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player"){
            isEnabled = true;
            good.SetActive(true);
            noise.clip = switchOn;
            noise.Play();
        }
        if (collision.gameObject.tag == "Dog" && isEnabled == false){
            bad.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player"){
            isEnabled = false;
            good.SetActive(false);
            noise.clip = switchOff;
            noise.Play();
        }
        if (collision.gameObject.tag == "Dog" && isEnabled == false)
        {
            bad.SetActive(false);
        }
    }
}
