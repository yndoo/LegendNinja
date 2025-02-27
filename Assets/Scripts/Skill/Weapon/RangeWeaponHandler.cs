using UnityEngine;
using Random = UnityEngine.Random;

public class RangeWeaponHandler : WeaponHandler
{
    [Header("Ranged Attack Data")]
    [SerializeField] private Transform projectileSpawnPosition; // 투사체가 생성될 위치

    [SerializeField] private int bulletIndex; // 탄환 인덱스 (사용할 탄환 유형 식별)
    public int BulletIndex { get { return bulletIndex; } }

    [SerializeField] public float bulletSize = 1; // 투사체 크기
    public float BulletSize { get { return bulletSize; } }

    [SerializeField] private float duration; // 투사체 지속 시간
    public float Duration { get => duration; set => duration = value; }

    [SerializeField] private float spread; // 투사체 발사 각도 랜덤화 (퍼짐 정도)
    public float Spread { get => spread; set => spread = value; }

    [SerializeField] private float numberofProjectilesPerShot; // 한 번의 공격 시 발사할 투사체 개수
    public float NumberOfProjectilesPerShot { get => numberofProjectilesPerShot; set => numberofProjectilesPerShot = value; }


    [SerializeField] private float multipleProjectilesAngel; // 투사체 간 간격 (각도)
    public float MultipleProjectilesAngle { get => multipleProjectilesAngel; set => multipleProjectilesAngel = value; }

    [SerializeField] private Color projectileColor; // 투사체 색상
    public Color ProjectileColor { get { return projectileColor; } }

    private ProjectileManager projectileManager; // 투사체 생성 및 관리

    public RangeWeaponHandler(Transform projectileSpawnPosition, float damage, float speed, float cooldown, int bulletIndex, float bulletSize, float duration, float spread, float _numberofProjectilesPerShot, float _multipleProjectilesAngel, Color projectileColor, ProjectileManager projectileManager)
    {
        this.projectileSpawnPosition = projectileSpawnPosition;
        Damage = damage;
        AttackSpeed = speed;
        Delay = cooldown;
        
        this.bulletIndex = bulletIndex;
        this.bulletSize = bulletSize;
        this.duration = duration;
        this.spread = spread;
        NumberOfProjectilesPerShot = _numberofProjectilesPerShot;
        MultipleProjectilesAngle = _multipleProjectilesAngel;
        this.projectileColor = projectileColor;
        this.projectileManager = projectileManager;
    }

    /// <summary>
    /// Start()에서 ProjectileManager를 가져옴.
    /// </summary>
    protected override void Start()
    {
        base.Start();
        projectileManager = ProjectileManager.Instance; // 싱글톤 인스턴스 가져오기

        if (projectileManager == null)
        {
            Debug.LogError("ProjectileManager.Instance가 null입니다. ProjectileManager가 씬에 존재하는지 확인하세요!");
        }
    }

    /// <summary>
    /// 공격 시 여러 개의 투사체를 생성하여 발사.
    /// </summary>
    public override void Attack(Vector3 direction)
    {
        base.Attack(direction);

        float projectilesAngleSpace = multipleProjectilesAngel; // 투사체 간 간격 설정
        float numberOfProjectilesPerShot = numberofProjectilesPerShot; // 발사할 투사체 개수

        // 투사체를 가운데 정렬하기 위한 최소 각도 계산
        float minAngle = -(numberOfProjectilesPerShot / 2f) * projectilesAngleSpace + 0.5f * multipleProjectilesAngel;

        // 지정된 개수만큼 투사체 생성
        for (int i = 0; i < numberOfProjectilesPerShot; i++)
        {
            float angle = minAngle + projectilesAngleSpace * i; // 기본 각도 설정
            float randomSpread = Random.Range(-spread, spread); // 랜덤한 퍼짐 효과 추가
            angle += randomSpread;
            CreateProjectile(direction, angle);
        }
    }
    

    /// <summary>
    /// 투사체를 생성하여 발사.
    /// </summary>
    /// <param name="_lookDirection">캐릭터가 바라보는 방향</param>
    /// <param name="angle">투사체 회전 각도</param>
    private void CreateProjectile(Vector2 _lookDirection, float angle)
    {
        if (projectileManager == null)
        {
            Debug.LogError("ProjectileManager가 null입니다! 싱글톤이 올바르게 설정되었는지 확인하세요.");
            return;
        }
        projectileManager.ShootBullet(
            this,
            projectileSpawnPosition.position,
            RotateVector2(_lookDirection, angle)); // 투사체 방향 회전 후 발사
    }

    /// <summary>
    /// 벡터를 특정 각도로 회전시키는 메서드.
    /// </summary>
    /// <param name="v">회전할 벡터</param>
    /// <param name="degree">회전할 각도</param>
    /// <returns>회전된 벡터</returns>
    private static Vector2 RotateVector2(Vector2 v, float degree)
    {
        return Quaternion.Euler(0, 0, degree) * v;
    }
}
