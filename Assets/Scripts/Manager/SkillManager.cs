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
                foreach(WeaponHandler weaponHandler in player.weaponList)
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
                player.weaponList[0].NumberofProjectilesPerShot += skill.value;
                player.weaponList[0].MultipleProjectilesAngel += 10;
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
        if(string.IsNullOrEmpty(skill.weaponPrefab))
        {
            Debug.LogError($"무기 프리팹이 설정되지 않음 : {skill.name}");
            return;
        }
        GameObject weaponPrefab = Resources.Load<GameObject>(skill.weaponPrefab);
        if (weaponPrefab == null)
        {
            Debug.LogError($"무기 프리팹을 찾을 수 없음: {skill.weaponPrefab}");
            return;
        }
        GameObject newWeapon = Instantiate(weaponPrefab, player.PlayerPivot.transform);
        RangeWeaponHandler newWeaponHandler = newWeapon.GetComponent<RangeWeaponHandler>();
        //player.weaponList.Add(new RangeWeaponHandler(player.PlayerPivot.transform, bulletIndex, 1, 5, 0, 1, 10, weaponColor, ProjectileManager.Instance));
        
        if(newWeaponHandler != null)
        {
            player.weaponList.Add(newWeaponHandler);

            // 스킬 데이터 반영하기
            newWeaponHandler.Damage = skill.damage;
            newWeaponHandler.AttackSpeed = skill.speed;
            newWeaponHandler.Delay = skill.cooldown;
            //newWeaponHandler.Type = skill.type;

            Debug.Log($"{skill.name} 무기 추가됨! (데미지: {skill.damage}, 속도: {skill.speed}, 쿨타임: {skill.cooldown})");
        }
        else Debug.LogError("RangeWeaponHandler가 프리팹에 없음!");

    }
    public void AttackWithWeapons(Vector3 direction)
    {
        foreach (WeaponHandler weaponHandler in player.weaponList)
        {
            weaponHandler.Attack(direction);
        }
    }


}
