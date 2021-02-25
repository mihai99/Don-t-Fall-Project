using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpTrigger : MonoBehaviour
{
    public GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void Jump()
    {
        player.GetComponent<SimpleCharacterControl>().JumpingAndLanding();
    }
}
