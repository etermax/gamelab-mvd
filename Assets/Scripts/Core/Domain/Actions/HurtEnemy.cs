namespace Core.Domain.Actions
{
    public class HurtEnemy
    {
        const int PointsByEnemy = 100;
        
        public int Execute (IEnemy enemy)
            {
                enemy.DecrementLife();
                if (!enemy.IsDeath() && enemy.GetLife() <= 0)
                {
                    enemy.Death();
                    return PointsByEnemy;
                }
                return 0;
            }   
    }
}