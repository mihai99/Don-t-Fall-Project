using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class tutorialManager : MonoBehaviour
{

    public GameObject storyPoint, chestPoint, powerupsPoint, revivePoint, starPoint, dimondPoint, lifePoint, winPoint, trapPoint;
    public bool controls, story, powerups, chest, revive, star, dimond, life, win, trap;
    public bool showText;
    public GameObject text;
    public RectTransform transform;
    public RectTransform TutorialTitle;
    // Start is called before the first frame update
    void Start()
    {
        controls = false;
        story = false;
        chest = false;
        powerups = false;
        revive = false;
        star = false;
        dimond = false;
        life = false;
        win = false;
        trap = false;
        showText = false;
        ShowControls();
    }

    void ShowControls()
    {
        if(!controls && !showText)
        {
            text.GetComponent<Text>().text = "Use the joystick on the left to move, tap on the right to jump!";
            controls = true;
            showText = true;
            Invoke("Hide", 2);
        }
    }
    void ShowChest()
    {
        if (!chest && !showText)
        {
            text.GetComponent<Text>().text = "You'll find some powerups in this chest, use them wisely";
            chest = true;
            showText = true;
            Invoke("Hide", 2);
        }
    }
    void ShowPowerups()
    {
        if (!powerups && !showText)
        {
            text.GetComponent<Text>().text = "If the level gets too hard, try using a powerup like the shield from the left menu to make it easier.";
            powerups = true;
            showText = true;
            Invoke("Hide", 2);
        }
    }
    void ShowStory()
    {
        if (!story && !showText)
        {
            text.GetComponent<Text>().text = "Catch your running cat at the end of the level";
            story = true;
            showText = true;
            Invoke("Hide", 2);
        }
    }
    void ShowRevive()
    {
        if (!revive && !showText)
        {
            text.GetComponent<Text>().text = "You'll respawn here if you die!";
            showText = true;
            revive = true;
            Invoke("Hide", 2);
        }
    }
    void ShowStar()
    {
        if (!star && !showText)
        {
            text.GetComponent<Text>().text = "Gather the stars as you go through the level!";
            showText = true;
            star = true;
            Invoke("Hide", 2);
        }
    }
    void ShowDimond()
    {
        if (!dimond && !showText)
        {
            text.GetComponent<Text>().text = "Gather dimonds to buy premium powerups and skins. Dimonds are harder to get!";
            showText = true;
            dimond = true;
            Invoke("Hide", 2);
        }
    }
    void ShowLife()
    {
        if (!life && !showText)
        {
            text.GetComponent<Text>().text = "This heart will get you one more life!";
            showText = true;
            life = true;
            Invoke("Hide", 2);
        }
    }
    void ShowWin()
    {
        if (!win && !showText)
        {
            text.GetComponent<Text>().text = "You win if you get to the chest! \n WELL DONE!!! \n Get ready for the real fun";
            showText = true;
            win = true;
            Invoke("Hide", 2);
            Invoke("FinishedTutorial", 2);
        }
    }
    void ShowTrap()
    {
        if (!life && !showText)
        {
            text.GetComponent<Text>().text = "Avoid the traps at all cost!";
            showText = true;
            trap = true;
            Invoke("Hide", 2);
        }
    }

    public void Hide()
    {
        showText = false;
    }
    void FinishedTutorial()
    {
        GameObject.FindGameObjectWithTag("canvas").GetComponent<GameManager>().nextLevel();
        GameObject.FindGameObjectWithTag("saver").GetComponent<levelsCompletedHandler>().tutorialDone = true;
        GameObject.FindGameObjectWithTag("saver").GetComponent<levelsCompletedHandler>().SaveLevels();
    }
    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (!revive && Vector3.Distance(player.transform.position, revivePoint.transform.position) < 2)
            ShowRevive();
        else if (!star && Vector3.Distance(player.transform.position, starPoint.transform.position) < 2)
            ShowStar();
        else if (!dimond && Vector3.Distance(player.transform.position, dimondPoint.transform.position) < 5)
            ShowDimond();
        else if(!life && Vector3.Distance(player.transform.position, lifePoint.transform.position) < 2)
            ShowLife();
        else if (!trap && Vector3.Distance(player.transform.position, trapPoint.transform.position) < 5)
            ShowTrap();
        else if (!win && Vector3.Distance(player.transform.position, winPoint.transform.position) < 2)
            ShowWin();

        if(win == true && TutorialTitle.localPosition.y < 1200)
        {
            TutorialTitle.localPosition += new Vector3(0, 100 * Time.deltaTime, 0);
        }
        else if(win == false && TutorialTitle.localPosition.y > 480)
        {
            TutorialTitle.localPosition -= new Vector3(0, 1200 * Time.deltaTime, 0);
        }
       
        if (showText && transform.sizeDelta.x <= 1200)
            transform.sizeDelta += new Vector2(4800 * Time.deltaTime, 0);
        if (!showText && transform.sizeDelta.x >= 0)
            transform.sizeDelta += new Vector2(-4800 * Time.deltaTime, 0);
    }
}
