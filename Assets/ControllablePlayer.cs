using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class ControllablePlayer : MonoBehaviour
{

    [SerializeField] private int playerID = 0;
    [SerializeField] private Player player;

    public bool usingHuman;
    public bool usingDog;

    public GameObject vCam1;
    public GameObject vCam2;

    public PlayerMove human;
    public DogMove dog;

    // Start is called before the first frame update
    void Start()
    {
        player = ReInput.players.GetPlayer(playerID);
        usingHuman = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (usingHuman == true){
            dog.enabled = false;
            human.enabled = true;
            usingDog = false;
            vCam1.SetActive(true);
            vCam2.SetActive(false);
        }

        if (usingDog == true){
            dog.enabled = true;
            human.enabled = false;
            usingHuman = false;
            vCam1.SetActive(false);
            vCam2.SetActive(true);
        }

        
    }
}
