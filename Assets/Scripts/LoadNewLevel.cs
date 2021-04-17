using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewLevel : MonoBehaviour
{
    public string newLevel;

    public void SinglePlayerbutton()
    {
        SceneManager.LoadScene(newLevel);
    }
}
