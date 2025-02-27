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
    public int bulletIndex; // 탄 인덱스
    public float bulletSize; // 탄 사이즈
    public float duration;  // 유지시간
    public float spread;    // 각도 랜덤화
    public float numberofProjectilesPerShot;    // 탄 개수
    public float multipleProjectilesAngel;  //탄 각도

    public string weaponPrefab;
}

[Serializable]
public class SkillList
{
    public SkillData[] skills;
}
