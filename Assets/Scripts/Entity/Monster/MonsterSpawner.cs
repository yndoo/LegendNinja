using PublicDefinitions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    private MonsterDatabase monsterDB;
    private WaveDatabase waveDB;
    private WaveManager waveManager;

    void Start()
    {
        monsterDB = DataTableLoader.LoadMonsterData("MonsterTable");
        waveDB = DataTableLoader.LoadWaveData("WaveDataTable");
        waveManager = WaveManager.instance;

        WaveSpawn();
    }
    /// <summary>
    /// 현재 wave에 맞는 몬스터를 랜덤 소환.
    /// </summary>
    void WaveSpawn()
    {
        WaveData waveData = waveDB.WaveDatas[waveManager.CurrentWave - 1]; // 현재 웨이브 데이터
        Spawn(monsterDB.Small[0]); // 101몬스터 테스트용 코드
        Spawn(monsterDB.Small[1]); // 102몬스터 테스트용 코드
        Spawn(monsterDB.Small[2]); // 103몬스터 테스트용 코드
        Spawn(monsterDB.Boss[0]); // 301몬스터 테스트용 코드

        //// 소형 몬스터 랜덤 뽑기
        //List<MonsterData> small = monsterDB.Small.OrderBy(x => Random.value).Take(waveData.smallType).ToList(); // smallType개 종류 뽑기   
        //for(int i = 0; i < waveData.smallCount; i++)    // 그 중 smallCount마리 소환
        //{
        //    int idx = Random.Range(0, waveData.smallType);
        //    //Spawn(small[idx]); 

        //    Debug.Log($"소형 소환 | 인덱스 : {idx}, id : {small[idx].id}");
        //}

        //// 중형 몬스터 랜덤 뽑기
        //List<MonsterData> mid = monsterDB.Medium.OrderBy(x => Random.value).Take(waveData.mediumType).ToList(); // {mediumType} 종류 뽑기   
        //for (int i = 0; i < waveData.mediumCount; i++)  // 그 중 mediumCount마리 소환
        //{
        //    int idx = Random.Range(0, waveData.mediumType);
        //    Spawn(mid[idx]);

        //    Debug.Log($"중형 소환 | 인덱스 : {idx}, id : {mid[idx].id}");
        //}

        //// 보스 뽑기 
        //if (waveData.bossType > 0)
        //{
        //    MonsterData boss = monsterDB.Boss.OrderBy(x => Random.value).Take(waveData.bossType).First<MonsterData>();
        //    Spawn(boss);

        //    Debug.Log($"보스 소환 | id : {boss.id}");
        //}
    }

    void Spawn(MonsterData data)
    {
        GameObject go = Resources.Load<GameObject>($"Prefab/Monster/{data.id}");
        if (go == null) return;

        Vector3 randomPos = waveManager.GetRandomPosition();
        while (!waveManager.IsPositionOccupied(randomPos))
        {
            randomPos = waveManager.GetRandomPosition();
        }

        if (data.type == EAttackType.Melee)
        { 
            Instantiate(go, randomPos, Quaternion.identity).AddComponent<MeleeMonster>().InitMonster(data); 
        }
        else if (data.type == EAttackType.Ranged)
        {
            Instantiate(go, randomPos, Quaternion.identity).AddComponent<RangedMonster>().InitMonster(data);
        }
        else if (data.id >= 300)
        {
            Instantiate(go, randomPos, Quaternion.identity).AddComponent<BossMonster>().InitMonster(data);
        }

        waveManager.AliveEnemyCount++;
    }
}
