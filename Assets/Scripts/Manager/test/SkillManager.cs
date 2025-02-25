using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SkillData;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance { get; private set; }

    private List<Skill> skills;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        LoadSkills();
    }

    private void LoadSkills()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("skills");
        if (jsonFile != null)
        {
            SkillList skillList = JsonUtility.FromJson<SkillList>(jsonFile.text);
            skills = skillList.skills;
            Debug.Log("스킬 데이터 로드 완료!");
        }
        else
        {
            Debug.LogError("스킬 Json 파일을 찾을 수 없습니다.");
        }
    }

    public List<Skill> GetSkills()
    {
        return skills;
    }

    public void ApplySkill(Skill skill, WeaponHandler weapon)
    {
        Debug.Log($" ApplySkill() 호출됨! {skill.name} 적용 중...");
        if (weapon == null)
        {
            Debug.LogError(" WeaponHandler가 없습니다!");
            return;
        }
        switch (skill.type)
        {
            case "Attack":
                weapon.Power += skill.value;
                Debug.Log($"공격력{weapon.Power}");
                break;
            case "Speed":
                weapon.Speed += skill.value;
                Debug.Log($"공격 속도 증가! 현재 속도: {weapon.Speed}");
                break;
            case "Delay":
                weapon.Delay -= skill.value;
                Debug.Log($"공격 딜레이 감소! 현재 딜레이: {weapon.Delay}");
                break;
        }
        Debug.Log($"스킬 적용: {skill.name} ({skill.type} +{skill.value})");
    }
}
