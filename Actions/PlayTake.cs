﻿using UnityEngine;
using M8.Animator;

namespace HutongGames.PlayMaker.Actions.M8.Animator {
    [Tooltip("Play a take from the animator timeline.")]
    public class PlayTake : AnimateActionBase {
        [Tooltip("Name of the take to play.")]
        public FsmString take;

        [Tooltip("Override take's loop count to be infinite. If this is true, waitForComplete is ignored and this action will complete.")]
        public FsmBool loop;
        
        public FsmFloat startAt;
        [HideIf("IsStartAtNone")]
        public FsmBool startAtIsFrame;
        
        public override void Reset() {
            take = null;
            loop = null;
            startAt = new FsmFloat { UseVariable=true };
            startAtIsFrame = null;
        }

        // Code that runs on entering the state.
        public override void OnEnter() {
            base.OnEnter();

            if(!startAt.IsNone) {
                if(!string.IsNullOrEmpty(take.Value)) {
                    if(startAtIsFrame.Value)
                        animate.PlayAtFrame(take.Value, startAt.Value, loop.Value);
                    else
                        animate.PlayAtTime(take.Value, startAt.Value, loop.Value);
                }
                else
                    Debug.LogWarning("Take is empty.");
            }
            else {
                if(!string.IsNullOrEmpty(take.Value)) {
                    animate.Play(take.Value, loop.Value);
                }
                else
                    Debug.LogWarning("Take is empty.");
            }

            Finish();
        }
        
        public bool IsStartAtNone() {
            return startAt.IsNone;
        }
    }
}
