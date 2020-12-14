using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public GameObject door;
    public HumanSwitch hum;
    public DogSwitch dog;

    public GameObject noise;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hum.isEnabled == true && dog.isEnabled == true){
            noise.SetActive(true);
            StartCoroutine(open());
        }
    }

    IEnumerator open(){
        
        yield return new WaitForSeconds(1f);
        door.SetActive(false);
    }
}
