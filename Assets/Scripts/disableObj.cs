using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class disableObj : MonoBehaviour
{
    public string targetScene;
    public GameObject disable;
    public GameObject enable;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void doIt(){
        disable.SetActive(false);
        enable.SetActive(true);
    }

    public void SinglePlayerbutton()
    {
        StartCoroutine(loadlevel());
        IEnumerator loadlevel()
        {
            yield return new WaitForSeconds(0.1f);
            SceneManager.LoadScene(targetScene);
        }
    }
}
