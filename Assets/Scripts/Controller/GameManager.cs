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
    public float playTime = 0;

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

    [Header("��������")]
    public int stage = 0;

    //[Header("������")]
    public bool isBossPhase;
    //public float surviveTime;
    //public float restTime;


    [Header("����Ʈ �Ŵ���")]
    public GameObject questManager;

    [Header("�й� �ǳ�")]
    public GameObject losePanel;


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

    [Header("���� Ŭ���� Boolean")]
    public bool isWin = false;

    public static float GameTime { get; set; } = 0;
    public static bool gameStop = false;

    private void Awake()
    {
        Instance = this;

    }
    private void Update()
    {
        GameTime += Time.deltaTime;
        questManager.GetComponent<QuestController>().goalTime -= Time.deltaTime;
        SetExpAndLevel();
        //ShowQuestText();
        //if(isBossPhase)
        //{
        //    questManager.SetActive(false);
        //}
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
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Time.timeScale = 1;
    }
    public void GameOver()
    {
        losePanel.SetActive(true);
        Stop();
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
        if (weaponName == Define.Skills.Koran)
        {
            questManager.GetComponent<QuestController>().goalLevel--;
        }

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
            bool isCombine = CheckCombination();
            if (isCombine)
            {
                foreach (GameObject obj in skillParent.GetComponentsInChildren<GameObject>())
                {
                    if (obj.GetComponent<SkillController>().weaponType == Define.Skills.Koran)
                    {
                        Destroy(obj);
                    }
                    else if (obj.GetComponent<SkillController>().weaponType == Define.Skills.Wand)
                    {
                        Destroy(obj);
                    }
                }
                GameObject _skill = Resources.Load<GameObject>("Skills/Korand");
                // ��ų ����
                Instantiate(_skill, skillParent.transform);

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
            case Define.Skills.Korand:
                name = "�ڶ���";
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

    public bool CheckCombination()
    {
        bool isOne = false;
        foreach (SkillController skill in skillParent.GetComponentsInChildren<SkillController>()) 
        { 
            if (skill.weaponType == Define.Skills.Koran && skill._level >= 1)
            {
                if (isOne == true)
                {
                    return true;
                }
                isOne = true;
            }
            else if (skill.weaponType == Define.Skills.Wand && skill._level >= 1)
            {
                if (isOne == true)
                {
                    return true;
                }
                isOne = true;
            }
        }
        return false;
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
