using NUnit.Framework;
using Presentation.Game;

namespace Core.Domain.Actions
{
    [TestFixture, Category("Actions")]
    public class CanLayBombTest
    {
        CanLayBomb action;
        PlayerStats prefs;
        bool canLayBomb;

        [Test]
        public void UserCanLayBombWhenHasBombsAndNoBombHasBeenLayed()
        {
            GivenACanLayBomb();
            WhenUserHasBombsAndNoBombsHasBeenLayed();
            ThenBombCanBeLayed();
        }

        [Test]
        public void UserCanNotLayABombWhenHasBombsAndABombHasBeenLayed()
        {
            GivenACanLayBomb();
            WhenUserHasBombsAndABombHasBeenLayed();
            ThenBombCanNotBeLayed();
        }

        [Test]
        public void UserCanNotLayABombWhenDoesNotHaveOne()
        {
            GivenACanLayBomb();
            WhenUserDoesNotHaveABomb();
            ThenBombCanNotBeLayed();
        }

        private void WhenUserDoesNotHaveABomb()
        {
            prefs = new PlayerStats
            {
                BombLayed = false,
                Bombs = 0
            };
            canLayBomb = action.Execute(prefs);
        }

        private void ThenBombCanNotBeLayed()
        {
            Assert.IsFalse(canLayBomb);
        }

        private void WhenUserHasBombsAndABombHasBeenLayed()
        {
            prefs = new PlayerStats
            {
                BombLayed = true,
                Bombs = 1
            };
            canLayBomb = action.Execute(prefs);
        }

        private void ThenBombCanBeLayed()
        {
            Assert.IsTrue(canLayBomb);
        }

        private void WhenUserHasBombsAndNoBombsHasBeenLayed()
        {
            prefs = new PlayerStats
            {
                BombLayed = false,
                Bombs = 1
            };
            canLayBomb = action.Execute(prefs);
        }

        private void GivenACanLayBomb()
        {
            action = new CanLayBomb();
        }
    }
}