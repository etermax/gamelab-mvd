using UnityEngine;

namespace Scenario.View
{
    public interface BackgroundParallaxView
    {
        Vector3 GetCamPosition();
        float GetParallaxScale();
        Transform[] GetBackgrouds();
        float GetParallaxReductionFactor();
        float GetSmoothing();
    }
}