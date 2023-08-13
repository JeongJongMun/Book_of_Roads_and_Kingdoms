using TMPro;
using UnityEditor;
using UnityEngine;

public class QuestController : MonoBehaviour
{
    // �������� 1 (��ī) ����Ʈ
    [Header("����Ʈ 1: 100ų")]
    public int goalKill = 100;

    [Header("����Ʈ 2: 150�� ����")]
    public float goalTime = 150.0f;

    [Header("����Ʈ 3: �ڶ� 3����")]
    public int goalLevel = 3;

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
        if (goalKill < 50)
        {
            text1.text = "���� óġ: "+ (100 - goalKill) + "/100";
        }
        
        if (goalTime < 90)
        {
            text2.text = "���� ���� �ð�: " + (150 - (int)goalTime).ToString() + "/150";
        }
        if (goalLevel < 2)
        {
            text3.text = "�ڶ� ����: " + (3 - goalLevel) + "/3";
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
