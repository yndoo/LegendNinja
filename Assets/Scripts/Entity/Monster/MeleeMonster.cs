using PublicDefinitions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeMonster : BaseMonster
{
    
    private void Awake()
    {
        monsterAnimator = GetComponentInChildren<Animator>();
        GetComponentInChildren<CircleCollider2D>().radius = 3f;
    }

    private void Update()
    {
        
    }

    public override void Attack()
    {
        base.Attack();

        TargetPlayer.Damage(AttackPower);
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
