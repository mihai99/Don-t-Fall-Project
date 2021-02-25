using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{

    private Slider Slider;
    private void Start()
    {
        Slider = GetComponent<Slider>();
        Invoke("LoadGame", 0.5f);
    }
    private void LoadGame()
    {
        StartCoroutine(LoadSceneAsync());
    }
    private IEnumerator LoadSceneAsync()
    {
        AsyncOperation loading =  SceneManager.LoadSceneAsync("mainScene");
        while(!loading.isDone)
        {
            Slider.value = Mathf.Clamp01(loading.progress / .9f);
            yield return null;
        }
    }
}
