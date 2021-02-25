using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    void StartGravity()
    {
        this.GetComponent<Rigidbody>().useGravity = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }
    void Start()
    {
        Invoke("StartGravity", 1);
        Invoke("Destroy(this.gameObject)", 10);
        if(GameObject.FindGameObjectWithTag("Player").GetComponent<playerManager>().invTimerObj.activeSelf == true)
        {
            GetComponent<Collider>().enabled = false;
        }
    }
  
    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, speed*Time.deltaTime);
    }
}
