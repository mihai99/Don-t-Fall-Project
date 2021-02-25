using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitHandler : MonoBehaviour
{
    public GameObject Saver;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape)) {
            this.GetComponent<gameMenuHandler>().CloseGame();
        }
    }
    public void QuitGame()
    {
        Saver.GetComponent<inventoryHandler>().SaveInventory();
        Saver.GetComponent<optionsHandler>().SaveOptions();
        Saver.GetComponent<levelsCompletedHandler>().SaveLevels();
        Saver.GetComponent<charactersOwnedHandler>().SaveCharacters();
        Saver.GetComponent<mapSkinsHandler>().SaveMapSkins();
        Saver.GetComponent<HighScoreHandler>().SaveHighScore();
        Saver.GetComponent<FirebaseHandler>().UpdateHighScoreToDatabase();
        Application.Quit();
    }
}
