using PowerUp;
using UnityEngine;
using UnityEngine.UI;

public class BuyPowerUpButton : MonoBehaviour
{
    [SerializeField] public PowerUpType PowerUpType;
    [SerializeField] public int Price;
    
    [SerializeField] private Color offColor;
    [SerializeField] private Color onColor;
    [SerializeField] private Button button;
    [SerializeField] private Text priceText;
    [SerializeField] private Text descriptionText;


    public void CheckIfAffordable(int coins)
    {
        if (coins >= Price)
        {
            button.interactable = true;
            priceText.color = onColor;
            descriptionText.color = onColor;
        }
        else
        {
            button.interactable = false;
            priceText.color = offColor;
            descriptionText.color = offColor;
        }
    }
}