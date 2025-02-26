using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMonster : Character
{
    private static readonly int MoveX = Animator.StringToHash("MoveX");
    private static readonly int MoveY = Animator.StringToHash("MoveY");
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");

    protected GameObject Target;
    protected Player TargetPlayer;
    protected Animator monsterAnimator;
    protected MonsterData myData;
    protected bool TargetFollowMode { get; set; }
    protected float AttackCoolDown { get; set; }
    protected Vector3 TargetDir { get; set; }

    private void Awake()
    {
        
    }
    private void Update()
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
        if (AttackCoolDown > 0f) return;
        AttackCoolDown = AttackTime;
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
    }
    public void MoveToTarget()
    {
        if(Vector3.Distance(transform.position, Target.transform.position) <= AttackRange)
        {
            Attack();
            return;
        }

        TargetDir = (Target.transform.position - transform.position).normalized;
        transform.position += TargetDir * (0.05f * MoveSpeed);
        // 애니메이션 방향 지정
        monsterAnimator.SetFloat(MoveX, TargetDir.x);
        monsterAnimator.SetFloat(MoveY, TargetDir.y);
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
