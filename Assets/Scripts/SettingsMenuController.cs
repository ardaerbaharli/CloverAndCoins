using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuController : MonoBehaviour
{
    [SerializeField] private ToggleSwitch volumeToggle;
    [SerializeField] private ToggleSwitch vibrationToggle;
    [SerializeField] private DoubleSwitch languageToggle;
    [SerializeField] private GameObject mainMenuPanel;


    public void GoBack()
    {
        mainMenuPanel.SetActive(true);
        gameObject.SetActive(false);
    }


    private void OnEnable()
    {
        StartCoroutine(Config.LoadLocale(Config.ActiveLanguage));
        volumeToggle.Toggle(Config.IsVolumeOn);
        vibrationToggle.Toggle(Config.IsVibrationOn);
        languageToggle.SetSwitchNames("en", "ru");
        languageToggle.ActivateSwitch(Config.ActiveLanguage);
        
        
        volumeToggle.valueChanged += OnVolumeChanged;
        vibrationToggle.valueChanged += OnVibrationChanged;
        languageToggle.OnSwitchChanged += OnLanguageChanged;
    }


    private void OnDisable()
    {
        volumeToggle.valueChanged -= OnVolumeChanged;
        vibrationToggle.valueChanged -= OnVibrationChanged;
    }


    private void OnLanguageChanged(string s)
    {
        if (Config.ActiveLanguage.Equals(s)) return;
        Config.ActiveLanguage = s;
        StartCoroutine(Config.LoadLocale(Config.ActiveLanguage));
    }

    private void OnVibrationChanged(bool value)
    {
        Config.IsVibrationOn = value;
    }

    private void OnVolumeChanged(bool value)
    {
        Config.IsVolumeOn = value;
        SoundManager.instance.SetSound(value);
    }
}