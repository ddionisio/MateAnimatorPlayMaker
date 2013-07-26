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

        [RequiredField]
        public FsmString take;

        [Tooltip("Wait for animation to finish before completing this action. Be careful when setting this to true, certain animations loop forever. Also, this is ignored if loop is set to true.")]
        public FsmBool waitForComplete;

        [Tooltip("Override take's loop count to be infinite. If this is true, waitForComplete is ignored and this action will complete.")]
        public FsmBool loop;

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
            loop = false;
        }

        // Code that runs on entering the state.
        public override void OnEnter() {
            InitData();
            aData.Play(take.Value, loop.Value);

            if(!loop.Value && waitForComplete.Value && aData.currentPlayingTake != null) {
                sequenceWaitCompleted = false;
                aData.currentPlayingTake.sequenceCompleteCallback += SequenceComplete;
            }
            else {
                sequenceWaitCompleted = true;
                Finish();
            }
        }

        public override void OnExit() {
            //just in case
            if(!sequenceWaitCompleted && aData != null && aData.currentPlayingTake != null)
                aData.currentPlayingTake.sequenceCompleteCallback -= SequenceComplete;
        }

        public void SequenceComplete(AMTake take) {
            sequenceWaitCompleted = true;
            take.sequenceCompleteCallback -= SequenceComplete;
            Finish();
        }
    }
}
