
namespace HutongGames.PlayMaker.Actions.M8.Animator {
    [Tooltip("Resume animator timeline. This will only work if there is a current Take set to AnimatorData (via Play)")]
    public class Resume : AnimateActionBase {

        public override void OnEnter() {
            base.OnEnter();
            animate.Resume();
            Finish();
        }
    }
}
