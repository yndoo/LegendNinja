using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterProjectile : MonoBehaviour
{
    [SerializeField] private LayerMask levelCollisionLayer; // 레벨 내 충돌 감지용 레이어 마스크
    public float MyPower { get; set; }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 맵 오브젝트 충돌 시 제거
        if (levelCollisionLayer.value == (levelCollisionLayer.value | (1 << collision.gameObject.layer)))
        {
            Destroy(this.gameObject);
        }

        // 플레이어 충돌 시 타격 후 제거
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().Damage(MyPower);
            Destroy(this.gameObject);
        }
    }
}
