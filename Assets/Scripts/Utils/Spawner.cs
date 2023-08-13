using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;



public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnDatas;
    List<SpawnData> spawnDataList;

    float timer;
    float delay;

    public int spawnIndex;
    public bool spawnEnd;
    public bool isBossSpawned;
    void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
        spawnDataList = new List<SpawnData>();
    }
    void Start()
    {
        ReadSpawnFile(GameManager.Instance.stage);
        delay = spawnDataList[0].delay;

    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > spawnDataList[spawnIndex].delay)
        {
            timer = 0;
            Spawn();
        }
        if(GameManager.Instance.isBossPhase && !isBossSpawned)
        {
            GameObject enemy = GameManager.Instance.poolManager.Get(spawnDataList[spawnIndex].id);
            enemy.transform.localScale = Vector2.one * 0.25f;
            enemy.transform.position = spawnPoint[spawnDataList[spawnIndex].spawnTransNum].position;
            enemy.GetComponent<EnemyController>().isBoss = true;
            isBossSpawned = true;
        }
    }

    void Spawn()
    {
        GameObject enemy = GameManager.Instance.poolManager.Get(spawnDataList[spawnIndex].id);
        enemy.transform.position = spawnPoint[spawnDataList[spawnIndex].spawnTransNum].position;
        enemy.GetComponent<EnemyController>().Init(spawnDataList[spawnIndex]);

        spawnIndex++;
        if (spawnIndex == spawnDataList.Count)
        {
            spawnIndex = 0;
        }
        //다음 리스폰 딜레이 갱신
        if(GameManager.Instance.isBossPhase)
        {
            delay = spawnDataList[spawnIndex].delay * 0.5f;
        }
        else
        {
            delay = spawnDataList[spawnIndex].delay * 0.5f;
        }
    }

    public void ReadSpawnFile(int stage)
    {
        spawnDataList.Clear();
        spawnIndex = 0;

        TextAsset textFile = Resources.Load("Stage"+GameManager.Instance.stage) as TextAsset;
        StringReader stringReader = new StringReader(textFile.text);

        while (stringReader != null)
        {
            string line = stringReader.ReadLine();

            if (line == null)
                break;

            int id = int.Parse(line.Split(',')[0]);

            //리스폰 데이터 생성
            SpawnData spawnData = ScriptableObject.CreateInstance<SpawnData>();
            spawnData.id = int.Parse(line.Split(',')[0]);
            spawnData.maxHp = spawnDatas[id].maxHp;
            spawnData.hp = spawnDatas[id].hp;
            spawnData.damage = spawnDatas[id].damage;
            spawnData.exp = spawnDatas[id].exp;
            spawnData.speed = spawnDatas[id].speed;

            spawnData.delay = float.Parse(line.Split(',')[1]);
            spawnData.spawnTransNum = int.Parse(line.Split(',')[2]);

            spawnDataList.Add(spawnData);
        }

        //텍스트 파일 닫기
        stringReader.Close();
    }
}

