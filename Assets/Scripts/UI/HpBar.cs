using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public Transform player;  // 플레이어 오브젝트
    public Image progress;    // 체력바 이미지

    public Vector3 offset = new Vector3(0f, 1.5f, 0f); // 체력바 오프셋 (플레이어 위치에서 위로 조금 띄우기)

    private Player playerScript; // Player 스크립트 참조

    private void Start()
    {
        playerScript = player.GetComponent<Player>(); // 플레이어 스크립트 가져오기
    }

    private void Update()
    {
        // 플레이어가 있으면 체력바 위치 업데이트
        if (player != null)
        {
            transform.position = Camera.main.WorldToScreenPoint(player.position + offset);

            // 플레이어의 체력에 맞춰 fillAmount 업데이트
            if (playerScript != null)
            {
                float fillAmount = Mathf.Clamp(playerScript.Health / playerScript.MaxHealth, 0f, 1f);
                progress.fillAmount = fillAmount; // 체력에 맞춰 채워짐
            }
        }
    }
}
