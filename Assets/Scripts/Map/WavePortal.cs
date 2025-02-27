using UnityEngine;
using UnityEngine.SceneManagement;

public class WavePortal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) //플레이어가 포탈에 들어갔을 때
        {
            FindObjectOfType<WaveManager>().TryStartNextWave();
            //SceneManager.LoadScene(0,LoadSceneMode.) 옆에 맵만들고 다음 스테이지


        }


    }


}
