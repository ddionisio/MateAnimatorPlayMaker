
namespace HutongGames.PlayMaker.Actions.M8.Animator {
    [Tooltip("Get the current take that is playing.")]
    public class GetCurrentTake : AnimateActionBase {
        [UIHint(UIHint.Variable)]
        [RequiredField]
        public FsmString output;

        public FsmBool everyFrame;

        public override void Reset() {
            output = null;
            everyFrame = false;
        }

        // Code that runs on entering the state.
        public override void OnEnter() {
            base.OnEnter();

            output.Value = animate.currentPlayingTakeName;

            if(!everyFrame.Value)
                Finish();
        }

        public override void OnUpdate() {
            if(!UpdateCache())
                return;

            output.Value = animate.currentPlayingTakeName;
        }
    }
}
