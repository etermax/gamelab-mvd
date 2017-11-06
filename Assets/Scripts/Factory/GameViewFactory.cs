using DefaultNamespace;

namespace Factory
{
    public class GameViewFactory
    {
        public static GamePresenter GetGamePresenter(GameView gameView)
        {
            return new GamePresenter(gameView);
        }
    }
}