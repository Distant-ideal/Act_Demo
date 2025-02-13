using NBC.ActionEditor;
using UnityEngine;

namespace NBC.ActionEditorExample
{
    [Name("碰撞器轨道")]
    [Description("生成各种碰撞器逻辑")]
    [ShowIcon(typeof(ParticleSystem))]
    [Color(1f, 1f, 1f)]
    public class CollisionTrack : Track
    {
        [MenuName("碰撞器层")] [OptionParam(typeof(TrackLayer))]
        public int Layer;
    }
}