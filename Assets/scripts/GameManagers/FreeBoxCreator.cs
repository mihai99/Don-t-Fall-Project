using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeBoxCreator : MonoBehaviour
{
    public GameObject Chest;
    // Start is called before the first frame update
    void Start()
    {
        var chance = Random.Range(0,20);
        if (chance <= 2)
            Instantiate(Chest, transform.position + new Vector3(0, 1, 0), Quaternion.Euler(-90, 0, -90), transform.parent);
    }
}
