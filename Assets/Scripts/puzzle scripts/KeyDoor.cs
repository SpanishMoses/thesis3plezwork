using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{

    public GameObject door;
    public AudioSource openNoise;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player"){
            MainPlayer main = collision.transform.GetComponent<MainPlayer>();
            if (main.gotKey == true){
                main.gotKey = false;
                StartCoroutine(open());
            }
        }
        if (collision.gameObject.tag == "Dog"){
            Dog dog = collision.transform.GetComponent<Dog>();
            if (dog.gotKey == true){
                dog.gotKey = false;
                StartCoroutine(open());
            }
        }
    }

    IEnumerator open(){
        openNoise.Play();
        yield return new WaitForSeconds(1f);
        door.SetActive(false);
    }
}
