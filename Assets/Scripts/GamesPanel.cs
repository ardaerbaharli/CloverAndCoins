using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamesPanel : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(Config.LoadLocale(Config.ActiveLanguage));
    }

    private void Awake()
    {
        Application.targetFrameRate = 30;

    }

    public void GoBack()
    {
        SceneManager.LoadScene("Main");
    }

    public void PlayButton(string gameName)
    {
        SoundManager.instance.PauseMenuSong();
        switch (gameName)
        {
            case "Game13":
                SoundManager.instance.PauseGame2Background();
                break;
            case "GameBigger":
                SoundManager.instance.PauseGame1Background();
                break;
        }

        SceneManager.LoadScene(gameName);
    }
}