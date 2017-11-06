using System.Linq;
using Presentation.Player.Presenter;
using UnityEngine;

namespace Presentation.Player.View
{
    public class PlayerHealth : MonoBehaviour, HealthView
    {
        public AudioClip[] ouchClips; // Array of clips to play when the player is damaged.
        private SpriteRenderer healthBar; // Reference to the sprite renderer of the health bar.
        private PlayerControl playerControl; // Reference to the PlayerControl script.
        HealthPresenter presenter;
        Animator animator;


        void Awake()
        {
            playerControl = GetComponent<PlayerControl>();
            animator = GetComponent<Animator>();
            healthBar = GameObject.Find("HealthBar").GetComponent<SpriteRenderer>();
            presenter = new HealthPresenter(this, transform);
        }

        void Start()
        {
            presenter.OnStart(healthBar.transform.localScale);
        }

        void OnCollisionEnter2D(Collision2D col)
        {
            // If the colliding gameobject is an Enemy...
            if (col.gameObject.CompareTag("Enemy"))
            {
                presenter.OnEnemyCollision(col.transform);
            }
        }

        public void Animate(string animation)
        {
            animator.SetTrigger(animation);
        }

        public void TriggerAllColliders()
        {
            GetComponents<Collider2D>().ToList()
                .ForEach(col => col.isTrigger = true);
        }

        public void MovePlayerToFront()
        {
            GetComponentsInChildren<SpriteRenderer>().ToList()
                .ForEach(spr => spr.sortingLayerName = "UI");
        }

        public void DisablePlayerControl()
        {
            GetComponent<PlayerControl>().enabled = false;
        }

        public void DisableGunShot()
        {
            GetComponentInChildren<Gun>().enabled = false;
        }

        public void DisablePlayerJump()
        {
            playerControl.jump = false;
        }

        public void AddHurtForce(Vector3 hurtForce)
        {
            GetComponent<Rigidbody2D>().AddForce(hurtForce);
        }

        public void UpdateHealthBar(float health, Vector3 healthScale)
        {
            healthBar.material.color = Color.Lerp(Color.green, Color.red, 1 - health * 0.01f);
            healthBar.transform.localScale = new Vector3(healthScale.x * health * 0.01f, 1, 1);
        }

        public void PlayRandomHurtSound()
        {
            int i = Random.Range(0, ouchClips.Length);
            AudioSource.PlayClipAtPoint(ouchClips[i], transform.position);
        }
    }
}