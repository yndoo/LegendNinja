using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WeaponHandler : MonoBehaviour
{
    //  무기 공격 관련 정보
    [Header("Attack Info")]

    [SerializeField] private float delay = 1f;
    public float Delay { get => delay; set => delay = value; }
    // 공격 간격(쿨타임) - 무기가 공격할 때마다 일정 시간 딜레이 적용

    [SerializeField] private float weaponSize = 1f;
    public float WeaponSize { get => weaponSize; set => weaponSize = value; }
    // 무기 크기 - 공격 판정 범위에 영향을 줄 수 있음

    [SerializeField] private float power = 1f;
    public float Power { get => power; set => power = value; }
    // 공격력 - 적에게 가하는 피해량을 결정

    [SerializeField] private float speed = 1f;
    public float Speed { get => speed; set => speed = value; }
    // 무기 속도 - 발사체가 날아가는 속도

    [SerializeField] private float attackRange = 10f;
    public float AttackRange { get => attackRange; set => attackRange = value; }
    // 공격 범위 - 원거리 공격일 경우, 최대 사거리를 의미

    public LayerMask target;
    // 공격 대상 설정 - 특정 Layer(적, 몬스터 등)만 공격할 수 있도록 설정

    //  넉백(적을 밀쳐내는 효과) 관련 정보
    [Header("Knock Back Info")]

    [SerializeField] private bool isOnknockBack = false;
    public bool IsOnknockBack { get => isOnknockBack; set => isOnknockBack = value; }
    // 넉백 효과 적용 여부 - true면 적이 공격을 맞았을 때 밀려남

    [SerializeField] private float knockbackPower = 0.1f;
    public float KnockbackPower { get => knockbackPower; set => knockbackPower = value; }
    // 넉백 세기 - 적을 얼마나 강하게 밀쳐낼지 결정

    [SerializeField] private float knockbackTime = 0.5f;
    public float KnockbackTime { get => knockbackTime; set => knockbackTime = value; }
    // 넉백 지속 시간 - 적이 넉백 상태에 머무는 시간

    //  애니메이션 관련 해시값 (공격 애니메이션 트리거)
    private static readonly int IsAttack = Animator.StringToHash("IsAttack");
    // 애니메이터에서 "IsAttack" 파라미터를 해시 값으로 저장하여 성능 최적화


    // Animator 컴포넌트 참조
    private Animator animator;
    // SpriteRenderer 컴포넌트 참조
    private SpriteRenderer weaponRenderer;

    protected virtual void Start()
    {

    }

    // 초기화 메서드
    protected virtual void Awake()
    {
        // 부모 객체에서 Animator 컴포넌트 가져오기
        animator = GetComponentInParent<Animator>();
        // 자식 객체에서 SpriteRenderer 컴포넌트 가져오기
        weaponRenderer = GetComponentInChildren<SpriteRenderer>();

        // 애니메이터 속도 설정
        animator.speed = 1.0f / delay;
        // 무기 크기 설정
        transform.localScale = Vector3.one * weaponSize;
    }

    // 공격 메서드
    public virtual void Attack()
    {
        AttackAnimation();
    }

    // 공격 애니메이션 트리거 메서드
    private void AttackAnimation()
    {
        animator.SetTrigger(IsAttack);
    }

    // 무기 회전 메서드
    public virtual void Rotate(bool isLeft)
    {
        weaponRenderer.flipY = isLeft;
    }
}
