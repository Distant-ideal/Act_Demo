using Unity.VisualScripting;
using UnityEngine;

namespace NBC.ActionEditorExample
{
    public class PlayAnimationClipTask : SkillClipBase
    {
        public Animator PlayerAnimator;
        private PlayAnimation PlayAnimation => ActionClip as PlayAnimation;
         
        protected override void Begin()
        {
            var audioClipName = string.Empty;
            if (PlayAnimation.animationClip != null)
                audioClipName = PlayAnimation.animationClip.name;

            PlayerAnimator = Player.gameObject.GetComponent<Animator>();
            if (audioClipName != null)
                PlayerAnimator.CrossFade(audioClipName, 0.1f);
            else
                Debug.Log("播放动画为空");
            Debug.Log($"播放一个动画：{PlayAnimation.resPath}");
        }

        protected override void End()
        {
            PlayerAnimator.CrossFade("Idle", 0.1f);
            Debug.Log($"播放动画结束：{PlayAnimation.resPath}");
        }

        protected override void Tick()
        {

        }
    }
}