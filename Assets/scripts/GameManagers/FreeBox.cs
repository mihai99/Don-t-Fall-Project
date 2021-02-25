using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class FreeBox : MonoBehaviour
{
    public List<InGamePowerups.PowerupTypes> powerups;
    private inventoryHandler Inventory;
    // Start is called before the first frame update
    void Start()
    {
        Inventory = GameObject.FindGameObjectWithTag("saver").GetComponent<inventoryHandler>();
        if(powerups.Count == 0)
        {
        for(int i=0;i<= Random.Range(1, 4);i++)
            powerups.Add((InGamePowerups.PowerupTypes)Random.Range(0, 5));
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("canvas").GetComponent<AlertManager>().SetAlert(AlertManager.AlertTypes.PowerupGiftRecieved);
            foreach(var powerUp in powerups)
            {
                switch(powerUp)
                {
                    case InGamePowerups.PowerupTypes.ExtraLife:
                        {
                            Inventory.extraLifeCount++;
                            break;
                        }
                    case InGamePowerups.PowerupTypes.ExtraMoney:
                        {
                            Inventory.doubleMoneyCount++;
                            break;
                        }
                    case InGamePowerups.PowerupTypes.Invulnerability:
                        {
                            Inventory.invulnerabilityCount++;
                            break;
                        }
                    case InGamePowerups.PowerupTypes.PremiumExtraMoney:
                        {
                            Inventory.premiumMoneyBoostCount++;
                            break;
                        }
                    case InGamePowerups.PowerupTypes.PremiumInvulnerability:
                        {
                            Inventory.premiumInvulnerabilityCount++;
                            break;
                        }
                }
            }
            Inventory.SaveInventory();
        }
        if(collision.transform.tag != "terrain")
        {
            Destroy(this.gameObject);
        }
    }
}
