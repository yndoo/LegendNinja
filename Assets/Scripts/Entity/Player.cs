﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
public class Player : Character
{
    public GameObject PlayerPivot;
    private Animator animator;
    //private static readonly int IsAttack = Animator.StringToHash("IsAttack");

    private Rigidbody2D rb;
    protected Vector2 MoveDirection;

    public List<RangeWeaponHandler> weaponList;
    private SkillManager skillManager;  // 스킬 따로 관리하기
    //[SerializeField] private WeaponHandler weaponHandler; 삭제 가능

    private float AttackCoolDown = 0f; //쿨타임
    public float AttackMaxCoolDown = 1f; // 플레이어 딜레이 줄이기 위해 추가

    protected SpriteRenderer playerRenderer;
    private Color originalColor;

    private void Awake()
    {
        playerRenderer = GetComponentInChildren<SpriteRenderer>();
        originalColor = playerRenderer.color;
    }
    void Start()
    {
        weaponList.Add(new RangeWeaponHandler(PlayerPivot.transform, 0, 1, 5, 0, 1, 10, Color.white, ProjectileManager.Instance));
        skillManager = FindAnyObjectByType<SkillManager>();

        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        // 임의로 값 설정 했습니다.
        MaxHealth = 100f;
        Health = 100f;
        AttackPower = 10f;
        MoveSpeed = 4f;
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
    private IEnumerator DisableAttackLayer()
    {
        yield return new WaitForSeconds(0.5f); // 공격 애니메이션 길이에 맞게 조정
        animator.SetLayerWeight(2, 0); // Attack 레이어 비활성화
    }
    public void Move()
    {
        // WASD
        float MoveX = Input.GetAxisRaw("Horizontal");
        float MoveY = Input.GetAxisRaw("Vertical");
        Vector2 MoveDirection = new Vector2(MoveX, MoveY).normalized;
        if (MoveDirection.x != 0 || MoveDirection.y != 0)
        {
            animator.SetLayerWeight(1, 1);
        }
        else
        {
            animator.SetLayerWeight(1, 0);
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
            animator.SetLayerWeight(2, 1);
            // 적의 방향 계산
            Vector3 direction = (target.position - PlayerPivot.transform.position).normalized;

            skillManager.AttackWithWeapons(direction); // 추가 기능 : 스킬 가져오기

            //weaponHandler.Attack(direction);  // 삭제 가능
            StartCoroutine(DisableAttackLayer());
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
        playerRenderer.color = playerRenderer.color - new Color(0, 0.3f, 0.3f, 0f);
        Invoke("ResetColor", 0.3f);
        if (Health <= 0)
        {
            playerRenderer.color = playerRenderer.color - new Color(1f, 1f, 1f, 0.4f);
            Destroy(this.gameObject, 0.3f);
        }
    }
    void ResetColor()
    {
        playerRenderer.color = originalColor;
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