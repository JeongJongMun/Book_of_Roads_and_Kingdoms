using System.Collections;
using UnityEngine;

public class CatController : SkillController
{

    public Animator animator;

    PlayerController playerController;

    bool _isCool = false;
    public override int _weaponType { get { return (int)Define.Skills.Cat; } }

    private void Start()
    {
        _cooldown = 5;
        _countPerCreate = 3;

        animator = GetComponent<Animator>();
        playerController = GameManager.Instance.player;
    }

    void Update()
    {
        CatMovement();
        if (!_isCool)
        {
            StartCoroutine(SpawnWeapon());
        }
    }

    private void CatMovement()
    {
        if (playerController.isWalking)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    IEnumerator SpawnWeapon()
    {
        _isCool = true;
        //Managers.Sound.Play("Shoot_03");
        for (int i = 0; i < _countPerCreate; i++)
        {
            float angle = SetTarget();

            GameObject _go = Resources.Load<GameObject>("Projectile/ThrowingRock");
            GameObject go = Instantiate(_go, transform.position, Quaternion.identity, null);

            SetWeapon(go, angle);
            //if (i == _countPerCreate - 1)
            //    break;
        }
        yield return new WaitForSeconds(_cooldown);
        _isCool = false;
    }

    float SetTarget()
    {
        return Random.Range(0f, 360f);
    }

    protected void SetWeapon(GameObject weapon, float angle)
    {
        ThrowingRock fireball = weapon.GetComponent<ThrowingRock>();
        angle += Random.Range(-5f, 5f);
        fireball.dirVec = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);
        fireball.damage = _damage;
        fireball.speed = _speed;
    }
}
