using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField]
    private int stage = 1;
    private int curWave = 1;

    private MonsterDatabase monsterDB;
    private WaveDatabase waveDB;

    void Start()
    {
        monsterDB = DataTableLoader.LoadMonsterData("MonsterTable");
        waveDB = DataTableLoader.LoadWaveData("WaveDataTable");
    }

    void RandomSpawn()
    {
        WaveData data = waveDB.WaveDatas[curWave - 1];
    }
}
