using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class menuDisplayHandler : MonoBehaviour
{  
    public bool showMoney;
    public GameObject moneyText, dimondText;
    public bool isPowerupsMenu;
    public bool isLevelsMenu;
    public bool isHighScoresMenu;
    public bool isDimondsShop;
    private inventoryHandler inventoryScript;
    private RectTransform rectTransform;
    public bool shown;
  
    void Start()
    {      

        if(showMoney)
        {
            inventoryScript = GameObject.FindGameObjectWithTag("saver").GetComponent<inventoryHandler>();
        }
        
        shown = false;
        rectTransform = this.GetComponent<RectTransform>();
    
        rectTransform.localPosition = new Vector3(0, 1200, 0);
    }
    public void Show(bool blockBypassOnDimonds)
    {
        GameObject.FindGameObjectWithTag("greyScreen").GetComponent<greyScreenHandler>().Show();
        this.gameObject.SetActive(true);
        shown = true;
        if (isDimondsShop)
        {
            GameObject.FindGameObjectWithTag("NotEnoughMoney").GetComponent<NotEnoughMoney>().CanBypass = blockBypassOnDimonds;
            GameObject.FindGameObjectWithTag("NotEnoughDimonds").GetComponent<NotEnoughDimonds>().CanBypass = blockBypassOnDimonds;
        }

        if (isLevelsMenu)
        {
            levelsCompletedHandler saver = GameObject.FindGameObjectWithTag("saver").GetComponent<levelsCompletedHandler>();
            if (saver.tutorialDone == false && saver.lastLevel == 0)
            {
                GameObject.FindGameObjectWithTag("canvas").GetComponent<gameMenuHandler>().StartTutorial();
                shown = false;
            }
           
            this.GetComponent<levelsMenuHandler>().SetUp();
        }
        if (isPowerupsMenu)
        {
            GameObject.FindGameObjectWithTag("canvas").GetComponent<powerupsHandler>().SetOwnedTexts();
        }

        if(isHighScoresMenu)
        {
            Debug.Log("draw highscores");
        }

        if(showMoney)
        {
            UpdateMoneyTexts();  
        }
      
       
    }
    public void UpdateMoneyTexts()
    {
        this.dimondText.GetComponent<Text>().text = inventoryScript.dimonds.ToString();
        this.moneyText.GetComponent<Text>().text = inventoryScript.money.ToString();
    }
    public void ShowAfterCloseTutorialQuestion()
    {
        GameObject.FindGameObjectWithTag("greyScreen").GetComponent<greyScreenHandler>().Show();
        shown = true;
    }
    public void Hide(bool HideGreyScreen)
    {
        if(this.shown == true)
        {
            if (!HideGreyScreen)
            {
                if(isDimondsShop)
                {
                    GameObject.FindGameObjectWithTag("greyScreen").GetComponent<greyScreenHandler>().Hide();
                    GameObject.FindGameObjectWithTag("OverlayGreyScreen").GetComponent<greyScreenHandler>().Hide();
                    GameObject.FindGameObjectWithTag("NotEnoughMoney").GetComponent<NotEnoughMoney>().CanBypass = true;
                    GameObject.FindGameObjectWithTag("NotEnoughDimonds").GetComponent<NotEnoughDimonds>().CanBypass = true;
                }
                else
                {
                    GameObject.FindGameObjectWithTag("greyScreen").GetComponent<greyScreenHandler>().Hide();

                }
            }
            shown = false;
        }
     
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (shown && rectTransform.localPosition.y > 0)
            rectTransform.transform.position = Vector3.MoveTowards(
                rectTransform.transform.position,
                new Vector3(rectTransform.position.x, 0, rectTransform.position.z),
                60);
        if(!shown && rectTransform.localPosition.y<1200)
            rectTransform.transform.Translate(0, 3000*Time.deltaTime, 0);    
        
        if(shown && isDimondsShop)
        {
            if (GetComponent<dimondShopHandler>().AdManager.RewardedIsLoaded() == false)
            {
                GetComponent<dimondShopHandler>().DimondsForAddButton
                    .transform.GetChild(0).gameObject
                    .GetComponent<Image>().color = new Color(0.4f, 0.4f, 0.4f);
                GetComponent<dimondShopHandler>().DimondsForAddButton
                    .GetComponent<Image>().color = new Color(0.4f, 0.4f, 0.4f);
                GetComponent<dimondShopHandler>().DimondsForAddButton
                    .GetComponent<Button>().interactable = false;

                GetComponent<dimondShopHandler>().MoneyForAddButton
                   .transform.GetChild(0).gameObject
                   .GetComponent<Image>().color = new Color(0.4f, 0.4f, 0.4f);
                GetComponent<dimondShopHandler>().MoneyForAddButton
                    .GetComponent<Image>().color = new Color(0.4f, 0.4f, 0.4f);
                GetComponent<dimondShopHandler>().MoneyForAddButton
                    .GetComponent<Button>().interactable = false;
            }
            else
            {
                GetComponent<dimondShopHandler>().DimondsForAddButton
                  .transform.GetChild(0).gameObject
                  .GetComponent<Image>().color = new Color(1, 1, 1);
                GetComponent<dimondShopHandler>().DimondsForAddButton
                    .GetComponent<Image>().color = new Color(1, 1, 1);
                GetComponent<dimondShopHandler>().DimondsForAddButton
                    .GetComponent<Button>().interactable = true;

                GetComponent<dimondShopHandler>().MoneyForAddButton
                .transform.GetChild(0).gameObject
                .GetComponent<Image>().color = new Color(1, 1, 1);
                GetComponent<dimondShopHandler>().MoneyForAddButton
                    .GetComponent<Image>().color = new Color(1, 1, 1);
                GetComponent<dimondShopHandler>().MoneyForAddButton
                    .GetComponent<Button>().interactable = true;
            }
           
        }
    }
}
