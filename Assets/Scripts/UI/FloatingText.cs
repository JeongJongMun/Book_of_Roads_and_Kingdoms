using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float moveSpeed;
    public float destroyTime;
    public Vector3 vector;
    private void Start()
    {
        vector = transform.position;
    }

    void Update()
    {
        vector.Set(transform.position.x, transform.position.y + (moveSpeed + Time.deltaTime), transform.position.z);

        transform.position = vector;

        destroyTime -= Time.deltaTime;

        if (destroyTime <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
