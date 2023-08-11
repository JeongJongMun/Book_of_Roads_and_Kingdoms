using UnityEngine;

public class Born : MonoBehaviour
{
    public Vector3 dirVec = new Vector3(1, 0, 0);

    public int damage = 10;
    public float speed = 10f;
    public float force = 0f;
    public float size = 1f;
    public int panatrate = 1;
    private int piercing = 0;
    public float rotationSpeed = 50.0f;

    [SerializeField] private float lifeTime = 2f;
    [SerializeField] private float createTime = 0f;

    private void OnEnable()
    {
        createTime = GameManager.GameTime;
    }
    void FixedUpdate()
    {
        if (GameManager.GameTime - createTime > lifeTime)
        {
            Destroy(gameObject);
        }
        OnMove();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject go = other.gameObject;
        if (go.CompareTag("Enemy"))
        {
            piercing++;
            go.GetComponent<EnemyController>().OnDamaged(damage, force);
            if (piercing >= panatrate)
            {
                Destroy(gameObject);
            }
        }
    }


    void OnMove()
    {
        float angle = Mathf.Atan2(dirVec.y, dirVec.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        transform.position += dirVec * (speed * Time.fixedDeltaTime);
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime); 
    }
}
