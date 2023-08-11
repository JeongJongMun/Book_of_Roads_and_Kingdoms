using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
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
            Debug.Log(ran[i]);

            skills[ran[i]].SetActive(true);
        }
    }

    // ��ų ���ý� ��ų ȹ��OR������
    public void OnClickSkill(GameObject skill)
    {
        Define.Weapons weaponName = (Define.Weapons)Enum.Parse(typeof(Define.Weapons), skill.transform.GetChild(1).name);
        GameManager.Instance.GetOrSetSkill(weaponName);
        GameManager.Instance.Resume();
        this.gameObject.SetActive(false);
    }
}
