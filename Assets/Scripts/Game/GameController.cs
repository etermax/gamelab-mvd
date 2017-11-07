using Factory;
using UnityEngine;

namespace Game
{

    public interface GameView
    {
        void UpdateScore(int score);
    }
    
    public class GameController : MonoBehaviour, GameView
    {
        public Score scoreController;
        private GamePresenter gamePresenter;

        private void Awake()
        {
            gamePresenter = GameViewFactory.GetGamePresenter(this);
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
    }
}