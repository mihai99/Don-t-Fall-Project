using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct Character {
    public PlayerModel name;
    public int dimondCost, moneyCost;
    public Image background;
    public Text priceText;
    public bool premium;
    public GameObject obj;
}


public class CharacterSkinsManager : MonoBehaviour
{

    private GameObject manager;
    private GameObject saver;
    private charactersOwnedHandler charactersOwnedManager;
    public Character[] characters;
    private GameObject newPlayer;
    public Sprite selectedCharacterBackground;
    public Sprite normalBackgroundSprite, premiumBackgroundSprite;
    private bool showAlerts;
    public Text moneyText, dimondsText;
  
    private async Task Start()
    {
        
        manager = GameObject.FindGameObjectWithTag("canvas");
        saver = GameObject.FindGameObjectWithTag("saver");
        charactersOwnedManager = GameObject.FindGameObjectWithTag("saver").GetComponent<charactersOwnedHandler>();
        await charactersOwnedManager.LoadCharacters();        
        SetPriceText();
        showAlerts = false;
        SwapPlayerModel(charactersOwnedManager.selected);
        showAlerts = true;

    }
    public void SetPriceText()
    {
        for (int i = 0; i < characters.Length; i++)
            if (charactersOwnedManager.owned[(int)characters[i].name] == true)
            {
                characters[i].priceText.text = "Select";
                if(characters[i].priceText.gameObject.transform.childCount != 0)
                {
                    characters[i].priceText.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                }
                if (characters[i].premium == true)
                    characters[i].priceText.gameObject.transform.parent.GetChild(1).gameObject.SetActive(false);
            }
            else if (characters[i].premium == true)
            {
                characters[i].priceText.text = characters[i].dimondCost.ToString();
                characters[i].priceText.gameObject.transform.parent.GetChild(1).gameObject.SetActive(true);
            }
            else
            {
                if(characters[i].moneyCost == 0 && characters[i].dimondCost == 0)
                {
                    characters[i].priceText.text = "Free";
                }
                else
                {
                    characters[i].priceText.text = characters[i].moneyCost.ToString();
                }
                if (characters[i].priceText.gameObject.transform.childCount != 0)
                {
                    characters[i].priceText.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                }
            }
    }
    public void SetNewPlayerModel()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Camera.main.GetComponent<cameraFollow>().player = player;
        manager.GetComponent<PlayerJumpTrigger>().player = player;
        manager.GetComponent<GameManager>().player = player;
        manager.GetComponent<powerupsHandler>().player = player;
    }
    public int GetCharacter(PlayerModel model)
    {
     
        for (int i=0;i<characters.Length;i++)
        {
            if(characters[i].name == model)
            {
                if (charactersOwnedManager.owned[(int)model] == true)
                {
                    if (showAlerts == true) manager.GetComponent<AlertManager>().SetAlert(AlertManager.AlertTypes.CharacterSelected);
                    return 1;
                }
                if (characters[i].moneyCost != 0 && saver.GetComponent<inventoryHandler>().money < characters[i].moneyCost)
                {
                    GameObject.FindGameObjectWithTag("NotEnoughMoney").GetComponent<NotEnoughMoney>().Show(characters[i].moneyCost - saver.GetComponent<inventoryHandler>().money);
                    return 0;
                }
                if (characters[i].dimondCost !=0 && saver.GetComponent<inventoryHandler>().dimonds < characters[i].dimondCost)
                {
                    GameObject.FindGameObjectWithTag("NotEnoughDimonds").GetComponent<NotEnoughDimonds>().Show(characters[i].dimondCost - saver.GetComponent<inventoryHandler>().dimonds);
                    return 0;
                }
                manager.GetComponent<AlertManager>().SetAlert(AlertManager.AlertTypes.ItemBought);
                

                saver.GetComponent<inventoryHandler>().money -= characters[i].moneyCost;
                saver.GetComponent<inventoryHandler>().dimonds -= characters[i].dimondCost;
                moneyText.text = saver.GetComponent<inventoryHandler>().money.ToString();
                dimondsText.text = saver.GetComponent<inventoryHandler>().dimonds.ToString();
                saver.GetComponent<inventoryHandler>().SaveInventory();
                charactersOwnedManager.owned[(int)model] = true;
                charactersOwnedManager.SaveCharacters();
                SetPriceText();
                return 1;


            }
        }

        return 0;
    }
    public void SwapPlayerModel(PlayerModel model)
    {

        GameObject oldPlayer = GameObject.FindGameObjectWithTag("Player").gameObject;
        int response = GetCharacter(model);
      
        if(response == 1)
        {
            for (int i = 0; i < characters.Length; i++)
                if (characters[i].name == model)
                {
                    newPlayer = characters[i].obj;
                    characters[i].background.sprite = selectedCharacterBackground;
                    charactersOwnedManager.selected = model;
                    charactersOwnedManager.SaveCharacters();
                }
                else
                {
                    characters[i].background.sprite = characters[i].premium == true ? premiumBackgroundSprite : normalBackgroundSprite;
                }

            Vector3 position = manager.GetComponent<GameManager>().playerStartPos;
            Quaternion rotation = manager.GetComponent<GameManager>().playerStartRot;
            newPlayer.SetActive(true);
            newPlayer.transform.position = position;
            newPlayer.transform.rotation = rotation;
            if (newPlayer != oldPlayer)
            {
                oldPlayer.transform.position = new Vector3(0, -100, 0);
                oldPlayer.SetActive(false);
            }


            SetNewPlayerModel();
        }
        
       


    }
}
