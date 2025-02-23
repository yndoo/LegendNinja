using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeMonster : BaseMonster
{
    protected override void Start()
    {
        base.Start();
        MoveSpeed = 1f;
        AttackRange = 1f;
    }
    public override void Attack()
    {
        Debug.Log("Melee Attack");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 플레이어 감지됨
            PlayerDetectStart(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 플레이어 감지 해제
            PlayerDetectEnd();
        }
    }
}
