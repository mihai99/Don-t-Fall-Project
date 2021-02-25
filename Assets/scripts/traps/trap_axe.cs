using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap_axe : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Animation>()["Anim_GreatAxeTrap_Play"].speed = 0.75f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
