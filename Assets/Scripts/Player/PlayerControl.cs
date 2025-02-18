using System.Security.Cryptography;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
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

        // 伤害触发器位置
        public Transform HitLocation;

        // 伤害触发器
        public GameObject HitObject;

        private GameObject[] Enemys;

        private bool IsBeAttack = false;

        public PlayerState playerState;

        void Awake()
        {
            playerState = new PlayerState();
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
                // DoAttack();
                // Jump();
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
                if (!playerState.IsAttackState && playerState.CanMove)
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

        // 回复移动状态
        public void RestoreMoveState(AnimationEvent AnimationEvent1)
        {
            if (AnimationEvent1.stringParameter == "dodge_end")
            {
                playerState.CanMove = true;
                Animator1.SetBool("CanMove", true);
            }
        }

        // 跳跃
        public void Jump()
        {
            if (playerState.CanMove && !playerState.IsDodge)
            {
                if (playerState.IsGround && Input.GetKeyDown(KeyCode.Space))
                {
                    Animator1.SetBool("IsGround", false);
                    Animator1.CrossFade("Jump", 0.1f);
                    playerState.IsGround = false;
                    Invoke("JumpEnd", 1.0f);
                }
            }
        }

        // 跳跃结束
        public void JumpEnd()
        {
            playerState.IsGround = true;
            Animator1.SetBool("IsGround", true);
        }

        // 闪避事件
        public void Dodge()
        {
            if (playerState.IsGround)
            {
                if (Input.GetKeyDown(KeyCode.LeftShift) && !playerState.IsDodge)
                {
                    playerState.IsDodge = true;
                    Animator1.CrossFade("dodge_back", 0f);
                    Invoke("DodgeEnd", 0.5f);
                    // AttackEnd();
                }
            }
        }

        // 闪避事件
        public void DodgeEnd()
        {
            playerState.IsDodge = false;
            // 延迟清空攻击连段
            // CancelInvoke("ClearAttackState");
            // Invoke("ClearAttackState", ClearAttackStateTime);
        }

        public void GetHit()
        {
            if (!playerState.IsDodge)
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
}