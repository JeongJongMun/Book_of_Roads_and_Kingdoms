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

    public bool isLive;
    public int stage;

    [Header("경험치 바")]
    public Slider exp_slider;

    [Header("스킬 선택 창")]
    public GameObject skillSelectPanel;

    [Header("스킬들 부모")]
    public GameObject skillParent;

    [Header("중지 패널")]
    public GameObject pausePanel;

    [Header("설정 패널")]
    public GameObject settingPanel;

    [Header("일시정지창 스킬 목록")]
    public GameObject[] skillList;

    [Header("보스전")]
    public bool isBossPhase;
    public float surviveTime;
    public float restTime;

    [Header("클리어 시 나오는 지도")]
    public GameObject map;


    [Header("퀘스트 판넬")]
    public GameObject questPanel;

    [Header("Lose 판넬")]
    //public GameObject losePanel;


    [Header("현재 스킬 레벨")]
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
        if (isWin)
        {
            //ShowMap();
        }
    }

    public void ReStart() //LosePanel에 넣을거
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.UiButton);
        SceneManager.LoadScene(0);
    }

    public void LevelUpEvent()
    {
        // 스킬 선택창 보여주기
        skillSelectPanel.SetActive(true);

        AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp);

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
        // 스킬 레벨 업
        skillLevel[weaponName]++;
        AudioManager.instance.PlaySfx(AudioManager.Sfx.UiButton);
        if (skillLevel[weaponName] == 1)
        {
            GameObject _skill = Resources.Load<GameObject>("Skills/" + weaponName.ToString());
            // 스킬 생성
            Instantiate(_skill, skillParent.transform);
        }
        // 이미 보유 중이라면 레벨업
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

        // 스킬을 일시정지 창에 표시
        SetSkillInPause(weaponName);
    }
    public void SetSkillInPause(Define.Skills weaponName)
    {
        string name = "";

        switch (weaponName)
        {
            case Define.Skills.Bone:
                name = "뼈다귀";
                break;
            case Define.Skills.Candle:
                name = "촛농";
                break;
            case Define.Skills.Koran:
                name = "코란";
                break;
            case Define.Skills.Wand:
                name = "카두세우스의 지팡이";
                break;
            case Define.Skills.Cat:
                name = "고양이";
                break;
            case Define.Skills.Shortbow:
                name = "단궁";
                break;
            case Define.Skills.Samshir:
                name = "삼쉬르";
                break;
            case Define.Skills.Quiver:
                name = "화살통";
                break;
            case Define.Skills.Damascus:
                name = "다마스커스";
                break;
            case Define.Skills.Water:
                name = "물";
                break;
            case Define.Skills.Gold:
                name = "금";
                break;
            case Define.Skills.Shield:
                name = "방패";
                break;
            case Define.Skills.Armor:
                name = "갑옷";
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

    public void ShowMap()
    {
        if (itemNum == 3 || questItem >= 3)
        {
            //isBossPhase = true;
            map.SetActive(true);
        }
    }
>>>>>>> Stashed changes
    public void NextStage() //버튼
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.UiButton);
        SceneManager.LoadScene(stage+1);
    }

    // 중지 버튼
    public void OnClickPauseBtn()
    {
        pausePanel.SetActive(!pausePanel.activeSelf);
        if (Time.timeScale == 0)
            Resume();
        else Stop();
    }

    // 설정 버튼
    public void OnClickSettingBtn()
    {
        settingPanel.SetActive(!settingPanel.activeSelf);
    }
    // 나가기 버튼
    public void OnClickExitBtn()
    {
        SceneManager.LoadScene("Main");
    }
}
