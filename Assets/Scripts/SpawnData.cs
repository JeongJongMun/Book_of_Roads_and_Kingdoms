using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnData", menuName = "Scriptble Object/SpawnData")]

public class SpawnData : ScriptableObject
{
    public int id;
    public int maxHp;
    public int hp;
    public int damage;
    public int exp;
    public float speed;
    public float delay;
    public int spawnTransNum;
}
