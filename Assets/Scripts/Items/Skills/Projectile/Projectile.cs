using UnityEngine;

public abstract class Projectile : MonoBehaviour
{

    public Vector3 dirVec = new Vector3(1, 0, 0);

    public int damage = 1;
    public float speed = 1f;
    public float rotationSpeed = 100.0f;

    protected float lifeTime = 2f;
    protected float createTime = 0f;

    public void OnEnable()
    {
        createTime = GameManager.GameTime;
    }
    public virtual void FixedUpdate()
    {
        if (GameManager.GameTime - createTime > lifeTime)
        {
            Destroy(gameObject);
        }
        OnMove();
    }
    public abstract void OnMove();
}
