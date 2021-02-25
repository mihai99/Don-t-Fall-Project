using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

[System.Serializable]
 public class optionsData
 {
    
     public string name;     
     public bool sfx;
     public float volume;
     public optionsData(string nameInt,  bool sfxInt, float volumeInt)
     {
       
         name = nameInt;
         sfx = sfxInt;
         volume = volumeInt;
    }
 }

public class optionsHandler : MonoBehaviour
{
    

    public string playerName;

    public bool sfx;
    public float volume;

    public async Task Awake()
    {
        InitData();
        await LoadOptions();
    }
    private void InitData()
    {
        playerName = "New Player";
        sfx = true;
        volume = 100;
    }
    public void SaveOptions()
     {
         string destination = Application.persistentDataPath + "/saveOptions.dat";
         FileStream file;

        if (File.Exists(destination))
        {
            file = File.OpenWrite(destination);
            optionsData data = new optionsData(playerName, sfx, volume);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(file, data);
            this.GetComponent<FirebaseHandler>().UpdateHighScoreToDatabase();
            file.Close();
        }
        else
        {
            file = File.Create(destination);
            SaveOptions();
        }
     }
 
     public async Task<bool> LoadOptions()
     {
         string destination = Application.persistentDataPath + "/saveOptions.dat";
         FileStream file;

        if (File.Exists(destination))
        {
            file = File.OpenRead(destination);
            BinaryFormatter bf = new BinaryFormatter();
            optionsData data = (optionsData)bf.Deserialize(file);
            file.Close();
            Debug.Log("player name: " + data.name.ToString());
            playerName = data.name;
            sfx = data.sfx;
            volume = data.volume;
            return true;
        }
        else
        {
            InitData();
            SaveOptions();
            return false;
        }       
     }
    
}

