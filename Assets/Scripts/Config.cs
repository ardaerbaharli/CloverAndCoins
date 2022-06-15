using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PowerUp;
using UnityEngine;
using UnityEngine.Localization.Settings;

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

    public static IEnumerator LoadLocale(string languageIdentifier)
    {
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale =
            LocalizationSettings.AvailableLocales.Locales.First(x => x.Identifier.Code == languageIdentifier);
    }

    public static void BoughtPowerUp(PowerUpType type)
    {
        var typeString = type.ToString();
        var numberOfPowerUpsInType = PlayerPrefs.GetInt(typeString, 0);
        PlayerPrefs.SetInt(typeString, numberOfPowerUpsInType + 1);
    }

    public static Dictionary<PowerUpType, int> PowerUps;
    
    public static void DecreasePowerUp(PowerUpType type)
    {
        PowerUps[type]--;
        var typeString = type.ToString();
        var numberOfPowerUpsInType = PlayerPrefs.GetInt(typeString, 0);
        PlayerPrefs.SetInt(typeString, numberOfPowerUpsInType - 1);
    }

    public static void LoadPowerUps()
    {
        PowerUps = new Dictionary<PowerUpType, int>();
        foreach (var powerUpType in Enum.GetValues(typeof(PowerUpType)))
        {
            var typeString = powerUpType.ToString();
            var numberOfPowerUpsInType = PlayerPrefs.GetInt(typeString, 0);
            PowerUps.Add((PowerUpType) powerUpType, numberOfPowerUpsInType);
        }
    }
}