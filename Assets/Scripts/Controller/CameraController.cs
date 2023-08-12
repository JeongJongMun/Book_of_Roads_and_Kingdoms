using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;

    [SerializeField]
    Vector2 center;
    [SerializeField]
    Vector2 mapSize;

    [SerializeField]
    float height;
    [SerializeField]
    float width;

    [SerializeField]
    float smoothing = 0.2f;
    public float speed;
    float timer;

    private void Start()
    {
        Camera.main.orthographicSize = 3;
        height = 12;
        width = height * Screen.width / Screen.height;
    }

    private void Update()
    {
        timer += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        Vector3 targetPos = new Vector3(player.position.x, player.position.y, this.transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);
        LimitCameraArea();

        if (Camera.main.orthographicSize <= 12 && timer >= 1)
            Camera.main.orthographicSize += Time.fixedDeltaTime * speed;
    }

    void LimitCameraArea()
    {
        //transform.position = Vector3.Lerp(transform.position, player.position + transform.position, Time.deltaTime * speed);
        float lx = mapSize.x - width;
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);

        float ly = mapSize.y - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

        transform.position = new Vector3(clampX, clampY, -10f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, mapSize * 2);
    }
}
