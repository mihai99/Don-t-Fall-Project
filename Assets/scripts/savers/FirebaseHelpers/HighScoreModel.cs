using System;
using UnityEngine;

[System.Serializable]
public class HighScoreModel
{
    public Guid PlayerId { get; set; }
    public int LastLevel { get; set; }
    public string PlayerName { get; set; }
    public int Score { get; set; }

    public static HighScoreModel BuildLocalHighScore(Guid playerId)
    {
        var saver = GameObject.FindGameObjectWithTag("saver");
        var model = new HighScoreModel
        {
            PlayerId = playerId,
            PlayerName = saver.GetComponent<optionsHandler>().playerName,
            LastLevel = saver.GetComponent<levelsCompletedHandler>().lastLevel,
            Score = saver.GetComponent<HighScoreHandler>().highScore,
        };               
        return model;
    }
}

