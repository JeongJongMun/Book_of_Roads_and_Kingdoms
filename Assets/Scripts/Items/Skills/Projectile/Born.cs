using UnityEngine;

public class Born : Projectile
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
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        transform.position += dirVec * (speed * Time.fixedDeltaTime);
    }
}
