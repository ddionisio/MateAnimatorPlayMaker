using UnityEngine;
using M8.Animator;

namespace HutongGames.PlayMaker.Actions.M8.Animator {
    [Tooltip("Play a take based on index from the animator timeline.")]
    public class PlayTakeIndex : AnimateActionBase {
        public FsmInt takeIndex;

        [Tooltip("Override take's loop count to be infinite. If this is true, waitForComplete is ignored and this action will complete.")]
        public FsmBool loop;

        public FsmFloat startAt;
        [HideIf("IsStartAtNone")]
        public FsmBool startAtIsFrame;

        public override void Reset() {
            takeIndex = 0;
            loop = null;
            startAt = new FsmFloat { UseVariable = true };
            startAtIsFrame = null;
        }

        // Code that runs on entering the state.
        public override void OnEnter() {
            base.OnEnter();

            if(!startAt.IsNone) {
                if(!takeIndex.IsNone) {
                    if(startAtIsFrame.Value)
                        animate.PlayAtFrame(takeIndex.Value, startAt.Value, loop.Value);
                    else
                        animate.PlayAtTime(takeIndex.Value, startAt.Value, loop.Value);
                }
                else
                    Debug.LogWarning("Take Index is none.");

            }
            else {
                if(!takeIndex.IsNone) {
                    animate.Play(takeIndex.Value, loop.Value);
                }
                else
                    Debug.LogWarning("Take Index is none.");
            }

            Finish();
        }

        public bool IsStartAtNone() {
            return startAt.IsNone;
        }
    }
}
