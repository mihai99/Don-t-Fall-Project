using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class timerScript : MonoBehaviour
{
    public float timeStart;
    public GameObject timer;
    public float timePassed;
    private Text textUI;
    // Start is called before the first frame update
    void Start(){
        textUI =  timer.GetComponent<Text>();
    }
    

    // Update is called once per frame
    void Update()
    {
        timePassed = Time.time-timeStart;
        int minutesPassed = (int)timePassed/60;
        float secondsPassed = timePassed%60;

        textUI.text = (minutesPassed>10?minutesPassed.ToString():"0"+minutesPassed.ToString()) + ":" + (secondsPassed>10.0f?secondsPassed.ToString("F1"):"0"+secondsPassed.ToString("F1"));
    }
}
