using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dimondShopHandler : MonoBehaviour
{
    public AdManagerInGame AdManager;
    public GameObject DimondsForAddButton;
    public GameObject MoneyForAddButton;
    public Text[] DimondsText;
    public Text[] MoneyText;

    public void AddDimonds(int amount)
    {
        GameObject.FindGameObjectWithTag("saver").GetComponent<inventoryHandler>().dimonds += amount;
        GameObject.FindGameObjectWithTag("saver").GetComponent<inventoryHandler>().SaveInventory();
        foreach(var text in DimondsText)
        {
            text.text = GameObject.FindGameObjectWithTag("saver").GetComponent<inventoryHandler>().dimonds.ToString();
        }
        GetComponent<menuDisplayHandler>().UpdateMoneyTexts();
        GameObject.FindGameObjectWithTag("canvas").GetComponent<AlertManager>().SetAlert(AlertManager.AlertTypes.GotDimondsAlert);
    }
    public void AddGold(int amount)
    {
        GameObject.FindGameObjectWithTag("saver").GetComponent<inventoryHandler>().money += amount;
        GameObject.FindGameObjectWithTag("saver").GetComponent<inventoryHandler>().SaveInventory();
        foreach (var text in MoneyText)
        {
            text.text = GameObject.FindGameObjectWithTag("saver").GetComponent<inventoryHandler>().money.ToString();
        }
        GetComponent<menuDisplayHandler>().UpdateMoneyTexts();
        GameObject.FindGameObjectWithTag("canvas").GetComponent<AlertManager>().SetAlert(AlertManager.AlertTypes.GotMoneyAlert);
    }
}
