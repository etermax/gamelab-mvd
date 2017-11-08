using Core.Domain.Factories;
using Presentations.Game;

namespace Presentations.Factory
{
    public static class GameViewFactory
    {
        public static GamePresenter GetGamePresenter(GameView gameView)
        {
            return new GamePresenter(gameView, Provider.ProvideSaveScore(),
                Provider.ProvideLoadScoreFromRepository(),
                Provider.ProvideVerifiesHighScoreBeated());
        }
    }
}