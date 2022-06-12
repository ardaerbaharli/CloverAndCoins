using UnityEngine;

namespace PowerUp
{
    public abstract class PowerUpEffect : MonoBehaviour
    {
        public PowerUpType PowerUpType;
        public abstract void Activate(PlayerController player);

    }
}