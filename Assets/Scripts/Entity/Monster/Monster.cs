using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : BaseMonster
{
    protected override void Start()
    {
        base.Start();

    }
    private void Update()
    {
        AttackCoolDown -= Time.deltaTime;
    }
    public override void Attack()
    {
        if (AttackCoolDown > 0f) return;

        base.Attack();
        Debug.Log("Melee Attack");
        AttackCoolDown = AttackTime;
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
