
using Core.Domain.Actions;

namespace Game
{
    public class GamePresenter
    {
        private readonly GameView gameView;
        private readonly SaveScore saveScore;
        private const int PointsByEnemy = 100;
        private int score;

        public GamePresenter(GameView gameView, SaveScore saveScore)
        {
            this.gameView = gameView;
            this.saveScore = saveScore;
        }

        public void OnRocketImpactsEnemy(IRocket rocket, IEnemy enemy)
        {
            enemy.Hurt();
            if (enemy.IsStrongEnemy() && enemy.GetHealth() > 0)
                enemy.SetDamagedState();
            if (!enemy.IsDeath() && enemy.GetHealth() <= 0)
            {
                enemy.Death();
                IncrementPoints(PointsByEnemy);
            }
                
            rocket.Explode();
        }

        private void IncrementPoints(int pointsByEnemy)
        {
            score += pointsByEnemy;
            saveScore.Execute(score);
            gameView.UpdateScore(score);
        }

        public void OnRocketImpactsHealtPack(IRocket rocket, IBomb bomb)
        {
            bomb.Explode();
            rocket.Explode();
        }

        public void OnRocketImpactsWithSomethingElse(IRocket rocket)
        {
            rocket.Explode();
        }

        public void OnEnemyHitsWithObstacle(IEnemy enemy)
        {
            enemy.Flip();
        }
    }
}