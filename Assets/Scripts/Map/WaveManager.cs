using UnityEngine;
using System.Collections.Generic;
using UnityEditorInternal;
using System.Runtime.CompilerServices;

public class WaveManager : MonoBehaviour
{

    public static WaveManager instance { get; private set; }
    public int totalWaves = 5; // 총 웨이브 수

    [Header("스폰 스크립트 연결")]
    public MonsterSpawner monsterSpawner; //몬스터 랜덤 스폰 스크립트
    public ObstacleSpawner obstacleSpawner; //장애물 랜덤 생성 스크립트
    public WavePortal wavePortal; //포탈 스크립트 (다음 웨이브 시작 트리거)
    

    public int AliveEnemyCount {  get; set; }

    private int currentWave = 0;
    private bool waveCleared = false;
    public Vector2 mapSize = new Vector2(10, 10); //맵크기


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        StartNextWave();
    }

    public void StartNextWave()
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

        List<Vector2> spawnedPosition = new List<Vector2>(); //생성된 위치 리스트

        for (int i = 0; i < obstacleCount; i++)
        {
            Vector2 randomPosition = GetRandomPosition();
            while (IsPositionOccupied(randomPosition,spawnedPosition))
            {
                randomPosition = GetRandomPosition();
            }
           
            int randomIndex = Random.Range(0, obstacleSpawner.obstaclePrefabs.Length);
            obstacleSpawner.SpawnObstacles(randomPosition, randomIndex);

            spawnedPosition.Add(randomPosition);
        }
        
    }

    //랜덤위치생성
    private Vector2 GetRandomPosition()
    {
        float x = Random.Range(-mapSize.x / 2 ,mapSize.y / 2);
        float y = Random.Range(-mapSize.y /2 ,mapSize.x / 2);
        return new Vector2(x, y);
    }

    //장애물 위치 겹치는지 확인
    private bool IsPositionOccupied(Vector2 position,List<Vector2> spawnedPosition)
    {
        foreach (Vector2 spawnedPosiotion in spawnedPosition)
        {
            if (Vector2.Distance(position, spawnedPosiotion) < 1f)
            {
                return true;
            }
        }

        return false;
    }

    public void CheckWaveClear()
    {
        if (AliveEnemyCount == 0) //남은 적이 없으면 웨이브 클리어
        {
            Debug.Log($"웨이브 {currentWave} 클리어! 포탈로 이동하세요.");
            waveCleared = true;
            //wavePortal.ActivatePortal(); //포탈 활성화
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
