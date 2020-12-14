using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewLevel : MonoBehaviour
{

    public void SinglePlayerbutton(string newGameLevel)
    {
        SceneManager.LoadScene(newGameLevel);
    }
}
