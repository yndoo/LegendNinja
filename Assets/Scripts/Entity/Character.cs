using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    #region 캐릭터 능력치
    public float Health { get; set; }
    public float MaxHealth { get; set; }
    public float AttackPower { get; set; }
    public float Defense { get; set; }
    public float MoveSpeed { get; set; }
    public float AttackRange {  get; set; }
    public float AttackSpeed {  get; set; }
    public float AttackTime { get; set; }
    #endregion

    protected Weapon EquippedWeapon;
    public virtual void EquipWeapon(Weapon weapon)
    {

    }
    public virtual void Attack()
    {

    }
    public abstract void Damage(float damage);

}
