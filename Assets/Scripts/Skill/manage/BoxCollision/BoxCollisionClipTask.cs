using UnityEngine;
using UnityEditor;

namespace NBC.ActionEditorExample
{
    public class BoxCollisionClipTask : SkillClipBase
    {
        private GameObject _collisioObj;
        private BoxCollider _boxCollider;
        private BoxCollision BoxCollision => ActionClip as BoxCollision;
        protected override void Begin()
        {
            CreateCollision();
            //播放粒子
            Debug.Log("生成碰撞器");
        }

        protected override void End()
        {
            Object.Destroy(_collisioObj);
            //回收粒子
            Debug.Log("销毁碰撞器");
        }

        private void CreateCollision()
        {
            Debug.Log(BoxCollision.resPath);
            var obj = AssetDatabase.LoadAssetAtPath<GameObject>(BoxCollision.resPath);
            if (obj != null)
            {
                _collisioObj = Object.Instantiate(obj);
                _boxCollider = _collisioObj.GetComponent<BoxCollider>();
                _collisioObj.transform.position = Vector3.zero;
                _boxCollider.size = BoxCollision.collisionsize;
            }
        }
    }
}