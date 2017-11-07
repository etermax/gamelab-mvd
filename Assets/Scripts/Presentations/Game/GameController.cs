using Core.Domain.Stats;
using Presentations.Factory;
using UnityEngine;

namespace Presentations.Game
{
    public interface GameView
    {
        void UpdateScore(int score);
        void UpdatePreviousScore(PlayerScore playerScore);
        void ShowHighScoreBeatedMessage();
    }

    public class GameController : MonoBehaviour, GameView
    {
        public Score scoreController;
        private GamePresenter gamePresenter;

        private void Awake()
        {
            gamePresenter = GameViewFactory.GetGamePresenter(this);
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
            gamePresenter.OnRocketImpactsHealtPack(rocket, bomb);
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

        public void ShowHighScoreBeatedMessage()
        {
            //TODO: Implement me please!
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