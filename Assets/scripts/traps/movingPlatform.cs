using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatform : MonoBehaviour
{
    public  enum MovementMode{
        Horizontal, 
        Vertical,
        UpDown
    };
    public MovementMode mode = MovementMode.Horizontal;
    public float speed;
    public float distance;
    private Vector3 start, end;
    private Vector3 startVec, endVec;
    private bool dir;
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.transform.tag == "Player")
        {
            collision.gameObject.transform.parent = this.transform;

        }
    }
     private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.transform.tag == "Player")
        {
            collision.gameObject.transform.parent = null;

        }
    }
    void Start()
    {
        dir = false;
        if(mode == MovementMode.Horizontal) 
        {
            start = transform.localPosition + new Vector3(-distance, 0, 0);
            end = transform.localPosition+ new Vector3(distance, 0, 0);
        }
        else if(mode == MovementMode.Vertical)
        {
            start = transform.localPosition + new Vector3(0, 0, -distance);
            end = transform.localPosition + new Vector3(0, 0, distance);
        }
        else
        {
            start = transform.localPosition + new Vector3(0, -distance, 0);
            end = transform.localPosition + new Vector3(0, distance, 0);
        }
        startVec = start-transform.localPosition;
        endVec = end-transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(dir==false)
            if(Vector3.Distance(transform.localPosition, start) >=0.5f)
                this.transform.Translate(startVec*Time.deltaTime*speed);
            else dir = true;
        if(dir==true)
            if(Vector3.Distance(transform.localPosition, end) >=0.5f)
                this.transform.Translate(endVec*Time.deltaTime*speed);
            else dir = false;  
    }
}
