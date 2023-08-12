using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KorandController : SkillController
{
    public override int _weaponType { get { return (int)Define.Skills.Koran; } }

    private float timer = 0f;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other.GetComponent<EnemyController>().korandTerm <= 0f)
            {
                other.GetComponent<EnemyController>().OnDamaged(_damage);
                other.GetComponent<EnemyController>().korandTerm = 0.1f;
            }
        }

    }
    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > 10f)
        {
            Heal();
        }
    }

    private void Heal()
    {
        int healAmount = 5 / GameManager.Instance.player._stat.MaxHP;
        Debug.LogFormat("{0} Healed", healAmount);
        timer = 0f;
        GameManager.Instance.player._stat.HP += healAmount;
    }
}
