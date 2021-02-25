using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class HighScoreMenuHandler : MonoBehaviour
{
    private GameObject Saver;
    public GameObject HighScorePrefab;
    public List<GameObject> HighScoresObjectsList;
    public GameObject ScrollContainer;
    public Sprite SelfHighScoreBackground;

    private void Start()
    {
        Saver = GameObject.FindGameObjectWithTag("saver");
        HighScoresObjectsList = new List<GameObject>();
    }
   
  
    public void DrawHighScores()
    {
        if(Saver.GetComponent<FirebaseHandler>().LoadedHighScores)
        {
            List<HighScoreModel> highScoreModels = Saver.GetComponent<GetHelpers>()
                .HighScoreList.OrderByDescending(x => x.Score).ToList();
            var yPos = 270;
            HighScoresObjectsList.ForEach(x => Destroy(x.gameObject));
            HighScoresObjectsList = new List<GameObject>();
            foreach (var highScore in highScoreModels)
            {
                var obj = Instantiate(HighScorePrefab, 
                    Vector3.zero, 
                    Quaternion.identity, 
                    ScrollContainer.transform);
               
                if (highScore.PlayerId == Saver.GetComponent<FirebaseHandler>().PlayerId)
                    obj.GetComponent<Image>().sprite = SelfHighScoreBackground;

                obj.GetComponent<RectTransform>().localPosition = new Vector3(-20, yPos, 0);
                yPos -= 120;

                obj.transform.GetChild(0).gameObject.GetComponent<Text>().text = highScore.Score.ToString();
                obj.transform.GetChild(1).gameObject.GetComponent<Text>().text = highScore.PlayerName.ToString();
                obj.transform.GetChild(2).gameObject.GetComponent<Text>().text = highScore.LastLevel.ToString();
                HighScoresObjectsList.Add(obj);
               
            }
            ScrollContainer.GetComponent<scrollFixerShop>().maxY = Mathf.Max(0, (highScoreModels.Count - 6) * 120 + 50);
            GetComponent<menuDisplayHandler>().Show(false);
            var scrollFix = GetComponentInChildren<scrollFixerShop>();
            if (scrollFix != null)
            {
                if (scrollFix.ScrollbarComponent != null)
                {
                    scrollFix.SetupScrollbar();
                }
            }
        }
        else
        {
            Debug.Log("highscores not loaded");
        }

        Saver.GetComponent<FirebaseHandler>().ClearHighscores();
        Saver.GetComponent<FirebaseHandler>().GetHighscoresList();
    }
}
