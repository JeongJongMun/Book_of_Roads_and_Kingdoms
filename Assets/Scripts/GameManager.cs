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
        //������ �ִ� ���⵵ ������
        
    }

    public void ShowText()
    {
        textPanel.SetActive(true);
        textPanel.transform.GetChild(0).transform.GetComponent<TMP_Text>().text = "������ " + itemNum + "�� ȹ���ߴ�.";
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
    public void NextStage() //��ư
    {
        SceneManager.LoadScene(2);
    }

}
