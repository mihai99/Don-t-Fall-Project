using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameMenuPowerupButton : MonoBehaviour
{
    public InGamePowerups.PowerupTypes type;
    public bool Used = false;
    public bool OwnsThisPowerup = true;
    private GameObject manager;
    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("canvas");
    }
    public void InitButton()
    {
        GetComponent<Image>().color = new Color(1, 1, 1, (OwnsThisPowerup == true || Used == false)? 1 : 0.2f);
        transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, (OwnsThisPowerup == true || Used == false) ? 1 : 0.2f);
    }
    public void SelectPowerup()
    {
        if(OwnsThisPowerup == false)
        {
            manager.GetComponent<AlertManager>().SetAlert(AlertManager.AlertTypes.PowerupNotOwned);
        }
        else if(Used == true)
        {
            manager.GetComponent<AlertManager>().SetAlert(AlertManager.AlertTypes.PowerupAllreadyUsed);
        }
        else
        {
            switch(type)
            {
                case InGamePowerups.PowerupTypes.ExtraLife: {
                        manager.GetComponent<powerupsHandler>().UseExtraLife();
                        break;
                    }
                case InGamePowerups.PowerupTypes.ExtraMoney:
                    {
                        manager.GetComponent<powerupsHandler>().UseDoubleMoeny();
                        break;
                    }
                case InGamePowerups.PowerupTypes.Invulnerability:
                    {
                        manager.GetComponent<powerupsHandler>().UsedInvulnerability();
                        break;
                    }
                case InGamePowerups.PowerupTypes.PremiumInvulnerability:
                    {
                        manager.GetComponent<powerupsHandler>().UsedPremiumInvulnerability();
                        break;
                    }
                case InGamePowerups.PowerupTypes.PremiumExtraMoney:
                    {
                        manager.GetComponent<powerupsHandler>().UsePremiumDoubleMoney();
                        break;
                    }
            }
            Deactivate();
        }
    }
    public void Deactivate()
    {
        Used = true;
        GetComponent<Image>().color = new Color(1, 1, 1, 0.4f);
        transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0.4f);
    }
       
}
