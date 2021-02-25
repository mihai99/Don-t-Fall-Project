using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotEnoughMoney : MonoBehaviour
{
    public Text Text;
    public GameObject WatchVideoButton;
    public GameObject BuyWithMoneyButton;
    private AdManagerInGame AdManager;
    public GameObject DimondShop;
    public greyScreenHandler Background;
    public int currentAmmount;
    public int startAmmount;
    public NotEnoughDimonds NotEnoughDimondsObj;
    public bool Shown;
    public bool CanBypass = false;
    private void Start()
    {
        AdManager = GameObject.FindGameObjectWithTag("adManager").GetComponent<AdManagerInGame>();
    }
    public void Show(int ammount)
    {
        if (ammount > 0)
        {
            CanBypass = true;
            Shown = true;
            currentAmmount = ammount;
            startAmmount = ammount + GameObject.FindGameObjectWithTag("saver").GetComponent<inventoryHandler>().money;
            Text.text = ammount.ToString();
            Background.Show();
            GetComponent<menuDisplayHandler>().Show(false);

            if (AdManager.RewardedIsLoaded() && ammount <= 500)
            {
                WatchVideoButton.SetActive(true);
                BuyWithMoneyButton.SetActive(false);
            }
            else
            {
                WatchVideoButton.SetActive(false);
                BuyWithMoneyButton.SetActive(true);
            }
        }
    }
    public void WatchAd()
    {
        AdManager.ShowRewardVideoForGold();
    }
    public void OpenShop()
    {
        GetComponent<menuDisplayHandler>().Hide(false);
        DimondShop.GetComponent<menuDisplayHandler>().Show(true);
    }
    private void Update()
    {
        if (currentAmmount >= 0)
        {
            var money = GameObject.FindGameObjectWithTag("saver").GetComponent<inventoryHandler>().money;
            currentAmmount = startAmmount - money;
        }
        else if (!NotEnoughDimondsObj.Shown && CanBypass)
        {
            DimondShop.GetComponent<menuDisplayHandler>().Hide(true);
            Background.Hide();
            Shown = false;
        }
    }
}
