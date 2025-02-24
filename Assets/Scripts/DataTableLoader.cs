using Newtonsoft.Json;
using PublicDefinitions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

#region 몬스터 데이터 틀
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
    public EAttackType type;
    public StatData stats;
}

public class MonsterDatabase
{
    public List<MonsterData> Small;
    public List<MonsterData> Medium;
    public MonsterData Boss;
}
#endregion

#region 웨이브 데이터 틀
public class WaveData
{ 
    public int wave;
    public int smallType;
    public int mediumType;
    public int smallCount;
    public int mediumCount;
    public int bossType;
}
public class WaveDatabase
{
    public List<WaveData> WaveDatas;
}
#endregion

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

        Debug.Log("MonsterTable 로드 완료");
        return MonsterDB;
    }

    public static WaveDatabase LoadWaveData(string jsonFileName)
    {
        TextAsset JsonFile = Resources.Load<TextAsset>(jsonFileName);
        if (JsonFile == null)
        {
            Debug.Log("json 파일이 null입니다.");
        }

        WaveDatabase WaveDB = JsonConvert.DeserializeObject<WaveDatabase>(JsonFile.text);

        Debug.Log("WaveDataTable 로드 완료");
        return WaveDB;
    }
}
