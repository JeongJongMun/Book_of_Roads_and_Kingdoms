using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : BaseController
{

    [Header("조이스틱 객체")]
    public VariableJoystick joystick;

    [Header("Ground의 다음 위치")]
    public Vector3 nextPos;

    public float hitTime;
    public Scaner scaner;
    public bool isHit;

    [Header("플레이어 스텟")]
    protected PlayerStat _stat;

    [Header("아이템 수집")]
    GameObject scanObj;
    public float scanDistance;
    RaycastHit2D hit;
    

    protected override void Init()
    {
        _stat = GetComponent<PlayerStat>();
        _rigid = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _anime = GetComponent<Animator>();
        _type = Define.WorldObject.Player;
    }
    void Update()
    {
        ScanItem();
    }
    private void FixedUpdate()
    {
        Move();
    }
    void Move()
    {

        float xAxis = Input.GetAxisRaw("Horizontal");
        float yAxis = Input.GetAxisRaw("Vertical");
        // Ground의 다음 위치 설정
        nextPos = new Vector3(xAxis, yAxis, 0);

        Vector2 moveVec = new Vector2(xAxis, yAxis).normalized * _stat.MoveSpeed * Time.fixedDeltaTime;
        _rigid.MovePosition(_rigid.position + moveVec);
        if (moveVec.x != 0)
        {
            _sprite.flipX = moveVec.x > 0 ? false : true;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (isHit) return;
        if (!GameManager.Instance.isLive) return;
        if (collision.gameObject.tag == "Enemy")
        {
            _stat.HP -= (int)collision.gameObject.GetComponent<Enemy>().damage;
            if (GameManager.Instance.health <= 0)
            {
                //animator.SetTrigger("dead");
                //GameManager.Instance.GameOver();
                //GameManager.Instance.Stop();
                return;
            }
            isHit = true;
            _sprite.color = new Color(1, 0.5f, 0.5f, 1);
            Invoke("ExitHit", hitTime);
        }
    }
    void ExitHit()
    {
        isHit = false;
        _sprite.color = Color.white;
    }

    public override void OnDamaged(int damage, float force = 0)
    {
        //Managers.Event.PlayHitPlayerEffectSound();
        //_stat.HP -= Mathf.Max(damage - _stat.Defense, 1);
        //OnDead();
    }


    public override void OnDead()
    {
        //if (_stat.HP < 0)
        //{
        //    _anime.SetTrigger("dead");
        //    _stat.HP = 0;
        //    Managers.UI.ShowPopupUI<UI_GameOver>();
        //    Managers.GamePause();
        //}

    }

    public void ScanItem()
    {
        hit = Physics2D.CircleCast(transform.position, 2, Vector2.zero,0,LayerMask.GetMask("CollectableItem"));
        if(hit.collider != null)
            scanObj = hit.collider.gameObject;

        if (Input.GetButtonDown("Jump") && scanObj != null)
        {
            GameManager.Instance.itemNum++;
            GameManager.Instance.ShowText();
            Destroy(scanObj);
        }
    }


}
