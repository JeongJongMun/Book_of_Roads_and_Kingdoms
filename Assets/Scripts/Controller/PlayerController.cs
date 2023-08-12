using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : BaseController
{

    [Header("조이스틱 객체")]
    public FloatingJoystick joystick;

    [Header("Ground의 다음 위치")]
    public Vector3 nextPos;

    [Header("플레이어 움직임 여부")]
    public bool isWalking;

    public float hitTime;
    public Scaner scaner;
    public bool isHit;

    [Header("플레이어 스텟")]
    public PlayerStat _stat;

    [Header("아이템 수집")]
    GameObject scanObj;
    public float scanDistance;
    RaycastHit2D hit;
    

    protected override void Init()
    {
        _stat = GetComponent<PlayerStat>();
        _rigid = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
        _type = Define.WorldObject.Player;
    }
    void Update()
    {
        GameManager.Instance.HideText();
        ScanItem();
    }
    private void FixedUpdate()
    {
        Move();
    }
    void Move()
    {
        if (GameManager.Instance.isShowText)
            return;

        float xAxis = joystick.Horizontal;
        float yAxis = joystick.Vertical;

        if (xAxis != 0 || yAxis != 0)
        {
            if (!isWalking)
            {
                AudioManager.instance.walkPlayer.Play();
            }
            else
            {
                AudioManager.instance.walkPlayer.Pause();
            }
            isWalking = true;
            _anim.SetBool("isWalking", isWalking);

        }
        else
        {
            isWalking = false;
            _anim.SetBool("isWalking", isWalking);
        }

        // Ground의 다음 위치 설정
        nextPos = new Vector3(xAxis, yAxis, 0);

        Vector2 moveVec = new Vector2(xAxis, yAxis).normalized * _stat.MoveSpeed * Time.fixedDeltaTime;
        _rigid.MovePosition(_rigid.position + moveVec);
        if (moveVec.x != 0)
        {
            _sprite.flipX = moveVec.x > 0 ? true : false;
        }
    }


    void OnCollisionStay2D(Collision2D collision)
    {
        if (isHit) return;
        if (!GameManager.Instance.isLive) return;
        if (collision.gameObject.tag == "Enemy")
        {
            StartCoroutine(KnockBack());
            _stat.HP -= (int)collision.gameObject.GetComponent<EnemyController>().damage;
            if (_stat.HP <= 0)
            {
               
                //animator.SetTrigger("dead");
                GameManager.Instance.GameOver(false);
                AudioManager.instance.PlaySfx(AudioManager.Sfx.StageFail);
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
            if (GameManager.Instance.itemNum == 3 || GameManager.Instance.stage == 1)
            {
                GameManager.Instance.isBossPhase = true;
                //map.SetActive(true);
            }
            GameManager.Instance.ShowText();
            Destroy(scanObj);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "wayPoint0")
        {
            GameManager.Instance.checkWayPoints[0] = true;
        }
        if (collision.gameObject.tag == "wayPoint1" && GameManager.Instance.checkWayPoints[0])
        {
            GameManager.Instance.checkWayPoints[1] = true;
        }
        if (collision.gameObject.tag == "wayPoint2" && GameManager.Instance.checkWayPoints[1])
        {
            GameManager.Instance.checkWayPoints[2] = true;
        }
        if (collision.gameObject.tag == "wayPoint3" && GameManager.Instance.checkWayPoints[2])
        {
            GameManager.Instance.checkWayPoints[3] = true;
        }
    }



    IEnumerator KnockBack()
    {
        
        yield return null;
        Vector3 dirVec = (GameManager.Instance.player.transform.position - transform.position).normalized;
        _rigid.AddForce(dirVec * 10, ForceMode2D.Impulse);
    }
}
