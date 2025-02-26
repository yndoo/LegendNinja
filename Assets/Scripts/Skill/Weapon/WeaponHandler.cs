using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [Header("Attack Info")]
    [SerializeField] private float delay = 1f; // 공격 딜레이 (초)
    public float Delay { get => delay; set => delay = value; }

    [SerializeField] private float weaponSize = 1f; // 무기의 크기
    public float WeaponSize { get => weaponSize; set => weaponSize = value; }

    [SerializeField] private float power = 10f; // 공격력
    public float Power { get => power; set => power = value; }

    [SerializeField] private float attackSpeed = 10f; // 투사체 속도 증가
    public float AttackSpeed { get => attackSpeed; set => attackSpeed = value; }

    [SerializeField] private float attackRange = 10f; // 공격 범위
    public float AttackRange { get => attackRange; set => attackRange = value; }

    public LayerMask target; // 공격 대상 레이어 설정

    [Header("Knock Back Info")]
    [SerializeField] private bool isOnKnockback = false; // 넉백 적용 여부
    public bool IsOnKnockback { get => isOnKnockback; set => isOnKnockback = value; }

    [SerializeField] private float knockbackPower = 0.1f; // 넉백 힘
    public float KnockbackPower { get => knockbackPower; set => knockbackPower = value; }

    [SerializeField] private float knockbackTime = 0.5f; // 넉백 지속 시간
    public float KnockbackTime { get => knockbackTime; set => knockbackTime = value; }

    // 공격 애니메이션 트리거 해시 값
    private static readonly int IsAttack = Animator.StringToHash("IsAttack");

    // 이 무기를 소유한 BaseController (플레이어나 적 캐릭터)
    public BaseController Controller { get; private set; }

    //private Animator animator; // 무기의 애니메이션 컨트롤러
    private SpriteRenderer weaponRenderer; // 무기의 스프라이트 렌더러

    /// <summary>
    /// 초기화 작업을 수행하는 메서드
    /// </summary>
    protected virtual void Awake()
    {
        // 부모 객체에서 BaseController를 가져옴
        Controller = GetComponentInParent<BaseController>();

        // 하위 객체에서 Animator 및 SpriteRenderer를 가져옴
        //animator = GetComponentInChildren<Animator>();
        weaponRenderer = GetComponentInChildren<SpriteRenderer>();

        // 공격 속도에 따라 애니메이션 속도 조절
        //animator.speed = 1.0f / delay;

        // 무기의 크기 설정
        transform.localScale = Vector3.one * weaponSize;
    }

    /// <summary>
    /// Start에서 수행할 추가적인 동작 (현재는 비워둠, 필요 시 오버라이드 가능)
    /// </summary>
    protected virtual void Start()
    {
        Debug.Log($"초기 공격력: {Power}, 초기 속도: {AttackSpeed}, 초기 딜레이: {Delay}");
        SkillManager skillManager = FindObjectOfType<SkillManager>();
        if(skillManager == null)
        {
            Debug.LogError("SkillManager를 찾을 수 없습니다.");
            return;
        }

    }

    /// <summary>
    /// 공격을 실행하는 메서드 (자식 클래스에서 오버라이드 가능)
    /// </summary>
    public virtual void Attack(Vector3 direction)
    {
        //AttackAnimation();
    }

    /// <summary>
    /// 공격 애니메이션을 실행하는 메서드
    /// </summary>
    //public void AttackAnimation()
    //{
    //    animator.SetTrigger(IsAttack);
    //}

    /// <summary>
    /// 무기의 방향을 회전시키는 메서드
    /// </summary>
    /// <param name="isLeft">true면 왼쪽, false면 오른쪽</param>
    public virtual void Rotate(bool isLeft)
    {
        weaponRenderer.flipY = isLeft;
    }
}
