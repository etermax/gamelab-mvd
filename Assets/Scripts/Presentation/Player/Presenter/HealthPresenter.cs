using Presentation.Player.View;
using UnityEngine;

namespace Presentation.Player.Presenter
{
    public class HealthPresenter
    {
        float health = 100f;
        const string DieAnimation = "Die";
        readonly HealthView view;
        private readonly Transform transform;
        float lastHitTime;
        Vector3 healthScale;
        const float repeatDamagePeriod = 2f;
        const float hurtForce = 10f;
        const float damageAmount = 10f;
        Animator animator;

        public HealthPresenter(HealthView view, Transform transform)
        {
            this.view = view;
            this.transform = transform;
        }

        public void OnStart(Vector3 healthScale)
        {
            this.healthScale = healthScale;
        }

        public void OnEnemyCollision(Transform transform)
        {
            if (CanBeHitten())
            {
                // ... and if the player still has health...
                if (IsPlayerAlive())
                {
                    TakeDamage(transform);
                    lastHitTime = Time.time;
                }
                else
                {
                    view.TriggerAllColliders();
                    view.MovePlayerToFront();
                    view.DisablePlayerControl();
                    view.DisableGunShot();
                    view.Animate(DieAnimation);
                }
            }
        }

        private bool IsPlayerAlive()
        {
            return health > 0f;
        }

        private bool CanBeHitten()
        {
            return Time.time > lastHitTime + repeatDamagePeriod;
        }

        void TakeDamage(Transform enemy)
        {
            view.DisablePlayerJump();
            view.AddHurtForce(GetHurtVector(enemy) * hurtForce);

            DecreacePlayerHealth(damageAmount);

            view.UpdateHealthBar(health, healthScale);
            view.PlayRandomHurtSound();
        }

        private void DecreacePlayerHealth(float damageAmount)
        {
            health -= damageAmount;
        }

        private Vector3 GetHurtVector(Transform enemy)
        {
            return transform.position - enemy.position + Vector3.up * 5f;
        }
    }
}