namespace PowerUp
{
    public class SmokingPipe : PowerUpEffect
    {
        public readonly PowerUpType powerUpType = PowerUpType.SmokingPipe;

        public override void Activate(PlayerController player)
        {
            player.activatedSmokingPipe = true;
        }
    }
}