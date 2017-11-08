using Core.Domain.Actions;

namespace Presentation.Game
{
    public class GamePresenter
    {
        const int PointsByEnemy = 100;
        
        readonly GameView gameView;
        readonly SaveScore saveScore;
        readonly LoadPreviousScore loadPreviousScore;
        readonly VerifiesHighScoreBeated verifiesHighScoreBeated;
        
        int score;
        
        private bool beated;

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
            if (!beated && verifiesHighScoreBeated.Execute(score))
            {
                beated = true;
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
            if (beated)
                gameView.UpdateHighScore(score);
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