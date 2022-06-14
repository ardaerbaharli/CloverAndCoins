namespace PowerUp
{
    public class Drink : PowerUpEffect
    {
        public readonly PowerUpType powerUpType = PowerUpType.Drink;

        public override void Activate(PlayerController player)
        {
            player.remainingGuesses = 2;
            player.activatedPowerUp = this;
        }
    }
}