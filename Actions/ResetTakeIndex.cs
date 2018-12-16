
namespace HutongGames.PlayMaker.Actions.M8.Animator {
    [Tooltip("Reset animator based on given take index. Useful for initializing the display state of an object.")]
    public class ResetTakeIndex : AnimateActionBase {
        public FsmInt takeIndex;

        public override void Reset() {
            takeIndex = null;
        }

        // Code that runs on entering the state.
        public override void OnEnter() {
            base.OnEnter();

            animate.ResetTake(takeIndex.Value);

            Finish();
        }
    }
}
