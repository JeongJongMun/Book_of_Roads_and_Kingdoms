using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    public int weaponID;
    public string weaponName;
    public List<Dictionary<string, int>> weaponLevelDatas = new List<Dictionary<string, int>>();

    public List<Dictionary<string, int>> StatInitialize(Define.Skills skillName)
    {
        weaponLevelDatas.Clear();
        switch (skillName)
        {
            case Define.Skills.Bone:
                for (int i = 0; i < 5; i++)
                {
                    Dictionary<string, int> level = new Dictionary<string, int>() { { "damage", damage_bone[i] }, { "countPerCreate", countPerCreate_born[i] }, };
                    weaponLevelDatas.Add(level);
                }
                return weaponLevelDatas;

            case Define.Skills.Candle:
                for (int i = 0; i < 5; i++)
                {
                    Dictionary<string, int> level = new Dictionary<string, int>() { { "damage", damage_candle[i] }, { "countPerCreate", countPerCreate_candle[i] }, };
                    weaponLevelDatas.Add(level);
                }
                return weaponLevelDatas;

            case Define.Skills.Cat:
                for (int i = 0; i < 5; i++)
                {
                    Dictionary<string, int> level = new Dictionary<string, int>() { { "damage", damage_cat[i] }, { "countPerCreate", countPerCreate_cat[i] }, };
                    weaponLevelDatas.Add(level);
                }
                return weaponLevelDatas;

            case Define.Skills.Shortbow:
                for (int i = 0; i < 5; i++)
                {
                    Dictionary<string, int> level = new Dictionary<string, int>() { { "damage", damage_shortbow[i] }, { "countPerCreate", countPerCreate_shortbow[i] }, };
                    weaponLevelDatas.Add(level);
                }
                return weaponLevelDatas;

            case Define.Skills.Koran:
                for (int i = 0; i < 5; i++)
                {
                    Dictionary<string, int> level = new Dictionary<string, int>() { 
                        { "damage", damage_koran[i] }, 
                        { "countPerCreate", countPerCreate_koran[i] },
                        { "size", size_koran[i] },
                    };
                    weaponLevelDatas.Add(level);
                }
                return weaponLevelDatas;

            case Define.Skills.Samshir:
                for (int i = 0; i < 5; i++)
                {
                    Dictionary<string, int> level = new Dictionary<string, int>() { { "damage", damage_samshir[i] }, { "countPerCreate", countPerCreate_samshir[i] }, };
                    weaponLevelDatas.Add(level);
                }
                return weaponLevelDatas;

            case Define.Skills.Wand:
                for (int i = 0; i < 5; i++)
                {
                    Dictionary<string, int> level = new Dictionary<string, int>() { { "damage", damage_wand[i] }, { "countPerCreate", countPerCreate_wand[i] }, };
                    weaponLevelDatas.Add(level);
                }
                return weaponLevelDatas;

            default:
                return null;
        }
    }

    private List<int> damage_bone = new List<int> { 10, 10, 20, 30, 50 };
    private List<int> countPerCreate_born = new List<int> { 1, 2, 2, 3, 3 };

    private List<int> damage_candle = new List<int> { 10, 10, 20, 30, 50 };
    private List<int> countPerCreate_candle = new List<int> { 1, 2, 2, 3, 3 };

    private List<int> damage_cat = new List<int> { 5, 5, 5, 5, 5 };
    private List<int> countPerCreate_cat = new List<int> { 3, 5, 7, 10, 15 };

    //코란은 탄막이 날아가는 방식이 아니라 주석달아놈
    private List<int> damage_koran = new List<int> { 1, 2, 2, 3, 4 };
    private List<int> countPerCreate_koran = new List<int> { 1, 2, 2, 3, 4 };
    private List<int> size_koran = new List<int> { 1, 1, 2, 3, 4 };

    private List<int> damage_shortbow = new List<int> { 10, 10, 10, 10, 10 };
    private List<int> countPerCreate_shortbow = new List<int> { 1, 2, 3, 4, 5 };

    //삼쉬르는 탄막이 아니라 주석달아놈
    private List<int> damage_samshir = new List<int> { 5, 10, 10, 15, 20 };
    private List<int> countPerCreate_samshir = new List<int> { 1, 2, 2, 3, 3 };

    private List<int> damage_wand = new List<int> { 5, 10, 10, 15, 20 };
    private List<int> countPerCreate_wand = new List<int> { 1, 2, 2, 3, 3 };

}