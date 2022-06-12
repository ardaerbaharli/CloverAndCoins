namespace PowerUp
{
    public class Horseshoe : PowerUpEffect
    {
        public readonly PowerUpType powerUpType = PowerUpType.Horseshoe;

        public override void Activate(PlayerController player)
        {
            player.numberOfChoices = 3;
        }
    }
}