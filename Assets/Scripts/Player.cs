using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("���̽�ƽ ��ü")]
    public VariableJoystick joystick;

    [Header("Ground�� ���� ��ġ")]
    public Vector3 nextPos;

    [Header("�̵� �ӵ�")]
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

        // Ground�� ���� ��ġ ����
        nextPos = new Vector3(xAxis, yAxis, 0);

        Vector2 moveVec = new Vector2(xAxis,yAxis).normalized * moveSpeed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + moveVec);
        if (moveVec.x != 0)
        {
            spriter.flipX = moveVec.x > 0 ? false : true;
        }
    }
}
