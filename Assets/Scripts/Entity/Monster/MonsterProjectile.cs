using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterProjectile : MonoBehaviour
{
    public float MyPower { get; set; }
    public int MyIndex { get; set; }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().Damage(MyPower);
        }
    }
    private void FixedUpdate()
    {
        
    }
}
