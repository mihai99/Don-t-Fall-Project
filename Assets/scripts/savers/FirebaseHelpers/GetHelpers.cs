using Firebase.Database;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;
using System.Linq;

public class GetHelpers : MonoBehaviour
{
    public List<HighScoreModel> HighScoreList;

    public void GetAllHighscores(DatabaseReference database)
    {
        Debug.Log("Called get highscores");
        HighScoreList = new List<HighScoreModel>();
        database.Database
        .GetReference("HighScores")
        .GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("error at get");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                var result = snapshot.Value as Dictionary<string, object>;

                foreach (KeyValuePair<string, object> resultObject in result)
                {
                    var convertedObject = resultObject.Value as Dictionary<string, object>;
                    var model = new HighScoreModel
                    {
                        PlayerId = Guid.Parse(resultObject.Key),
                        Score = int.Parse(convertedObject["score"].ToString()),
                        PlayerName = convertedObject["playerName"].ToString(),
                        LastLevel = int.Parse(convertedObject["lastLevel"].ToString()),
                    };
                    Debug.Log(model);
                    HighScoreList.Add(model);
                }
            }
        });
    }
}

