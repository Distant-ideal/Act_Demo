using NBC.ActionEditor;
using UnityEditor;
using UnityEngine;

namespace Combat
{
    [Name("动画片段")]
    [Description("播放一个动画剪辑的行为")]
    [Color(0.48f, 0.71f, 0.84f)]
    [Attachable(typeof(AnimationTrack))]
    public class PlayAnimation : ActionClip
    {
        [SerializeField] [HideInInspector] private float length = 1f;
        [SerializeField] [HideInInspector] private float blendIn = 0.25f;
        [SerializeField] [HideInInspector] private float blendOut = 0.25f;

        [MenuName("播放动画")] [SelectObjectPath(typeof(AnimationClip))]
        public string resPath = "";

        private AnimationClip _animationClip;

        public AnimationClip animationClip
        {
            get
            {
                if (string.IsNullOrEmpty(resPath))
                {
                    _animationClip = null;
                    return null;
                }

                if (_animationClip == null)
                {
#if UNITY_EDITOR
                    _animationClip = AssetDatabase.LoadAssetAtPath<AnimationClip>(resPath);
#endif
                }

                return _animationClip;
            }
        }

        [Range(0.1f, 10f)] [MenuName("播放速度")] public float playbackSpeed = 1;


        public override float Length
        {
            get => length;
            set => length = value;
        }

        public override float BlendIn
        {
            get => blendIn;
            set => blendIn = value;
        }

        public override float BlendOut
        {
            get => blendOut;
            set => blendOut = value;
        }

        public override bool isValid => animationClip != null;

        public override string info => isValid ? animationClip.name : base.info;

    }
}