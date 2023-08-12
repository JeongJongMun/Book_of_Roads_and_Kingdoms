using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;

    [SerializeField] 
    float smoothing = 0.2f;
    public float speed;
    float timer;
    private void Start()
    {
        Camera.main.orthographicSize = 3;
    }
    private void Update()
    {
        timer += Time.deltaTime;
    }
    private void FixedUpdate()
    {
        Vector3 targetPos = new Vector3(player.position.x, player.position.y, this.transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);

        if (Camera.main.orthographicSize <= 15 && timer >= 2)
            Camera.main.orthographicSize += Time.fixedDeltaTime * speed;


    }
}
