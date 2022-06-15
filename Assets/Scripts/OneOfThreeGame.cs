using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PowerUp;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization.Components;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Random;

public class OneOfThreeGame : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Text numberOfDrinkPowerUps;
    [SerializeField] private Text numberOfSmokingPipePowerUps;
    [SerializeField] private Text lepText;
    [SerializeField] private List<Hat> hats;
    [SerializeField] private GameObject coin;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private float hatLiftedPosY;
    [SerializeField] private float hatLoweredPosY;
    [SerializeField] private GameObject hatsContainer;
    [SerializeField] private Text coinsText;

    private Vector3 leftHatPos, rightHatPos;
    private int luckyNumber;

    private void Awake()
    {
        Application.targetFrameRate = 30;

    }

    private void Start()
    {
        SoundManager.instance.Play1of3Song();
    }

    private void OnEnable()
    {
        StartCoroutine(Config.LoadLocale(Config.ActiveLanguage));

        playerController.remainingGuesses = 1;
        Config.LoadPowerUps();
        numberOfDrinkPowerUps.text = "x" + Config.PowerUps[PowerUpType.Drink];
        numberOfSmokingPipePowerUps.text = "x" + Config.PowerUps[PowerUpType.SmokingPipe];

        luckyNumber = Range(0, 3);

        foreach (var hat in hats)
        {
            hat.onClicked += OnHatClick;
        }

        leftHatPos = new Vector3(hats.First().rect.anchoredPosition.x, hatLoweredPosY, 0);
        rightHatPos = new Vector3(hats.Last().rect.anchoredPosition.x, hatLoweredPosY, 0);
    }

    public void StartGame()
    {
        GetComponent<Button>().enabled = false;
        StartCoroutine(StartGameC());
    }

    private IEnumerator StartGameC()
    {
        yield return new WaitForSeconds(0.3f);
        hats.ForEach(x => StartCoroutine(LiftHat(x)));
        coin.SetActive(true);

        yield return new WaitForSeconds(1);
        StartCoroutine(LowerHats());

        yield return new WaitUntil(() => hats.All(x => !x.isMoving));
        coin.SetActive(false);

        StartCoroutine(ShuffleHats());
    }


    private IEnumerator LowerHats()
    {
        foreach (var hat in hats)
        {
            var hatRect = hat.rect;
            var hatPos = hatRect.anchoredPosition;
            var target = new Vector3(hatPos.x, hatLoweredPosY, 0);
            StartCoroutine(MoveTo(hatRect, target, 0.5f, hat));
            yield return null;
        }
    }

    private IEnumerator MoveTo(RectTransform r, Vector3 targetPos, float time, Hat randomHat)
    {
        randomHat.isMoving = true;

        var startPos = r.anchoredPosition;
        var deltaTime = 0f;
        while (deltaTime < 1)
        {
            deltaTime += Time.deltaTime / time;
            r.anchoredPosition = Vector3.Lerp(startPos, targetPos, deltaTime);
            yield return null;
        }

        randomHat.isMoving = false;
    }

    private IEnumerator ShuffleHats()
    {
        print("ShuffleHats");
        // Move the hats to left and right between the left and right hat positions for 1 second

        foreach (var hat in hats)
        {
            hat.StartMoving(5, leftHatPos, rightHatPos, hatLoweredPosY);
            yield return null;
        }

        yield return new WaitUntil(() => hats.All(x => !x.isMoving));
        hatsContainer.GetComponent<HorizontalLayoutGroup>().enabled = true;
        hats.ForEach(x => x.gameObject.GetComponent<Button>().enabled = true);
    }

    private IEnumerator LiftHat(Hat hat)
    {
        var hatPos = hat.rect.anchoredPosition;
        var target = new Vector3(hatPos.x, hatLiftedPosY, 0);
        yield return StartCoroutine(MoveTo(hat.rect, target, 0.5f, hat));
    }

    private void OnHatClick(int id)
    {
        hatsContainer.GetComponent<HorizontalLayoutGroup>().enabled = false;
        if (playerController.remainingGuesses <= 0) return;

        StartCoroutine(OnHatClickCoroutine(id));
    }

    private IEnumerator OnHatClickCoroutine(int id)
    {
        playerController.remainingGuesses--;
        var didWin = false;
        if (playerController.activatedSmokingPipe || id == luckyNumber)
        {
            luckyNumber = id;
            didWin = true;
        }

        var coinRect = coin.GetComponent<RectTransform>();
        var coinPos = coinRect.localPosition;
        coinRect.localPosition = new Vector2(hats[luckyNumber].rect.localPosition.x, coinPos.y);

        if (didWin)
        {
            coin.SetActive(true);
        }

        yield return StartCoroutine(LiftHat(hats[id]));

        yield return new WaitForSeconds(1);
        if (didWin)
        {
            winPanel.SetActive(true);
            playerController.Win();
        }
        else
        {
            if (playerController.remainingGuesses > 0)
            {
                lepText.GetComponent<LocalizeStringEvent>().SetEntry("TooBadTryAgain");
            }
            else
            {
                playerController.Lose();
                coinsText.text = Config.Coins.ToString();
                gameOverPanel.SetActive(true);
            }
        }
    }

    public void ReplayButton()
    {
        SceneManager.LoadScene("Game13");
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

    public void ActivatePowerUpButton()
    {
        if (playerController.activatedPowerUp) return;
        var clickedButton = EventSystem.current.currentSelectedGameObject.GetComponent<PowerUpButton>();
        if (Config.PowerUps[clickedButton.PowerUpType] <= 0) return;
        Config.DecreasePowerUp(clickedButton.PowerUpType);
        numberOfDrinkPowerUps.text = "x" + Config.PowerUps[PowerUpType.Drink];
        numberOfSmokingPipePowerUps.text = "x" + Config.PowerUps[PowerUpType.SmokingPipe];
        playerController.ActivatePowerUp(clickedButton.PowerUpType);
    }

    public void GoBack()
    {
        SceneManager.LoadScene("Games");
    }
}