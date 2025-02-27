using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SocialPlatforms;
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
        Time.timeScale = 0f;    // ���� �ð� ���߱�

        // ��ų ����Ʈ���� �����ϰ� 3�� ����
        List<SkillData> availableSkills = new List<SkillData>(skillList.skills);

        List<SkillData> randomSkills = new List<SkillData>();

        // ��ų�� 3�� �̻��̸� ���� ����, �ƴϸ� �״�� ���
        int skillCount = Mathf.Min(3, availableSkills.Count);
        for(int i = 0; i < skillCount; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, availableSkills.Count);
            randomSkills.Add(availableSkills[randomIndex]);
            availableSkills.RemoveAt(randomIndex); // �ߺ�����
        }

        // ��ư ���� ����
        for (int i = 0; i < skillButtons.Length; i++)
        {
            if (i < randomSkills.Count)
            {
                SkillData skill = randomSkills[i]; // skill ���� ����

                skillButtons[i].gameObject.SetActive(true);

                if (skillTitle.Length > i)
                {
                    skillTitle[i].gameObject.SetActive(true);
                    skillTitle[i].text = skill.name;
                }

                if (skillDescriptions.Length > i) // ��ų ���� ����
                {
                    skillDescriptions[i].gameObject.SetActive(true);
                    skillDescriptions[i].text = skill.description;
                }

                // ��������Ʈ ����
                if (skillImages.Length > i)
                {
                    Sprite skillSprite = Resources.Load<Sprite>(skill.sprite);
                    if (skillSprite != null)
                    {
                        skillImages[i].sprite = skillSprite;
                    }
                    else
                    {
                        Debug.LogError($"Skill sprite not found: {skill.sprite}");
                    }
                }

                // ��ư Ŭ�� �̺�Ʈ ����
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
        }
    }

    public void SelectSkill(SkillData skillData)
    {
        Debug.Log($"[{skillData.name}] ��ų ���õ�! (ID: {skillData.id})");

        skillManager.ApplySkill(skillData.id);


        CloseSkillPanel();  // ��ų ���� �� �г� �ݱ�
    }

    private void CloseSkillPanel()
    {
        panel.SetActive(false);
        Time.timeScale = 1f;    // ���� �ð� �ٽ� �帣��
    }

    public void OpenPanel()
    {
        panel.SetActive(true);
        SetupSkillButtons();
        //Time.timeScale = 0f;    // ���� �ð� ���߱�
    }
}
