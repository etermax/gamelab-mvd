using Core.Domain.Factories;
using Presentation.Game;

namespace Presentation.Factory
{
    public static class GameViewFactory
    {
        public static GamePresenter GetGamePresenter(GameView gameView)
        {
            return new GamePresenter(gameView, Provider.ProvideSaveScore(),
                Provider.ProvideLoadScoreFromRepository(),
                Provider.ProvideVerifiesHighScoreBeated(),
                Provider.ProvideHurtEnemy());
        }
    }
}