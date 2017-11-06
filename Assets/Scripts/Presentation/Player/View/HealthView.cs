using UnityEngine;

namespace Presentation.Player.View
{
    public interface HealthView
    {
        void Animate(string dieAnimation);
        void TriggerAllColliders();
        void MovePlayerToFront();
        void DisablePlayerControl();
        void DisableGunShot();
        void DisablePlayerJump();
        void AddHurtForce(Vector3 hurtForce);
        void UpdateHealthBar(float health, Vector3 healthScale);
        void PlayRandomHurtSound();
    }
}
