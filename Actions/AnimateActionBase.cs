using UnityEngine;

using M8.Animator;

namespace HutongGames.PlayMaker.Actions.M8.Animator {
    [ActionCategory("Mate Animator")]
    [ActionTarget(typeof(Animate), "gameObject", false)]
    public abstract class AnimateActionBase : FsmStateAction {
        [DisplayOrder(0)]
        [RequiredField]
        [Tooltip("The Game Object to work with. NOTE: The Game Object must have a M8.Animator.Animate component attached.")]
        [CheckForComponent(typeof(Animate))]
        public FsmOwnerDefault gameObject;

        protected Animate animate { get; private set; }

        private GameObject mAnimGO;

        // Code that runs on entering the state.
        public override void OnEnter() {
            var curGO = Fsm.GetOwnerDefaultTarget(gameObject);
            if(animate == null || curGO != mAnimGO) {
                mAnimGO = curGO;
                animate = mAnimGO.GetComponent<Animate>();
            }
        }
    }
}
