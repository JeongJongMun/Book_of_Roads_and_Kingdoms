using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{

    public Vector3 dirVec = new Vector3(1, 0, 0);

    public int damage = 10;
    public float speed = 3f;
    public float force = 0f;
    public float size = 1f;
    public int panatrate = 1;
    private int piercing = 0;

    [SerializeField] private float lifeTime = 2f;
    [SerializeField] private float createTime = 0f;

    private void OnEnable()
    {
        createTime = GameManager.GameTime;
    }
    void FixedUpdate()
    {
        if (GameManager.GameTime - createTime > lifeTime)
        {
            DoExplosion();
        }
        OnMove();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject go = other.gameObject;
        if (go.CompareTag("Enemy"))
        {
            piercing++;
            if (piercing >= panatrate)
            {
                DoExplosion();
            }
            else
                go.GetComponent<EnemyController>().OnDamaged(damage, force / 2);
        }
    }

    void DoExplosion()
    {
        //Managers.Sound.Play("Explosion_02");
        GameObject _explosion = Resources.Load<GameObject>("Projectile/Explosion");
        GameObject explosion = Instantiate(_explosion, transform.position, Quaternion.identity, null);
        explosion.transform.localScale = Vector3.one * size;
        //Explosion explosionStat = explosion.FindChild<Explosion>();
        //explosionStat.damage = (int)(damage * 1.1);
        //explosionStat.force = force;
        Destroy(gameObject);
    }

    void OnMove()
    {
        float angle = Mathf.Atan2(dirVec.y, dirVec.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        transform.position += dirVec * (speed * Time.fixedDeltaTime);
    }
}
