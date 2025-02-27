using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class SkillSelectionUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Button[] skillButtons;
    [SerializeField] private TextMeshProUGUI[] skillTitle;
    [SerializeField] private TextMeshProUGUI[] skillDescriptions;
    [SerializeField] private Image[] skillImages;

    private SkillManager skillManager;
    private SkillList skillList;

    private void Start()
    {
        skillManager = FindObjectOfType<SkillManager>();
        skillList = skillManager.GetSkillList();
        SetupSkillButtons();
    }

    public void SetupSkillButtons()
    {
        // 스킬 리스트에서 랜덤하게 3개 선택
        List<SkillData> availableSkills = new List<SkillData>(skillList.skills);
        List<SkillData> randomSkills = new List<SkillData>();

        // 스킬이 3개 이상이면 랜덤 선택, 아니면 그대로 사용
        int skillCount = Mathf.Min(3, availableSkills.Count);
        for(int i = 0; i < skillCount; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, availableSkills.Count);
            randomSkills.Add(availableSkills[randomIndex]);
            availableSkills.RemoveAt(randomIndex); // 중복방지
        }

        // 버튼 실행 설정
        for (int i = 0; i < skillButtons.Length; i++)
        {
            if (i < randomSkills.Count)
            {
                SkillData skill = randomSkills[i]; // skill 변수 선언

                skillButtons[i].gameObject.SetActive(true);

                if (skillTitle.Length > i)
                {
                    skillTitle[i].gameObject.SetActive(true);
                    skillTitle[i].text = skill.name;
                }

                if (skillDescriptions.Length > i) // 스킬 설명 설정
                {
                    skillDescriptions[i].gameObject.SetActive(true);
                    skillDescriptions[i].text = skill.description;
                }

                // 스프라이트 변경
                if (skillImages.Length > i)
                {
                    Sprite skillSprite = Resources.Load<Sprite>(skill.sprite); // 올바른 변수 사용
                    if (skillSprite != null)
                    {
                        skillImages[i].sprite = skillSprite;
                    }
                    else
                    {
                        Debug.LogError($"Skill sprite not found: {skill.sprite}");
                    }
                }

                // 버튼 클릭 이벤트 설정
                skillButtons[i].onClick.RemoveAllListeners();
                skillButtons[i].onClick.AddListener(() => SelectSkill(skill));
            }
            else
            {
                skillButtons[i].gameObject.SetActive(false);
                if (skillDescriptions.Length > i)
                {
                    skillDescriptions[i].gameObject.SetActive(false);
                }
            }

            //  사용한 스킬일 경우 배제

        }
    }

    private void removeSkills()
    {
        // 사용한 스킬을 사용하면

        //OneSkills()
    }

    public void SelectSkill(SkillData skillData)
    {
        Debug.Log($"[{skillData.name}] 스킬 선택됨! (ID: {skillData.id})");

        skillManager.ApplySkill(skillData.id);

        CloseSkillPanel();  // 스킬 선택 후 패널 닫기
    }

    private void CloseSkillPanel()
    {
        panel.SetActive(false);
    }

    public void OpenPanel()
    {
        panel.SetActive(true);
        SetupSkillButtons();
    }
    // 태그 타입이 속성일 경우
    private string OneSkills(SkillData skill)
    {
        return skill.type = "Fire";
    }
}
