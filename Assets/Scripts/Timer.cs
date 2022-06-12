using System;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Text timerText;
    private TimeSpan remaining;

    private void Awake()
    {
        // set remaining to remaining time left for next device day
        remaining = new TimeSpan(24, 0, 0) - DateTime.Now.TimeOfDay;
        InvokeRepeating(nameof(UpdateText), 0, 30);
    }

    private void UpdateText()
    {
        timerText.text = $"{remaining.Hours}:{remaining.Minutes}";
    }
}