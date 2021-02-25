using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class powerupsHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject moneyObj, dimondObj;
    public GameObject saver;
    private inventoryHandler inventory;
    public GameObject player;
    public Text ownedExtraLife, ownedInvulnerability, ownedMoneyBoost, ownedPremiumInvulnerability, ownedPremiumMoneyBoost;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        inventory = saver.GetComponent<inventoryHandler>() ;
        moneyObj.GetComponent<Text>().text = inventory.money.ToString();
        dimondObj.GetComponent<Text>().text = inventory.dimonds.ToString();
    }
    public void SetOwnedTexts()
    {
        const string prefix = "Owned: ";
        ownedExtraLife.text = prefix +  inventory.extraLifeCount.ToString();
        ownedInvulnerability.text = prefix + inventory.invulnerabilityCount.ToString();
        ownedMoneyBoost.text = prefix + inventory.doubleMoneyCount.ToString();
        ownedPremiumInvulnerability.text = prefix + inventory.premiumInvulnerabilityCount.ToString();
        ownedPremiumMoneyBoost.text = prefix + inventory.premiumMoneyBoostCount.ToString();
    }
    public void extraLive()
    {
       
        if(inventory.money>=500)
        {
            GameObject.FindGameObjectWithTag("canvas").GetComponent<AlertManager>().SetAlert(AlertManager.AlertTypes.ItemBought);

            inventory.extraLifeCount++;
            inventory.money -= 500;
            moneyObj.GetComponent<Text>().text=inventory.money.ToString();
            inventory.SaveInventory();
            SetOwnedTexts();
        }
        else
        {
            GameObject.FindGameObjectWithTag("NotEnoughMoney").GetComponent<NotEnoughMoney>().Show(500 - inventory.money);
        }

    }
     public void invulnerability()
    {

        if (inventory.money >= 1000)
        {
            GameObject.FindGameObjectWithTag("canvas").GetComponent<AlertManager>().SetAlert(AlertManager.AlertTypes.ItemBought);

            inventory.invulnerabilityCount++;
            inventory.money -= 1000;
            inventory.SaveInventory();
            moneyObj.GetComponent<Text>().text = inventory.money.ToString();
            SetOwnedTexts();
        }
        else
        {
            GameObject.FindGameObjectWithTag("NotEnoughMoney").GetComponent<NotEnoughMoney>().Show(1000 - inventory.money);
        }

    }
    
    public void premiumInvulnerability()
    {
        if(inventory.dimonds>=20)
        {
            GameObject.FindGameObjectWithTag("canvas").GetComponent<AlertManager>().SetAlert(AlertManager.AlertTypes.ItemBought);

            inventory.premiumInvulnerabilityCount++;   
            inventory.dimonds -= 20;
            inventory.SaveInventory();
            dimondObj.GetComponent<Text>().text=inventory.dimonds.ToString();
            SetOwnedTexts();

        }
        else  
        {
            GameObject.FindGameObjectWithTag("NotEnoughDimonds").GetComponent<NotEnoughDimonds>().Show(20 - inventory.dimonds);
        }
    }
    public void doubleMoney()
    {      
        if(inventory.money>=750)
        {
            GameObject.FindGameObjectWithTag("canvas").GetComponent<AlertManager>().SetAlert(AlertManager.AlertTypes.ItemBought);

            inventory.doubleMoneyCount++;                
            inventory.money -= 750;
            inventory.SaveInventory();
            moneyObj.GetComponent<Text>().text=inventory.money.ToString();
            SetOwnedTexts();
        }
        else
        {
            GameObject.FindGameObjectWithTag("NotEnoughMoney").GetComponent<NotEnoughMoney>().Show(750 - inventory.money);
        }
    }
    public void premiumDoubleMoney()
    {
        if (inventory.dimonds >= 15)
        {
            GameObject.FindGameObjectWithTag("canvas").GetComponent<AlertManager>().SetAlert(AlertManager.AlertTypes.ItemBought);

            inventory.premiumMoneyBoostCount++;
            inventory.dimonds -= 15;
            inventory.SaveInventory();
            dimondObj.GetComponent<Text>().text = inventory.dimonds.ToString();
            SetOwnedTexts();
        }
        else
        {
            GameObject.FindGameObjectWithTag("NotEnoughDimonds").GetComponent<NotEnoughDimonds>().Show(15 - inventory.dimonds);
        }
    }
    public void UseDoubleMoeny()
    {
        if (inventory.doubleMoneyCount > 0)
        {
            GetComponent<AlertManager>().SetAlert(AlertManager.AlertTypes.UsedDoubleMoney);
            inventory.doubleMoneyCount--;
            player.GetComponent<playerManager>().doubleMoney = true;
            inventory.SaveInventory();
        }
    }
    public void UsePremiumDoubleMoney()
    {
        if (inventory.premiumMoneyBoostCount > 0)
        {
            GetComponent<AlertManager>().SetAlert(AlertManager.AlertTypes.UsedDoubleMoney);
            inventory.premiumMoneyBoostCount--;
            player.GetComponent<playerManager>().premiumMoney = true;
            inventory.SaveInventory();
        }
    }
    public void UseExtraLife()
    {
        if (inventory.extraLifeCount > 0)
        {
            GetComponent<AlertManager>().SetAlert(AlertManager.AlertTypes.UserExtraLife);
            player.GetComponent<playerManager>().lives++;
            player.GetComponent<playerManager>().livesImg[player.GetComponent<playerManager>().lives-1].SetActive(true);
            inventory.extraLifeCount--;
            inventory.SaveInventory();
        }
    }
    public void UsedInvulnerability()
    {
        if (inventory.invulnerabilityCount > 0)
        {
            GetComponent<AlertManager>().SetAlert(AlertManager.AlertTypes.UsedInvulnerability);
            inventory.invulnerabilityCount--;
            player.GetComponent<playerManager>().inv = true;
            player.GetComponent<playerManager>().invIsHere.SetActive(true);
            inventory.SaveInventory();
        }
    }
    public void UsedPremiumInvulnerability()
    {
        if (inventory.premiumInvulnerabilityCount > 0)
        {
            GetComponent<AlertManager>().SetAlert(AlertManager.AlertTypes.UsedInvulnerability);
            inventory.premiumInvulnerabilityCount--;
            player.GetComponent<playerManager>().premiumInvulnerability = true;
            player.GetComponent<playerManager>().inv = true;
            player.GetComponent<playerManager>().invIsHere.SetActive(true);
            inventory.SaveInventory();
        }        
    }
   
  
}
