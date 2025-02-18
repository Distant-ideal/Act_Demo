using UnityEngine;
using UnityEditor;

namespace Combat
{
    public class BoxCollisionClipTask : SkillClipBase
    {
        private GameObject _collisioObj;
        private BoxCollider _boxCollider;
        private BoxCollision BoxCollision => ActionClip as BoxCollision;

        private Vector3 originalPos;
        protected override void Begin()
        {
            CreateCollision();
            if (Player.gameObject != null)
            {
                originalPos = Player.gameObject.transform.position;
            }

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
                GameObject boxcoliderlist = GameObject.Find("BoxColiderList");
                _collisioObj = Object.Instantiate(obj);
                _collisioObj.transform.parent = boxcoliderlist.transform;
                
                _boxCollider = _collisioObj.GetComponent<BoxCollider>();
                _collisioObj.transform.position = originalPos + BoxCollision.positionoffset;
                _boxCollider.size = BoxCollision.collisionsize;
            }
        }
    }
}