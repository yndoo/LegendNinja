using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField]
    private int stage = 1;
    private int curWave = 5; // 현재 wave 값. 나중에 매니저에서 가져오는 wave값으로 대체될 수 있음.

    private MonsterDatabase monsterDB;
    private WaveDatabase waveDB;

    void Start()
    {
        monsterDB = DataTableLoader.LoadMonsterData("MonsterTable");
        waveDB = DataTableLoader.LoadWaveData("WaveDataTable");

        RandomMonsterSpawn();
    }
    /// <summary>
    /// 현재 wave에 맞는 몬스터를 랜덤 소환.
    /// </summary>
    void RandomMonsterSpawn()
    {
        WaveData waveData = waveDB.WaveDatas[curWave - 1]; // 현재 웨이브 데이터

        // 소형 몬스터 랜덤 뽑기
        List<MonsterData> small = monsterDB.Small.OrderBy(x => Random.value).Take(waveData.smallType).ToList(); // smallType개 종류 뽑기   
        // 그 중 smallCount마리 소환
        for(int i = 0; i < waveData.smallCount; i++)
        {
            int idx = Random.Range(0, waveData.smallType);
            GameObject go = Resources.Load<GameObject>($"Prefab/Monster/{small[idx].id}");
            if (go != null) Instantiate(go);

            Debug.Log($"소형 소환 | 인덱스 : {idx}, id : {small[idx].id}");
        }

        // 중형 몬스터 랜덤 뽑기
        List<MonsterData> mid = monsterDB.Medium.OrderBy(x => Random.value).Take(waveData.mediumType).ToList(); // {mediumType} 종류 뽑기   
        // 그 중 mediumCount마리 소환
        for (int i = 0; i < waveData.mediumCount; i++)
        {
            int idx = Random.Range(0, waveData.mediumType);
            GameObject go = Resources.Load<GameObject>($"Prefab/Monster/{mid[idx].id}");
            if (go != null) Instantiate(go);

            Debug.Log($"중형 소환 | 인덱스 : {idx}, id : {mid[idx].id}");
        }

        if (waveData.bossType > 0)
        {
            // 보스 뽑기 
            MonsterData boss = monsterDB.Boss.OrderBy(x => Random.value).Take(waveData.bossType).First<MonsterData>();
            GameObject go = Resources.Load<GameObject>($"Prefab/Monster/{boss.id}");
            if (go != null) Instantiate(go);
            Debug.Log($"보스 소환 | id : {boss.id}");
        }
    }
}
