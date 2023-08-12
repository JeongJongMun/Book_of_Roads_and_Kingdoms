using TMPro;
using UnityEditor;
using UnityEngine;

public class QuestController : MonoBehaviour
{
    // �������� 1 (��ī) ����Ʈ
    [Header("����Ʈ 1: 500ų")]
    public int goalKill = 500;

    [Header("����Ʈ 2: 300�� ����")]
    public float goalTime = 300.0f;

    [Header("����Ʈ 3: �ڶ� 5����")]
    public int goalLevel = 5;

    [Header("����Ʈ 1 �ؽ�Ʈ")]
    public TMP_Text text1;

    [Header("����Ʈ 2 �ؽ�Ʈ")]
    public TMP_Text text2;

    [Header("����Ʈ 3 �ؽ�Ʈ")]
    public TMP_Text text3;

    [Header("���� ������ ī��Ʈ")]
    public int dropcount = 0;

    [Header("���� ȹ�� ī��Ʈ")]
    public int count = 0;

    [Header("����Ʈ ���� �г�")]
    public GameObject successPanel;

    [Header("����Ʈ ���� boolean")]
    public bool isQuestOneClear = false;
    public bool isQuestTwoClear = false;
    public bool isQuestThreeClear = false;

    private void Update()
    {
        CheckSuccess();
    }


    public void CheckSuccess()
    {
        // �ر�
        if (goalKill < 400)
        {
            text1.text = "���� óġ: "+ (500 - goalKill) + "/500";
        }
        
        if (goalTime < 180)
        {
            text2.text = "���� ���� �ð�: " + (300 - (int)goalTime).ToString() + "/300";
        }
        if (goalLevel < 4)
        {
            text3.text = "�ڶ� ����: " + (5 - goalLevel) + "/5";
        }

        // ����
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
