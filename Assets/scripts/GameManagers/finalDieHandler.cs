using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class finalDieHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public RectTransform rectTransform;
    public int DimondsForSkip;
    public int MoneyForRevive;
    public int MoneyForReviveRate;
    public GameObject DimondShop;
    public int used, usedMax;
    public GameObject slider;
    private inventoryHandler inventory;
    public float timer;
    private float expired;
    private bool initialized;
    public GameObject dieMenuUI;
    public GameObject reviveButton, skipButton, notEnoughMoneyButton;
    public GameObject BuyDimonds, WatchVideo;
    public GameObject restartButton, backToMenuButton;
    private bool showMenu;
    private bool dimondListener;
    void Start()
    {
        HideMenu();
        initialized = false;
        inventory = GameObject.FindGameObjectWithTag("saver").GetComponent<inventoryHandler>();
    }
    public void InitOnLevel(){
        used = 0;
        usedMax = (int)Random.RandomRange(2, 5);
    }
    public void disableTimer()
    {
        initialized = false;
    }
    private void MoveButtons(bool loaded)
    {
        if(loaded)
        {
            this.backToMenuButton.GetComponent<RectTransform>().localPosition = new Vector3(-350, -100, 0);
            this.restartButton.GetComponent<RectTransform>().localPosition = new Vector3(0, -100, 0);
        }
        else
        {
            this.backToMenuButton.GetComponent<RectTransform>().localPosition = new Vector3(-250, -100, 0);
            this.restartButton.GetComponent<RectTransform>().localPosition = new Vector3(250, -100, 0);
        }
        
    }
    public void ShowMenu()
    {
        GameObject.FindGameObjectWithTag("DeathScreen").GetComponent<greyScreenHandler>().Show();
        GameObject.FindGameObjectWithTag("DeathScreen").transform.GetChild(0).gameObject.SetActive(false);
        dieMenuUI.SetActive(true);
        this.showMenu = true;
    }
    public void HideMenu()
    {
        GameObject.FindGameObjectWithTag("DeathScreen").GetComponent<greyScreenHandler>().Hide();
        this.showMenu = false;
    }
    public void Init() {
        dieMenuUI.SetActive(true);
        expired = 0;
        initialized = true;
        ShowMenu();
        dimondListener = false;
        
        this.GetComponent<GameManager>().gameUI.SetActive(false);
        if(used == usedMax)
        {
            if(inventory.dimonds>=DimondsForSkip)
            {
                skipButton.SetActive(true);
                reviveButton.SetActive(false);
                notEnoughMoneyButton.SetActive(false);
            }
            else
            {
                skipButton.SetActive(false);
                reviveButton.SetActive(false);
                notEnoughMoneyButton.SetActive(true);                      
                BuyDimonds.SetActive(true);
                WatchVideo.SetActive(false);
                MoveButtons(true);
            }
        } 
        else
        {            
            if(inventory.money>=MoneyForRevive)
            {
                skipButton.SetActive(false);
                reviveButton.SetActive(true);
                reviveButton.transform.GetChild(0).GetComponent<Text>().text = (MoneyForRevive + used * MoneyForReviveRate).ToString();
                notEnoughMoneyButton.SetActive(false);
            }
            else 
            {                     
                skipButton.SetActive(false);
                reviveButton.SetActive(false);
                bool loadedAdd = GameObject.FindGameObjectWithTag("adManager").GetComponent<AdManagerInGame>().RewardedIsLoaded();
                notEnoughMoneyButton.SetActive(loadedAdd);
                WatchVideo.SetActive(true);
                BuyDimonds.SetActive(false);
                MoveButtons(loadedAdd);
            }              
        }        
    }
    public void ExtraLife(bool takeMoney)
    {
        if(takeMoney)
        {
            inventory.money -= MoneyForRevive + used * MoneyForReviveRate;
            inventory.SaveInventory();
        }

        GameObject.FindGameObjectWithTag("canvas").GetComponent<AlertManager>().SetAlert(AlertManager.AlertTypes.BoughtRevive);
        used++;
        initialized = false;
        this.GetComponent<GameManager>().gameUI.SetActive(true);
        HideMenu();
        GameObject.FindGameObjectWithTag("Player").GetComponent<playerManager>().lastRevive();
    }
    public void SkipLevel(bool takeMoney){      

        if(takeMoney)
        {
            inventory.dimonds -= DimondsForSkip;
            inventory.SaveInventory();
        }
           
        initialized = false;
        this.GetComponent<GameManager>().gameUI.SetActive(true);
        HideMenu();
        GameObject.FindGameObjectWithTag("saver").GetComponent<levelsCompletedHandler>().lastLevel ++;
        GameObject.FindGameObjectWithTag("saver").GetComponent<levelsCompletedHandler>().SaveLevels();
        this.GetComponent<GameManager>().nextLevel();

        GameObject.FindGameObjectWithTag("canvas").GetComponent<AlertManager>().SetAlert(AlertManager.AlertTypes.LevelSkippedAlert);

    }
    public void NotEnoughMoneyButton()
    {
        if(used < usedMax)
        {
            GameObject.FindGameObjectWithTag("adManager").GetComponent<AdManagerInGame>().ShowRewardVideoForReveive();
        }
        else
        {
            GameObject.FindGameObjectWithTag("NotEnoughDimonds").GetComponent<NotEnoughDimonds>().Show(DimondsForSkip - inventory.dimonds);
            dimondListener = true;          
        }
    }

    void FixedUpdate()
    {
        if(dimondListener)
        {
            if(GameObject.FindGameObjectWithTag("saver").GetComponent<inventoryHandler>().dimonds >= DimondsForSkip)
            {
       
                SkipLevel(true);
                DimondShop.GetComponent<menuDisplayHandler>().Hide(false);
                HideMenu();

            }

        }
        RectTransform transform = dieMenuUI.GetComponent<RectTransform>();

        if (showMenu && rectTransform.localPosition.y > 0)
            rectTransform.transform.Translate(0, -3000 * Time.deltaTime, 0);
        if (!showMenu && rectTransform.localPosition.y < 1200)
            rectTransform.transform.Translate(0, 3000 * Time.deltaTime, 0);

        if (!showMenu && transform.sizeDelta.x <= 1000)
            dieMenuUI.SetActive(false);

        if (initialized)
        {
            expired += Time.deltaTime;
            slider.GetComponent<Slider>().value = Mathf.Min(1, expired/timer);
            if(expired>timer)
            {
                initialized = false;
                HideMenu();
                this.GetComponent<GameManager>().backToMenu();
            }
        }
    }
}
