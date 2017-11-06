using System.Linq;
using NUnit.Framework;
using Scenario.View;
using UnityEngine;

namespace Scenario.Presenter
{
    public class BackgroundParallaxPresenter
    {
        BackgroundParallaxView view;
        Vector3 previousCamPosition;

        public BackgroundParallaxPresenter(BackgroundParallaxView view)
        {
            this.view = view;
        }

        public void OnStart(Vector3 currentCamPosition)
        {
            previousCamPosition = currentCamPosition;
        }

        public void OnUpdate()
        {
            var currentCamPosition = view.GetCamPosition();
            var parallax = GetParallax(currentCamPosition);

            var item = 0;
            view.GetBackgrouds().ToList().ForEach(background =>
            {
                var backgroundTargetPosX =
                    background.position.x + parallax * (item * view.GetParallaxReductionFactor() + 1);
                var backgroundTargetPos = new Vector3(backgroundTargetPosX, background.position.y,
                    background.position.z);
                background.position =
                    Vector3.Lerp(background.position, backgroundTargetPos, view.GetSmoothing() * Time.deltaTime);
                item++;
            });

            previousCamPosition = currentCamPosition;
        }

        private float GetParallax(Vector3 currentCamPosition)
        {
            return (previousCamPosition.x - currentCamPosition.x) * view.GetParallaxScale();
        }
    }
}