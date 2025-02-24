using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;
    private bool isStageCleared = false; //클리어 판단
    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // 클리어 시 가져오기 (예: 모든 적을 처치했을 때)
    public void ClearStage()
    {
        isStageCleared = true;
        Debug.Log("스테이지 클리어! 출구로 이동하세요.");
    }

    private void OnTriggerEnter2D(Collider2D other) //클리어 후 콜라이더에 들어갔을 때만 작동
    {
        if (other.CompareTag("Player") && isStageCleared) 
        {
            LoadNextStage();
        }
    }

    void LoadNextStage()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1); // 다음씬
    }

    //if 적 전부 처치시에 StageManager.instance.ClearStage(); 호출
}
