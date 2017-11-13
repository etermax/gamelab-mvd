using Core.Domain.Score;
using Core.Domain.Stats;
using NSubstitute;
using NUnit.Framework;

namespace Core.Domain.Actions
{
    [TestFixture, Category("Actions")]
    public class VerifiesHighScoreBeatedTest
    {
        StatsRepository statsRepository;
        VerifiesHighScoreBeated action;
        private bool result;
        const int HigherScore = 40;
        const int SavedScore = 20;
        const int LowerScore = 10;

        [SetUp]
        public void SetUp()
        {
            statsRepository = Substitute.For<StatsRepository>();
        }

        [Test]
        public void WhenVerifiesHighScoreWithALowerScore()
        {
            GivenAVerifiesAction();
            WhenVerifiesExecuteWithScore(LowerScore);
            ThenScoreWasNotBeaten();
        }
        
        [Test]
        public void WhenVerifiesHighScoreWithAHigherScore()
        {
            GivenAVerifiesAction();
            WhenVerifiesExecuteWithScore(HigherScore);
            ThenScoreWasBeaten();
        }

        private void ThenScoreWasBeaten()
        {
            Assert.IsTrue(result);
        }

        private void ThenScoreWasNotBeaten()
        {
            Assert.IsFalse(result);
        }

        private void WhenVerifiesExecuteWithScore(int score)
        {
            statsRepository.Get().Returns(new PlayerScore(SavedScore));
            result = action.Execute(score);
        }

        private void GivenAVerifiesAction()
        {
            action = new VerifiesHighScoreBeated(statsRepository);
        }
    }
}