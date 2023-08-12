using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    public int weaponID;
    public string weaponName;
    public List<Dictionary<string, int>> weaponLevelDatas = new List<Dictionary<string, int>>();

    public List<Dictionary<string, int>> Initialize(Define.Skills skillName)
    {
        switch (skillName)
        {
            case Define.Skills.Born:
                for (int i = 0; i < 5; i++)
                {
                    Dictionary<string, int> level = new Dictionary<string, int>() { { "damage", damage_born[i] }, { "countPerCreate", countPerCreate_born[i] }, };
                    weaponLevelDatas.Add(level);
                }
                return weaponLevelDatas;

            default:
                return null;
        }
    }

    private List<int> damage_born = new List<int> { 10, 10, 20, 30, 50 };
    private List<int> countPerCreate_born = new List<int> { 1, 2, 2, 3, 3 };

}