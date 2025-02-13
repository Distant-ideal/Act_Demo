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
    // ��������
    private NavMeshAgent Agent;
    private float HpNow = 100.0f;
    // ���λ��
    public Transform player;
    public float IdleDis = 100.0f;
    public float AttackDis = 3.0f;
    public float HpMax = 100.0f;

    // �˺�������λ��
    public Transform HitLocation;

    // �˺�������
    public GameObject HitObject;

    // �˺�������λ��
    public Transform AttackLocation;

    // �˺�������
    public GameObject AttackObject;


    // Start is called before the first frame update
    void Start()
    {
        // HpNow = HpMax;
        // Anim = GetComponent<Animator>();
        // Agent = GetComponent<NavMeshAgent>();
        // player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // LookAtPlayer();
        // EnemyState();
    }
       
    // �����缲
    public void LookAtPlayer()
    {
        if(!IsDead)
        {
            Vector3 PlayerPos = player.position;
            PlayerPos.y = transform.position.y;
            transform.LookAt(PlayerPos);
        }
    }

    // �����¼�
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
       
    //�л�״̬
    public void EnemyState()
    {
        if (!IsDead)
        {
            float distance1 = Vector3.Distance(transform.position, player.position);
            switch (CurrentState)
            {
                // �ȴ�
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
                // ׷��
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
                //����״̬
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
        Debug.Log("1111111111111111111");
        HpNow = HpNow - Random.Range(8, 12);
        // ������Ч
        Instantiate(HitObject, HitLocation.position, HitLocation.rotation);

        // ���Ŷ���
        Anim.CrossFade("GetHit", 0.1f);
    }

    // �����˺�������
    public void CreateDamage(AnimationEvent AnimationEvent1)
    {
        Instantiate(AttackObject, AttackLocation.position, AttackLocation.rotation);
    }
}
