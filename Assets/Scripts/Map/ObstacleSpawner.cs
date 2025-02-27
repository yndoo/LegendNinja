using UnityEngine;
using System.Collections.Generic;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs; //장애물 프리팹 배열
    public Vector2 mapSize = new Vector2(10, 10); //맵 크기
    private List<GameObject> spawnedObstacles = new List<GameObject>(); //생성된 장애물 리스트

    public void SpawnObstacles(Vector2 position, int prefabIndex)
    {
        if (obstaclePrefabs.Length == 0) return;

        prefabIndex = Mathf.Clamp(prefabIndex, 0, obstaclePrefabs.Length - 1);
        GameObject obstacle = Instantiate(obstaclePrefabs[prefabIndex], position, Quaternion.identity);
        spawnedObstacles.Add(obstacle); //생성된 장애물을 리스트에 추가
       
    }

    Vector2 GetRandomPosition()
    {
        float x = Random.Range(-mapSize.x / 2, mapSize.x / 2);
        float y = Random.Range(-mapSize.y / 2, mapSize.y / 2);
        return new Vector2(x, y);
    }

    //웨이브가 변경될 때 기존 장애물 삭제
    public void ClearObstacles()
    {
        foreach (GameObject obstacle in spawnedObstacles)
        {
            if (obstacle != null)
            {
                Destroy(obstacle);
            }
        }
        spawnedObstacles.Clear(); //리스트 초기화
    }
}
