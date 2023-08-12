using UnityEngine;

public class Explosion : MonoBehaviour
{
    public int damage = 10;
    public float force = 0f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<EnemyController>().OnDamaged(damage);
        }
    }
    private void Start()
    {
        Destroy(gameObject, 0.5f);
    }
}
