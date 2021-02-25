using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceAdder : MonoBehaviour
{
   public void AddMoney()
    {
        GameObject.FindGameObjectWithTag("saver").GetComponent<inventoryHandler>().money += 2000;
        GameObject.FindGameObjectWithTag("saver").GetComponent<inventoryHandler>().SaveInventory();
    }
    public void AddDimonds()
    {
        GameObject.FindGameObjectWithTag("saver").GetComponent<inventoryHandler>().dimonds += 5;
        GameObject.FindGameObjectWithTag("saver").GetComponent<inventoryHandler>().SaveInventory();
    }
    public void UnlockLevels()
    {
        GameObject.FindGameObjectWithTag("saver").GetComponent<levelsCompletedHandler>().lastLevel = 
            GameObject.FindGameObjectWithTag("canvas").GetComponent<GameManager>().levels.Length;
        GameObject.FindGameObjectWithTag("saver").GetComponent<levelsCompletedHandler>().SaveLevels();
    }
}
