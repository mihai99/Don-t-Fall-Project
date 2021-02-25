using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinMenuHandler : MonoBehaviour
{
    public Text BestScore;
    public Text LevelScore;
    public GameObject Saver;
    public void Show(float score, int currentLevel)
    {
        BestScore.text = "Best level score: " + Saver.GetComponent<HighScoreHandler>().scorePerLevels[currentLevel].ToString();
        LevelScore.text = "You won, level score: " + ((int)score).ToString();
        GetComponent<menuDisplayHandler>().Show(false);
    }

    public void StartNextLevel()
    {
        GetComponent<menuDisplayHandler>().Hide(false);
        GameObject.FindGameObjectWithTag("canvas").GetComponent<GameManager>().nextLevel();
    }
}
