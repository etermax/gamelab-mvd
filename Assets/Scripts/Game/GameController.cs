using Factory;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameController : MonoBehaviour
    {
        private GamePresenter gamePresenter;

        private void Awake()
        {
            gamePresenter = GameViewFactory.GetGamePresenter();
        }

        public void OnRocketImpactsEnemy(Rocket rocket, Enemy enemy)
        {
            gamePresenter.OnRocketImpactsEnemy(rocket, enemy);
            Debug.Log("OnRocketImpactsEnemy");
        }

        public void OnRocketImpactsBomb(Rocket rocket, Bomb bomb)
        {
            gamePresenter.OnRocketImpactsHealtPack(rocket, bomb);
            Debug.Log("OnRocketImpactsHealtPack");
        }

        public void OnRocketImpactsWithSomethingElse(Rocket rocket)
        {
            gamePresenter.OnRocketImpactsWithSomethingElse(rocket);
        }
    }
}