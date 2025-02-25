using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static SkillData;

public class SkillSelectionUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Button[] skillButtons;
    [SerializeField] private TextMeshProUGUI skillDescription;

    private SkillManager skillManager;
    private WeaponHandler playerWeapon;
    private List<Skill> availableSkills; // SkillData → Skill 로 변경

    private void Start()
    {
        skillManager = SkillManager.Instance;
        if (skillManager == null)
        {
            Debug.LogError(" SkillManager.Instance가 NULL입니다! SkillManager가 씬에 있는지 확인하세요.");
        }

        playerWeapon = FindObjectOfType<WeaponHandler>();

        if (playerWeapon == null)
        {
            Debug.LogError(" WeaponHandler가 씬에서 발견되지 않았습니다!");
        }

        panel.SetActive(false);
    }

    public void ShowSkillSelection()
    {
        Debug.Log(" ShowSkillSelection() 호출됨!");

        if (skillManager == null)
        {
            Debug.LogError(" skillManager가 할당되지 않았습니다!");
            return;
        }

        availableSkills = skillManager.GetSkills();

        if (availableSkills == null || availableSkills.Count == 0)
        {
            Debug.LogError(" 스킬 데이터가 비어 있습니다! JSON 파일을 확인하세요.");
            return;
        }

        panel.SetActive(true);
        for (int i = 0; i < skillButtons.Length; i++)
        {
            if (skillButtons[i] == null)
            {
                Debug.LogError($" skillButtons[{i}]이(가) null입니다! Unity에서 버튼을 할당했는지 확인하세요.");
                continue;
            }

            if (i < availableSkills.Count)
            {
                Skill skill = availableSkills[i];

                TMP_Text buttonText = skillButtons[i].GetComponentInChildren<TMP_Text>(); // Text -> TMP_Text
                if (buttonText == null)
                {
                    Debug.LogError($" skillButtons[{i}]의 TMP_Text 컴포넌트가 존재하지 않습니다!");
                    continue;
                }

                buttonText.text = skill.name;
            }
        }

    }


    public void SelectSkill(Skill skill)
    {
        Debug.Log($" SelectSkill() 호출됨! 선택된 스킬: {skill.name} ({skill.type} +{skill.value})");

        skillManager.ApplySkill(skill, playerWeapon);
        skillDescription.text = $"선택한 스킬: {skill.name} ({skill.type} +{skill.value})";
        //  현재 공격력 확인용 디버그 로그 추가
        Debug.Log($"[Skill Applied] {skill.name} 적용됨! 현재 공격력: {playerWeapon.Power}");
        panel.SetActive(false);
    }
}
