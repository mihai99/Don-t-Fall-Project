using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialButtonHandler : MonoBehaviour
{
 
    public GameObject TutotialButton;
    public levelsCompletedHandler saver;
    private void Start()
    {
        saver = GameObject.FindGameObjectWithTag("saver").GetComponent<levelsCompletedHandler>();
        OnEnable();
    }
    private void OnEnable()
    {
        if (saver.tutorialDone == true || saver.lastLevel > 0)
            this.TutotialButton.SetActive(true);
        else
            this.TutotialButton.SetActive(false);
    }
}
