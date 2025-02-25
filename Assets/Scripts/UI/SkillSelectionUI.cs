using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillSelectionUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Button[] skillButtons;
    [SerializeField] private TextMeshProUGUI skillDescription;

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
        for (int i = 0; i < skillButtons.Length; i++)
        {
            int index = i;
            if (i < skillList.skills.Length)
            {
                skillButtons[i].gameObject.SetActive(true);
                skillButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = skillList.skills[i].name;
                skillButtons[i].onClick.AddListener(() => SelectSkill(skillList.skills[index]));
            }
            else
            {
                skillButtons[i].gameObject.SetActive(false);
            }
        }
    }

    public void SelectSkill(SkillData skillData)
    {
        skillDescription.text = $"{skillData.name}\n{skillData.type} +{skillData.value}";
        skillManager.ApplySkill(skillData.id);

        Debug.Log($"[{skillData.name}] 스킬 선택됨!");

        CloseSkillPanel();  // 스킬 선택 후 패널 닫기
    }

    private void CloseSkillPanel()
    {
        panel.SetActive(false);
    }

    public void OpenPanel()
    {
        panel.SetActive(true);
    }
}
