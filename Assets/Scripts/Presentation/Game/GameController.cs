using Core.Domain.Stats;
using Presentation.Providers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    }

    public class GameController : MonoBehaviour, GameView
    {
        public Score scoreController;
        private GamePresenter gamePresenter;
        public GameOverPopup GameOverPopup;

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
            scoreController.score = score;
        }

        public void UpdatePreviousScore(PlayerScore playerScore)
        {
            scoreController.highestScore = playerScore.Score;
        }

        public void UpdateHighScore(int score)
        {
            scoreController.highestScore = score;
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

        public void OnPlayerEmptyHealth(IPlayerHealth playerHealth, IPlayer player)
        {
            gamePresenter.OnPlayerEmptyHealth(playerHealth, player);
        }

        public void OnPlayerFalls(IPlayer player)
        {
            gamePresenter.OnPlayerFalls(player);
        }
    }
}