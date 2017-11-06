using DefaultNamespace;

namespace Factory
{
    public class GameViewFactory
    {
        public static GamePresenter GetGamePresenter()
        {
            return new GamePresenter();
        }
    }
}