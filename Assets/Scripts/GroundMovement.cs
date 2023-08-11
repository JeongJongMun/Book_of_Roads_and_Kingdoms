using UnityEngine;

public class GroundMovement : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;

        Vector3 playerPos = GameManager.Instance.player.transform.position;
        Vector3 myPos = transform.position;

        float diffX = Mathf.Abs(playerPos.x - myPos.x);
        float diffY = Mathf.Abs(playerPos.y - myPos.y);

        Vector3 _nextPos = GameManager.Instance.player.nextPos;

        float XPos = _nextPos.x < 0 ? -1 : 1;
        float YPos = _nextPos.y < 0 ? -1 : 1;

        switch (transform.tag)
        {
            case "Ground":
                if (diffX > diffY) {
                    transform.Translate(Vector2.right * XPos * 40);
                }
                else if (diffX < diffY)
                {
                    transform.Translate(Vector2.up * YPos * 40);
                }
                break;

        }

    }
}
