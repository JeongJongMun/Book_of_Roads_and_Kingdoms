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
    public float hitTime;
    public Scaner scaner;
    public bool isHit;


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

    void FixedUpdate()
    {
    }


    void OnCollisionStay2D(Collision2D collision)
    {
        if (isHit) return;
        if (!GameManager.Instance.isLive) return;
        if (collision.gameObject.tag == "Enemy")
        {
            GameManager.Instance.health -= (int)collision.gameObject.GetComponent<Enemy>().damage;
            if (GameManager.Instance.health <= 0)
            {
                //animator.SetTrigger("dead");
                //GameManager.Instance.GameOver();
                //GameManager.Instance.Stop();
                return;
            }
            isHit = true;
            spriter.color = new Color(1, 0.5f, 0.5f, 1);
            Invoke("ExitHit", hitTime);
        }
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



    void ExitHit()
    {
        isHit = false;
        spriter.color = Color.white;
    }


}
