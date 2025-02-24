using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : Character
{
    private Animator animator;
    private Rigidbody2D rb;

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

        // base.Attack();
    }

    public void UseSkill()
    {

    }
    public override void Damage(float damage)
    {
        Health -= damage; 
        
    }
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        // 임의로 값 설정 했습니다.
        MaxHealth = 100f;
        Health = 100f;
        AttackPower = 10f;
        MoveSpeed = 3f;
    }

    void Update()
    {
        Move();
    }
}
