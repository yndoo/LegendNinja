using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class StatData
{
    public float MaxHealth;
    public float AttackPower;
    public float Defense;
    public float MoveSpeed;
    public float AttackRange;
    public float AttackSpeed;
    public float AttackTime;
}

public class MonsterData
{
    public int id;
    public string name;
    public StatData stats;
}

public class MonsterDatabase
{
    public List<MonsterData> Small;
    public List<MonsterData> Medium;
    public MonsterData Boss;
}

public static class DataTableLoader
{
    /// <summary>
    /// 스테이지별 몬스터 데이터를 로드하는 함수
    /// </summary>
    /// <param name="jsonFileName">읽어올 json 파일명</param>
    /// <returns>정해진 형식대로 데이터를 반환</returns>
    public static MonsterDatabase LoadMonsterData(string jsonFileName)
    {
        TextAsset monsterJsonFile = Resources.Load<TextAsset>(jsonFileName);
        if (monsterJsonFile == null)
        {
            Debug.Log("json 파일이 null입니다.");
        }

        MonsterDatabase MonsterDB = JsonConvert.DeserializeObject<MonsterDatabase>(monsterJsonFile.text);

        //foreach (MonsterData monster in MonsterDB.Small)
        //{
        //    Debug.Log($"소형 몬스터: {monster.name}, 공격력: {monster.stats.AttackPower}");
        //}
        //foreach (MonsterData monster in MonsterDB.Medium)
        //{
        //    Debug.Log($"중형 몬스터: {monster.name}, 공격력: {monster.stats.AttackPower}");
        //}
        //Debug.Log($"보스 : {MonsterDB.Boss.name}");

        return MonsterDB;
    }
}
