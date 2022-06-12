using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuController : MonoBehaviour
{
    [SerializeField] private ToggleSwitch volumeToggle;
    [SerializeField] private ToggleSwitch vibrationToggle;
    [SerializeField] private ToggleSwitch languageToggleRu;
    [SerializeField] private ToggleSwitch languageToggleEn;
    [SerializeField] private GameObject mainMenuPanel;
    

    public void GoBack()
    {
        mainMenuPanel.SetActive(true);
        gameObject.SetActive(false);
    }


    private void OnEnable()
    {
        volumeToggle.Toggle(Config.IsVolumeOn);
        vibrationToggle.Toggle(Config.IsVibrationOn);

        volumeToggle.valueChanged += OnVolumeChanged;
        vibrationToggle.valueChanged += OnVibrationChanged;
        languageToggleRu.valueChanged += OnLanguageChangedRu;
        languageToggleEn.valueChanged += OnLanguageChangedEn;
    }

    private void OnDisable()
    {
        volumeToggle.valueChanged -= OnVolumeChanged;
        vibrationToggle.valueChanged -= OnVibrationChanged;
    }

    private void OnLanguageChangedEn(bool value)
    {
        languageToggleRu.Toggle(!value,false);
        Config.ActiveLanguage = "en";
        // Config.LoadLocale(Config.ActiveLanguage);
    }

    private void OnLanguageChangedRu(bool value)
    {
        languageToggleEn.Toggle(!value,false);
        Config.ActiveLanguage = "ru";
        // Config.LoadLocale(Config.ActiveLanguage);
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