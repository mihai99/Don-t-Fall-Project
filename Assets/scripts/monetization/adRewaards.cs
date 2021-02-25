using GoogleMobileAds.Api;
using UnityEngine;
using System;
public class adRewaards : MonoBehaviour
{
    private inventoryHandler inventory;
    private GameObject manager;

     private void Start() {
            inventory = GameObject.FindGameObjectWithTag("saver").GetComponent<inventoryHandler>();
            manager = GameObject.FindGameObjectWithTag("canvas");
     }

        public void EarnedRevive(object sender, Reward args)
        {
           GameObject.FindGameObjectWithTag("Player").transform.Translate(0, 2, 0);
           manager.GetComponent<finalDieHandler>().ExtraLife(false);     
           manager.GetComponent<finalDieHandler>().used = manager.GetComponent<finalDieHandler>().usedMax;
        GameObject.FindGameObjectWithTag("canvas").GetComponent<AlertManager>().SetAlert(AlertManager.AlertTypes.BoughtRevive);

    }

    public void EarnedDimonds(object sender, Reward args)
        {
            inventory.dimonds += 5;
            GameObject.FindGameObjectWithTag("canvas").GetComponent<AlertManager>().SetAlert(AlertManager.AlertTypes.GotDimondsAlert);
            inventory.SaveInventory();
            GetComponent<AdStore>().RequestRewardedAd();
            GameObject.FindGameObjectWithTag("DimondShop").GetComponent<menuDisplayHandler>().UpdateMoneyTexts();
         }

        public void EarnedGold(object sender, Reward args)
        {
            inventory.money += 500;
            GameObject.FindGameObjectWithTag("canvas").GetComponent<AlertManager>().SetAlert(AlertManager.AlertTypes.GotMoneyAlert);
            inventory.SaveInventory();
            GetComponent<AdStore>().RequestRewardedAd();
            GameObject.FindGameObjectWithTag("DimondShop").GetComponent<menuDisplayHandler>().UpdateMoneyTexts();
        }

}
