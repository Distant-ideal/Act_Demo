using System.Security.Cryptography;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // 记录轴向值
    float InputH;
    float InputV;
    // 记录按键方向
    Vector3 Direction;
    // 玩家控制器
    public CharacterController Controller1;
    public float MoveSpeed;

    public Camera Camera1;
    // 计算旋转角度
    private float TurnSmooth;
    // 旋转时长
    public float TurnTime = 0.1f;

    // 动画
    public Animator Animator1;

    // 普攻阶段
    private int NormalAttackNum = 0;

    // 是否在攻击状态
    private bool IsAttackState = false;

    // 是否在攻击状态
    private bool CanMove = true;

    // 普攻间隔 
    public float AttackDelayTime = 1.0f;

    // 普攻连段间隔 
    public float ClearAttackStateTime = 3.0f;

    // 是否在地面
    public bool IsGround = true;

    // 是否在地面
    public bool IsDodge = false;

    // 武器拖尾
    public GameObject WeaponEffect;

    // 伤害触发器位置
    public Transform AttackLocation;

    // 伤害触发器
    public GameObject AttackObject;

    // 伤害触发器位置
    public Transform HitLocation;

    // 伤害触发器
    public GameObject HitObject;

    private GameObject[] Enemys;

    private bool IsBeAttack = false;

    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        LockMouse();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsBeAttack)
        {
            Move();
            DoAttack();
            Jump();
        }

        Dodge();
        MouseControl();
    }

    // 触发事件
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyAttackBox")
        {
            GetHit();
        }
    }

    // 人物移动
    public void Move()
    {
        // h水平v垂直
        InputH = Input.GetAxis("Horizontal");
        InputV = Input.GetAxis("Vertical");

        Direction = new Vector3(InputH, 0, InputV); //normalized
        // 检测输入
        if (Direction.magnitude >= 0.1f)
        {
            if (!IsAttackState && CanMove)
            {
                //Animator1.SetBool("Run", true);
                Animator1.SetFloat("Speed", Direction.magnitude);
                // 计算目标角度
                float TargetAngle = Mathf.Atan2(Direction.x, Direction.z) * Mathf.Rad2Deg + Camera1.transform.eulerAngles.y;
                // 平滑角度过渡
                float Angle = Mathf.SmoothDampAngle(this.transform.eulerAngles.y, TargetAngle, ref TurnSmooth, TurnTime);
                // 设置角色旋转
                this.transform.rotation = Quaternion.Euler(0, Angle, 0);
                // 计算新的移动方向
                Vector3 MoveDirection = Quaternion.Euler(0, TargetAngle, 0) * Vector3.forward;
                // 移动
                Controller1.Move(MoveDirection * MoveSpeed * Time.deltaTime);
            }

        }
        else
        {
            Animator1.SetFloat("Speed", 0f);
        }
    }

    // 锁定鼠标
    public void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // 解锁鼠标
    public void UnLockMouse()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    // 鼠标控制器
    public void MouseControl()
    {
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            UnLockMouse();
        }

        if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            LockMouse();
        }
    }

    public void DoAttack()
    {
        // 是否处于攻击状态

        if (!IsAttackState && IsGround)//判断是否处于攻击状态
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && !IsDodge)//判断是否按下按键
            {
                WeaponEffect.SetActive(true);

                SetActorAttackRotation();

                //普通攻击播放动画
                if (NormalAttackNum == 0)
                {
                    Animator1.CrossFade("combo1", 0.1f);
                }
                else if (NormalAttackNum == 1)
                {
                    Animator1.CrossFade("combo2", 0.1f);
                }
                else if (NormalAttackNum == 2)
                {
                    Animator1.CrossFade("combo3", 0.1f);
                }
                else
                {
                    Animator1.CrossFade("combo4", 0.1f);
                }

                //普通攻击连段
                if (NormalAttackNum < 3)
                {
                    NormalAttackNum = NormalAttackNum + 1;
                }
                else
                {
                    NormalAttackNum = 0;
                }

                //处理攻击状态
                IsAttackState = true;
                CanMove = false;
                Animator1.SetBool("CanMove", false);
                //延迟回复攻击状态
                Invoke("AttackEnd", AttackDelayTime);
                //延迟清空攻击连段
                CancelInvoke("ClearAttackState");
                Invoke("ClearAttackState", ClearAttackStateTime);
            }
        }
    }

    // 攻击结束
    public void AttackEnd()
    {
        WeaponEffect.SetActive(false);
        IsAttackState = false;
    }

    // 清空连段
    public void ClearAttackState()
    {
        NormalAttackNum = 0;
    }

    // 回复移动状态
    public void RestoreMoveState(AnimationEvent AnimationEvent1)
    {
        if (NormalAttackNum == AnimationEvent1.intParameter || AnimationEvent1.stringParameter == "dodge_end")
        {
            CanMove = true;
            Animator1.SetBool("CanMove", true);
        }
    }

    // 跳跃
    public void Jump()
    {
        if (CanMove && !IsDodge)
        {
            if (IsGround && Input.GetKeyDown(KeyCode.Space))
            {
                Animator1.SetBool("IsGround", false);
                Animator1.CrossFade("Jump", 0.1f);
                IsGround = false;
                Invoke("JumpEnd", 1.0f);
            }
        }
    }

    // 跳跃结束
    public void JumpEnd()
    {
        IsGround = true;
        Animator1.SetBool("IsGround", true);
    }

    // 闪避事件
    public void Dodge()
    {
        if (IsGround)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && !IsDodge)
            {
                IsDodge = true;
                Animator1.CrossFade("dodge_back", 0f);
                Invoke("DodgeEnd", 0.5f);
                AttackEnd();
            }
        }
    }

    // 闪避事件
    public void DodgeEnd()
    {
        IsDodge = false;
        //延迟清空攻击连段
        CancelInvoke("ClearAttackState");
        Invoke("ClearAttackState", ClearAttackStateTime);
    }

    // 生成伤害触发器
    public void CreateDamage(AnimationEvent AnimationEvent1)
    {
        Instantiate(AttackObject, AttackLocation.position, AttackLocation.rotation);
    }

    public void SetActorAttackRotation()
    {
        Enemys = GameObject.FindGameObjectsWithTag("Enemy");

        GameObject NearestEnemy = null;

        float MinDistance = 10000;
        Vector3 CurrentPos = transform.position;

        foreach (GameObject enemy in Enemys)
        {
            float distance = Vector3.Distance(enemy.transform.position, CurrentPos);
            if (distance < MinDistance)
            {
                NearestEnemy = enemy;
                MinDistance = distance;
            }
        }

        Vector3 EnemyPos = NearestEnemy.transform.position;
        EnemyPos.y = transform.position.y;
        transform.LookAt(EnemyPos);
    }

    public void GetHit()
    {
        if (!ISDodge)
        {
            IsBeAttack = true;

            // 生成特效
            Instantiate(HitObject, HitLocation.position, HitLocation.rotation);

            // 播放动画
            Animator1.CrossFade("GetHit", 0.1f);

            Invoke("GetHitEnd", 1.0f);

            Animator1.SetFloat("Speed", 0);
        }  
    }

    public void GetHitEnd()
    {
        IsBeAttack = false;
    }
}
