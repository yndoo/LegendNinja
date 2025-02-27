using UnityEngine;

public class WavePortal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) //플레이어가 포탈에 들어갔을 때
        {
            FindObjectOfType<WaveManager>().TryStartNextWave();
        }
    }
}
