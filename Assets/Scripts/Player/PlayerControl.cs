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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }   

    // 人物移动
    public void Move()
    {
        // h水平v垂直
        InputH = Input.GetAxis("Horizontal");
        InputV = Input.GetAxis("Vertical");
        
        Direction = new Vector3(InputH, 0, InputV); //normalized
        // 移动
        Controller1.Move(Direction * MoveSpeed * Time.deltaTime);
    }
}
