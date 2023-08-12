using UnityEngine;

public class Arrow : Projectile
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject go = other.gameObject;
        if (go.CompareTag("Enemy"))
        {
            go.GetComponent<EnemyController>().OnDamaged(damage);
            Destroy(gameObject);
        }
    }

    public override void OnMove()
    {
        float angle = Mathf.Atan2(dirVec.y, dirVec.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        transform.position += dirVec * (speed * Time.fixedDeltaTime);
    }
}
