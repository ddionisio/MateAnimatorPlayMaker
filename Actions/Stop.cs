
namespace HutongGames.PlayMaker.Actions.M8.Animator {
    [Tooltip("Stop animator timeline.")]
    public class Stop : AnimateActionBase {

        public override void OnEnter() {
            base.OnEnter();
            animate.Stop();
            Finish();
        }
    }
}
