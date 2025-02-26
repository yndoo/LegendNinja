using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedMonster : BaseMonster
{
    private int shootNum;
    private void Awake()
    {
        monsterAnimator = GetComponentInChildren<Animator>();
        GetComponentInChildren<CircleCollider2D>().radius = 4f;
    }
    protected override void Update()
    {
        base.Update();

    }
    public override void Attack()
    {
        base.Attack();

        if (AttackCoolDown > 0f) return;

        for(int i = 0; i < 5; i++)
        {
            Invoke("Shooting", AttackSpeed * i);
        }
        AttackCoolDown = AttackTime;
    }
    void Shooting()
    {
        Debug.Log("Ranged Attack");
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
