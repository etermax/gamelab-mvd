using Core.Domain.Factories;
using Presentations.Game;

namespace Presentations.Factory
{
    public static class GameViewFactory
    {
        public static GamePresenter GetGamePresenter(GameView gameView)
        {
            return new GamePresenter(gameView, ActionsFactory.ProvideSaveScore(),
                ActionsFactory.ProvideLoadScoreFromRepository(),
                ActionsFactory.ProvideVerifiesHighScoreBeated());
        }
    }
}