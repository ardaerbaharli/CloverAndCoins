using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Random;

public class BonusMenuController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private Leaf leaf1;
    [SerializeField] private Leaf leaf2;
    [SerializeField] private Leaf leaf3;
    [SerializeField] private Leaf leaf4;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject blur;

    [SerializeField] private int answer;

    public void GoBack()
    {
        mainMenuPanel.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OkButton()
    {
        blur.SetActive(false);
        winPanel.SetActive(false);
        losePanel.SetActive(false);
    }

    private void OnEnable()
    {
        answer = Range(1, 5);

        leaf1.onPointerDown_ += OnLeafClick;
        leaf2.onPointerDown_ += OnLeafClick;
        leaf3.onPointerDown_ += OnLeafClick;
        leaf4.onPointerDown_ += OnLeafClick;
    }

    private void OnDisable()
    {
        leaf1.onPointerDown_ -= OnLeafClick;
        leaf2.onPointerDown_ -= OnLeafClick;
        leaf3.onPointerDown_ -= OnLeafClick;
        leaf4.onPointerDown_ -= OnLeafClick;
    }

    private void OnLeafClick(int id)
    {
        if (Config.EligibleForBonus)
            CheckAnswer(id);

        Config.LastBonusDay = DateTime.Now.Day;
    }

    private void CheckAnswer(int selection)
    {
        blur.SetActive(true);
        if (selection == answer)
        {
            SoundManager.instance.Won();
            Config.Coins++;
            winPanel.SetActive(true);
        }
        else
        {
            SoundManager.instance.Failed();
            losePanel.SetActive(true);
        }
    }
}