using System.ComponentModel.Design;
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
        readonly CanLayBomb canLayBomb;
        readonly LayBomb layBomb;
        readonly PlayerStats playerStats;

        private bool beated;

        public GamePresenter(GameView gameView,
            SaveScore saveScore,
            LoadPreviousScore loadPreviousScore,
            VerifiesHighScoreBeated verifiesHighScoreBeated,
            HurtEnemy hurtEnemy, CanLayBomb canLayBomb, LayBomb layBomb)
        {
            this.gameView = gameView;
            this.saveScore = saveScore;
            this.loadPreviousScore = loadPreviousScore;
            this.verifiesHighScoreBeated = verifiesHighScoreBeated;
            this.hurtEnemy = hurtEnemy;
            this.canLayBomb = canLayBomb;
            this.layBomb = layBomb;
            playerStats = new PlayerStats();
        }

        public void OnStart()
        {
            LoadPreviousScore();
            gameView.HideBombFromHud();
        }

        private void VerifiesBeatHighScore()
        {
            if (!beated && verifiesHighScoreBeated.Execute(playerStats.Score))
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
            playerStats.Score += points;
            VerifiesBeatHighScore();
            gameView.UpdateScore(playerStats.Score);
            if (beated) gameView.UpdateHighScore(playerStats.Score);
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
            saveScore.Execute(playerStats.Score);
        }

        public void OnPlayerFalls(IPlayer player)
        {
            saveScore.Execute(playerStats.Score);
            gameView.Stop();
            gameView.ShowPopup();
        }

        public void OnRestartConfirmed()
        {
            gameView.RestartGame();
        }

        public void OnButton2Pressed()
        {
            var playerPosition = gameView.GetPlayerPosition();
            if (canLayBomb.Execute(playerStats))
            {
                layBomb.Execute(playerStats);
                gameView.LeaveBombAt(playerPosition);
                if (playerStats.Bombs < 1)
                    gameView.HideBombFromHud();
            }
        }

        public void OnBombPickedUp()
        {
            playerStats.Bombs++;
            gameView.ShowBombOnHud();
        }

        public void OnBombExplode()
        {
            playerStats.BombLayed = false;
        }
    }
}