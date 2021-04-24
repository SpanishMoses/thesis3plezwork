using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changescenes : MonoBehaviour
{
    public string targetScene;

    public void SinglePlayerbutton()
    {
        StartCoroutine(loadlevel());
        IEnumerator loadlevel()
        {
            yield return new WaitForSeconds(0.5f);
            SceneManager.LoadScene(targetScene);
        }
    }
}
