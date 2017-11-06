
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
            if (enemy.GetHealth() < 0)
                enemy.Death();
            rocket.Explode();
        }
    }
}