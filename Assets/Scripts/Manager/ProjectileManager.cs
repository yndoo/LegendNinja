using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    private static ProjectileManager instance;
    public static ProjectileManager Instance { get { return instance; } }

    [SerializeField] private GameObject[] projectilePrefabs;

    private void Awake()
    {
        instance = this;
    }

    public void ShootBullet(RangeWeaponHandler rangeWeaponHandler, Vector2 startPostiion, Vector2 direction)
    {

        GameObject origin = projectilePrefabs[rangeWeaponHandler.BulletIndex];
        GameObject obj = Instantiate(origin, startPostiion, Quaternion.identity);

        if (obj == null)
        {
            Debug.LogError(" ProjectileController가 없습니다! 프리팹 확인 필요!");
            return;
        }

        ProjectileController projectileController = obj.GetComponent<ProjectileController>();
        projectileController.Init(direction, rangeWeaponHandler);
    }

}