using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PowerUp;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Random;

public class BiggerGame : MonoBehaviour
{
    [SerializeField] private List<Pot> pots;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Text numberOfBagPipePowerUps;
    [SerializeField] private Text numberOfHorseshoePowerUps;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Text coinsText;
    [SerializeField] private Text lepT;

    private void Awake()
    {
        Application.targetFrameRate = 30;

    }

    private void Start()
    {
        SoundManager.instance.PlayBiggerSong();
    }

    private void OnEnable()
    {
        StartCoroutine(Config.LoadLocale(Config.ActiveLanguage));

        playerController.numberOfChoices = 1;
        Config.LoadPowerUps();
        numberOfBagPipePowerUps.text = "x" + Config.PowerUps[PowerUpType.BagPipe];
        numberOfHorseshoePowerUps.text = "x" + Config.PowerUps[PowerUpType.Horseshoe];

        foreach (var pot in pots)
        {
            pot.onClicked += OnPotClick;
            var value = Range(1, 101);
            while (pots.Any(x => x.value == value))
            {
                value = Range(1, 101);
            }

            pot.SetValue(value);
        }

        // select pot with biggest value
        pots.OrderByDescending(x => x.value).First().isBiggest = true;
    }

    private void OnPotClick(Pot pot)
    {
        if (playerController.numberOfChoices <= 0) return;

        StartCoroutine(OnPotClickCoroutine(pot));
    }

    public void GoBack()
    {
        SceneManager.LoadScene("Games");
    }

    public void ActivatePowerUpButton()
    {
        if (playerController.activatedPowerUp) return;
        var clickedButton = EventSystem.current.currentSelectedGameObject.GetComponent<PowerUpButton>();
        if (Config.PowerUps[clickedButton.PowerUpType] <= 0) return;
        Config.DecreasePowerUp(clickedButton.PowerUpType);
        numberOfBagPipePowerUps.text = "x" + Config.PowerUps[PowerUpType.BagPipe];
        numberOfHorseshoePowerUps.text = "x" + Config.PowerUps[PowerUpType.Horseshoe];

        playerController.ActivatePowerUp(clickedButton.PowerUpType);
    }

    private IEnumerator OnPotClickCoroutine(Pot pot)
    {
        playerController.numberOfChoices--;
        var didWin = pot.isBiggest;
        if (!didWin && playerController.numberOfChoices > 0)
        {
            lepT.GetComponent<LocalizeStringEvent>().SetEntry("TooBadTryAgain");
            yield break;
        }

        foreach (var p in pots)
        {
            p.ShowValue();
            yield return new WaitForSeconds(0.5f);
        }

        yield return new WaitForSeconds(1f);

        if (didWin)
        {
            winPanel.SetActive(true);
            playerController.Win();
        }
        else
        {
            playerController.Lose();
            coinsText.text = Config.Coins.ToString();
            gameOverPanel.SetActive(true);
        }
    }

    public void ReplayButton()
    {
        SceneManager.LoadScene("GameBigger");
    }

    public void GamesButton()
    {
        SceneManager.LoadScene("Games");
    }

    public void OkButton()
    {
        winPanel.SetActive(false);
        coinsText.text = Config.Coins.ToString();

        gameOverPanel.SetActive(true);
    }
}