using System.Collections.Generic;
using PowerUp;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    [SerializeField] public List<PowerUpEffect> powerUps;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        Config.LoadPowerUps();
    }
}