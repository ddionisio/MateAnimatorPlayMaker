
namespace HutongGames.PlayMaker.Actions.M8.Animator {
    [Tooltip("Get the current take that is playing.")]
    public class GetCurrentTake : AnimateActionBase {
        [UIHint(UIHint.Variable)]
        [RequiredField]
        public FsmString output;

        public override void Reset() {
            output = null;
        }

        // Code that runs on entering the state.
        public override void OnEnter() {
            base.OnEnter();

            output.Value = animate.currentPlayingTakeName;

            Finish();
        }
    }
}
