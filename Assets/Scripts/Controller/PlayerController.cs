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

    private void FixedUpdate()
    {
        Move();
    }
    void Move()
    {
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
        if (GameManager.Instance.isWin) return;
        if (collision.gameObject.tag == "Enemy")
        {
            StartCoroutine(KnockBack());
            _stat.HP -= (int)collision.gameObject.GetComponent<EnemyController>().damage;
            if (_stat.HP <= 0)
            {
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


    IEnumerator KnockBack()
    {
        
        yield return null;
        Vector3 dirVec = (GameManager.Instance.player.transform.position - transform.position).normalized;
        _rigid.AddForce(dirVec * 10, ForceMode2D.Impulse);
    }
}
