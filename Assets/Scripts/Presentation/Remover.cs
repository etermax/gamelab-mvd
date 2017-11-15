using System.Collections;
using Presentation.Game;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Presentation
{
    public class Remover : MonoBehaviour
    {
        public GameObject splash;
        private GameController gameCotroller;

        private void Awake()
        {
            gameCotroller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        }

        void OnTriggerEnter2D(Collider2D collider)
        {
            // If the player hits the trigger...
            if (collider.gameObject.CompareTag("Player"))
            {    
                // .. stop the camera tracking the player
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>().enabled = false;

                // .. stop the Health Bar following the player
                var healtBar = GameObject.FindGameObjectWithTag("HealthBar");
                if (healtBar != null && healtBar.activeSelf)
                {
                    healtBar.SetActive(false);
                }

                // ... instantiate the splash where the player falls in.
                Instantiate(splash, collider.transform.position, transform.rotation);
                gameCotroller.OnPlayerFalls(collider.GetComponent<PlayerControl>());
            }
            else
            {
                // ... instantiate the splash where the enemy falls in.
                Instantiate(splash, collider.transform.position, transform.rotation);

                // Destroy the enemy.
                Destroy(collider.gameObject);
            }
        }

        IEnumerator ReloadGame()
        {
            // ... pause briefly
            yield return new WaitForSeconds(2);
            // ... and then reload the level.
        }
    }
}