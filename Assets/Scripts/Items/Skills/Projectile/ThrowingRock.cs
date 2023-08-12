using UnityEngine;

public class ThrowingRock : Projectile
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
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Rock);
            Destroy(gameObject);
        }
    }
    public override void OnMove()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        transform.position += dirVec * (speed * Time.fixedDeltaTime);
    }
}
