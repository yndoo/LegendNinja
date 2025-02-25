using UnityEngine;
using System.Collections.Generic;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs; //장애물 배열
    public int obstacleCount = 5; //생성할 장애물 개수
    public Vector2 mapSize = new Vector2(10, 10); // 맵 크기
    public LayerMask obstacleLayer; //체크용

    private List<Vector2> usedPositions = new List<Vector2>(); //이미 배치된 위치 저장

    void Start()
    {
        SpawnObstacles();
    }

    void SpawnObstacles()
    {
        List<GameObject> availableObstacles = new List<GameObject>(obstaclePrefabs); //장애물 리스트 생성

        for (int i = 0; i < obstacleCount && availableObstacles.Count > 0; i++)
        {
            Vector2 randomPos;
            int maxAttempts = 10;

            //장애물이 겹치지 않도록 위치 찾기
            do
            {
                randomPos = new Vector2(
                    Random.Range(-mapSize.x / 2, mapSize.x / 2),
                    Random.Range(-mapSize.y / 2, mapSize.y / 2)
                );
                maxAttempts--;
            } while (usedPositions.Contains(randomPos) || Physics2D.OverlapCircle(randomPos, 0.5f, obstacleLayer) && maxAttempts > 0);

            if (availableObstacles.Count > 0)
            {
                int randomIndex = Random.Range(0, availableObstacles.Count);
                GameObject obstacle = Instantiate(availableObstacles[randomIndex], randomPos, Quaternion.identity);
                availableObstacles.RemoveAt(randomIndex); //사용한 장애물 제거(중복 방지)
                usedPositions.Add(randomPos);
            }
        }
    }
}
