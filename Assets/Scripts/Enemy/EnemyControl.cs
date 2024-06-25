using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum BossState
{
    idle,
    attack,
    run,
}

public class EnemyControl : MonoBehaviour
{
    private BossState CurrentState = BossState.idle;
    private bool IsDead = false;
    private Animator Anim;
    // 导航网格
    private NavMeshAgent Agent;
    private float HpNow = 100.0f;
    // 玩家位置
    public Transform player;
    public float IdleDis = 100.0f;
    public float AttackDis = 3.0f;
    public float HpMax = 100.0f;

    // 伤害触发器位置
    public Transform HitLocation;

    // 伤害触发器
    public GameObject HitObject;

    // 伤害触发器位置
    public Transform AttackLocation;

    // 伤害触发器
    public GameObject AttackObject;


    // Start is called before the first frame update
    void Start()
    {
        HpNow = HpMax;
        Anim = GetComponent<Animator>();
        Agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        LookAtPlayer();
        EnemyState();
    }
       
    // 朝向顽疾
    public void LookAtPlayer()
    {
        if(!IsDead)
        {
            Vector3 PlayerPos = player.position;
            PlayerPos.y = transform.position.y;
            transform.LookAt(PlayerPos);
        }
    }

    // 触发事件
    private void OnTriggerEnter(Collider other)
    {
        if (!IsDead)
        {
            if(other.gameObject.tag == "AttackBox")
            {
                GetHit();
            }
        }
    }
       
    //切换状态
    public void EnemyState()
    {
        if (!IsDead)
        {
            float distance1 = Vector3.Distance(transform.position, player.position);
            switch (CurrentState)
            {
                // 等待
                case BossState.idle:
                    Anim.Play("RunTree");
                    Anim.SetFloat("MoveSpeed", 0f);
                    Anim.SetBool("Attack", false);
                    Agent.isStopped = true;

                    if(distance1 > AttackDis && distance1 <= IdleDis)
                    {
                        CurrentState = BossState.run;
                    }
                    break;
                // 追踪
                case BossState.run:
                    Agent.isStopped = false;
                    Agent.SetDestination(player.position);
                    Anim.SetFloat("MoveSpeed", 1f);
                    Anim.SetBool("Attack", false);
                    if(distance1 > IdleDis)
                    {
                        CurrentState = BossState.idle;
                    }
                    else if (distance1 <= AttackDis)
                    {
                        CurrentState = BossState.attack;
                    }
                    break;
                //攻击状态
                case BossState.attack:
                    Agent.isStopped = false;
                    EnemyAttack();
                    if (distance1 > AttackDis)
                    {
                        CurrentState = BossState.run;
                    }
                    else
                    {
                        Anim.SetFloat("MoveSpeed", 0f);
                    }
                    break; 
            }
        }
        else
        {
            Agent.isStopped = true;
        }
    }

    public void EnemyAttack()
    {
        if (Anim.GetBool("Attack"))
        {
            return;
        }

        Anim.SetBool("Attack", true);
    }

    public void GetHit()
    {
        HpNow = HpNow - Random.Range(8, 12);
        // 生成特效
        Instantiate(HitObject, HitLocation.position, HitLocation.rotation);

        // 播放动画
        Anim.CrossFade("GetHit", 0.1f);
    }

    // 生成伤害触发器
    public void CreateDamage(AnimationEvent AnimationEvent1)
    {
        Instantiate(AttackObject, AttackLocation.position, AttackLocation.rotation);
    }
}
