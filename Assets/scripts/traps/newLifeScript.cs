using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newLifeScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    private playerManager manager;
    void Start()
    {
        player= GameObject.FindGameObjectWithTag("Player");
        manager = player.GetComponent<playerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(this.transform.position, player.transform.position)<=0.7f)
        {
            Debug.Log("dasd");
            if( manager.lives<3)
            {
                manager.lives++;
                manager.livesImg[manager.lives-1].SetActive(true);
                Destroy(this.gameObject);
            }
        }
    }
}
