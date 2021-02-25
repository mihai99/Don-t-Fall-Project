using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;
using UnityEngine;

public class UpdateHelper : MonoBehaviour
{
    public void UpdatableHighScore(HighScoreModel model, DatabaseReference databsse)
    {    
        if(model == null)
        {
            throw new NullReferenceException("Model is null");
        }
        if(databsse == null)
        {
            throw new NullReferenceException("db is null");
        }
        databsse.Child("HighScores").Child(model.PlayerId.ToString()).Child("playerName").SetValueAsync(model.PlayerName);
        databsse.Child("HighScores").Child(model.PlayerId.ToString()).Child("lastLevel").SetValueAsync(model.LastLevel);
        databsse.Child("HighScores").Child(model.PlayerId.ToString()).Child("score").SetValueAsync(model.Score);
    }
}
