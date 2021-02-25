using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

[System.Serializable]
public class savedCharacters
{
    public List<bool> owned = new List<bool>();
    public PlayerModel selected;
    public savedCharacters(List<bool> owned, PlayerModel selected)
    {
        this.owned = owned;
        this.selected = selected;
    }
}
public class charactersOwnedHandler : MonoBehaviour
{

    public List<bool> owned;
    public PlayerModel selected;

    public async Task Awake()
    {
        InitData();
        await LoadCharacters();
    }

    private void InitData()
    {
        selected = PlayerModel.Basic;
        owned = new List<bool>() {
                true, false, false, false, false
            };
    }

    public void SaveCharacters()
    {
        string destination = Application.persistentDataPath + "/saveCharacters.dat";
        FileStream file;

        if (File.Exists(destination))
        {
            file = File.OpenWrite(destination);
            savedCharacters data = new savedCharacters(owned, selected);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(file, data);
            file.Close();
        }
        else
        {
            file = File.Create(destination);
            SaveCharacters();
        }
    }

    public async Task<bool> LoadCharacters()
    {
        string destination = Application.persistentDataPath + "/saveCharacters.dat";
        FileStream file;

        if (File.Exists(destination))
        {
            file = File.OpenRead(destination);
            BinaryFormatter bf = new BinaryFormatter();
            savedCharacters data = (savedCharacters)bf.Deserialize(file);
            file.Close();
            this.owned = data.owned;
            this.selected = data.selected;
            return true;
        }
        else
        {
            InitData();
            SaveCharacters();
            return false;
        }       
    }
}
