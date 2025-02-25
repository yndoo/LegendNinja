using PublicDefinitions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : BaseMonster
{
    private void Awake()
    {
        monsterAnimator = GetComponentInChildren<Animator>();
    }

    public override void Attack()
    {
        base.Attack();
        switch (myData.type)
        {
            case EAttackType.Melee:
                break;
            case EAttackType.Ranged:
                break;
            case EAttackType.AoE:
                break;
            default:
                break;
        }

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

    /// <summary>
    /// data와 stat 초기화
    /// </summary>
    /// <param name="data">몬스터 데이터</param>
    public void InitMonster(MonsterData data)
    {
        myData = data;

        Health = MaxHealth = data.stats.MaxHealth;
        AttackPower = data.stats.AttackPower;
        Defense = data.stats.Defense;
        MoveSpeed = data.stats.MoveSpeed;
        AttackRange = data.stats.AttackRange;
        AttackSpeed = data.stats.AttackSpeed;
        AttackTime = data.stats.AttackTime;
    }
}
