using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public float damage;
    public long exp;
    //public RuntimeAnimatorController[] animCon;
    public bool isDead;
    public Rigidbody2D target;
    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;
    WaitForFixedUpdate wait;
    Collider2D coll;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        //anim = GetComponent<Animator>();
        wait = new WaitForFixedUpdate();
        coll = GetComponent<Collider2D>();
    }

    void Update()
    {
    }
    void FixedUpdate()
    {
        //anim.SetBool("Dead", isDead);
        //if (isDead || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit")) return;
        Vector2 dirVec = target.position - rigid.position;
        Vector2 moveVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        spriter.flipX = dirVec.x > 0 ? false : true;
        rigid.MovePosition(rigid.position + moveVec);
        rigid.velocity = Vector2.zero;
    }

    void OnEnable()
    {
        target = GameManager.Instance.player.GetComponent<Rigidbody2D>();
        coll.enabled = true;
        spriter.sortingOrder = 2;
        rigid.simulated = true;
        isDead = false;
        health = maxHealth;
    }

    public void Init(SpawnData data)
    {
        //anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed * (GameManager.Instance.stage * 0.5f + 1);
        maxHealth = data.health * (GameManager.Instance.stage * 0.5f + 1);
        health = data.health * (GameManager.Instance.stage * 0.5f + 1);
        damage = data.damage * (GameManager.Instance.stage * 0.5f + 1);
        exp = (long)(data.exp * (GameManager.Instance.stage * 0.5f + 1));
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Weapon") || isDead) return;
        StartCoroutine(KnockBack());
    }

    public void OnDamaged(int damage, float force = 0)
    {
        //Managers.Event.PlayHitEnemyEffectSound();
        //_anime.SetTrigger("Hit");
        int calculateDamage = Mathf.Max(damage, 1); // 방어력 만큼 깍아야함
        health -= calculateDamage;
        rigid.AddForce((rigid.position - target.position).normalized * (force * 200f));
        //FloatDamageText(calculateDamage);


        OnDead();
    }

    public void OnDead()
    {
        if (health <= 0)
        {
            isDead = true;
            health = 0;
            gameObject.SetActive(false);
            SpawnExp();
            //    Managers.Event.DropItem(_stat, transform);
            //    transform.localScale = Vector3.one;
            //    Managers.Game.Despawn(gameObject);
        }
    }
    void SpawnExp()
    {
        GameObject _expGo = Resources.Load<GameObject>("Item/Exp");
        GameObject expGo = Instantiate(_expGo, transform.position, Quaternion.identity, null);
        expGo.transform.position = transform.position;
        Exp_Item expPoint = expGo.GetComponent<Exp_Item>();
        expPoint._exp = exp;
        expPoint._expMul = 1;

        //if (expPoint._expMul == 1)
        //    expGo.GetComponent<SpriteRenderer>().sprite = expPoint._sprite[0];
        //else if (expPoint._expMul == 2)
        //    expGo.GetComponent<SpriteRenderer>().sprite = expPoint._sprite[1];
        //else
        //    expGo.GetComponent<SpriteRenderer>().sprite = expPoint._sprite[2];

    }

    IEnumerator KnockBack()
    {
        yield return wait;
        Vector3 dirVec = (GameManager.Instance.player.transform.position - transform.position).normalized;
        rigid.AddForce((-1) * dirVec * 3, ForceMode2D.Impulse);
    }
}