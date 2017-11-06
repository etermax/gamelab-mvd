
namespace DefaultNamespace
{
    public class GamePresenter
    {
        public GamePresenter()
        {
            
        }

        public void OnRocketImpactsEnemy(IRocket rocket, IEnemy enemy)
        {
            enemy.Hurt();
            if (enemy.IsStrongEnemy() && enemy.GetHealth() > 0)
                enemy.SetDamagedState();
            if (enemy.GetHealth() <= 0)
                enemy.Death();
            rocket.Explode();
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