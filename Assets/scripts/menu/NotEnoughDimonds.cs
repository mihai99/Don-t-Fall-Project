using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotEnoughDimonds : MonoBehaviour
{
    public Text Text;
    public GameObject WatchVideoButton;
    public GameObject BuyWithMoneyButton;
    private AdManagerInGame AdManager;
    public GameObject DimondShop;
    public greyScreenHandler Background;
    public int currentAmmount;
    public int startAmmount;
    public NotEnoughMoney NotEnoughMoneyObj;
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
            startAmmount = ammount + GameObject.FindGameObjectWithTag("saver").GetComponent<inventoryHandler>().dimonds;
            Text.text = ammount.ToString();
            Background.Show();
            GetComponent<menuDisplayHandler>().Show(false);

            if (AdManager.RewardedIsLoaded() && ammount <= 5)
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
        AdManager.ShowRewardVideoForDimonds();
    }
    public void OpenShop()
    {
        GetComponent<menuDisplayHandler>().Hide(false);
        DimondShop.GetComponent<menuDisplayHandler>().Show(true);
    }
    private void Update()
    {
        if (currentAmmount > 0)
        {
            var dimonds = GameObject.FindGameObjectWithTag("saver").GetComponent<inventoryHandler>().dimonds;
            currentAmmount = startAmmount - dimonds;
        }
        else if(!NotEnoughMoneyObj.Shown && CanBypass)
        {
            DimondShop.GetComponent<menuDisplayHandler>().Hide(true);
            Background.Hide();
            Shown = false;
        }
    }
}
