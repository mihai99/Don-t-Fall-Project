using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reviveActivation : MonoBehaviour
{
    public GameObject player;
    public bool active;
   
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];       
        
    }
 
    void activateRespawn()
    {
        this.GetComponent<MeshRenderer>().enabled = true;
        active = true;
        if( player.GetComponent<playerManager>().respawn != null)
            GameObject.FindGameObjectWithTag("canvas").GetComponent<AlertManager>().SetAlert(AlertManager.AlertTypes.NewRevivePointAlert);
        player.GetComponent<playerManager>().oldCameraPosition = Camera.main.GetComponent<cameraFollow>().cameraPosition;
        player.GetComponent<playerManager>().respawn = (GameObject)this.gameObject;
        
    }
    // Update is called once per frame
    void Update()
    {
        try
        {
            if (Vector3.Distance(player.transform.position, this.transform.position) <= 1.0f && !active)
                activateRespawn();
        }
        catch { }
    }
}
