using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMonster : Character
{
    protected GameObject Target;
    protected bool TargetFollowMode { get; set; }
    
    protected float AttackCoolDown;
    public override void Attack()
    {
        base.Attack();
    }

    protected virtual void Start()
    {

    }

    /// <summary>
    /// 데미지 입었을 때 처리 (애니메이션 등)
    /// </summary>
    public override void Damage()
    {
        
    }

    private void FixedUpdate()
    {
        if(TargetFollowMode == true)
        {
            MoveToTarget();
        }
    }

    /// <summary>
    /// 플레이어 감지됐을 때 처리
    /// </summary>
    /// <param name="player">감지된 타겟(플레이어)</param>
    protected void PlayerDetectStart(GameObject player)
    {
        Target = player;
        TargetFollowMode = true;

        // TO DO : 애니메이션 Move 처리
    }

    /// <summary>
    /// 플레이어 감지 해제 처리
    /// </summary>
    protected void PlayerDetectEnd()
    {
        Target = null;
        TargetFollowMode = false;

        // TO DO : 애니메이션 Idle 처리
    }
    public void MoveToTarget()
    {
        if(Vector3.Distance(transform.position, Target.transform.position) <= AttackRange)
        {
            Attack();
            return;
        }

        Vector3 Direction = (Target.transform.position - transform.position).normalized;
        transform.position += Direction * (0.1f * MoveSpeed);
    }
}
