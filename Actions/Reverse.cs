
namespace HutongGames.PlayMaker.Actions.M8.Animator {
    [Tooltip("Reverse animator timeline. This will only work if there is a current Take set to AnimatorData (via Play)")]
    public class Reverse : AnimateActionBase {
        public enum Type {
            Toggle,
            True,
            False
        }

        [ObjectType(typeof(Type))]
        public FsmEnum mode;

        public override void Reset() {
            mode = null;
        }

        public override void OnEnter() {
            base.OnEnter();

            switch((Type)mode.Value) {
                case Type.Toggle:
                    animate.Reverse();
                    break;

                case Type.True:
                    animate.isReversed = true;
                    break;

                case Type.False:
                    animate.isReversed = false;
                    break;
            }

            Finish();
        }
    }
}
