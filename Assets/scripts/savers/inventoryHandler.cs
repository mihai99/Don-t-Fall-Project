using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

[System.Serializable]
 public class savedInventoryItems
 {
     public int doubleMoneys;
     public int extraLife;     
     public int invulnerability;
     public int premiumInvulnerability;
     public int premiumMoneyBoost;
     public int money;
     public int dimonds;

     //List<bool> skins;
     public savedInventoryItems(int doubleMoneyInt, int extraLifeInt, int invulnerabilityInt, int premiumInvulnerabilityInt, int premiumMoneyBoostInt, int moneyInt, int dimondsInt)
     {
         this.doubleMoneys=doubleMoneyInt;
         this.extraLife = extraLifeInt;
         this.invulnerability = invulnerabilityInt;
         this.premiumInvulnerability = premiumInvulnerabilityInt;
         this.premiumMoneyBoost = premiumMoneyBoostInt;
         this.money = moneyInt;
         this.dimonds = dimondsInt;
    }
 }

public class inventoryHandler : MonoBehaviour
{
        public int doubleMoneyCount, extraLifeCount, invulnerabilityCount, premiumInvulnerabilityCount, premiumMoneyBoostCount;
        public int money;
        public int dimonds;
        public GameObject inGameMoneyTextObj, inGameDimondsTextObj;
    
    public async Task Awake()
    {
        InitData();
        await LoadInventory();
    }

    private void InitData()
    {
        doubleMoneyCount = 0;
        extraLifeCount = 0;
        invulnerabilityCount = 0;
        premiumInvulnerabilityCount = 0;
        premiumMoneyBoostCount = 0;
        money = 0;
        dimonds = 0;
    }

    public void SaveInventory()
    {
         string destination = Application.persistentDataPath + "/savePowerup.dat";
         FileStream file;

        if (File.Exists(destination)) {
            file = File.OpenWrite(destination);
            savedInventoryItems data = new savedInventoryItems(doubleMoneyCount, extraLifeCount, invulnerabilityCount, premiumInvulnerabilityCount, premiumMoneyBoostCount, money, dimonds);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(file, data);
            file.Close();
        }
        else
        {
            file = File.Create(destination);
            SaveInventory();
        }
     }
 
     public async Task<bool> LoadInventory()
     {
         string destination = Application.persistentDataPath + "/savePowerup.dat";
         FileStream file;

        if (File.Exists(destination))
        {
            file = File.OpenRead(destination);
            BinaryFormatter bf = new BinaryFormatter();
            savedInventoryItems data = (savedInventoryItems)bf.Deserialize(file);
            file.Close();

            extraLifeCount = data.extraLife;
            invulnerabilityCount = data.invulnerability;
            premiumInvulnerabilityCount = data.premiumInvulnerability;
            premiumMoneyBoostCount = data.premiumMoneyBoost;
            doubleMoneyCount = data.doubleMoneys;
            money = data.money;
            dimonds = data.dimonds;

            return true;
        }
        else
        {
            InitData();
            SaveInventory();
            return false;
        }
     }
}
