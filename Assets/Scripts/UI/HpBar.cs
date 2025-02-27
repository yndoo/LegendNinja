using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public Transform player;  
    public Image progress;    // 체력바 채워진 이미지

    public Vector3 offset = new Vector3(0f, 0f, 0f); 

    private Player playerScript; 
    private Canvas canvas;

    private void Start()
    {
        playerScript = player.GetComponent<Player>(); 
        canvas = canvas.GetComponent<Canvas>();
    }

    private void Update()
    {
        if (player != null)
        {
            transform.position = player.position + offset;

            if (playerScript != null)
            {
                float fillAmount = Mathf.Clamp(playerScript.Health / playerScript.MaxHealth, 0f, 1f);
                progress.fillAmount = fillAmount; 
            }
        }
    }
}
