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

    public bool isLive;
    public int stage;

    [Header("����ġ ��")]
    public Slider exp_slider;

    [Header("��ų ���� â")]
    public GameObject skillSelectPanel;

    [Header("��ų�� �θ�")]
    public GameObject skillParent;

    [Header("���� �г�")]
    public GameObject pausePanel;

    [Header("���� �г�")]
    public GameObject settingPanel;

    [Header("�Ͻ�����â ��ų ���")]
    public GameObject[] skillList;

    [Header("������")]
    public bool isBossPhase;
    public float surviveTime;
    public float restTime;

    [Header("Ŭ���� �� ������ ����")]
    public GameObject map;


    [Header("����Ʈ �ǳ�")]
    public GameObject questPanel;

    [Header("Lose �ǳ�")]
    //public GameObject losePanel;


    [Header("���� ��ų ����")]
    public Dictionary<Define.Skills, int> skillLevel = new Dictionary<Define.Skills, int>()
    {
        {Define.Skills.Bone, 0},
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

    [Header("��ų ���� ����")]
    public WeaponStats weaponStats;

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
        //ShowQuestText();
        if(isBossPhase)
        {
            questPanel.SetActive(false);
        }
    }

    //void LateUpdate()
    //{
    //    timerText.gameObject.SetActive(isBossPhase);
    //    if(isBossPhase)
    //        surviveTime -= Time.deltaTime;
    //    timerText.text = string.Format("{0:D2}:{1:D2}", (int)surviveTime / 60, (int)surviveTime % 60);
    //    // 290   04:50
    //    if (surviveTime < 0)
    //    {
    //        isBossPhase = false;
    //        GameOver(true);
    //    }

    //}
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
        if (isWin)
        {
            //ShowMap();
        }
    }

    public void ReStart() //LosePanel�� ������
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.UiButton);
        SceneManager.LoadScene(0);
    }

    public void LevelUpEvent()
    {
        // ��ų ����â �����ֱ�
        skillSelectPanel.SetActive(true);

        AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp);

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
        // ��ų ���� ��
        skillLevel[weaponName]++;
        AudioManager.instance.PlaySfx(AudioManager.Sfx.UiButton);
        if (skillLevel[weaponName] == 1)
        {
            GameObject _skill = Resources.Load<GameObject>("Skills/" + weaponName.ToString());
            // ��ų ����
            Instantiate(_skill, skillParent.transform);
        }
        // �̹� ���� ���̶�� ������
        else
        {
            foreach (SkillController skill in skillParent.GetComponentsInChildren<SkillController>())
            {
                if (skill.weaponType == weaponName)
                {
                    skill._level++;
                    skill.WeaponLevelUp();
                }
            }
        }

        // ��ų�� �Ͻ����� â�� ǥ��
        SetSkillInPause(weaponName);
    }
    public void SetSkillInPause(Define.Skills weaponName)
    {
        string name = "";

        switch (weaponName)
        {
            case Define.Skills.Bone:
                name = "���ٱ�";
                break;
            case Define.Skills.Candle:
                name = "�г�";
                break;
            case Define.Skills.Koran:
                name = "�ڶ�";
                break;
            case Define.Skills.Wand:
                name = "ī�μ��콺�� ������";
                break;
            case Define.Skills.Cat:
                name = "�����";
                break;
            case Define.Skills.Shortbow:
                name = "�ܱ�";
                break;
            case Define.Skills.Samshir:
                name = "�ｬ��";
                break;
            case Define.Skills.Quiver:
                name = "ȭ����";
                break;
            case Define.Skills.Damascus:
                name = "�ٸ���Ŀ��";
                break;
            case Define.Skills.Water:
                name = "��";
                break;
            case Define.Skills.Gold:
                name = "��";
                break;
            case Define.Skills.Shield:
                name = "����";
                break;
            case Define.Skills.Armor:
                name = "����";
                break;
            default:
                break;
        }
        foreach (GameObject skill in skillList)
        {
            if (skill.name == weaponName.ToString())
            {
                skill.transform.GetChild(0).GetComponent<TMP_Text>().text = name + "\n" + "Lv " + skillLevel[weaponName];
                break;
            }
        }
    }

<<<<<<< Updated upstream
=======
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

    public void ShowMap()
    {
        if (itemNum == 3 || questItem >= 3)
        {
            //isBossPhase = true;
            map.SetActive(true);
        }
    }
>>>>>>> Stashed changes
    public void NextStage() //��ư
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.UiButton);
        SceneManager.LoadScene(stage+1);
    }

    // ���� ��ư
    public void OnClickPauseBtn()
    {
        pausePanel.SetActive(!pausePanel.activeSelf);
        if (Time.timeScale == 0)
            Resume();
        else Stop();
    }

    // ���� ��ư
    public void OnClickSettingBtn()
    {
        settingPanel.SetActive(!settingPanel.activeSelf);
    }
    // ������ ��ư
    public void OnClickExitBtn()
    {
        SceneManager.LoadScene("Main");
    }
}
