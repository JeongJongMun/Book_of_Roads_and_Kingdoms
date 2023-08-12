using System.Collections;
using TMPro;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public float damage;
    public long exp;
    public bool isDead;
    public bool isBoss;
    public Rigidbody2D target;
    Rigidbody2D rigid;
    SpriteRenderer spriter;
    WaitForFixedUpdate wait;
    Collider2D coll;
    Animator animator;

    [Header("피격 데미지 텍스트")]
    public TMP_Text damagedText;

    [Header("피격 데미지 속도")]
    public float textSpeed;

    [Header("피격 데미지 지속시간")]
    public float lifeTime;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        wait = new WaitForFixedUpdate();
        coll = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (GameManager.Instance.isWin) return;
        Vector2 dirVec = target.position - rigid.position;
        Vector2 moveVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        spriter.flipX = dirVec.x > 0 ? true : false;
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


        if(isBoss)
        {
            int value = 2;
            speed = data.speed;
            maxHealth = data.maxHp * value;
            health = data.hp * value;
            damage = data.damage * value;
            exp = (long)(data.exp) * value;
        }
        else
        {
            speed = data.speed;
            maxHealth = data.maxHp;
            health = data.hp;
            damage = data.damage;
            exp = (long)(data.exp);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Skill") || isDead) return;
        StartCoroutine("KnockBack");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Skill") || isDead) return;
        StartCoroutine("KnockBack");
    }

    public void OnDamaged(int damage)
    {
        //Managers.Event.PlayHitEnemyEffectSound();
        //_anime.SetTrigger("Hit");
        int calculateDamage = Mathf.Max(damage, 1); // 방어력 만큼 깍아야함
        health -= calculateDamage;

        // 피격 데미지 띄우기
        CreateFloatingNumber(calculateDamage);
        animator.SetTrigger("doHit");

        OnDead();
    }

    public void OnDead()
    {
        if (health <= 0)
        {
            isDead = true;
            health = 0;
            SpawnExp();
            gameObject.SetActive(false);
            GameManager.Instance.questManager.GetComponent<QuestController>().goalKill--;
            if (GameManager.Instance.questManager.GetComponent<QuestController>().dropcount > 0)
            {
                GameManager.Instance.questManager.GetComponent<QuestController>().dropcount--;
                GameObject mask = Resources.Load<GameObject>("Item/mask" + (GameManager.Instance.questManager.GetComponent<QuestController>().count + 1));
                Debug.Log("Item/mask" + (GameManager.Instance.questManager.GetComponent<QuestController>().count + 1));
                Instantiate(mask, transform.position, Quaternion.identity, null);
            }
        }
    }
    void SpawnExp()
    {
        GameObject _expGo = Resources.Load<GameObject>("Item/Exp");
        GameObject expGo = Instantiate(_expGo, transform.position, Quaternion.identity, null);
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

    private void CreateFloatingNumber(int damage)
    {
        TMP_Text floatingText = Instantiate(damagedText, transform.position, Quaternion.identity); 

        floatingText.text = damage.ToString();
    }
}