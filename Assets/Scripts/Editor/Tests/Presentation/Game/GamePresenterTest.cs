using NUnit.Framework;
using Core.Domain.Actions;
using Core.Domain.Score;
using NSubstitute;

namespace Presentation.Game
{
	[TestFixture, Category("Game Presenter")]
	public class GamePresenterTest
	{
		private GamePresenter gamePresenter;
		private GameView gameView;
		private SaveScore saveScore;
		private LoadPreviousScore loadPreviousScore;
		private VerifiesHighScoreBeated verifiesHighScoreBeated;
		private StatsRepository statsRepository;
		private IEnemy enemy;
	
		[SetUp]
		public void SetUp()
		{
			statsRepository = Substitute.For<StatsRepository>();
			gameView = Substitute.For<GameView>();
			saveScore = Substitute.For<SaveScore>(statsRepository);
			loadPreviousScore = Substitute.For<LoadPreviousScore>(statsRepository);
			verifiesHighScoreBeated = Substitute.For<VerifiesHighScoreBeated>(statsRepository);
			enemy = Substitute.For<IEnemy>();
		}
		
		[Test]
		public void EnemyChangesDirectionWhenItHitAnObstacle()
		{
			GivenAGamePresenter();
			WhenEnemyHitsAnObstacle();
			ThenEnemyFlips();
		}
	
		private void ThenEnemyFlips()
		{
			enemy.Received(1).Flip();
		}
	
		private void WhenEnemyHitsAnObstacle()
		{
			gamePresenter.OnEnemyHitsWithObstacle(enemy);
		}
	
		private void GivenAGamePresenter()
		{
			gamePresenter = new GamePresenter(gameView, saveScore, loadPreviousScore, verifiesHighScoreBeated);
		}
	
	}
}

