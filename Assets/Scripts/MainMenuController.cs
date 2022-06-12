using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject settngsPanel;
    [SerializeField] private GameObject bonusPanel;
    [SerializeField] private GameObject storePanel;
    [SerializeField] private GameObject chooseGamePanel;

    [SerializeField] private Text coinsText;

    private void OnEnable()
    {
        coinsText.text = Config.Coins.ToString();
    }

    public void StoreButton()
    {
        storePanel.SetActive(true);
        gameObject.SetActive(false);
    }

    public void SettingsButton()
    {
        settngsPanel.SetActive(true);
        gameObject.SetActive(false);
    }

    public void GameButton()
    {
        chooseGamePanel.SetActive(true);
        gameObject.SetActive(false);
    }

    public void BonusButton()
    {
        bonusPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}