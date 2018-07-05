
namespace HutongGames.PlayMaker.Actions.M8.Animator {
    [Tooltip("Get the current time of the currently playing Take. Given value is set to 0 if no take is playing.")]
    public class GetCurrentTime : AnimateActionBase {
        [RequiredField]
        [UIHint(UIHint.Variable)]
        public FsmFloat output;

        public FsmBool fullElapse;
        public FsmBool everyFrame;

        public override void Reset() {
            output = null;
            fullElapse = null;
            everyFrame = null;
        }

        public override void OnEnter() {
            base.OnEnter();

            GrabValue();
            if(!everyFrame.Value)
                Finish();
        }

        public override void OnUpdate() {
            GrabValue();
        }

        void GrabValue() {
            output.Value = fullElapse.Value ? animate.runningFullTime : animate.runningTime;
        }
    }
}
