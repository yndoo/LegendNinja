using UnityEngine;
using System.Collections.Generic;
using UnityEditorInternal;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;

public class WaveManager : MonoBehaviour
{

    public static WaveManager instance { get; private set; }
    public int totalWaves = 5; // 총 웨이브 수

    [Header("스폰 스크립트 연결")]
    public MonsterSpawner monsterSpawner; //몬스터 랜덤 스폰 스크립트
    public ObstacleSpawner obstacleSpawner; //장애물 랜덤 생성 스크립트
    public WavePortal wavePortal; //포탈 스크립트 (다음 웨이브 시작 트리거)
    public SkillSelectionUI skillSelectionUI; // 스킬 UI
    public GameObject ClearText;
    public GameObject FailPanel;

    public int AliveEnemyCount {  get; set; }
    public int CurrentWave {  get; set; }
    public int CurrentStage;

    private bool waveCleared = false;
    private List<Vector2> spawnedPosition;
    public Vector2 mapSize = new Vector2(10, 10); //맵크기


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        if(CheckWaveClear())
        {
            //StartNextWave();
        }
    }

    private void Start()
    {
        StartNextWave();
    }

    public void StartNextWave()
    {
        if (CurrentWave >= totalWaves)
        {         
            return;
        }

        CurrentWave++;
        waveCleared = false; // 새 웨이브 시작

        //플레이어 위치 초기화
        Player playerScript = FindObjectOfType<Player>();
        if (playerScript != null)
        {
            playerScript.ResetPlayerPosition();
        }

        //웨이브마다 장애물 증가
        int obstacleCount = Mathf.Clamp(2 + CurrentWave, 2, 4); 

        obstacleSpawner.ClearObstacles(); //기존 장애물 제거

        spawnedPosition = new List<Vector2>(); //생성된 위치 리스트

        // 새로운 장애물 배치
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

        // 현재 웨이브 몬스터 스폰
        monsterSpawner.WaveSpawn();

        // 스킬 UI On
        skillSelectionUI.OpenPanel();
    }

    //랜덤위치생성
    public Vector2 GetRandomPosition()
    {
        float x = Random.Range(-mapSize.x / 2 ,mapSize.y / 2);
        float y = Random.Range(-mapSize.y /2 ,mapSize.x / 2);
        return new Vector2(x, y);
    }

    //장애물 위치 겹치는지 확인
    private bool IsPositionOccupied(Vector2 position,List<Vector2> _spawnedPosition)
    {
        foreach (Vector2 spawnedPosiotion in _spawnedPosition)
        {
            if (Vector2.Distance(position, spawnedPosiotion) < 4f) 
            {
                return true;
            }
        }

        return false;
    }

    //장애물 위치 겹치는지 확인 (외부용)
    public bool IsPositionOccupied(Vector2 position)
    {
        if (spawnedPosition == null) return true;
        foreach (Vector2 sPos in spawnedPosition)
        {
            if (Vector2.Distance(position, sPos) < 2f)
            {
                return true;
            }
        }

        return false;
    }

    public bool CheckWaveClear()
    {
        if (AliveEnemyCount == 0) //남은 적이 없으면 웨이브 클리어
        {
            Debug.Log($"웨이브 {CurrentWave} 클리어! 포탈로 이동하세요.");
            waveCleared = true;
            wavePortal.ActivePortal(); //포탈 활성화
        }
        return waveCleared;
    }

    public void TryStartNextWave()
    {
        if(CurrentWave >= 5)
        {
            ClearText.SetActive(true);
            Invoke("StageEnd", 1f);
            return;
        }

        if (waveCleared)
        {
            wavePortal.DeactivePortal(); //포탈 비활성화
            StartNextWave();
        }
    }

    /// <summary>
    /// 스테이지 넘기는 기능
    /// </summary>
    void StageEnd()
    {
        if (CurrentStage == 1)
        {
            wavePortal.DeactivePortal(); //포탈 비활성화
            UnityEngine.SceneManagement.SceneManager.LoadScene("Stage2");
        }
        else if (CurrentStage == 2)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("EndScene");
        }
    }

    public void GameOverOn()
    {
        FailPanel.SetActive(true);
    }
}
