
namespace HutongGames.PlayMaker.Actions.M8.Animator {
    [Tooltip("Pause animator timeline.")]
    public class Pause : AnimateActionBase {

        public override void OnEnter() {
            base.OnEnter();
            animate.Pause();
            Finish();
        }
    }
}
