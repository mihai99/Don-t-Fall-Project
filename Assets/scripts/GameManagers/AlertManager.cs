using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class AlertManager : MonoBehaviour
{
    public enum AlertTypes{
        GotMoneyAlert,
        GotDimondsAlert,
        GotMoneyFromGameAlert,
        GotDimondFromGameAlert,
        NewRevivePointAlert,
        CharacterSelected,
        ItemBought,
        LevelSkippedAlert,
        MapSkinSelected,
        BoughtRevive,
        GotStar,
        UsedDoubleMoney, 
        UsedInvulnerability, 
        UserExtraLife, 
        PowerupNotOwned, 
        PowerupAllreadyUsed,
        PowerupGiftRecieved,
        LevelAlert
    }
  
    [System.Serializable]
    public struct AlertVariables{
        public AlertTypes type;
        public Color textColor;
        public string Text;
        public AudioSource Audio;
    }
    public GameObject AlertPrefab;
    public AlertVariables LevelAlert;
    public AlertVariables[] AllAlerts;
    private bool isShown;
    private int ColorDirection;
    private List<AlertVariables> AlertsQuery = new List<AlertVariables>();
    private float Timer;

    private void ShowAlert()
    {
        if(AlertsQuery.Count > 0)
        {
            Timer = 0;
            var alertToShow = AlertsQuery.First();
            var alertObject = Instantiate(AlertPrefab, transform);
            alertToShow.Audio.Play();
            alertObject.GetComponent<AlertAnimation>().Init(alertToShow.Text, alertToShow.textColor);
            AlertsQuery.Remove(alertToShow);
        }if(AlertsQuery.Count > 0)
        {
            Invoke("ShowAlert", 0.5f);
        }
    }
    public void SetAlert(AlertTypes type)
    {
        AlertsQuery.Add(AllAlerts.ToList().FirstOrDefault(x => x.type == type));
        if (Timer >= 0.5f)
        {
            ShowAlert();
            Timer = 0;
        }
    }
    public void SetLevelAlert(int levelNo)
    {
        LevelAlert.Text = "You've reached level " + (levelNo+1).ToString();
        AlertsQuery.Add(LevelAlert);
        if (Timer >= 0.5f)
        {
            ShowAlert();
            Timer = 0;
        }
    }
    private void Update()
    {
        Timer = Mathf.Min(1, Timer + Time.deltaTime);
    }
}
