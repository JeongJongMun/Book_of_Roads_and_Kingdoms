using TMPro;
using UnityEditor;
using UnityEngine;

public class QuestController : MonoBehaviour
{
    // 스테이지 1 (메카) 퀘스트
    [Header("퀘스트 1: 500킬")]
    public int goalKill = 500;

    [Header("퀘스트 2: 300초 생존")]
    public float goalTime = 300.0f;

    [Header("퀘스트 3: 코란 5레벨")]
    public int goalLevel = 5;

    [Header("퀘스트 1 텍스트")]
    public TMP_Text text1;

    [Header("퀘스트 2 텍스트")]
    public TMP_Text text2;

    [Header("퀘스트 3 텍스트")]
    public TMP_Text text3;

    [Header("유물 떨구기 카운트")]
    public int dropcount = 0;

    [Header("유물 획득 카운트")]
    public int count = 0;

    [Header("퀘스트 성공 패널")]
    public GameObject successPanel;

    [Header("퀘스트 성공 boolean")]
    public bool isQuestOneClear = false;
    public bool isQuestTwoClear = false;
    public bool isQuestThreeClear = false;

    private void Update()
    {
        CheckSuccess();
    }


    public void CheckSuccess()
    {
        // 해금
        if (goalKill < 400)
        {
            text1.text = "몬스터 처치: "+ (500 - goalKill) + "/500";
        }
        
        if (goalTime < 180)
        {
            text2.text = "남은 생존 시간: " + (300 - (int)goalTime).ToString() + "/300";
        }
        if (goalLevel < 4)
        {
            text3.text = "코란 레벨: " + (5 - goalLevel) + "/5";
        }

        // 성공
        if (goalKill < 1 && !isQuestOneClear)
        {
            dropcount++;
            isQuestOneClear = true;
        }
        if (goalTime < 1 && !isQuestTwoClear)
        {
            dropcount++;
            isQuestTwoClear = true;
        }
        if (goalLevel < 1 && !isQuestThreeClear)
        {
            dropcount++;
            isQuestThreeClear = true;
        }
        if (count == 3)
        {
            Invoke("Success", 1.0f);
        }
    }

    public void Success()
    {
        successPanel.SetActive(true);
        GameManager.Instance.isWin = true;
    }
}
