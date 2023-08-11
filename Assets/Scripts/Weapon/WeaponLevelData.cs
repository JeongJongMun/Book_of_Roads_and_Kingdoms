using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class WeaponData
{
    public Define.Weapons weaponId;
    public string weaponName;
    public List<WeaponLevelData> weaponLevelData = new List<WeaponLevelData>();

    //public List<WeaponLevelData> GetWeaponLevelData(Define.Weapons weaponId)
    //{
    //    switch (weaponId)
    //    {
    //        case Define.Weapons.Poison:
    //            return 
    //    }
    //}
}
public class WeaponLevelData
{
    public int level;
    public int damage;
    public float moveSpeed;
    public float force;
    public float cooldown;
    public float size;
    public int penetrate;
    public int countPerCreate;

    //WeaponLevelData(Define.Weapons _weaponId)
    //{
    //    switch
    //}
}