using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleportSoundFix : MonoBehaviour
{
    public AudioSource continuousSound;
    public float distance;
    private GameObject player;
    private bool active;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (GetComponent<CustomTeleporter>())
            active = GetComponent<CustomTeleporter>().instantTeleport == true;
        else active = true;
        continuousSound.volume = 0;
    }

    // Update is called once per frame
    void Update()
    {
       
        float realDist = Vector3.Distance(this.transform.position, player.transform.position); 
        
        if( realDist < 5)
        {
            continuousSound.Play();
            if(active)
                continuousSound.volume= (5-realDist)/10;
        }    
        else
            continuousSound.Stop();
         
    
    }
}
