using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("플레이어 객체")]
    public GameObject playerObject;

    [Header("Player Controller 스크립트")]
    public PlayerController player;

    [Header("Pool Manager")]
    public PoolManager poolManager;

    [Header("플레이 시간")]
    public int playTime;

    public float health;
    public bool isLive;
    public int stage;

    [Header("경험치 바")]
    public Slider exp_slider;

    [Header("스킬 선택 창")]
    public GameObject skillSelectPanel;

    public Dictionary<Define.Weapons, int> weaponLevel = new Dictionary<Define.Weapons, int>()
    {
        {Define.Weapons.Born, 0},
        {Define.Weapons.Candle , 0},
        {Define.Weapons.Koran , 0},
        {Define.Weapons.Poison , 0},
        {Define.Weapons.Cat , 0},
        {Define.Weapons.Camel , 0},
        {Define.Weapons.Shortbow , 0},
        {Define.Weapons.Damascus , 0},
        {Define.Weapons.Samshir , 0},
        {Define.Weapons.Water , 0},
        {Define.Weapons.Gold , 0},
        {Define.Weapons.Shield , 0},
        {Define.Weapons.Armor , 0},
    };

    public static float GameTime { get; set; } = 0;
    public static bool gameStop = false;

    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        GameTime += Time.deltaTime;
        SetExpAndLevel();
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
    public void GameOver()
    {
        Debug.Log("게임 오버");
    }

    public void LevelUpEvent()
    {
        // 스킬 선택창 보여주기
        skillSelectPanel.SetActive(true);
        // 랜덤 스킬 3개 보여주기
        skillSelectPanel.GetComponent<LevelUp>().RandomShow();
        Stop();
    }

    void SetExpAndLevel()
    {
        PlayerStat player = playerObject.GetComponent<PlayerStat>();
        double ratio = player.Exp / (double)player.MaxExp;
        if (ratio < 0)
            ratio = 0;
        else if (ratio > 1)
            ratio = 1;
        exp_slider.value = (float)ratio;
        //GetText((int)Texts.LevelText).text = player.Level.ToString();
    }
    public void GetOrSetSkill(Define.Weapons weaponName)
    {
        Debug.LogFormat("{0} Level {1} -> {2}", weaponName.ToString(), weaponLevel[weaponName], weaponLevel[weaponName]+1);
        weaponLevel[weaponName]++;
        if (weaponLevel[weaponName] == 1)
        {
            // 무기 소환
        }

    }
}
