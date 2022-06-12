using System;
using UnityEngine;

public class Config
{
    public static bool IsVolumeOn
    {
        get => PlayerPrefsX.GetBool("IsVolumeOn", true);
        set => PlayerPrefsX.SetBool("IsVolumeOn", value);
    }

    public static bool IsVibrationOn
    {
        get => PlayerPrefsX.GetBool("IsVibrationOn", true);
        set => PlayerPrefsX.SetBool("IsVibrationOn", value);
    }

    public static string ActiveLanguage
    {
        get => PlayerPrefs.GetString("ActiveLanguage", "en");
        set => PlayerPrefs.SetString("ActiveLanguage", value);
    }

    public static bool EligibleForBonus => LastBonusDay != DateTime.Now.Day;

    public static int LastBonusDay
    {
        get => PlayerPrefs.GetInt("LastBonusDay", DateTime.Now.Day);
        set => PlayerPrefs.SetInt("LastBonusDay", value);
    }

    public static int Coins
    {
        get => PlayerPrefs.GetInt("Coins", 0);
        set => PlayerPrefs.SetInt("Coins", value);
    }

    public static float MaxSoundVolume = -10f;

    // public static void LoadLocale(string languageIdentifier)
    // {
    // LocalizationSettings.SelectedLocale =
    // LocalizationSettings.AvailableLocales.Locales.First(x => x.Identifier.Code == languageIdentifier);
    // }
}