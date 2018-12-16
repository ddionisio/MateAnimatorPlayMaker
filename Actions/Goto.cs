
namespace HutongGames.PlayMaker.Actions.M8.Animator {
    [Tooltip("Go to a specific time. If take is empty, use current playing take.")]
    public class Goto : AnimateActionBase {
        public FsmString take;

        [RequiredField]
        public FsmFloat time;
        public FsmBool isFrame;

        public override void Reset() {
            take = new FsmString { UseVariable = true };
            time = new FsmFloat { Value=0f };

            isFrame = false;
        }

        public override void OnEnter() {
            base.OnEnter();

            if(!string.IsNullOrEmpty(take.Value)) {
                if(isFrame.Value)
                    animate.GotoFrame(take.Value, time.Value);
                else
                    animate.Goto(take.Value, time.Value);
            }
            else {
                if(isFrame.Value)
                    animate.GotoFrame(time.Value);
                else
                    animate.Goto(time.Value);
            }

            Finish();
        }
    }
}
