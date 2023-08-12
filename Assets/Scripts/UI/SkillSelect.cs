using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillSelect : MonoBehaviour
{
    [SerializeField]
    public GameObject[] skills;

    public int maxLevelNum;
    public void RandomShow()
    {
        for (int i = 0; i < skills.Length; i++)
        {
            skills[i].SetActive(false);
        }
        int[] ran = new int[3];
        HashSet<int> usedIndices = new HashSet<int>(); // �̹� ����� �ε����� �����ϴ� HashSet

        for (int i = 0; i < ran.Length; i++)
        {
            int randomIndex;
            do
            {
                randomIndex = UnityEngine.Random.Range(0, skills.Length); // ���� ����
            }
            while (usedIndices.Contains(randomIndex)); // �̹� ����� �ε������ �ٽ� ����

            ran[i] = randomIndex; // ������ �迭�� �Ҵ�
            usedIndices.Add(randomIndex); // ����� �ε����� ǥ��
        }

        for (int i = 0; i < ran.Length; i++)
        {
            skills[ran[i]].SetActive(true);
        }
    }

    // ��ų ���ý� ��ų ȹ��OR������
    public void OnClickSkill(GameObject skill)
    {
        Define.Skills weaponName = (Define.Skills)Enum.Parse(typeof(Define.Skills), skill.name);
        int _level = skill.transform.GetChild(2).GetComponent<TMP_Text>().text[3] - '0';
        string temp = skill.transform.GetChild(2).GetComponent<TMP_Text>().text;
        _level++;

        skill.transform.GetChild(2).GetComponent<TMP_Text>().text = temp.Substring(0, 3) + _level;
        GameManager.Instance.GetOrSetSkill(weaponName);
        GameManager.Instance.Resume();
        gameObject.SetActive(false);
    }
}
