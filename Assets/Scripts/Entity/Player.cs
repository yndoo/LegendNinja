using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : Character
{
    //public GameObject Shuriken;
    public GameObject PlayerPivot;

    private Animator animator;
    private static readonly int IsAttack = Animator.StringToHash("IsAttack");

    private Rigidbody2D rb;

    //[SerializeField] private WeaponHandler weaponHandler;
    public List<RangeWeaponHandler> weaponList;

    private float AttackCoolDown = 0f; //쿨타임
    public float AttackMaxCoolDown = 1f;
    
    void Start()
    {
        weaponList.Add(new RangeWeaponHandler(PlayerPivot.transform, 0, 1, 5, 0, 1, 10, Color.white, ProjectileManager.Instance));

        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        // 임의로 값 설정 했습니다.
        MaxHealth = 100f;
        Health = 100f;
        AttackPower = 10f;
        MoveSpeed = 3f;

        base.AttackSpeed = 1f;
    }

    void Update()
    {
        Move();
        if (rb.velocity.magnitude > 0)
        {
            AttackCoolDown = 0.5f;
            return;
        }

        // 공격 쿨타임 처리
        if (AttackCoolDown <= 0f)
        {
            Attack();
            AttackCoolDown = AttackMaxCoolDown; // 1초 쿨타임
        }
        else
        {
            AttackCoolDown -= Time.deltaTime;
        }
    }
    public void Move()
    {
        
        // WASD
        float MoveX = Input.GetAxisRaw("Horizontal");
        float MoveY = Input.GetAxisRaw("Vertical");

        Vector2 MoveDirection = new Vector2(MoveX, MoveY).normalized;

        // 애니메이션 처리
        if (MoveDirection.magnitude > 0)  // 이동 중일 때
        {
            animator.SetBool("IsMove", true);
        }
        else
        {
            animator.SetBool("IsMove", false);
        }
        animator.SetFloat("MoveX", MoveDirection.x);
        animator.SetFloat("MoveY", MoveDirection.y);

        rb.velocity = MoveDirection * MoveSpeed;
    }

    public override void Attack()
    {
        
        Transform target = FindCloseMonster();  // 가장 가까운 적 찾기
        if (target != null)
        {
            animator.SetTrigger(IsAttack);
            //// 표창을 발사할 위치에서 발사
            //GameObject shuriken = Instantiate(Shuriken, PlayerPivot.transform.position, Quaternion.identity);
            //Rigidbody2D shurikenRb = shuriken.GetComponent<Rigidbody2D>();

            // 적의 방향 계산
            Vector3 direction = (target.position - PlayerPivot.transform.position).normalized;

            //// 표창에 방향과 힘을 적용
            //shurikenRb.velocity = direction * AttackPower;
            foreach(WeaponHandler weaponHandler in weaponList)
            {
                weaponHandler.Attack(direction); // 리스트 인덱스로 받아오기
            }
        }
        else
        {
            Debug.Log("적이 없음!");
        }
    }

    public void UseSkill()
    {

    }
    public override void Damage(float damage)
    {
        Health -= damage; 
        
    }
    Transform FindCloseMonster()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster"); // "Monster" 태그 가진 오브젝트 찾기
        Transform ClosestEnemy = null; //가장 가까운 적을 저장할 변수

        float MaxDistance = 50f; // 화면 상에서의 최대거리 설정
        float ClosestDistance = MaxDistance; // 최대거리 설정한 값으로 초기화
        
        Vector2 PlayerPos = transform.position; // 플레이어 위치

        foreach (GameObject monster in monsters)
        {
            float Distance = Vector2.Distance(PlayerPos, monster.transform.position); // 플레이어와 적의 거리 계산
            if (Distance < ClosestDistance) //지금까지 저장된 가장 가까운 거리보다 작으면
            {
                ClosestDistance = Distance; // 새로운 가장 가까운 거리로 저장 
                ClosestEnemy = monster.transform; // 해당 적의 transform 저장
            }
        }

        return ClosestEnemy; // 가장 가까운 적 반환 (없으면 null)
    }
}
