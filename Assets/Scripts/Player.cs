using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("조이스틱 객체")]
    public VariableJoystick joystick;

    [Header("Ground의 다음 위치")]
    public Vector3 nextPos;

    [Header("이동 속도")]
    public float moveSpeed;

    Rigidbody2D rigid;
    public Animator animator;
    SpriteRenderer spriter;
    public RuntimeAnimatorController[] animatorControllers;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        float xAxis = joystick.Horizontal;
        float yAxis = joystick.Vertical;

        // Ground의 다음 위치 설정
        nextPos = new Vector3(xAxis, yAxis, 0);

        Vector2 moveVec = new Vector2(xAxis,yAxis).normalized * moveSpeed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + moveVec);
        if (moveVec.x != 0)
        {
            spriter.flipX = moveVec.x > 0 ? false : true;
        }
    }
}
