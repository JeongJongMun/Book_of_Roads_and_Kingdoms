using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject playerObject;
    public PlayerController player;
    public PoolManager poolManager;
    public int playTime;

    public float health;
    public bool isLive;
    public int stage;
    public int itemNum;
    public bool isShowText;

    public GameObject map;
    public GameObject textPanel;

    public Dictionary<string, int> weaponLevel = new Dictionary<string, int>()
    {
        {"Fireball", 0},
        {"Born", 0}
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
        ShowMap();
    }

    public void WeaponLevel(string weaponName)
    {
        weaponLevel[weaponName]++;
        //가지고 있는 무기도 레벨업
        
    }

    public void ShowText()
    {
        textPanel.SetActive(true);
        textPanel.transform.GetChild(0).transform.GetComponent<TMP_Text>().text = "조각을 " + itemNum + "개 획득했다.";
        isShowText = true;
        //Time.timeScale = 0;
    }

    public void HideText()
    {
        if(Input.GetButtonDown("Jump") && isShowText)
        {
            textPanel.SetActive(false);
            //Time.timeScale = 1;
            isShowText = false;
        }
    }



    public void ShowMap()
    {
        if (itemNum == 3)
        {
            map.SetActive(true);
        }
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
    public void NextStage() //버튼
    {
        SceneManager.LoadScene(2);
    }

}
