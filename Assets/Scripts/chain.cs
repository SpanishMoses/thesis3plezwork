using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class chain : MonoBehaviour
{
    public Image img;
    public Sprite frozen;
    public Sprite broke;
    public MainPlayer play;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (play.follow == true){
            img.sprite = frozen;
        }
        if (play.follow == false){
            img.sprite = broke;
        }
    }
}
