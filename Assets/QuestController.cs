using TMPro;
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


    public void CheckSuccess()
    {
        if (goalKill < 400)
        {
            text1.text = goalKill + "/500";
        }
    }
}
