using System.Collections;
using System.Collections.Generic;
using NBC.ActionEditorExample;
using UnityEngine;

public class Init : MonoBehaviour
{
    bool is_play = false;
    void Start()
    {
    }

    void Update()
    {
        if (!is_play && Input.GetKey(KeyCode.Space))
        {
            is_play = true;
            //添加个模拟配置
            SkillConfig skillConfig = new SkillConfig
            {
                Id = 66,
                Atk = 666,
                Def = 88,
                EventName = "1111"
            };

            //添加个模拟技能
            SkillPlayAttack skill = new SkillPlayAttack
            {
                SkillConfig = skillConfig
            };

            var Player = GameObject.Find("Player");
            var hero = Player.GetComponent<HeroPlayer>(); //演示的role脚本直接继承MonoManager，减小代码量
            Debug.Log(Player);
            hero.AddSkill(skill);
            
            //释放技能
            hero.UseSkill(66);
        }
    }
}