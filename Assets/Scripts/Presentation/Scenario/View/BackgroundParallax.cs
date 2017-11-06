using Scenario.Presenter;
using UnityEngine;

namespace Scenario.View
{
    public class BackgroundParallax : MonoBehaviour, BackgroundParallaxView
    {
        public Transform[] backgrounds; // Array of all the backgrounds to be parallaxed.
        public float parallaxScale; // The proportion of the camera's movement to move the backgrounds by.
        public float parallaxReductionFactor; // How much less each successive layer should parallax.
        public float smoothing; // How smooth the parallax effect should be.


        private Transform cam; // Shorter reference to the main camera's transform.
        BackgroundParallaxPresenter presenter;


        void Awake()
        {
            cam = Camera.main.transform;
            presenter = new BackgroundParallaxPresenter(this);
        }


        void Start()
        {
            presenter.OnStart(cam.position);
        }

        void Update()
        {
            presenter.OnUpdate();
        }

        public Vector3 GetCamPosition()
        {
            return cam.position;
        }

        public float GetParallaxScale()
        {
            return parallaxScale;
        }

        public Transform[] GetBackgrouds()
        {
            return backgrounds;
        }

        public float GetParallaxReductionFactor()
        {
            return parallaxReductionFactor;
        }

        public float GetSmoothing()
        {
            return smoothing;
        }
    }
}