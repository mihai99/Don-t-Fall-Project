using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddeRestartOnTutorialLevel : MonoBehaviour
{
    public List<GameObject> GameObjects;
    private void Start()
    {       
        OnEnable();
    }
    private void OnEnable()
    {
        if (GameObject.FindGameObjectsWithTag("tutorialLevel").Length > 0)
        {
            GameObjects.ForEach(x => x.SetActive(false));
        }
        else
        {
            GameObjects.ForEach(x => x.SetActive(true));
        }
    }
    public void ForceShow()
    {
       
       GameObjects.ForEach(x => x.SetActive(true));       
    }
}
