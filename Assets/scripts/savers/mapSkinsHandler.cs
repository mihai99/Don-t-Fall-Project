using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;


[System.Serializable]
public class savedMapSkins
{
    public List<bool> owned = new List<bool>();
    public MapSkinType selected;
    public savedMapSkins(List<bool> owned, MapSkinType selected)
    {
        this.owned = owned;
        this.selected = selected;
    }
}
public class mapSkinsHandler : MonoBehaviour
{
        // Start is called before the first frame update

        public List<bool> owned;
        public MapSkinType selected;
    public async Task Awake()
    {
        InitData();
        await LoadMapSkins();
    }
    private void InitData()
    {
        this.selected = MapSkinType.Basic;
        owned = new List<bool>() {
                    true, false, false, false, false, false
                };
    }
    public void SaveMapSkins()
    {
        string destination = Application.persistentDataPath + "/saveMapSkins.dat";
        FileStream file;

        if (File.Exists(destination))
        {
            file = File.OpenWrite(destination);
            savedMapSkins data = new savedMapSkins(owned, selected);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(file, data);
            file.Close();
        }
        else
        {
            file = File.Create(destination);
            SaveMapSkins();
        }
    }

        public async Task<bool> LoadMapSkins()
        {
            string destination = Application.persistentDataPath + "/saveMapSkins.dat";
            FileStream file;

            if (File.Exists(destination))
            {
                file = File.OpenRead(destination);
                BinaryFormatter bf = new BinaryFormatter();
                savedMapSkins data = (savedMapSkins)bf.Deserialize(file);
                file.Close();

                this.owned = data.owned;
                this.selected = data.selected;
                return true;
            }
            else
            {
                InitData();
                SaveMapSkins();            
                return false;
            }
        }
    }
