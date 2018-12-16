
namespace HutongGames.PlayMaker.Actions.M8.Animator {
    [Tooltip("Reset animator based on given take. Useful for initializing the display state of an object.")]
    public class ResetTake : AnimateActionBase {
        public FsmString take;

        public override void Reset() {
            take = null;
        }

        // Code that runs on entering the state.
        public override void OnEnter() {
            base.OnEnter();

            animate.ResetTake(take.Value);

            Finish();
        }
    }
}
