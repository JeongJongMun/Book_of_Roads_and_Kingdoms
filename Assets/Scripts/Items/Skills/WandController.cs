using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandController : SkillController
{
    public override int _weaponType { get { return (int)Define.Skills.Koran; } }

    private float timer = 0f;

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
        int healAmount = 1 / GameManager.Instance.player._stat.MaxHP;
        Debug.LogFormat("{0} Healed", healAmount);
        timer = 0f;
        GameManager.Instance.player._stat.HP += healAmount;
    }
}
