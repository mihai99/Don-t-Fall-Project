using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class gameMenuHandler : MonoBehaviour
{
    
    public enum ButtonMode {
        restart, 
        home,
        startTutorial,
        startTutorialFromMenu,
        CloseGame
    }
    public GameObject levelsMenu;
    public GameObject menuUI;
    public GameObject menuUIText;
    public ButtonMode selectedOption = ButtonMode.home;
    private RectTransform menuPosition;
    private bool openMenu;
    public GameObject CancelButton, OkButton;
    private void Start() {
        openMenu = false;
      
        this.menuPosition = this.menuUI.GetComponent<RectTransform>();
        this.menuPosition.sizeDelta = new Vector2(-1000,100);
    }
    private void SetButtonsPosition(bool trueOnRight)
    {
        if(trueOnRight)
        {
            CancelButton.GetComponent<RectTransform>().localPosition = new Vector3(-200, -150, 0);
            OkButton.GetComponent<RectTransform>().localPosition = new Vector3(200, -150, 0);
        }
        else
        {
            CancelButton.GetComponent<RectTransform>().localPosition = new Vector3(200, -150, 0);
            OkButton.GetComponent<RectTransform>().localPosition = new Vector3(-200, -150, 0);
        }
    }
    public void OpenMenu()
    {
        openMenu = true;
        GameObject.FindGameObjectWithTag("greyScreen").GetComponent<greyScreenHandler>().Show();
    }
    public void CloseMenu()
    {
        GameObject.FindGameObjectWithTag("greyScreen").GetComponent<greyScreenHandler>().Hide();
        if (selectedOption == ButtonMode.startTutorial)
        {
            levelsMenu.GetComponent<menuDisplayHandler>().ShowAfterCloseTutorialQuestion();
        }
        openMenu = false;
    }
    public void HomeButton()
    {
        SetButtonsPosition(false);
        menuUIText.GetComponent<Text>().text = "Go back to main menu?";
        selectedOption = ButtonMode.home;
        OpenMenu();
    }
   
    public void RestartButton()
    {
        SetButtonsPosition(true);
        menuUIText.GetComponent<Text>().text = "Restart this level?";
        selectedOption = ButtonMode.restart;
        OpenMenu();
    }

    public void CloseGame()
    {
        SetButtonsPosition(false);
        menuUIText.GetComponent<Text>().text = "Do you really want to quit?";
        selectedOption = ButtonMode.CloseGame;
        OpenMenu();
    }
    public void StartTutorial()
    {
        SetButtonsPosition(true);
        menuUIText.GetComponent<Text>().text = "Do you wanna play the tutorial?";
        selectedOption = ButtonMode.startTutorial;
        OpenMenu();
    }
    public void StartTutorialFromMenuButton()
    {
        SetButtonsPosition(true);
        menuUIText.GetComponent<Text>().text = "Do you wanna play the tutorial?";
        selectedOption = ButtonMode.startTutorialFromMenu;
        OpenMenu();
    }
    public void YeahButton()
    {
        GameObject.FindGameObjectWithTag("greyScreen").GetComponent<greyScreenHandler>().Hide();
        this.openMenu = false;
        if (this.selectedOption == ButtonMode.restart)
        {
            this.GetComponent<GameManager>().restart();
        }
        else if(this.selectedOption == ButtonMode.home)
        {
            this.GetComponent<GameManager>().backToMenu();
        }
        else if (this.selectedOption == ButtonMode.startTutorial)
        {
            this.GetComponent<GameManager>().initLevel(-1, false);
        }
        else if(this.selectedOption == ButtonMode.startTutorialFromMenu)
        {
            this.GetComponent<GameManager>().initLevel(-1, false);
        }
        else if (this.selectedOption == ButtonMode.CloseGame)
        {
            this.GetComponent<ExitHandler>().QuitGame();
        }
    }
   private void FixedUpdate() {

        if (openMenu && menuPosition.localPosition.y > 0)
            menuPosition.transform.position = Vector3.MoveTowards(
               menuPosition.transform.position,
               new Vector3(menuPosition.position.x, 0, menuPosition.position.z),
               60);
        if (!openMenu && menuPosition.localPosition.y < 1200)
            menuPosition.transform.Translate(0, 3000 * Time.deltaTime, 0);


    }

    
}
