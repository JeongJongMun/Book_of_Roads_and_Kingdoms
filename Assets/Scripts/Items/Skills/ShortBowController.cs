using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShortBowController : SkillController
{
    bool _isCool = false;
    public override int _weaponType { get { return (int)Define.Skills.Shortbow; } }

    void Update()
    {
        if (!_isCool)
        {
            StartCoroutine(SpawnWeapon());
        }
    }

    IEnumerator SpawnWeapon()
    {
        _isCool = true;
        float angle = SetTarget();
        //Managers.Sound.Play("Shoot_03");
        for (int i = 0; i < _countPerCreate; i++)
        {
            GameObject _go = Resources.Load<GameObject>("Projectile/Arrow");
            GameObject go = Instantiate(_go, transform.position, Quaternion.identity, null);
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Bow);
            SetWeapon(go, angle);
            if (i == _countPerCreate - 1)
                break;
        }
        yield return new WaitForSeconds(_cooldown);
        _isCool = false;
    }

    float SetTarget()
    {
        List<GameObject> FoundEnemys = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        float shortestDist = float.MaxValue;
        GameObject shortestDistEnemy = gameObject;
        foreach (GameObject enemy in FoundEnemys)
        {
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist < shortestDist)
            {
                shortestDist = dist;
                shortestDistEnemy = enemy;
            }
        }
        Vector3 dirVec = (shortestDistEnemy.transform.position - transform.position).normalized;
        return Mathf.Atan2(dirVec.y, dirVec.x) * Mathf.Rad2Deg;
    }

    protected void SetWeapon(GameObject weapon, float angle)
    {
        Arrow arrow = weapon.GetComponent<Arrow>();
        angle += Random.Range(-5f, 5f);
        arrow.dirVec = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);
        arrow.damage = _damage;
        arrow.speed = _speed;
    }
}
