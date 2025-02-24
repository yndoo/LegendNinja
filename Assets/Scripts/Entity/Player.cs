using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private Rigidbody2D rb;

    public void Move()
    {
        // WASD
        float MoveX = Input.GetAxisRaw("Horizontal");
        float MoveY = Input.GetAxisRaw("Vertical");

        Vector2 MoveDirection = new Vector2(MoveX, MoveY).normalized;

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
        rb = GetComponent<Rigidbody2D>();
        // 임의로 값 설정 했습니다.
        MaxHealth = 100f;
        Health = 100f;
        AttackPower = 10f;
        MoveSpeed = 5f;
    }

    void Update()
    {
        Move();
    }
}
