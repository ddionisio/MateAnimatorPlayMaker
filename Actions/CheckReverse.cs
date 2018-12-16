
namespace HutongGames.PlayMaker.Actions.M8.Animator {
    [Tooltip("Check if Mate Animator is playing in reverse.")]
    public class CheckReverse : AnimateActionBase {
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
            if(!UpdateCache())
                return;

            DoCheck();
        }

        void DoCheck() {
            bool isReversed = animate.isReversed;

            if(!storeResult.IsNone)
                storeResult.Value = isReversed;

            Fsm.Event(isReversed ? isTrue : isFalse);
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
