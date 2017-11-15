using NUnit.Framework;
using Presentation.Game;

namespace Core.Domain.Actions
{
    [TestFixture, System.ComponentModel.Category("Actions")]
    public class CanLayBombTest
    {
        private CanLayBomb canLayBomb;
        private PlayerStats stats;

        [Test]
        public void UserHasBombAndDidNotLayedBomb()
        {
            GivenACanLayBomb();
            GivenAStateWithBombAndNotLayedBomb();
            UserCanLayBombWhenItTry();
        }
        
        [Test]
        public void UserHasBombAndDidLayedBomb()
        {
            GivenACanLayBomb();
            GivenAStateWithBombAndLayedBomb();
            UserCantLayBombWhenItTry();
        }
        
        [Test]
        public void UserHasNotBombAndDidLayedBomb()
        {
            GivenACanLayBomb();
            GivenAStateWithoutBombAndNotLayedBomb();
            UserCantLayBombWhenItTry();
        }
        

        private void UserCanLayBombWhenItTry()
        {
            Assert.IsTrue(canLayBomb.Execute(stats));
        }
        
        private void UserCantLayBombWhenItTry()
        {
            Assert.IsFalse(canLayBomb.Execute(stats));
        }

        private void GivenAStateWithBombAndNotLayedBomb()
        {
            stats = new PlayerStats
            {
                BombLayed = false,
                Bombs = 1
            };
        }
        
        private void GivenAStateWithBombAndLayedBomb()
        {
            stats = new PlayerStats
            {
                BombLayed = true,
                Bombs = 1
            };
        }
        
        private void GivenAStateWithoutBombAndNotLayedBomb()
        {
            stats = new PlayerStats
            {
                BombLayed = false,
                Bombs = 0
            };
        }

        private void GivenACanLayBomb()
        {
            canLayBomb = new CanLayBomb();
        }
    }
}