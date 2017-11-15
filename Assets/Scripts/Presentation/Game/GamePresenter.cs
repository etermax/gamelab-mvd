using Core.Domain.Actions;

namespace Presentation.Game
{
    public class GamePresenter
    {
        readonly GameView gameView;
        readonly SaveScore saveScore;
        readonly LoadPreviousScore loadPreviousScore;
        readonly VerifiesHighScoreBeated verifiesHighScoreBeated;
        readonly HurtEnemy hurtEnemy;
        int score;
        
        private bool beated;

        public GamePresenter(GameView gameView,
            SaveScore saveScore,
            LoadPreviousScore loadPreviousScore,
            VerifiesHighScoreBeated verifiesHighScoreBeated, 
            HurtEnemy hurtEnemy)
        {
            this.gameView = gameView;
            this.saveScore = saveScore;
            this.loadPreviousScore = loadPreviousScore;
            this.verifiesHighScoreBeated = verifiesHighScoreBeated;
            this.hurtEnemy = hurtEnemy;
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
            var points = hurtEnemy.Execute(enemy);
            if (enemy.IsStrongEnemy() && enemy.GetLife() > 0)
                enemy.RenderDamagedState();
            if (enemy.IsDeath())
                enemy.RenderDeath();                
            IncrementPoints(points);
            rocket.Explode();
        }

        private void IncrementPoints(int points)
        {
            score += points;
            VerifiesBeatHighScore();
            gameView.UpdateScore(score);
            if (beated) gameView.UpdateHighScore(score);
        }

        public void OnRocketImpactsBomb(IRocket rocket, IBomb bomb)
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
            gameView.Stop();
            gameView.ShowPopup();
        }

        public void OnRestartConfirmed()
        {
            gameView.RestartGame();
        }
    }
}