using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishLevel : MonoBehaviour
{
    public GameObject finishedLogo;
    public GameObject fade;

    public GameObject mus1;
    public GameObject mus2;
    public AudioClip cheerMus;

    public bool playerCrossed;
    public bool dogCrossed;

    public float time;
    public float maxTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCrossed == true && dogCrossed == true){
            mus1.SetActive(false);
            mus2.SetActive(true);
            finishedLogo.SetActive(true);
            time += Time.deltaTime;
            if (time >= maxTime){
                fade.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerCrossed = true;
        }

        if (collision.gameObject.tag == "Dog")
        {
            dogCrossed = true;
        }
    }
}
