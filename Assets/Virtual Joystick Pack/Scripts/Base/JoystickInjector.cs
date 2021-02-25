using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickInjector : MonoBehaviour
{
    private void OnEnable()
    {
        foreach(GameObject playerObject in GameObject.FindGameObjectsWithTag("Player"))
        {
            playerObject.GetComponent<SimpleCharacterControl>().Joystick = this.GetComponent<Joystick>();
        }
    }
  
}
