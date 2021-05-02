using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.SceneManagement;

public class mainmenustuff : MonoBehaviour
{

    [SerializeField] private int playerID = 0;
    [SerializeField] private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = ReInput.players.GetPlayer(playerID);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetButton("quit"))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.L)){
            SceneManager.LoadScene("level 3 cutscene");
        }
    }
}
