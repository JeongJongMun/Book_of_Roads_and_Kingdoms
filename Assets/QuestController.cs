using TMPro;
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


    public void CheckSuccess()
    {
        if (goalKill < 400)
        {
            text1.text = goalKill + "/500";
        }
    }
}
