using PublicDefinitions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedMonster : BaseMonster
{
    private int shootNum;
    private void Awake()
    {
        monsterAnimator = GetComponentInChildren<Animator>();
        GetComponentInChildren<CircleCollider2D>().radius = 4f;
    }
    protected override void Update()
    {
        base.Update();

    }
    public override void Attack()
    {
        base.Attack();

        if (AttackCoolDown > 0f) return;

        for(int i = 0; i < 5; i++)
        {
            Invoke("Shooting", AttackSpeed * i);
        }
        AttackCoolDown = AttackTime;
    }
    void Shooting()
    {
        Debug.Log("Ranged Attack");
        
        try
        {
            GameObject go = Instantiate(Resources.Load<GameObject>($"Prefab/MonsterProjectile"), transform.position, transform.rotation);
            Vector3 dir = (Target.transform.position - this.transform.position);
            dir.Normalize();
            go.GetComponent<MonsterProjectile>().MyPower = AttackPower;
            go.GetComponent<Rigidbody2D>().velocity = dir * 5;
        }
        catch(System.Exception ex)
        {
            Debug.LogException(ex);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 플레이어 감지됨
            PlayerDetectStart(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 플레이어 감지 해제
            PlayerDetectEnd();
        }
    }
}
