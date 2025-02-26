using UnityEngine;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour
{
    public int totalWaves = 5; // 총 웨이브 수

    [Header("스폰 스크립트 연결")]
    public MonsterSpawner monsterSpawner; //몬스터 랜덤 스폰 스크립트
    public ObstacleSpawner obstacleSpawner; //장애물 랜덤 생성 스크립트
    public WavePortal wavePortal; //포탈 스크립트 (다음 웨이브 시작 트리거)

    private int currentWave = 0;
    private bool waveCleared = false;

    void Start()
    {
        StartNextWave();
    }

    void StartNextWave()
    {
        if (currentWave >= totalWaves)
        {         
            return;
        }

        currentWave++;
        waveCleared = false; // 새 웨이브 시작

        int obstacleCount = Mathf.Clamp(3 + (currentWave - 1) * 2, 3, 9); //웨이브마다 장애물 증가
        //몬스터 수 증가       

        obstacleSpawner.ClearObstacles(); //기존 장애물 제거
        obstacleSpawner.SpawnObstacles(obstacleCount); //새로운 장애물 생성     
    }

    public void CheckWaveClear()
    {
        if (monsterSpawner.GetAliveEnemyCount() == 0) //남은 적이 없으면 웨이브 클리어
        {
            Debug.Log($"웨이브 {currentWave} 클리어! 포탈로 이동하세요.");
            waveCleared = true;
            wavePortal.ActivatePortal(); //포탈 활성화
        }
    }

    public void TryStartNextWave()
    {
        if (waveCleared)
        {
            StartNextWave();
        }
    }
}
