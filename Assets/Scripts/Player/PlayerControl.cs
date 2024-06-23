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

    // 普攻间隔 
    public float AttackDelayTime = 1.0f;

    // 普攻连段间隔 
    public float ClearAttackStateTime = 1.0f;

    public GameObject WeaponEffect;

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
        Move();
        MouseControl();
        DoAttack();
    }   

    // 人物移动
    public void Move()
    {
        if (IsAttackState == false)
        {
            // h水平v垂直
            InputH = Input.GetAxis("Horizontal");
            InputV = Input.GetAxis("Vertical");
            
            Direction = new Vector3(InputH, 0, InputV); //normalized
            // 检测输入
            if(Direction.magnitude >= 0.1f)
            {
                Animator1.SetBool("Run", true);
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
            else
            {
                Animator1.SetBool("Run", false);
            }
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
        if(Input.GetKey(KeyCode.LeftAlt))
        {
            UnLockMouse();
        }

        if(Input.GetKeyUp(KeyCode.LeftAlt))
        {
            LockMouse();
        }
    }

    public void DoAttack()  
    {
        // 是否处于攻击状态
        if(IsAttackState == false)
        {
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {   
                WeaponEffect.SetActive(true);
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

                if (NormalAttackNum >= 3)
                {
                    NormalAttackNum = 0;
                }
                else
                {
                    NormalAttackNum += 1;
                }

                IsAttackState = true;
                // 延迟恢复攻击状态
                Invoke("AttakEnd", AttackDelayTime);

                // 延迟清空连段
                CancelInvoke("ClearAttackState");
                Invoke("ClearAttackState", ClearAttackStateTime);
            }
        }
        
    }

    // 攻击结束
    public void AttakEnd() 
    {
        WeaponEffect.SetActive(false);
        IsAttackState = false;
    }

    // 清空连段
    public void ClearAttackState()
    {
        NormalAttackNum = 0;
    }
}
