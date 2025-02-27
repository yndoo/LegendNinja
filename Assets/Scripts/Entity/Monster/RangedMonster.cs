using PublicDefinitions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedMonster : BaseMonster
{
    private int shootNum;
    protected override void Awake()
    {
        base.Awake();
        monsterAnimator = GetComponentInChildren<Animator>();
        GetComponentInChildren<CircleCollider2D>().radius = 9f;
    }
    protected override void Update()
    {
        base.Update();

    }
    public override void Attack()
    {
        base.Attack();

        if (AttackCoolDown > 0f)
        {
            monsterAnimator.SetBool(IsAttack, false);
            return;
        }

        monsterAnimator.SetBool(IsAttack, true);
        for (int i = 0; i < 5; i++)
        {
            Invoke("Shooting", AttackSpeed * i);
        }
        AttackCoolDown = AttackTime;
    }
    
    void Shooting()
    {
        try
        {
            GameObject go = Instantiate(Resources.Load<GameObject>($"Prefab/MonsterProjectile"), transform.position, transform.rotation);
            Vector3 dir = Target.transform.position - this.transform.position;
            monsterAnimator.SetFloat(MoveX, dir.x);
            monsterAnimator.SetFloat(MoveY, dir.y);
            dir.Normalize();
            go.GetComponent<MonsterProjectile>().MyPower = AttackPower;
            go.GetComponent<Rigidbody2D>().velocity = dir * 7;
            Debug.Log("Ranged Attack");
        }
        catch(System.Exception ex)
        {
            Debug.LogException(ex);
        }


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
