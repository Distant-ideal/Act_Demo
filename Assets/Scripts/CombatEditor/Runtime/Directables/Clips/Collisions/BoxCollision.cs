using NBC.ActionEditor;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace NBC.ActionEditorExample
{
    [Name("盒型碰撞片段")]
    [Description("生成一个盒型碰撞器")]
    [Color(0.0f, 1f, 1f)]
    [Attachable(typeof(CollisionTrack))]
    public class BoxCollision : ActionClip
    {
        [SerializeField] [HideInInspector] private float length = 1f;

        [MenuName("碰撞对象")] [SelectObjectPath(typeof(GameObject))]
        public string resPath = "";

        [MenuName("碰撞大小")] public Vector3 collisionsize;
        private GameObject _collisioObj;

        private GameObject boxCollisionClip
        {
            get
            {
                if (_collisioObj == null)
                {
#if UNITY_EDITOR
                    _collisioObj = AssetDatabase.LoadAssetAtPath<GameObject>(resPath);
#endif
                }

                return _collisioObj;
            }
        }


        public override float Length
        {
            get => length;
            set => length = value;
        }

        public override bool isValid => boxCollisionClip != null;

        public override string info => isValid ? boxCollisionClip.name : base.info;

        public CollisionTrack Track => (CollisionTrack)parent;
    }
}