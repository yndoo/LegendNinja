using UnityEngine;
using System.Collections.Generic;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs; // 장애물 배열
    public int obstacleCount = 5; // 생성할 장애물 개수
    public Vector2 mapSize = new Vector2(10, 10); // 맵 크기
    public LayerMask obstacleLayer; // 장애물 체크용 레이어

    private List<Vector2> usedPositions = new List<Vector2>(); // 사용된 위치 저장

    void Start()
    {
        SpawnObstacles();
    }

    void SpawnObstacles()
    {
        for (int i = 0; i < obstacleCount; i++)
        {
            Vector2 randomPos;
            int maxAttempts = 10;

            // 장애물이 겹치지 않도록 위치 찾기
            do
            {
                randomPos = new Vector2(
                    Random.Range(-mapSize.x / 2, mapSize.x / 2),
                    Random.Range(-mapSize.y / 2, mapSize.y / 2)
                );
                maxAttempts--;
            } while (usedPositions.Contains(randomPos) || Physics2D.OverlapCircle(randomPos, 0.5f, obstacleLayer) && maxAttempts > 0);

            if (maxAttempts > 0)
            {
                int randomIndex = Random.Range(0, obstaclePrefabs.Length); // 랜덤 장애물 선택
                GameObject obstacle = Instantiate(obstaclePrefabs[randomIndex], randomPos, Quaternion.identity);
                usedPositions.Add(randomPos);
            }
        }
    }
}
