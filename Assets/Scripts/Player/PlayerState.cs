using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    // 先排列方便查看后续做整合优化
    public class PlayerState
    {
        // 是否在攻击状态
        public bool IsAttackState = false;

        // 是否在攻击状态
        public bool CanMove = true;

        // 是否在地面
        public bool IsGround = true;

        // 是否在地面
        public bool IsDodge = false;

    }
}

