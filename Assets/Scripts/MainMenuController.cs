using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject bonusPanel;
    [SerializeField] private GameObject storePanel;

    [SerializeField] private Text coinsText;


    private void Awake()
    {
        // Fix fps
        Application.targetFrameRate = 30;
    }

    private void Start()
    {
        SoundManager.instance.PauseGame1Background();
        SoundManager.instance.PauseGame2Background();
        SoundManager.instance.PlayMenuSong();
    }

    private void OnEnable()
    {
        StartCoroutine(Config.LoadLocale(Config.ActiveLanguage));

        coinsText.text = Config.Coins.ToString();
    }


    public void StoreButton()
    {
        storePanel.SetActive(true);
        gameObject.SetActive(false);
    }

    public void SettingsButton()
    {
        settingsPanel.SetActive(true);
        gameObject.SetActive(false);
    }

    public void GameButton()
    {
        SceneManager.LoadScene("Games");
    }

    public void BonusButton()
    {
        bonusPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}