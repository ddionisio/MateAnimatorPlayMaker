using UnityEngine;
using HutongGames.PlayMaker;

namespace M8.PlayMaker {
    [ActionCategory("Mate Animator")]
    [Tooltip("Play a take from the animator timeline.")]
    public class AMPlayTake : FsmStateAction {
        [RequiredField]
        [Tooltip("The Game Object to work with. NOTE: The Game Object must have an AnimatorData component attached.")]
        [CheckForComponent(typeof(AnimatorData))]
        public FsmOwnerDefault gameObject;

        public FsmString take;

        [Tooltip("Wait for animation to finish before completing this action. Be careful when setting this to true, certain animations loop forever. Also, this is ignored if loop is set to true.")]
        public FsmBool waitForComplete;

        public FsmEvent waitForCompleteEvent;

        [Tooltip("Override take's loop count to be infinite. If this is true, waitForComplete is ignored and this action will complete.")]
        public FsmBool loop;

        [UIHint(UIHint.Variable)]
        public FsmFloat startAt;

        public bool startAtIsFrame;

        private AnimatorData aData;
        private void InitData() {
            GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
            if(go == null) {
                return;
            }

            aData = go.GetComponent<AnimatorData>();
        }

        private bool sequenceWaitCompleted = true;

        public override void Reset() {
            take = null;
            waitForComplete = false;
            waitForCompleteEvent = null;
            loop = false;
            startAt = null;
            startAtIsFrame = false;
        }

        // Code that runs on entering the state.
        public override void OnEnter() {
            InitData();

            if(!startAt.IsNone) {
                if(!string.IsNullOrEmpty(take.Value)) {
                    if(startAtIsFrame)
                        aData.PlayAtFrame(take.Value, startAt.Value, loop.Value);
                    else
                        aData.PlayAtTime(take.Value, startAt.Value, loop.Value);
                }
            }
            else {
                if(!string.IsNullOrEmpty(take.Value)) {
                    aData.Play(take.Value, loop.Value);
                }
                else {
                    aData.PlayDefault(loop.Value);
                }
            }

            if(!loop.Value && waitForComplete.Value && aData.currentPlayingSequence != null) {
                sequenceWaitCompleted = false;
                aData.takeCompleteCallback += SequenceComplete;
            }
            else {
                sequenceWaitCompleted = true;
                Finish();
            }
        }

        public override void OnExit() {
            //just in case
            if(!sequenceWaitCompleted && aData != null && aData.currentPlayingSequence != null)
                aData.takeCompleteCallback -= SequenceComplete;
        }

        public void SequenceComplete(AnimatorData anim, AMTakeData take) {
            sequenceWaitCompleted = true;
            anim.takeCompleteCallback -= SequenceComplete;

            if(!FsmEvent.IsNullOrEmpty(waitForCompleteEvent))
                Fsm.Event(waitForCompleteEvent);

            Finish();
        }
    }
}
