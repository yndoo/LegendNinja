using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : BaseMonster
{
    protected static readonly int IsHidden = Animator.StringToHash("IsHidden");

    [SerializeField] private LayerMask obstacleLayer;

    private readonly float SkillFullTime = 5f;
    private bool isBossSkillOn = false;
    private float skillRuntime = 5f;

    protected override void Awake()
    {
        base.Awake();

        monsterAnimator = GetComponentInChildren<Animator>();
        GetComponentInChildren<CircleCollider2D>().radius = 8f;
    }
    public override void Attack()
    {
        base.Attack();

        if (AttackCoolDown > 0f) return;

        AttackCoolDown = AttackTime;
        TargetPlayer.Damage(AttackPower);
    }
    public override void MoveToTarget()
    {
        if (isBossSkillOn)
        {
            skillRuntime -= Time.deltaTime;
            if (skillRuntime < 0f)
            {
                BossSkillEnd();
                return;
            }
            BossSkill();
            return;
        }

        if (Vector3.Distance(transform.position, Target.transform.position) <= AttackRange)
        {
            // 확률적으로 Hidden 스킬 사용
            int p = Random.Range(0, 100);
            if (p > 80)
            {
                isBossSkillOn = true;
                BossSkillStart();
                return;
            }
            // 기본 공격
            Attack();
            return;
        }

        // Move
        TargetDir = (Target.transform.position - transform.position).normalized;
        transform.position += TargetDir * (0.05f * MoveSpeed);

        // 애니메이션 세팅 & 방향 지정
        monsterAnimator.SetBool(IsAttack, false);
        monsterAnimator.SetBool(IsMoving, true);

        if(TargetDir.x < 0)
        {
            monsterRenderer.flipX = true;
        }
        else
        {
            monsterRenderer.flipX = false;
        }
    }

    /// <summary>
    /// 보스 스킬 첫 스타트 (랜덤 위치로 이동, 애니메이션 전환)
    /// </summary>
    void BossSkillStart()
    {
        monsterAnimator.SetBool(IsHidden, true);
        skillRuntime = SkillFullTime;
        gameObject.tag = "Untagged";
        GetComponent<Collider2D>().enabled = false;
    }

    /// <summary>
    /// 보스 스킬 사용하는 동안 동작
    /// </summary>
    void BossSkill()
    {
        monsterRenderer.color = monsterRenderer.color - new Color(0, 0, 0, Time.deltaTime * 0.8f);
    }

    /// <summary>
    /// 보스 스킬 끝나고 해줘야 하는 것들
    /// </summary>
    void BossSkillEnd()
    {
        isBossSkillOn = false;
        monsterRenderer.color = originalColor;
        monsterAnimator.SetBool(IsHidden, false);
        GetComponent<Collider2D>().enabled = true;
        gameObject.tag = "Monster";

        // 랜덤 위치로 이동
        Vector3 randomPos = new Vector2(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
        while (Physics2D.OverlapCircle(randomPos, 0.5f, obstacleLayer) != null)
        {
            randomPos.x = Random.Range(-3f, 3f);
            randomPos.y = Random.Range(-3f, 3f);
        }
        transform.position = randomPos;
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
