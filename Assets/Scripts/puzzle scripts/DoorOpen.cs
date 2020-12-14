using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public GameObject door;
    public HumanSwitch hum;
    public DogSwitch dog;

    public AudioSource openNoise;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hum.isEnabled == true && dog.isEnabled == true){
            StartCoroutine(open());
        }
    }

    IEnumerator open(){
        openNoise.Play();
        yield return new WaitForSeconds(1f);
        door.SetActive(false);
    }
}
