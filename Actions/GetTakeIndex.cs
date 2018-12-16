
namespace HutongGames.PlayMaker.Actions.M8.Animator {
    [Tooltip("Get the take index. If take is not found, index value will be -1.")]
    public class GetTakeIndex : AnimateActionBase {
        public FsmString take;

        [UIHint(UIHint.Variable)]
        [RequiredField]
        public FsmInt output;

        public override void Reset() {
            output = null;
        }

        // Code that runs on entering the state.
        public override void OnEnter() {
            base.OnEnter();

            output.Value = animate.GetTakeIndex(take.Value);

            Finish();
        }
    }
}
