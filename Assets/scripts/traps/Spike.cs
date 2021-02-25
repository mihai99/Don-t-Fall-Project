using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private float FallDistance;
    private GameObject player;
    private bool fallen;
    // Start is called before the first frame update
    void Start()
    {
        this.FallDistance = Random.Range(4, 6);
        player = GameObject.FindGameObjectWithTag("Player");
        Reset();
  
    }
    private void OnCollisionEnter(Collision collision)
    {
          
            this.gameObject.GetComponent<Collider>().enabled = false;
     
           Invoke("Reset", 1);
        
    }
    private void Reset()
    {
        this.GetComponent<MeshRenderer>().enabled = false; 
        this.GetComponent<Rigidbody>().useGravity = false;
        this.gameObject.GetComponent<Collider>().enabled = true;
        this.fallen = false;
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.transform.position = new Vector3(
            this.transform.position.x,
            15,
            this.transform.position.z);
    }
    //Update is called once per frame
    void Update()
    {
        //if(player == null)
        //     player = GameObject.FindGameObjectWithTag("player");

        transform.Rotate(0, 360 * Time.deltaTime, 0);
        Vector2 thisPos2 = new Vector2(this.transform.position.x, this.transform.position.z);
        Vector2 playerPos2 = new Vector2(player.transform.position.x, player.transform.position.z);
        if(!fallen && Vector2.Distance(thisPos2, playerPos2)<FallDistance)
        {
            this.GetComponent<MeshRenderer>().enabled = true;
            this.GetComponent<Rigidbody>().useGravity = true;
            this.fallen = true;
        }
        Mathf.Clamp(this.GetComponent<Rigidbody>().velocity.y, 0, 10);

    }
}
