using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SkillData

{
    public int id;
    public string name;
    public string type;
    public float value;
    public string description;
    public string sprite;

    // 무기 속성 추가
    public float damage;    // 주는 공격력
    public float speed;     // 날아가는 속도
    public float cooldown;  // 쿨타임
    public string weaponPrefab;
}

[Serializable]
public class SkillList
{
    public SkillData[] skills;
}
