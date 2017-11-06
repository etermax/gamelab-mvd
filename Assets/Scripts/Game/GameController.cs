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

        public void OnRocketImpactsEnemy(IRocket rocket, IEnemy getComponent)
        {
            gamePresenter.OnRocketImpactsEnemy(rocket, getComponent);
            Debug.Log("OnRocketImpactsEnemy");
        }

        public void OnRocketImpactsHealtPack(Rocket rocket, Bomb getComponent)
        {
            throw new System.NotImplementedException();
        }
    }
}