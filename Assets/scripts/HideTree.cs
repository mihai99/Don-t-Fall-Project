using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideTree : MonoBehaviour
{
 
    public void SetTree()
    {
        var position = transform.localPosition;
        var otherColliders = Physics.OverlapSphere(position, 5);
        this.GetComponent<MeshRenderer>().enabled = true;
        foreach(var other in otherColliders)
        {
            if(other.transform.name != "Terrain" && other.transform.tag != "Tree")
            {
                this.GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }
}
