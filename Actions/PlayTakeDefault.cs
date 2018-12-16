using UnityEngine;
using M8.Animator;

namespace HutongGames.PlayMaker.Actions.M8.Animator {
    [Tooltip("Play the default take from the animator timeline. Make sure to set this in Animator.")]
    public class PlayTakeDefault : AnimateActionBase {
        [Tooltip("Override take's loop count to be infinite. If this is true, waitForComplete is ignored and this action will complete.")]
        public FsmBool loop;

        public FsmFloat startAt;
        [HideIf("IsStartAtNone")]
        public FsmBool startAtIsFrame;

        public override void Reset() {
            loop = null;
            startAt = new FsmFloat { UseVariable = true };
            startAtIsFrame = null;
        }

        // Code that runs on entering the state.
        public override void OnEnter() {
            base.OnEnter();

            if(!startAt.IsNone) {
                if(startAtIsFrame.Value)
                    animate.PlayAtFrame(animate.defaultTakeIndex, startAt.Value, loop.Value);
                else
                    animate.PlayAtTime(animate.defaultTakeIndex, startAt.Value, loop.Value);
            }
            else {
                animate.Play(animate.defaultTakeIndex, loop.Value);
            }

            Finish();
        }

        public bool IsStartAtNone() {
            return startAt.IsNone;
        }
    }
}
