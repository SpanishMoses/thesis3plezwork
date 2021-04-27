using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nextLevel : MonoBehaviour
{
    public string newLevel;

    public void SinglePlayerbutton()
    {
        PlayerPrefs.SetFloat("CheckPointX", 192.61f);
        PlayerPrefs.SetFloat("CheckPointY", 8.25f);
        PlayerPrefs.SetFloat("CheckPointZ", 0);
        SceneManager.LoadScene(newLevel);
    }
}
