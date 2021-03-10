using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{

    public string targetScene;

    public void SinglePlayerbutton(string newGameLevel)
    {
        StartCoroutine(loadlevel());
        IEnumerator loadlevel()
        {
            yield return new WaitForSeconds(0.5f);
            SceneManager.LoadScene(newGameLevel);
        }
    }
}
