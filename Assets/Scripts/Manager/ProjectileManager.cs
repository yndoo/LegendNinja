using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    // ProjectileManager의 싱글톤 인스턴스
    private static ProjectileManager instance;
    public static ProjectileManager Instance { get { return instance; } }
    // 사용할 투사체 프리팹 배열
    [SerializeField] private GameObject[] projectilePrefabs;

    private void Awake()
    {
        instance = this;
    }
    #region
    /// <summary>
    /// 투사체(Projectile)를 생성하고 발사하는 메서드
    /// </summary>
    /// <param name="rangeWeaponHandler">발사하는 무기 핸들러</param>
    /// <param name="startPostiion">투사체의 시작 위치</param>
    /// <param name="direction">투사체가 날아갈 방향</param>
    #endregion
    public void ShootBullet(RangeWeaponHandler rangeWeaponHandler, Vector2 startPostiion, Vector2 direction)
    {
        // 지정된 무기 핸들러의 BulletIndex를 사용하여 적절한 탄환 프리팹 선택
        GameObject origin = projectilePrefabs[rangeWeaponHandler.BulletIndex];
        GameObject obj = Instantiate(origin, startPostiion, Quaternion.identity);
        
        // 프리팹이 올바르게 생성되지 않았을 경우 오류 메시지 출력 후 종료
        if (obj == null)
        {
            Debug.LogError(" ProjectileController가 없습니다! 프리팹 확인 필요!");
            return;
        }
        // 생성된 투사체에 ProjectileController가 있는지 확인 후 초기화 수행
        ProjectileController projectileController = obj.GetComponent<ProjectileController>();
        projectileController.Init(direction, rangeWeaponHandler);
    }

}