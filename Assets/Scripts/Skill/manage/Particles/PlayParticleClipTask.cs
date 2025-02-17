using UnityEngine;
using UnityEditor;

namespace Combat
{
    public class PlayParticleClipTask : SkillClipBase
    {
        private GameObject _effectObj;
        private PlayParticle PlayParticle => ActionClip as PlayParticle;

        private Vector3 originalPos;
        protected override void Begin()
        {
            if (Player.gameObject != null)
            {
                originalPos = Player.gameObject.transform.position;
            }
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
                GameObject effectlist = GameObject.Find("EffectList");
                _effectObj = Object.Instantiate(obj);
                _effectObj.transform.parent = effectlist.transform;
                _effectObj.transform.position = originalPos + PlayParticle.positionoffset;
            }
        }
    }
}