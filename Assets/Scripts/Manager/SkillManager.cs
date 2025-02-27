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
                player.AttackMaxCoolDown += skill.value;
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
                AddweaponList(skill);
                break;
        }

        Debug.Log($"{skill.name} 적용됨! 현재 Power: {player.weaponList[0].Damage}, Speed: {player.weaponList[0].AttackSpeed}");
    }

    public void AddweaponList(SkillData skill)
    {
        Debug.Log($"[] {skill.name} 무기 추가 시도 (ID: {skill.id})");

        player.weaponList.Add(new RangeWeaponHandler(player.PlayerPivot.transform, skill.damage, skill.speed, skill.cooldown,
            skill.bulletIndex, skill.bulletSize,
            skill.duration, skill.spread, skill.numberofProjectilesPerShot, skill.multipleProjectilesAngel, 
            Color.white, ProjectileManager.Instance));
        player.AttackCooldwonDivide();

        // 코루틴 딜레이 메소드 적용
        // 플레이어에서 코루틴 가져오기

        Debug.Log($" {skill.name} 무기 추가됨! (데미지: {skill.damage}, 속도: {skill.speed}, 쿨타임: {skill.cooldown})");
    }
   
    public void AttackWithWeapons(Vector3 direction, ref int index, List<RangeWeaponHandler> rangeWeaponHandlers)
    {
        rangeWeaponHandlers[index].Attack(direction);
        index++;

        if(index >= rangeWeaponHandlers.Count)
        {
            index = 0;
        }
    }


}
