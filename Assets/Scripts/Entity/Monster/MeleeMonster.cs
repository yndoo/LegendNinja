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
    public override void Attack()
    {
        base.Attack();

        if (AttackCoolDown > 0f) return;
        
        TargetPlayer.Damage(AttackPower);
        Debug.Log("근거리 공격!");
        AttackCoolDown = AttackTime;
    }
    public override void MoveToTarget()
    {
        if (Vector3.Distance(transform.position, Target.transform.position) <= AttackRange)
        {
            Attack();
            return;
        }

        // Move
        TargetDir = (Target.transform.position - transform.position).normalized;
        transform.position += TargetDir * (0.05f * MoveSpeed);

        // 애니메이션 세팅 & 방향 지정
        monsterAnimator.SetBool(IsAttack, false);
        monsterAnimator.SetBool(IsMoving, true);
        monsterAnimator.SetFloat(MoveX, TargetDir.x);
        monsterAnimator.SetFloat(MoveY, TargetDir.y);
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
