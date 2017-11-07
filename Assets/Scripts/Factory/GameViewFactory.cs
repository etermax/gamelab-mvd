using Core.Domain.Factories;
using Game;

namespace Factory
{
    public static class GameViewFactory
    {
        public static GamePresenter GetGamePresenter(GameView gameView)
        {
            return new GamePresenter(gameView, ActionsFactory.ProvideSaveScore());
        }
    }
}