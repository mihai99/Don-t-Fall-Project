using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class FirebaseHandler : MonoBehaviour
{
    public Guid PlayerId;
    private DatabaseReference Database;
    private GetHelpers GetHelper;
    private UpdateHelper UpdateHelper;
    public bool LoadedHighScores = false;
    // Start is called before the first frame update
    void Start()
    {
        GetHelper = GetComponent<GetHelpers>();
        UpdateHelper = GetComponent<UpdateHelper>();
        GetPlayerId();
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://ragequitter-5fc5a.firebaseio.com/");
        Database = FirebaseDatabase.DefaultInstance.RootReference;
        GetHighscoresList();
    }
    public void ClearHighscores() => GetHelper.HighScoreList = new List<HighScoreModel>();

    public void UpdateHighScoreToDatabase() => UpdateHelper.UpdatableHighScore(HighScoreModel.BuildLocalHighScore(PlayerId), Database);       
    
    public void GetHighscoresList() => GetHelper.GetAllHighscores(Database);

    private void GetPlayerId()
    {
        if (PlayerPrefs.GetInt("HasPlayed") == 1)
        {
            PlayerId = Guid.Parse(PlayerPrefs.GetString("PlayerId"));
        }
        else
        {
            PlayerId = Guid.NewGuid();
            PlayerPrefs.SetInt("HasPlayed", 1);
            PlayerPrefs.SetString("PlayerId", PlayerId.ToString());
            PlayerPrefs.Save();
        }
    }
    private void Update()
    {
        LoadedHighScores = GetHelper.HighScoreList.Count != 0 ? true : false;
    }
}
