using Core.Domain.Providers;
using Presentation.Game;

namespace Presentation.Providers
{
    public static class GameViewProvider
    {
        public static GamePresenter ProviderGamePresenter(GameView gameView)
        {
            return new GamePresenter(gameView, ActionsProvider.ProvideSaveScore(),
                ActionsProvider.ProvideLoadScoreFromRepository(),
                ActionsProvider.ProvideVerifiesHighScoreBeated(),
                ActionsProvider.ProvideHurtEnemy(),
                ActionsProvider.ProvideCanLayBomb(),
                ActionsProvider.ProvideLayBomb());
        }
    }
}