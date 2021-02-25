using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

[System.Serializable]
 public class currentLevelSaver
 {
     public int lastLevelDone;
     public int[] stars;
     public bool tutorialDone;
     public currentLevelSaver(int level, List<int> stars, bool tutorial)
     {
         this.lastLevelDone = level;
         this.stars = stars.ToArray();
        this.tutorialDone = tutorial;
     }
 }

public class levelsCompletedHandler : MonoBehaviour
{
    public int lastLevel;
    public List<int> stars;
    public bool tutorialDone;
    public levelsMenuHandler LevelsMenuHandler;
    public async Task Awake()
    {
        InitData();
        await LoadLevels();
    }
    public void InitData()
    {
        lastLevel = 0;
        stars = new List<int>();
        for (var i = 0; i < 100; i++)
            stars.Add(0);
        tutorialDone = false;
    }
    // Start is called before the first frame update public 
    public void SaveLevels()
     {
         string destination = Application.persistentDataPath + "/saveLevels.dat";
         FileStream file;

        if (File.Exists(destination))
        {
            file = File.OpenWrite(destination);
            currentLevelSaver data = new currentLevelSaver(lastLevel, stars, tutorialDone);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(file, data);
            file.Close();
        }
        else
        {
            file = File.Create(destination);
            SaveLevels();
        }
    }
 
     public async Task<bool> LoadLevels()
     {
         string destination = Application.persistentDataPath + "/saveLevels.dat";
         FileStream file;

        if (File.Exists(destination))
        {
            file = File.OpenRead(destination);
            BinaryFormatter bf = new BinaryFormatter();
            currentLevelSaver data = (currentLevelSaver)bf.Deserialize(file);
            file.Close();
            lastLevel = data.lastLevelDone;
            starsArrayToList(data.stars);
            tutorialDone = data.tutorialDone;
            return true;
        }
        else
        {
            InitData();
            this.SaveLevels();
            return false;
        }
     }
     
    public void starsArrayToList(int[] array)
    {
        for (int i = 0; i < array.Length; i++)
            this.stars[i] = array[i];
    }
}
