namespace PowerUp
{
    public class BagPipe : PowerUpEffect
    {
        public readonly PowerUpType powerUpType = PowerUpType.BagPipe;

        public override void Activate(PlayerController player)
        {
            player.numberOfChoices = 2;
            player.activatedPowerUp = this;

        }
    }
}