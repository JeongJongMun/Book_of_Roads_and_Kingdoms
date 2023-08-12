using UnityEngine;

public class Fireball : Projectile
{

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        GameObject go = other.gameObject;
        if (go.CompareTag("Enemy"))
        {
            go.GetComponent<EnemyController>().OnDamaged(damage);
            DoExplosion();
        }
    }
    public override void OnMove()
    {
        float angle = Mathf.Atan2(dirVec.y, dirVec.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle);
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        transform.position += dirVec * (speed * Time.fixedDeltaTime);
    }

    void DoExplosion()
    {
        //Managers.Sound.Play("Explosion_02");
        GameObject _explosion = Resources.Load<GameObject>("Projectile/Explosion");
        Instantiate(_explosion, transform.position, Quaternion.identity, null);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.fireballCrack);
        Destroy(gameObject);
    }
}
