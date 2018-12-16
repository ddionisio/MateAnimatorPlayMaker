
namespace HutongGames.PlayMaker.Actions.M8.Animator {
    [Tooltip("Get the current take index that is playing.")]
    public class GetCurrentTakeIndex : AnimateActionBase {
        [UIHint(UIHint.Variable)]
        [RequiredField]
        public FsmInt output;

        public FsmBool everyFrame;

        public override void Reset() {
            output = null;
            everyFrame = false;
        }

        // Code that runs on entering the state.
        public override void OnEnter() {
            base.OnEnter();

            output.Value = animate.currentPlayingTakeIndex;

            if(!everyFrame.Value)
                Finish();
        }

        public override void OnUpdate() {
            if(!UpdateCache())
                return;

            output.Value = animate.currentPlayingTakeIndex;
        }
    }
}
