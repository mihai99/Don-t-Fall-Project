using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swing : MonoBehaviour
{
    public float delta = 1.5f;  // Amount to move left and right from the start point
    public float speed = 2.0f;
    public float direction = 1;
    private Quaternion startPos;
    public bool X, Y, Z;
    void Start()
    {
        startPos = transform.rotation;
    }
    void Update()
    {
        Quaternion a = startPos;
        if(X)
        {
            a.x += direction * (delta * Mathf.Sin(Time.time * speed));
        }
        if (Y)
        {
            a.y += direction * (delta * Mathf.Sin(Time.time * speed));
        }
        if (Z)
        {
            a.z += direction * (delta * Mathf.Sin(Time.time * speed));
        }
        transform.rotation = a;
    }
}
