using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraSwap : MonoBehaviour
{
    public CameraPosition swapPosition = CameraPosition.Front;
    private CameraPosition oldPosition = CameraPosition.Front;
    private cameraFollow camScript;

    // Start is called before the first frame update
    void Start()
    {
        camScript = Camera.main.GetComponent<cameraFollow>();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            this.oldPosition = camScript.cameraPosition;
            camScript.cameraPosition = this.swapPosition;
        }
    }
    private void OnTriggerExit(Collider other)
    {
      
            if (other.transform.tag == "Player")
            {
            camScript.cameraPosition = oldPosition;
            }
        
    }


}
