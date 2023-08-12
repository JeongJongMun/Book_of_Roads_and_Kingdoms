using Unity.VisualScripting;
using UnityEngine;

public class SamshirAttack : MonoBehaviour
{
    public int damage = 5;
    public float force = 0f;
    private void Start()
    {
        //Vector3 dir = GameManager.Instance.playerObject.transform.position - transform.position;
        //Quaternion rot = Quaternion.LookRotation(dir.normalized);
        //transform.rotation = rot;
        
        Destroy(gameObject, 0.5f);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<EnemyController>().OnDamaged(damage);
        }
    }
}
