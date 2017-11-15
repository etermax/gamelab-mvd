using NUnit.Framework;

namespace Core.Domain.Actions
{
    [TestFixture, Category("Actions")]
    public class HurtEnemyTest
    {
        private HurtEnemy hurtEnemy;
        private Enemy enemy;

        [Test]
        public void EnemyIsHurtAndItDies()
        {
            GivenAHurtEnemy();
            GivenAnEnemyWithLastLife();
            WhenEnemyIsHurt();
            ThenEnemyDies();
        }

        private void ThenEnemyDies()
        {
            Assert.IsTrue(enemy.IsDeath());
        }

        private void WhenEnemyIsHurt()
        {
            hurtEnemy.Execute(enemy);
        }

        private void GivenAnEnemyWithLastLife()
        {
            enemy = new Enemy();
            enemy.Life = 1;
        }

        private void GivenAHurtEnemy()
        {
            hurtEnemy = new HurtEnemy();
        }
    }
}