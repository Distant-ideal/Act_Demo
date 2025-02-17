using UnityEngine;

namespace Combat
{
    public class MoveByClipTask : SkillClipBase
    {
        private Vector3 originalPos;

        protected override void Begin()
        {
            if (Player.gameObject != null)
            {
                originalPos = Player.gameObject.transform.position;
            }
        }

        protected override void End()
        {
            
        }

        protected override void Tick()
        {
            MoveBy clip = ActionClip as MoveBy;
            var target = originalPos + clip.move;
            float distance = Vector3.Distance(originalPos, target);
            Player.gameObject.transform.position = Vector3.Lerp(originalPos, target, Progress);
        }
    }
}