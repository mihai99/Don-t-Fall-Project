using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CatManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        GetComponent<NavMeshAgent>().destination = GameObject.FindGameObjectWithTag("Player").transform.position;
        
    }
}
