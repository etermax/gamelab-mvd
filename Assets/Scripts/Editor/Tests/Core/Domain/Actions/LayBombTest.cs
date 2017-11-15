using NUnit.Framework;
using Presentation.Game;

namespace Core.Domain.Actions
{
    [TestFixture, Category("Actions")]
    public class LayBombTest
    {
        [Test]
        public void Test()
        {
            var action = GivenALayBombAction();
            var stats = WhenLayBombExecutes(action);
            ThenBombIsLayed(stats);
        }

        private static void ThenBombIsLayed(PlayerStats stats)
        {
            Assert.IsTrue(stats.BombLayed);
            Assert.AreEqual(stats.Bombs, 0);
        }

        private static PlayerStats WhenLayBombExecutes(LayBomb action)
        {
            var stats = new PlayerStats
            {
                Bombs = 1,
                BombLayed = false
            };
            action.Execute(stats);
            return stats;
        }

        private static LayBomb GivenALayBombAction()
        {
            var action = new LayBomb();
            return action;
        }
    }
}