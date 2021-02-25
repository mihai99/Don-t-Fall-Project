using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap_needle : MonoBehaviour
{
    public float time;
    private Component[] animators;
    // Start is called before the first frame update
    void Start()
    {
        animators = GetComponentsInChildren<Animation>();
        playHide();
    }
    void playHide()
    {
        foreach (Animation anim in animators)
            anim.Play("Anim_TrapNeedle_Hide");
         Invoke("playShow", time);
    }
    void playShow()
    {
        foreach (Animation anim in animators)
            anim.Play("Anim_TrapNeedle_Show");
         Invoke("playHide", time);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
