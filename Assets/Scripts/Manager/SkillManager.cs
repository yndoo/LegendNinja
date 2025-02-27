using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    private SkillList skillList;
    public Player player;


    private void Start()
    {
        skillList = LoadSkills();
    }

    private SkillList LoadSkills()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("Skills");
        return JsonUtility.FromJson<SkillList>(jsonFile.text);
    }

    public SkillList GetSkillList()
    {
        // 만약 skillList가 아직 로드되지 않았다면, LoadSkills() 호출
        if (skillList == null)
        {
            skillList = LoadSkills();
        }
        return skillList;
    }
    public void ApplySkill(int skillId)
    {
        SkillData skill = skillList.skills.FirstOrDefault(s => s.id == skillId);
        if (skill == null) return;

        switch (skill.type)
        {
            case "power":
                foreach (WeaponHandler weaponHandler in player.weaponList)
                {
                    weaponHandler.Damage += skill.value;
                    Debug.Log($"power{weaponHandler.Damage}");
                }
                break;
            case "delay":
                foreach (WeaponHandler weaponHandler in player.weaponList)
                {
                    weaponHandler.Delay += skill.value;
                    Debug.Log($"power{weaponHandler.Delay}");
                }
                break;
            case "speed":
                foreach (WeaponHandler weaponHandler in player.weaponList)
                {
                    weaponHandler.AttackSpeed += skill.value;
                    Debug.Log($"AttackSpeed{weaponHandler.AttackSpeed}");
                }
                break;
            case "addProjectilesPerShot":
                player.weaponList[0].NumberOfProjectilesPerShot += skill.value;
                player.weaponList[0].MultipleProjectilesAngle += 10;
                break;
            case "Fire":
            case "Ice":
            case "Thunder":
            case "Plant":
            case "Rock":
               RangeWeaponHandler existingWeapon = player.weaponList
                .FirstOrDefault(w => w.WeaponType == skill.type);

                if (existingWeapon != null)
                {
                    // 무기 강화
                    existingWeapon.Damage += skill.value;

                    skill.damage += skill.value;
                    Debug.Log($"{skill.name} 강화됨! (새로운 Damage: {existingWeapon.Damage}, SkillData Damage: {skill.damage})");
                }
                else
                {
                    // 새로운 무기 추가
                    AddweaponList(skill);
                }
                break;
        }
    }

    public void AddweaponList(SkillData skill)
    {
        Debug.Log($"{skill.name} 무기 추가 시도 (ID: {skill.id})");


        RangeWeaponHandler rangeWeaponHandler = new GameObject(skill.name).AddComponent<RangeWeaponHandler>();
        rangeWeaponHandler.transform.SetParent(player.PlayerPivot.transform);
        rangeWeaponHandler.Init();
        rangeWeaponHandler.SetData(player.PlayerPivot.transform, skill.damage, skill.speed, skill.cooldown,
            skill.bulletIndex, skill.bulletSize,
            skill.duration, skill.spread, skill.numberofProjectilesPerShot, skill.multipleProjectilesAngel,
            Color.white, ProjectileManager.Instance, skill.type);
        player.weaponList.Add(rangeWeaponHandler);

        player.AttackCooldwonDivide();

        // 코루틴 딜레이 메소드 적용
        // 플레이어에서 코루틴 가져오기
        if (player.rb.velocity.magnitude <= 0)
        {
            rangeWeaponHandler.StartAttackCor();
        }
        Debug.Log($" {skill.name} 무기 추가됨! (데미지: {skill.damage}, 속도: {skill.speed}, 쿨타임: {skill.cooldown})");
    }
   
    //public void AttackWithWeapons(Vector3 direction, ref int index, List<RangeWeaponHandler> rangeWeaponHandlers)
    //{
    //    rangeWeaponHandlers[index].Attack();
    //    index++;

    //    if(index >= rangeWeaponHandlers.Count)
    //    {
    //        index = 0;
    //    }
    //}


}
