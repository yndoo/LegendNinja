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
                    weaponHandler.Power += skill.value;
                    Debug.Log($"power{weaponHandler.Power}");
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
                AddweaponList();
                break;
            case "Ice":
                AddweaponList();
                break;
            case "Thunder":
                AddweaponList();
                break;
            case "Plant":
                AddweaponList();
                break;
            case "Rock":
                AddweaponList();
                break;
        }

        Debug.Log($"{skill.name} 적용됨! 현재 Power: {player.weaponList[0].Power}, Speed: {player.weaponList[0].AttackSpeed}");
    }
    
    public void AddweaponList()
    {
        player.weaponList.Add(new RangeWeaponHandler(player.PlayerPivot.transform, 1, 1, 5, 0, 1, 10, Color.white, ProjectileManager.Instance));
    }
    public void AttackWithWeapons(Vector3 direction)
    {
        foreach (WeaponHandler weaponHandler in player.weaponList)
        {
            weaponHandler.Attack(direction);
        }
    }

}
