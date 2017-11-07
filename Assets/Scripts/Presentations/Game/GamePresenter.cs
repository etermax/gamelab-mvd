using Core.Domain.Actions;

namespace Presentations.Game
{
    public class GamePresenter
    {
        readonly GameView gameView;
        readonly SaveScore saveScore;
        readonly LoadPreviousScore loadPreviousScore;
        const int PointsByEnemy = 100;
        int score;
        readonly VerifiesHighScoreBeated verifiesHighScoreBeated;

        public GamePresenter(GameView gameView,
            SaveScore saveScore,
            LoadPreviousScore loadPreviousScore,
            VerifiesHighScoreBeated verifiesHighScoreBeated)
        {
            this.gameView = gameView;
            this.saveScore = saveScore;
            this.loadPreviousScore = loadPreviousScore;
            this.verifiesHighScoreBeated = verifiesHighScoreBeated;
        }

        public void OnStart()
        {
            LoadPreviousScore();
        }

        private void VerifiesBeatHighScore()
        {
            if (verifiesHighScoreBeated.Execute(score))
            {
                gameView.ShowHighScoreBeatedMessage();
            }
        }

        private void LoadPreviousScore()
        {
            var playerScore = loadPreviousScore.Execute();
            if (playerScore.Score > 0)
            {
                gameView.UpdatePreviousScore(playerScore);
            }
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
            VerifiesBeatHighScore();
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

        public void OnPlayerEmptyHealth(IPlayerHealth playerHealth, IPlayer player)
        {
            playerHealth.SetPlayerAsDead();
            player.DisablePlayer();
            saveScore.Execute(score);
        }

        public void OnPlayerFalls(IPlayer player)
        {
            player.Die();
            saveScore.Execute(score);
        }
    }
}