using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillAfterTime : MonoBehaviour
{
    public float Time;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Kill", Time);
    }

    // Update is called once per frame
    void Kill()
    {
        Destroy(this.gameObject);
    }
}
