using UnityEngine;

/// <summary>
/// 투사체(Projectile)를 관리하는 컨트롤러 클래스.
/// 투사체의 이동, 충돌 판정, 지속 시간 체크 및 삭제를 담당.
/// </summary>
public class ProjectileController : MonoBehaviour
{
    [SerializeField] private LayerMask levelCollisionLayer; // 레벨 내 충돌 감지용 레이어 마스크

    private RangeWeaponHandler rangeWeaponHandler; // 투사체를 발사한 무기 핸들러
    private float currentDuration; // 투사체가 존재한 시간
    private Vector2 direction; // 투사체 이동 방향
    private bool isReady; // 투사체가 활성화되었는지 여부

    private Rigidbody2D _rigidbody; // 투사체의 Rigidbody2D 컴포넌트
    private SpriteRenderer spriteRenderer; // 투사체의 스프라이트 렌더러

    public bool fxOnDestory = true; // 투사체 파괴 시 FX 효과 생성 여부

    /// <summary>
    /// Awake()에서 Rigidbody2D 및 SpriteRenderer 컴포넌트를 가져옴.
    /// </summary>
    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>(); // 하위 오브젝트에서 스프라이트 렌더러 가져오기
        _rigidbody = GetComponent<Rigidbody2D>(); // Rigidbody2D 컴포넌트 가져오기
    }

    /// <summary>
    /// Update()에서 지속 시간을 체크하고 투사체를 이동시킴.
    /// </summary>
    private void Update()
    {
        if (!isReady) return; // 투사체가 준비되지 않았다면 로직 실행 X

        currentDuration += Time.deltaTime; // 경과 시간 증가

        // 투사체의 지속 시간이 초과되면 삭제
        if (currentDuration > rangeWeaponHandler.Duration)
        {
            DestroyProjectile(transform.position, false);
        }

        // 투사체 이동 (방향 * 공격 속도)
        _rigidbody.velocity = direction * rangeWeaponHandler.AttackSpeed;
    }

    /// <summary>
    /// 충돌 감지 후, 적이나 지형과 충돌하면 투사체 삭제.
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 레벨 지형과 충돌했을 경우
        if (levelCollisionLayer.value == (levelCollisionLayer.value | (1 << collision.gameObject.layer)))
        {
            DestroyProjectile(collision.ClosestPoint(transform.position) - direction * .2f, fxOnDestory);
        }
        // 공격 대상(Enemy 등)과 충돌했을 경우
        else if (rangeWeaponHandler.target.value == (rangeWeaponHandler.target.value | (1 << collision.gameObject.layer)))
        {
            DestroyProjectile(collision.ClosestPoint(transform.position), fxOnDestory);
        }

        // 몬스터와 충돌했을 경우
        if (collision.CompareTag("Monster"))
        {
            BaseMonster monster = collision.GetComponent<BaseMonster>();

            if (monster != null)
            {
                // 몬스터에게 데미지 적용
                monster.Damage(rangeWeaponHandler.Damage);
                Debug.Log($"몬스터의 남은 HP: {monster.Health}");

                // 투사체 제거
                //DestroyProjectile(collision.ClosestPoint(transform.position), fxOnDestory);
            }
            else
            {
                Debug.LogWarning("BaseMonster 컴포넌트를 찾을 수 없음!");
            }
        }
    }

    /// <summary>
    /// 투사체를 초기화하고, 발사 방향과 속성을 설정함.
    /// </summary>
    public void Init(Vector2 direction, RangeWeaponHandler weaponHandler)
    {
        rangeWeaponHandler = weaponHandler; // 발사한 무기 핸들러 저장

        this.direction = direction; // 투사체 이동 방향 설정
        currentDuration = 0; // 지속 시간 초기화

        // 투사체 크기 및 색상 설정
        transform.localScale = Vector3.one * weaponHandler.bulletSize;
        spriteRenderer.color = weaponHandler.ProjectileColor;

        isReady = true; // 투사체 활성화
    }

    /// <summary>
    /// 투사체를 삭제하는 메서드.
    /// </summary>
    /// <param name="position">삭제될 위치</param>
    /// <param name="createFx">파괴 시 FX 효과 생성 여부</param>
    private void DestroyProjectile(Vector3 position, bool createFx)
    {
        Destroy(this.gameObject); // 투사체 삭제
    }
}
