using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableIfNotDevelopment : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       if(!Debug.isDebugBuild)
        {
            this.gameObject.SetActive(false);
        }  
    }
}
