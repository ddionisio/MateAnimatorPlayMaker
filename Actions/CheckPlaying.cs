
namespace HutongGames.PlayMaker.Actions.M8.Animator {
    [Tooltip("Check if Mate Animator is playing.")]
    public class CheckPlaying : AnimateActionBase {
        [UIHint(UIHint.Variable)]
        public FsmBool storeResult;

        public FsmEvent isTrue;
        public FsmEvent isFalse;

        public FsmBool everyFrame;
        
        public override void Reset() {
            isTrue = null;
            isFalse = null;
            storeResult = null;
            everyFrame = null;
        }

        // Code that runs on entering the state.
        public override void OnEnter() {
            base.OnEnter();

            DoCheck();
            if(!everyFrame.Value)
                Finish();
        }

        // Code that runs every frame.
        public override void OnUpdate() {
            DoCheck();
        }

        void DoCheck() {
            bool isPlaying = animate.isPlaying;

            if(!storeResult.IsNone)
                storeResult.Value = isPlaying;

            if(isPlaying)
                Fsm.Event(isTrue);
            else
                Fsm.Event(isFalse);
        }

        // Perform custom error checking here.
        public override string ErrorCheck() {
            if(everyFrame.Value &&
                FsmEvent.IsNullOrEmpty(isTrue) &&
                FsmEvent.IsNullOrEmpty(isFalse))
                return "Action sends no events!";
            return "";
        }
    }
}
