
namespace HutongGames.PlayMaker.Actions.M8.Animator {
    [Tooltip("Go to a specific time.")]
    public class Goto : AnimateActionBase {
        public FsmString take;

        [RequiredField]
        public FsmFloat time;

        public override void Reset() {
            take = new FsmString { UseVariable = true };
            time = new FsmFloat { Value=0f };
        }

        public override void OnEnter() {
            base.OnEnter();

            if(!string.IsNullOrEmpty(take.Value))
                animate.Goto(take.Value, time.Value);
            else
                animate.Goto(time.Value);

            Finish();
        }
    }
}
