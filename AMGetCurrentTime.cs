using UnityEngine;
using HutongGames.PlayMaker;

namespace M8.PlayMaker {
    [ActionCategory("Mate Animator")]
    [Tooltip("Get the current time of the currently playing Take. Given value is set to 0 if no take is playing.")]
    public class AMGetCurrentTime : FsmStateAction {
        [RequiredField]
        [Tooltip("The Game Object to work with. NOTE: The Game Object must have an AnimatorData component attached.")]
        [CheckForComponent(typeof(AnimatorData))]
        public FsmOwnerDefault gameObject;

        [RequiredField]
        [UIHint(UIHint.Variable)]
        public FsmFloat storeResult;

        public FsmBool everyFrame;

        private AnimatorData aData;
        private void InitData() {
            GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
            if(go == null) {
                return;
            }

            aData = go.GetComponent<AnimatorData>();
        }

        public override void Reset() {
            storeResult = null;
            everyFrame = null;
        }

        public override void OnEnter() {
            InitData();
            GrabValue();
            if(!everyFrame.Value)
                Finish();
        }

        public override void OnUpdate() {
            GrabValue();
        }

        void GrabValue() {
            if(aData.currentPlayingTake != null && aData.currentPlayingTake.sequence != null) {
                storeResult.Value = aData.currentPlayingTake.sequence.fullElapsed;
            }
            else {
                storeResult.Value = 0.0f;
            }
            /*bool playing = aData.isPlaying;

            if(!storeResult.IsNone)
                storeResult.Value = playing;

            if(playing)
                Fsm.Event(isTrue);
            else
                Fsm.Event(isFalse);*/
        }
    }
}