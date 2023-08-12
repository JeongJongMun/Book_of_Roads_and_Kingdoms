using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SamshirController : SkillController
{
    bool _isCool = false;
    public override int _weaponType { get { return (int)Define.Skills.Samshir; } }

    public float distance = 3.0f;

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

        for (int i = 0; i < _countPerCreate; i++)
        {
            GameObject _go = Resources.Load<GameObject>("Projectile/SamshirAttack");
            Vector3 spawnPosition = transform.position + transform.forward * distance;

            GameObject go = Instantiate(_go, spawnPosition, Quaternion.identity, null);
            //AudioManager.instance.PlaySfx(AudioManager.Sfx.fireballSpell);

            SetWeapon(go);
            if (i == _countPerCreate - 1)
                break;
        }
        yield return new WaitForSeconds(_cooldown);
        _isCool = false;
    }


    protected void SetWeapon(GameObject weapon)
    {
        SamshirAttack samshirAttack = weapon.GetOrAddComponent<SamshirAttack>();
        samshirAttack.damage = _damage;
    }
}
