using System.Collections.Generic;
using System.Diagnostics;
using PowerUp;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StoreMenuController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private Text coinsText;
    [SerializeField] private List<BuyPowerUpButton> buyPowerUpButtons;
    [SerializeField] private GameObject promptPanel;

    private void OnEnable()
    {
        StartCoroutine(Config.LoadLocale(Config.ActiveLanguage));
        coinsText.text = Config.Coins.ToString();
        buyPowerUpButtons.ForEach(b => b.CheckIfAffordable(Config.Coins));
    }

    public void GoBack()
    {
        mainMenuPanel.SetActive(true);
        gameObject.SetActive(false);
    }

    private int lastClickedPowerUpPrice;
    private PowerUpType lastClickedPowerUpType;

    public void BuyPowerUpButton(BuyPowerUpButton p)
    {
        lastClickedPowerUpPrice = p.Price;
        lastClickedPowerUpType = p.PowerUpType;
        if (!(Config.Coins >= lastClickedPowerUpPrice)) return;

        promptPanel.SetActive(true);
    }

    public void YesButton()
    {
        SoundManager.instance.BoughtPowerUp();
        Config.Coins -= lastClickedPowerUpPrice;
        Config.BoughtPowerUp(lastClickedPowerUpType);
        lastClickedPowerUpPrice = 0;
        lastClickedPowerUpType = PowerUpType.None;
        coinsText.text = Config.Coins.ToString();
        buyPowerUpButtons.ForEach(b => b.CheckIfAffordable(Config.Coins));
        promptPanel.SetActive(false);
    }

    public void NoButton()
    {
        promptPanel.SetActive(false);
    }
}