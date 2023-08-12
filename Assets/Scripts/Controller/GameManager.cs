using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("�÷��̾� ��ü")]
    public GameObject playerObject;

    [Header("Player Controller ��ũ��Ʈ")]
    public PlayerController player;

    [Header("Pool Manager")]
    public PoolManager poolManager;

    [Header("�÷��� �ð�")]
    public int playTime;

    //public float health;
    public bool isLive;
    public int stage;

    [Header("����ġ ��")]
    public Slider exp_slider;

    [Header("��ų ���� â")]
    public GameObject skillSelectPanel;

    [Header("��ų�� �θ�")]
    public GameObject skillParent;

    [Header("���� �г�")]
    public GameObject settingPanel;

    [Header("����â ��ų ���")]
    public GameObject[] skillList;

    [Header("������")]
    public bool isBossPhase;
    public float surviveTime;
    public float restTime;

    public TMP_Text timerText;

    [Header("������Ʈ ����")]
    public GameObject[] items; //stage 0 ���� �����
    public int questItem; //stage 0 ���� �����
    public bool isShowText;
    public GameObject map;
    public GameObject textPanel;
    public int itemNum;//stage 1 ������

    [Header("ī��ȸ��")]
    public int rotateNum;//stage 1 ȸ����
    public bool[] checkWayPoints;

    [Header("����Ʈ �ǳ�")]
    public GameObject questPanel;

    [Header("Lose �ǳ�")]
    public GameObject losePanel;


    public Dictionary<Define.Skills, int> weaponLevel = new Dictionary<Define.Skills, int>()
    {
        {Define.Skills.Born, 0},
        {Define.Skills.Candle , 0},
        {Define.Skills.Koran , 0},
        {Define.Skills.Wand , 0},
        {Define.Skills.Cat , 0},
        {Define.Skills.Camel , 0},
        {Define.Skills.Shortbow , 0},
        {Define.Skills.Damascus , 0},
        {Define.Skills.Samshir , 0},
        {Define.Skills.Water , 0},
        {Define.Skills.Gold , 0},
        {Define.Skills.Shield , 0},
        {Define.Skills.Armor , 0},
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
        RotateCheck();
        ShowQuestText();
        if(isBossPhase)
        {
            questPanel.SetActive(false);
        }
        if(SceneManager.GetActiveScene().buildIndex == 1)
        if (checkWayPoints[3])
        {
            rotateNum++;
            for(int i = 0; i < 4; i++)
            {
                checkWayPoints[i] = false;
            }
        }
    }

    void LateUpdate()
    {
        timerText.gameObject.SetActive(isBossPhase);
        if(isBossPhase)
            surviveTime -= Time.deltaTime;
        timerText.text = string.Format("{0:D2}:{1:D2}", (int)surviveTime / 60, (int)surviveTime % 60);
        // 290   04:50
        if (surviveTime < 0)
        {
            isBossPhase = false;
            GameOver(true);
        }

    }
    private void Start()
    {
        // ���� ���� �� ��ų �ϳ� ��
        LevelUpEvent();
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
    public void GameOver(bool isWin)
    {
        if(isWin)
        {
            ShowMap();
        }
        else
        {
            losePanel.SetActive(true);
        }
    }

    public void ReStart() //LosePanel�� ������
    {
        SceneManager.LoadScene(0);
    }

    public void LevelUpEvent()
    {
        // ��ų ����â �����ֱ�
        skillSelectPanel.SetActive(true);
        // ���� ��ų 3�� �����ֱ�
        skillSelectPanel.GetComponent<SkillSelect>().RandomShow();
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
    public void GetOrSetSkill(Define.Skills weaponName)
    {
        Debug.LogFormat("{0} Level {1} -> {2}", weaponName.ToString(), weaponLevel[weaponName], weaponLevel[weaponName]+1);
        weaponLevel[weaponName]++;
        if (weaponLevel[weaponName] == 1)
        {
            GameObject _skill = Resources.Load<GameObject>("Skills/" + weaponName.ToString());
            // ��ų ����
            Instantiate(_skill, skillParent.transform);
            // ��ų�� ����â�� ǥ��
            foreach (GameObject skill in skillList)
            {
                if (!skill.activeSelf)
                {
                    skill.SetActive(true);
                    skill.transform.GetChild(0).GetComponent<TMP_Text>().text = weaponName.ToString() + "\n" + "Lv " + weaponLevel[weaponName];
                    break;
                }
            }
        }

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
        if (Input.GetButtonDown("Jump") && isShowText)
        {
            textPanel.SetActive(false);
            //Time.timeScale = 1;
            isShowText = false;
        }
    }

    public void ShowQuestText()
    {
        if (stage == 0)
        {
            questPanel.transform.GetChild(0).transform.GetComponent<TMP_Text>().text = "���͸� ��� ���������� �����϶�! (" + questItem + "/3";
        }
        else if (stage == 1)
        {
            questPanel.transform.GetChild(0).transform.GetComponent<TMP_Text>().text = "ī�� �ֺ��� 3���� ���ƶ�! (" + rotateNum + "/3";
            if(rotateNum == 3)
            {
                foreach(GameObject item in items)
                {
                    try
                    {
                        item.SetActive(true);
                    }
                    catch
                    {

                    }
                }
                questPanel.transform.GetChild(0).transform.GetComponent<TMP_Text>().text = "���� Ž���Ͽ� ���������� �����϶�! (" + itemNum + "/3";
            }
        }
    }


    void RotateCheck() //2����������Ʈ
    {
        
    }
    public void ShowMap()
    {
        if (itemNum == 3 || questItem >= 3)
        {
            //isBossPhase = true;
            map.SetActive(true);
        }
    }
    public void NextStage() //��ư
    {
        SceneManager.LoadScene(stage+1);
    }

    // ���� ��ư
    public void OnClickSettingBtn()
    {
        settingPanel.SetActive(!settingPanel.activeSelf);
        if (Time.timeScale == 0)
            Resume();
        else Stop();
    }
}
