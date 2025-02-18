using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldAttack : MonoBehaviour
{
    // 武器拖尾
    public GameObject WeaponEffect;

    // 伤害触发器位置
    public Transform AttackLocation;

    // 伤害触发器
    public GameObject AttackObject;

    // 普攻间隔 
    public float AttackDelayTime = 1.0f;

    // 普攻连段间隔 
    public float ClearAttackStateTime = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
        // public void DoAttack()
        // {
        //     // 是否处于攻击状态

        //     if (!IsAttackState && IsGround)//判断是否处于攻击状态
        //     {
        //         if (Input.GetKeyDown(KeyCode.Mouse0) && !IsDodge)//判断是否按下按键
        //         {
        //             WeaponEffect.SetActive(true);

        //             SetActorAttackRotation();

        //             //普通攻击播放动画
        //             if (NormalAttackNum == 0)
        //             {
        //                 Animator1.CrossFade("combo1", 0.1f);
        //             }
        //             else if (NormalAttackNum == 1)
        //             {
        //                 Animator1.CrossFade("combo2", 0.1f);
        //             }
        //             else if (NormalAttackNum == 2)
        //             {
        //                 Animator1.CrossFade("combo3", 0.1f);
        //             }
        //             else
        //             {
        //                 Animator1.CrossFade("combo4", 0.1f);
        //             }

        //             //普通攻击连段
        //             if (NormalAttackNum < 3)
        //             {
        //                 NormalAttackNum = NormalAttackNum + 1;
        //             }
        //             else
        //             {
        //                 NormalAttackNum = 0;
        //             }

        //             //处理攻击状态
        //             IsAttackState = true;
        //             CanMove = false;
        //             Animator1.SetBool("CanMove", false);
        //             //延迟回复攻击状态
        //             Invoke("AttackEnd", AttackDelayTime);
        //             //延迟清空攻击连段
        //             CancelInvoke("ClearAttackState");
        //             Invoke("ClearAttackState", ClearAttackStateTime);
        //         }
        //     }
        // }

        // // 攻击结束
        // public void AttackEnd()
        // {
        //     WeaponEffect.SetActive(false);
        //     IsAttackState = false;
        // }

        // // 清空连段
        // public void ClearAttackState()
        // {
        //     NormalAttackNum = 0;
        // }

        
        // public void SetActorAttackRotation()
        // {
        //     Enemys = GameObject.FindGameObjectsWithTag("Enemy");

        //     GameObject NearestEnemy = null;

        //     float MinDistance = 10000;
        //     Vector3 CurrentPos = transform.position;

        //     foreach (GameObject enemy in Enemys)
        //     {
        //         float distance = Vector3.Distance(enemy.transform.position, CurrentPos);
        //         if (distance < MinDistance)
        //         {
        //             NearestEnemy = enemy;
        //             MinDistance = distance;
        //         }
        //     }

        //     Vector3 EnemyPos = NearestEnemy.transform.position;
        //     EnemyPos.y = transform.position.y;
        //     transform.LookAt(EnemyPos);
        // }

        //         // 生成伤害触发器
        // public void CreateDamage(AnimationEvent AnimationEvent1)
        // {
        //     Instantiate(AttackObject, AttackLocation.position, AttackLocation.rotation);
        // }
}
