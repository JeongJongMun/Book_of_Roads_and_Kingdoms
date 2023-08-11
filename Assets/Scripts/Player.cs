using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector3 nextPos;

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
    void Start()
    {

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
        float xAxis = Input.GetAxisRaw("Horizontal");
        float yAxis = Input.GetAxisRaw("Vertical");
        Debug.Log(xAxis);

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
