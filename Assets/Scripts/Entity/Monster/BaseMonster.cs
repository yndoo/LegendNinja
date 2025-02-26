using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMonster : Character
{
    protected static readonly int MoveX = Animator.StringToHash("MoveX");
    protected static readonly int MoveY = Animator.StringToHash("MoveY");
    protected static readonly int IsMoving = Animator.StringToHash("IsMoving");
    protected static readonly int IsAttack = Animator.StringToHash("IsAttack");

    protected GameObject Target;
    protected Player TargetPlayer;
    protected Animator monsterAnimator;
    protected MonsterData myData;
    protected bool TargetFollowMode { get; set; }
    protected float AttackCoolDown { get; set; }    // 공격 쿨타임, stat에서 AttackTime를 사용.
    //protected float AttacksInterval { get; set; }   // 원거리 공격 간격, stat에서 AttackSpeed와 연관.
    protected Vector3 TargetDir { get; set; }
    public abstract void MoveToTarget();

    protected virtual void Update()
    {
        AttackCoolDown -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if(TargetFollowMode == true)
        {
            MoveToTarget();
        }
    }

    public override void Attack()
    {
        base.Attack();
        monsterAnimator.SetBool(IsMoving, false);
        monsterAnimator.SetBool(IsAttack, true);
    }

    /// <summary>
    /// 피격 처리 (Health 감소, 애니메이션)
    /// </summary>
    /// <param name="damage">피격 데미지 크기</param>
    public override void Damage(float damage)
    {
        Health -= damage;
        if(Health <= 0)
        {
            // TO DO : 애니메이션 Die 처리
            Destroy(this.gameObject, 1f);
        }
    }

    /// <summary>
    /// 플레이어 감지됐을 때 처리
    /// </summary>
    /// <param name="player">감지된 타겟(플레이어)</param>
    protected void PlayerDetectStart(GameObject player)
    {
        Target = player;
        TargetPlayer = player.GetComponent<Player>();
        TargetFollowMode = true;
    }

    /// <summary>
    /// 플레이어 감지 해제 처리
    /// </summary>
    protected void PlayerDetectEnd()
    {
        Target = null;
        TargetFollowMode = false;
        
        monsterAnimator.SetBool(IsMoving, false);
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
