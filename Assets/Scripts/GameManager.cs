using System.Collections.Generic;
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

    public Dictionary<string, int> weaponLevel = new Dictionary<string, int>()
    {
        {"Fireball", 0},
        {"Born", 0}
    };

    private void Awake()
    {
        Instance = this;
    }

    public void WeaponLevel(string weaponName)
    {
        weaponLevel[weaponName]++;
        //가지고 있는 무기도 레벨업
        
    }

    public void Stop()
    {
        isLive = false;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;
    }
}
