using System.Collections.Generic;
using System.Linq;
using PowerUp;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private List<PowerUpEffect> powerUpEffects;

    #region OneOf3

    public int remainingGuesses;
    public bool activatedSmokingPipe;

    #endregion

    #region Which one has more coins

    public int numberOfChoices;

    #endregion

    public PowerUpEffect activePowerUp;
    public bool activatedPowerUp;

    public void ActivatePowerUp(PowerUpType powerUpType)
    {
        SoundManager.instance.ActivatePowerUp();
        activatedPowerUp = true;
        
        powerUpEffects.First(x => x.PowerUpType == powerUpType).Activate(this);
    }

    public void Lose()
    {
        Vibration.Vibrate(3);
        SoundManager.instance.Failed();
    }

    public void Win()
    {
        Vibration.Vibrate(3);
        SoundManager.instance.Won();
        Config.Coins++;

    }
}