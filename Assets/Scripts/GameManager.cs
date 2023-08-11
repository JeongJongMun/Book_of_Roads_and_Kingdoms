using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Player player;
    public PoolManager poolManager;
    public int playTime;

    public float health;
    public bool isLive;
    public int stage;
    private void Awake()
    {
        Instance = this;
    }


}
