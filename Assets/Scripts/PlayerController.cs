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
        activatedPowerUp = true;
        powerUpEffects.First(x => x.PowerUpType == powerUpType).Activate(this);
    }

    public void Lose()
    {
    }

    public void Win()
    {
        Config.Coins++;
    }
}