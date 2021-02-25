using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cannonFire : MonoBehaviour
{
    public GameObject barell;
    public GameObject player;
    public GameObject projectile;
    public float speed;
    public float distanceToPlayer;
    public float rate;
    private float timer;
    public bool fixedFire;
    // Start is called before the first frame update
    void Start()
    {
        if(!fixedFire)
        {
            distanceToPlayer = Random.Range(10, 20);
            rate = Random.Range(0.3f, 0.5f);
            speed = Random.Range(12, 17);
        }
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void fire()
    {
        if(timer<=0.1f)
        {
            GameObject bullet = (GameObject)Instantiate(projectile, barell.transform.position + new Vector3(0, 0.75f,0), transform.rotation);
            bullet.transform.forward = this.transform.forward;
            bullet.GetComponent<bulletDestroy>().speed=speed;
            timer = rate;
        }
        else
            timer-= Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position)<= distanceToPlayer)
            fire();
    }
}
