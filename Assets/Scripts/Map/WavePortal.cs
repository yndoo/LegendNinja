using UnityEngine;
using UnityEngine.SceneManagement;

public class WavePortal : MonoBehaviour
{
    private Collider2D portalCollider;
    private SpriteRenderer spriteRenderer;


    private void Start()
    {
        portalCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        DeactivePortal(); //시작 시 비활성화
    }

    public void ActivePortal()
    {
        portalCollider.enabled = true;
        spriteRenderer.enabled = true;
    }

    public void DeactivePortal()
    {
        portalCollider.enabled = false;
        spriteRenderer.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) //플레이어가 포탈에 들어갔을 때
        {
            WaveManager.instance.TryStartNextWave();
        }
    }
}
