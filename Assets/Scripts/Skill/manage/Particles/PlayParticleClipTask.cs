using UnityEngine;
using UnityEditor;

namespace NBC.ActionEditorExample
{
    public class PlayParticleClipTask : SkillClipBase
    {
        private GameObject _effectObj;
        private PlayParticle PlayParticle => ActionClip as PlayParticle;
        protected override void Begin()
        {
            CreateEffect();
            //播放粒子
            Debug.Log("开始播放粒子===");
        }

        protected override void End()
        {
            Object.Destroy(_effectObj);
            //回收粒子
            Debug.Log("粒子播放完毕===");
        }

        private void CreateEffect()
        {
            
            var obj = AssetDatabase.LoadAssetAtPath<GameObject>(PlayParticle.resPath);
            if (obj != null)
            {
                _effectObj = Object.Instantiate(obj);
                _effectObj.transform.position = Vector3.zero;
            }
        }
    }
}