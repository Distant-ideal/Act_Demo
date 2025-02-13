using NBC.ActionEditor;
using NBC.ActionEditorExample;
using UnityEngine;
using UnityEditor;

namespace ActionEditorExample
{
    /// <summary>
    /// 移动至预览
    /// </summary>
    [NBC.ActionEditor.CustomPreview(typeof(BoxCollision))]
    public class MoveByBoxCollisionPreviewPreview : PreviewBase<BoxCollision>
    {
        private GameObject _collisioObj;
        private BoxCollider _boxCollider;

        public override void Update(float time, float previousTime)
        {
        }

        
        public override void Enter()
        {
            if (_collisioObj == null)
            {
                //创建特效。
                //实际业务建议自行编写特效对象池
                CreateCollision();
            }
        }

        public override void Exit()
        {
            if (_collisioObj != null)
            {
                _collisioObj.gameObject.SetActive(false);
            }
        }


        private void CreateCollision()
        {
            var obj = AssetDatabase.LoadAssetAtPath<GameObject>(clip.resPath);
            if (obj != null)
            {
                _collisioObj = Object.Instantiate(obj);
                _boxCollider = _collisioObj.GetComponent<BoxCollider>();
                _collisioObj.transform.position = Vector3.zero;
                _boxCollider.size = clip.collisionsize;
            }
        }
    }
}