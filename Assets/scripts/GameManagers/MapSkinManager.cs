using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
[System.Serializable]
public struct MapSkin
{
    public MapSkinType name;
    public int dimondCost, moneyCost;
    public Image background;
    public Text priceText;
    public bool premium;
    public Material skin;
}


public class MapSkinManager : MonoBehaviour
{
    public Sprite selectedCharacterBackground;
    public Sprite normalBackgroundSprite, premiumBackgroundSprite;
    public Text moneyText;
    public Text dimondsText;
    public MapSkin[] MapSkins;
    private GameObject[] map;
    private GameObject saver;
    private mapSkinsHandler mapSkinsManager;
    private Sprite selected;
    private GameObject Manager;
    private bool showAlerts;
    
   
    void Start()
    {
       
        Manager = GameObject.FindGameObjectWithTag("canvas");
        saver = GameObject.FindGameObjectWithTag("saver");
        mapSkinsManager = saver.GetComponent<mapSkinsHandler>();
        SetPriceText();
        showAlerts = false;
        SwapMapSkin(mapSkinsManager.selected);
        showAlerts = true;

    }
    public void SetPriceText()
    {
        for (int i = 0; i < MapSkins.Length; i++)
            if (mapSkinsManager.owned[(int)MapSkins[i].name] == true)
            {
                MapSkins[i].priceText.text = "Select";
                if (MapSkins[i].priceText.gameObject.transform.childCount != 0)
                {
                    MapSkins[i].priceText.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                }
                if (MapSkins[i].premium == true)
                    MapSkins[i].priceText.gameObject.transform.parent.GetChild(1).gameObject.SetActive(false);
            }
            else if (MapSkins[i].premium == true)
            {
                MapSkins[i].priceText.text = MapSkins[i].dimondCost.ToString();
                MapSkins[i].priceText.gameObject.transform.parent.GetChild(1).gameObject.SetActive(true);
            }
            else
            {
                if (MapSkins[i].moneyCost == 0 && MapSkins[i].dimondCost == 0)
                {
                    MapSkins[i].priceText.text = "Free";
                }
                else
                {
                    MapSkins[i].priceText.text = MapSkins[i].moneyCost.ToString();
                }
                if (MapSkins[i].priceText.gameObject.transform.childCount != 0)
                {
                    MapSkins[i].priceText.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                }
            }
    }

    public int GetSkin(MapSkinType type)
    {
        var skin = MapSkins.FirstOrDefault<MapSkin>(m => m.name == type);
       
        if (mapSkinsManager.owned[(int)type] == true)
        {
            if(showAlerts == true) Manager.GetComponent<AlertManager>().SetAlert(AlertManager.AlertTypes.MapSkinSelected);
            return 1;
        }
        if (skin.moneyCost != 0 && saver.GetComponent<inventoryHandler>().money < skin.moneyCost)
        {
            GameObject.FindGameObjectWithTag("NotEnoughMoney").GetComponent<NotEnoughMoney>().Show(skin.moneyCost - saver.GetComponent<inventoryHandler>().money);
            return 0;
        }
        if (skin.dimondCost != 0 && saver.GetComponent<inventoryHandler>().dimonds < skin.dimondCost)
        {
            GameObject.FindGameObjectWithTag("NotEnoughDimonds").GetComponent<NotEnoughDimonds>().Show(skin.dimondCost - saver.GetComponent<inventoryHandler>().dimonds);
            return 0;
        }
        Manager.GetComponent<AlertManager>().SetAlert(AlertManager.AlertTypes.ItemBought);
                
        saver.GetComponent<inventoryHandler>().money -= skin.moneyCost;
        saver.GetComponent<inventoryHandler>().dimonds -= skin.dimondCost;
        moneyText.text = saver.GetComponent<inventoryHandler>().money.ToString();
        dimondsText.text = saver.GetComponent<inventoryHandler>().dimonds.ToString();
        saver.GetComponent<inventoryHandler>().SaveInventory();
        mapSkinsManager.owned[(int)type] = true;
        mapSkinsManager.SaveMapSkins();
        SetPriceText();
        SetPriceText();
        return 1;


     
    }
    public void SwapMapSkin(MapSkinType model)
    {

        int response = GetSkin(model);
        if (response == 1)
        {
            for (int i = 0; i < MapSkins.Length; i++)
                if (MapSkins[i].name == model)
                {

                    MapSkins[i].background.sprite = selectedCharacterBackground;
                    mapSkinsManager.selected = model;
                    mapSkinsManager.SaveMapSkins();
                }
                else
                {
                    MapSkins[i].background.sprite = MapSkins[i].premium == true ? premiumBackgroundSprite : normalBackgroundSprite;
                }

                       
            ApplySkin();
        }
    }

    public void ApplySkin()
    {
        
        map = GameObject.FindGameObjectsWithTag("terrain");
        for(int i=0;i<map.Length;i++)
            map[i].GetComponent<Renderer>().material = MapSkins.FirstOrDefault<MapSkin>(m => m.name == mapSkinsManager.selected).skin;

        map = GameObject.FindGameObjectsWithTag("destroyable");
        for(int i=0;i<map.Length;i++)
            map[i].GetComponent<Renderer>().material = MapSkins.FirstOrDefault<MapSkin>(m => m.name == mapSkinsManager.selected).skin;
    }
    
}
