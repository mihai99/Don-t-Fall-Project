using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap_hammer : MonoBehaviour
{
    public GameObject player;
    public float distance;
    private Animation anim;
    // Start is called before the first frame update
    void Start()
    {
         player = GameObject.FindGameObjectsWithTag("Player")[0];
        anim = this.GetComponent<Animation>();
        anim.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(player.transform.position, transform.position);
        if(dist<=distance)
            anim.Play("Anim_HammerTrap_Play");
       
        
    }
}
