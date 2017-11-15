using Core.Domain.Stats;
using Presentation.Providers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Presentation.Game
{
    public interface GameView
    {
        void UpdateScore(int score);
        void UpdatePreviousScore(PlayerScore playerScore);
        void UpdateHighScore(int score);
        void ShowPopup();
        void RestartGame();
        void Stop();
        Vector3 GetPlayerPosition();
        void LeaveBombAt(Vector3 playerPosition);
        void ShowBombOnHud();
        void HideBombFromHud();
    }

    public class GameController : MonoBehaviour, GameView
    {
        public Score ScoreController;
        public GameOverPopup GameOverPopup;
        public GUITexture BombHUD;
        public AudioClip BombsAway;
        public GameObject BombPrfab;
        public GameObject Hero;
        
        private GamePresenter gamePresenter;

        private void Awake()
        {
            gamePresenter = GameViewProvider.ProviderGamePresenter(this);
        }

        private void Start()
        {
            gamePresenter.OnStart();
        }

        public void OnRocketImpactsEnemy(Rocket rocket, Enemy enemy)
        {
            gamePresenter.OnRocketImpactsEnemy(rocket, enemy);
        }

        public void OnRocketImpactsBomb(Rocket rocket, Bomb bomb)
        {
            gamePresenter.OnRocketImpactsBomb(rocket, bomb);
        }

        public void OnRocketImpactsWithSomethingElse(Rocket rocket)
        {
            gamePresenter.OnRocketImpactsWithSomethingElse(rocket);
        }

        public void OnEnemyHitsWithObstacle(Enemy enemy)
        {
            gamePresenter.OnEnemyHitsWithObstacle(enemy);
        }

        public void UpdateScore(int score)
        {
            ScoreController.score = score;
        }

        public void UpdatePreviousScore(PlayerScore playerScore)
        {
            ScoreController.highestScore = playerScore.Score;
        }

        public void UpdateHighScore(int score)
        {
            ScoreController.highestScore = score;
        }

        public void ShowPopup()
        {
            GameOverPopup.ContinueButton.onClick.AddListener(ConfirmAction);
            GameOverPopup.gameObject.SetActive(true);
        }

        private void ConfirmAction()
        {
            gamePresenter.OnRestartConfirmed();
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        }

        public void Stop()
        {
            Time.timeScale = 0;
        }

        public Vector3 GetPlayerPosition()
        {
            return Hero.transform.position;
        }

        public void LeaveBombAt(Vector3 playerPosition)
        {
            Instantiate(BombPrfab, playerPosition, transform.rotation);
        }

        public void ShowBombOnHud()
        {
            BombHUD.enabled = true;
        }
        
        public void HideBombFromHud()
        {
            BombHUD.enabled = false;
        }

        public void OnPlayerEmptyHealth(IPlayerHealth playerHealth, IPlayer player)
        {
            gamePresenter.OnPlayerEmptyHealth(playerHealth, player);
        }

        public void OnPlayerFalls(IPlayer player)
        {
            gamePresenter.OnPlayerFalls(player);
        }

        public void OnButton2Pressed()
        {
            gamePresenter.OnButton2Pressed();
        }

        public void OnBombPickedUp()
        {
            gamePresenter.OnBombPickedUp();
        }

        public void OnBombExplode()
        {
            gamePresenter.OnBombExplode();
        }
    }
}