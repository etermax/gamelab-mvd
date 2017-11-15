using Core.Domain.Stats;
using Presentation.Providers;
using UnityEngine;

namespace Presentation.Game
{
    public interface GameView
    {
        void UpdateScore(int score);
        void UpdatePreviousScore(PlayerScore playerScore);
        void UpdateHighScore(int score);
    }

    public class GameController : MonoBehaviour, GameView
    {
        public Score scoreController;
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