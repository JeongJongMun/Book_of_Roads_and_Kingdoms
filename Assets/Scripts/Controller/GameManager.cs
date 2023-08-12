using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    //public float health;
    public bool isLive;
    public int stage;

    [Header("경험치 바")]
    public Slider exp_slider;

    [Header("스킬 선택 창")]
    public GameObject skillSelectPanel;

    [Header("스킬들 부모")]
    public GameObject skillParent;

    [Header("설정 패널")]
    public GameObject settingPanel;

    [Header("설정창 스킬 목록")]
    public GameObject[] skillList;

    [Header("보스전")]
    public bool isBossPhase;
    public float surviveTime;
    public float restTime;

    public TMP_Text timerText;

    [Header("오브젝트 수집")]
    public GameObject[] items; //stage 0 몬스터 드랍템
    public int questItem; //stage 0 몬스터 드랍템
    public bool isShowText;
    public GameObject map;
    public GameObject textPanel;
    public int itemNum;//stage 1 수집템

    [Header("카바회전")]
    public int rotateNum;//stage 1 회전수
    public bool[] checkWayPoints;

    [Header("퀘스트 판넬")]
    public GameObject questPanel;

    [Header("Lose 판넬")]
    public GameObject losePanel;


    [Header("현재 스킬 레벨")]
    public Dictionary<Define.Skills, int> skillLevel = new Dictionary<Define.Skills, int>()
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

    [Header("보유 스킬")]
    public List<SkillController> skillControllers = new List<SkillController>();

    [Header("스킬 레벨 정보")]
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
        // 게임 시작 시 스킬 하나 고름
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

    public void ReStart() //LosePanel에 넣을거
    {
        SceneManager.LoadScene(0);
    }

    public void LevelUpEvent()
    {
        // 스킬 선택창 보여주기
        skillSelectPanel.SetActive(true);
        // 랜덤 스킬 3개 보여주기
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
        Debug.LogFormat("{0} Level {1} -> {2}", weaponName.ToString(), skillLevel[weaponName], skillLevel[weaponName]+1);

        // 스킬 레벨 업
        skillLevel[weaponName]++;

        if (skillLevel[weaponName] == 1)
        {
            GameObject _skill = Resources.Load<GameObject>("Skills/" + weaponName.ToString());
            // 스킬 생성
            Instantiate(_skill, skillParent.transform);
            // 스킬을 보유 스킬에 추가
            if (!skillControllers.Contains(_skill.GetComponent<SkillController>())) 
            {
                skillControllers.Add(_skill.GetComponent<SkillController>());
            }
            // 스킬을 설정창에 표시
            foreach (GameObject skill in skillList)
            {
                if (!skill.activeSelf)
                {
                    skill.SetActive(true);
                    skill.transform.GetChild(0).GetComponent<TMP_Text>().text = weaponName.ToString() + "\n" + "Lv " + skillLevel[weaponName];
                    break;
                }
            }
        }
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
            questPanel.transform.GetChild(0).transform.GetComponent<TMP_Text>().text = "몬스터를 잡아 유물조각을 수집하라! (" + questItem + "/3";
        }
        else if (stage == 1)
        {
            questPanel.transform.GetChild(0).transform.GetComponent<TMP_Text>().text = "카바 주변을 3바퀴 돌아라! (" + rotateNum + "/3";
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
                questPanel.transform.GetChild(0).transform.GetComponent<TMP_Text>().text = "맵을 탐색하여 유물조각을 수집하라! (" + itemNum + "/3";
            }
        }
    }


    void RotateCheck() //2페이즈퀘스트
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
    public void NextStage() //버튼
    {
        SceneManager.LoadScene(stage+1);
    }

    // 설정 버튼
    public void OnClickSettingBtn()
    {
        settingPanel.SetActive(!settingPanel.activeSelf);
        if (Time.timeScale == 0)
            Resume();
        else Stop();
    }
}
