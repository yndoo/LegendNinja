using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager_ : MonoBehaviour
{
    
    public void StartScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ExitScene()
    {
        // 게임 종료
#if UNITY_EDITOR
        // 에디터에서 실행 중일 경우
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // 빌드에서 실행 중일 경우
        Application.Quit();
#endif
    }

}
