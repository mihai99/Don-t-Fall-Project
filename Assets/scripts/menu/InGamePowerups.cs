using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InGamePowerups : MonoBehaviour
{
    [System.Serializable]
    public enum PowerupTypes
    {
        ExtraLife, ExtraMoney, Invulnerability, PremiumExtraMoney, PremiumInvulnerability
    }
    public RectTransform MenuTransform;
    private bool Shown;
    public RectTransform ArrowTransform;
    public List<GameObject> PowerupButtons;
    public inventoryHandler inventory;

    
    private void Start()
    {
        Shown = false;
        MenuTransform.transform.localPosition = new Vector3(-1220, 120, 0);
    }
    public void InitMenu()
    {
        Shown = false;
        MenuTransform.transform.localPosition = new Vector3(-1220, 120, 0);
        ArrowTransform.localScale = new Vector3(1, 1, 1);
       
        PowerupButtons[0].GetComponent<InGameMenuPowerupButton>().OwnsThisPowerup = inventory.extraLifeCount > 0;
        PowerupButtons[1].GetComponent<InGameMenuPowerupButton>().OwnsThisPowerup = inventory.invulnerabilityCount > 0;
        PowerupButtons[2].GetComponent<InGameMenuPowerupButton>().OwnsThisPowerup = inventory.premiumInvulnerabilityCount > 0;
        PowerupButtons[3].GetComponent<InGameMenuPowerupButton>().OwnsThisPowerup = inventory.doubleMoneyCount > 0;
        PowerupButtons[4].GetComponent<InGameMenuPowerupButton>().OwnsThisPowerup = inventory.premiumMoneyBoostCount > 0;

        PowerupButtons.ForEach(x =>
        {
            x.GetComponent<InGameMenuPowerupButton>().InitButton();
            x.GetComponent<InGameMenuPowerupButton>().Used = false;
        });

    }
    public void ClickOutside()
    {
        if(Shown == true)
        {
            Shown = false;
            ArrowTransform.localScale = new Vector3(1, 1, 1);
        }
    }
    public void ClickArrow()
    {
        if(Shown == false)
        {
            Shown = true;
            ArrowTransform.localScale = new Vector3(-1,1, 1);
            PowerupButtons[0].GetComponent<InGameMenuPowerupButton>().OwnsThisPowerup = inventory.extraLifeCount > 0;
            PowerupButtons[1].GetComponent<InGameMenuPowerupButton>().OwnsThisPowerup = inventory.invulnerabilityCount > 0;
            PowerupButtons[2].GetComponent<InGameMenuPowerupButton>().OwnsThisPowerup = inventory.premiumInvulnerabilityCount > 0;
            PowerupButtons[3].GetComponent<InGameMenuPowerupButton>().OwnsThisPowerup = inventory.doubleMoneyCount > 0;
            PowerupButtons[4].GetComponent<InGameMenuPowerupButton>().OwnsThisPowerup = inventory.premiumMoneyBoostCount > 0;

            PowerupButtons.ForEach(x =>
            {
              //  x.GetComponent<InGameMenuPowerupButton>().InitButton();
            });
        }
        else
        {
            Shown = false;
            ArrowTransform.localScale = new Vector3(1, 1, 1);
        }
    }
    private void FixedUpdate()
    {
        if (Shown && MenuTransform.localPosition.x < -650)
            MenuTransform.transform.localPosition = Vector3.MoveTowards(
                MenuTransform.transform.localPosition,
                new Vector3(-650, MenuTransform.localPosition.y, MenuTransform.localPosition.z),
                60);
        if (!Shown && MenuTransform.localPosition.y > -1220)
            MenuTransform.localPosition = Vector3.MoveTowards(
               MenuTransform.localPosition,
               new Vector3(-1220, MenuTransform.localPosition.y, MenuTransform.localPosition.z),
               60);
    }
}
