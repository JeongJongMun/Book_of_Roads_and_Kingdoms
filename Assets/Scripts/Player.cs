using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector3 nextPos;

    public float moveSpeed;
    public float hitTime;
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






}
