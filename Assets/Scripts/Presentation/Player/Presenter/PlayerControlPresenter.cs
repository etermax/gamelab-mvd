using Presentation.Player.View;
using UnityEngine;

namespace Presentation.Player.Presenter
{
    public class PlayerControlPresenter
    {
        private const string JumpAnimation = "Jump";
        readonly PlayerView view;
        bool grounded;
        Animator animator;

        public PlayerControlPresenter(PlayerView view)
        {
            this.view = view;
        }

        public void OnUpdate()
        {
            grounded = view.isPlayerGrounded();
        }

        public void OnJumpButtonPressed()
        {
            if (grounded)
            {
                view.Jump();
            }
        }

        public void Initialize(Animator animator)
        {
            this.animator = animator;
        }

        public void OnJump()
        {
            animator.SetTrigger(JumpAnimation);
        }

        public void OnFixedUpdate(float hInput)
        {
            animator.SetFloat("Speed", Mathf.Abs(hInput));
        }
    }
}