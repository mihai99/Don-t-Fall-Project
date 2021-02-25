using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
public class optionsMeuHandler : MonoBehaviour
{
    private optionsHandler options;
    public GameObject volumeSlider;
    public GameObject sfxSelector;
    public GameObject nameInput;
    public Text NameText;
    private async Task Start()
    {
        options = GameObject.FindGameObjectWithTag("saver").GetComponent<optionsHandler>();
        await options.LoadOptions();
        volumeSlider.GetComponent<Slider>().value = options.volume;
        sfxSelector.GetComponent<Toggle>().isOn = options.sfx;
        nameInput.GetComponent<InputField>().text = options.playerName;
        setSFX();
        setVolume();
        setName();
    }
    public void setVolume()
    {
        options.volume = volumeSlider.GetComponent<Slider>().value;
        options.SaveOptions();
        AudioListener.volume = volumeSlider.GetComponent<Slider>().value;
   
    }
    public void setSFX()
    {
        options.sfx = sfxSelector.GetComponent<Toggle>().isOn;
        options.SaveOptions();
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("sfx"))
        {
            obj.GetComponent<AudioSource>().enabled = sfxSelector.GetComponent<Toggle>().isOn;
        }
    }
    public void setName()
    {
        NameText.text = nameInput.GetComponent<InputField>().text;
        options.playerName = nameInput.GetComponent<InputField>().text;
        options.SaveOptions();
    }
    public void exitGame()
    {
        options.SaveOptions();
        Application.Quit();
    }
}
