using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class levelsMenuHandler : MonoBehaviour
{
    public GameObject saverObj;
    public List<GameObject> levelsButton;
    private void Start()
    {
        var levelsButtonCount = this.gameObject.transform.GetChild(0).GetChild(0).childCount;
        this.transform.GetChild(0).GetChild(0).GetComponent<scrollFixerShop>().minY = 0;
        this.transform.GetChild(0).GetChild(0).GetComponent<scrollFixerShop>().maxY = ((levelsButtonCount / 3) - 3 ) * 300 - 400;
        for (int i = 0; i < levelsButtonCount; i++)
            this.levelsButton.Add(this.gameObject.transform.GetChild(0).GetChild(0).GetChild(i).gameObject);

        for (int i = 0; i < levelsButton.Count; i++)
        {
            levelsButton[i].transform.GetChild(0).gameObject.GetComponent<Text>().text = (i + 1).ToString();
            levelsButton[i].GetComponent<RectTransform>().localPosition = new Vector3(
                 (i%3)*300,
                 300 - (i/ 3) * 250,
                 0);
            var closureFix = i;
            levelsButton[i].GetComponent<Button>().onClick.AddListener(() => startLevelBut(closureFix));
        }
    }
   
    private void setColorForLevelsButton()
    {
        int maxDevelopedLevels = GameObject.FindGameObjectWithTag("canvas").GetComponent<GameManager>().levels.Length;
        int lastLevelCompleted = saverObj.GetComponent<levelsCompletedHandler>().lastLevel;
        this.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().localPosition = new Vector3(-300, lastLevelCompleted / 3 * 250 -250, 0);
        for (int i = 0; i < levelsButton.Count; i++)
            if ( levelsButton[i] && i <= lastLevelCompleted && i < maxDevelopedLevels)
            {
                levelsButton[i].GetComponent<Button>().enabled = true;
                levelsButton[i].GetComponent<Image>().color = Color.white;
                int stars = saverObj.GetComponent<levelsCompletedHandler>().stars[i];
                for (int j = 0; j < stars; j++)
                    levelsButton[i].transform.GetChild(1 + j).GetComponent<Image>().color = Color.yellow;
            }
            else
            {
                levelsButton[i].GetComponent<Button>().enabled = false;
                levelsButton[i].GetComponent<Image>().color = Color.black;
            }        
    }
    public void startLevelBut(int level)
    {
        GameObject.FindGameObjectWithTag("canvas").GetComponent<GameManager>().initLevel(level, false);
    }
    public void SetUp()
    {
        setColorForLevelsButton();
    }
}
