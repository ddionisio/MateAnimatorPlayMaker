using M8.Animator;

namespace HutongGames.PlayMaker.Actions.M8.Animator {
    [Tooltip("Play a take from the animator timeline.")]
    public class PlayTake : AnimateActionBase {
        public FsmString take;

        [Tooltip("Wait for animation to finish before completing this action. Be careful when setting this to true, certain animations loop forever. Also, this is ignored if loop is set to true.")]
        public FsmBool waitForComplete;

        public FsmEvent waitForCompleteEvent;

        [Tooltip("Override take's loop count to be infinite. If this is true, waitForComplete is ignored and this action will complete.")]
        public FsmBool loop;

        [UIHint(UIHint.Variable)]
        public FsmFloat startAt;
        [UIHint(UIHint.Variable)]
        public FsmBool startAtIsFrame;

        private bool sequenceWaitCompleted = true;

        public override void Reset() {
            take = null;
            waitForComplete = null;
            waitForCompleteEvent = null;
            loop = null;
            startAt = null;
            startAtIsFrame = null;
        }

        // Code that runs on entering the state.
        public override void OnEnter() {
            base.OnEnter();

            if(!startAt.IsNone) {
                if(!string.IsNullOrEmpty(take.Value)) {
                    if(startAtIsFrame.Value)
                        animate.PlayAtFrame(take.Value, startAt.Value, loop.Value);
                    else
                        animate.PlayAtTime(take.Value, startAt.Value, loop.Value);
                }
            }
            else {
                if(!string.IsNullOrEmpty(take.Value)) {
                    animate.Play(take.Value, loop.Value);
                }
                else {
                    animate.PlayDefault(loop.Value);
                }
            }

            if(!loop.Value && waitForComplete.Value && animate.currentPlayingSequence != null) {
                sequenceWaitCompleted = false;
                animate.takeCompleteCallback += SequenceComplete;
            }
            else {
                sequenceWaitCompleted = true;
                Finish();
            }
        }

        public override void OnExit() {
            //just in case
            if(!sequenceWaitCompleted && animate.currentPlayingSequence != null)
                animate.takeCompleteCallback -= SequenceComplete;
        }

        public void SequenceComplete(Animate anim, Take take) {
            sequenceWaitCompleted = true;
            anim.takeCompleteCallback -= SequenceComplete;

            if(!FsmEvent.IsNullOrEmpty(waitForCompleteEvent))
                Fsm.Event(waitForCompleteEvent);

            Finish();
        }
    }
}
