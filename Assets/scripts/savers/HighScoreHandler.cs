using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SavedHighScore
{
    public List<int> scorePerLevels;
    public SavedHighScore( List<int> scorePerLevels)
    {   
        this.scorePerLevels = scorePerLevels;
    }
}

public class HighScoreHandler : MonoBehaviour
{
    public int highScore;
    public List<int> scorePerLevels;
    public Text menuHighScoreText;

    public async Task Awake()
    {
        InitData();
        await LoadHighScores();
    }

    private void InitData()
    {
        this.highScore = 0;
        scorePerLevels = new List<int>();
        for (var i = 0; i < 100; i++)
            scorePerLevels.Add(0);
    }

    private void calculateHighScore()
    {
        highScore = 0;
        scorePerLevels.ForEach(x => highScore += x);
        this.menuHighScoreText.text = highScore.ToString();
    }

    public void SaveHighScore()
    {
        calculateHighScore();

        //Save local
        string destination = Application.persistentDataPath + "/savedHighScore.dat";
        FileStream file;

        if (File.Exists(destination))
        {
            file = File.OpenWrite(destination);
            SavedHighScore data = new SavedHighScore(scorePerLevels);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(file, data);
            file.Close();
            this.GetComponent<FirebaseHandler>().UpdateHighScoreToDatabase();
            
        }
        else
        {
            file = File.Create(destination);
            SaveHighScore();
        }
    }

    public async Task<bool> LoadHighScores()
    {
        string destination = Application.persistentDataPath + "/savedHighScore.dat";
        FileStream file;

        if (File.Exists(destination))
        {
            file = File.OpenRead(destination);
            BinaryFormatter bf = new BinaryFormatter();
            SavedHighScore data = (SavedHighScore)bf.Deserialize(file);
            file.Close();

            this.scorePerLevels = data.scorePerLevels;
            calculateHighScore();
            return true;
        }
        else
        {
            InitData();
            SaveHighScore();
            return false;
        }
    }
}
