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
    [SerializeField] private GameObject panel; // 스킬 선택 UI 패널
    [SerializeField] private Button[] skillButtons; // 스킬 선택 버튼 배열
    [SerializeField] private TextMeshProUGUI[] skillTitle; // 스킬 제목 UI
    [SerializeField] private TextMeshProUGUI[] skillDescriptions; // 스킬 설명 UI
    [SerializeField] private Image[] skillImages; // 스킬 아이콘 이미지

    private SkillManager skillManager; // 스킬 관리 클래스
    private SkillList skillList; // 전체 스킬 리스트

    private void Start()
    {
        skillManager = FindObjectOfType<SkillManager>(); // SkillManager 찾기
        skillList = skillManager.GetSkillList(); // SkillManager에서 스킬 리스트 가져오기
        SetupSkillButtons(); // 스킬 버튼 설정
    }

    /// <summary>
    /// 스킬 선택 UI를 설정하고 3개의 랜덤한 스킬을 표시
    /// </summary>
    public void SetupSkillButtons()
    {
        Time.timeScale = 0f; // 게임 일시정지 (스킬 선택 시 진행 멈춤)

        // 전체 스킬 리스트 복사하여 사용
        List<SkillData> availableSkills = new List<SkillData>(skillList.skills);
        List<SkillData> randomSkills = new List<SkillData>();

        // 최소 3개의 스킬을 랜덤으로 선택 (스킬이 부족하면 가능한 만큼만 선택)
        int skillCount = Mathf.Min(3, availableSkills.Count);
        for (int i = 0; i < skillCount; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, availableSkills.Count);
            randomSkills.Add(availableSkills[randomIndex]);
            availableSkills.RemoveAt(randomIndex); // 중복 방지를 위해 리스트에서 제거
        }

        // 스킬 버튼 UI 업데이트
        for (int i = 0; i < skillButtons.Length; i++)
        {
            if (i < randomSkills.Count) // 선택된 스킬이 존재하는 경우
            {
                SkillData skill = randomSkills[i];

                skillButtons[i].gameObject.SetActive(true); // 버튼 활성화

                if (skillTitle.Length > i) // 스킬 제목 설정
                {
                    skillTitle[i].gameObject.SetActive(true);
                    skillTitle[i].text = skill.name;
                }

                if (skillDescriptions.Length > i) // 스킬 설명 설정
                {
                    skillDescriptions[i].gameObject.SetActive(true);
                    skillDescriptions[i].text = skill.description;
                }

                // 스킬 아이콘 설정
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

                // 버튼 클릭 이벤트 설정
                skillButtons[i].onClick.RemoveAllListeners();
                skillButtons[i].onClick.AddListener(() => SelectSkill(skill));
            }
            else // 선택된 스킬이 부족하면 버튼 숨기기
            {
                skillButtons[i].gameObject.SetActive(false);
                if (skillDescriptions.Length > i)
                {
                    skillDescriptions[i].gameObject.SetActive(false);
                }
            }
        }
    }

    /// <summary>
    /// 선택한 스킬을 적용하고 패널을 닫음
    /// </summary>
    /// <param name="skillData">선택한 스킬 데이터</param>
    public void SelectSkill(SkillData skillData)
    {
        Debug.Log($"[{skillData.name}] 스킬 선택됨! (ID: {skillData.id})");

        skillManager.ApplySkill(skillData.id); // 선택한 스킬 적용

        CloseSkillPanel(); // 스킬 선택 창 닫기
    }

    /// <summary>
    /// 스킬 선택 패널 닫기 및 게임 재개
    /// </summary>
    private void CloseSkillPanel()
    {
        panel.SetActive(false);
        Time.timeScale = 1f; // 게임 진행 재개
    }

    /// <summary>
    /// 스킬 선택 패널을 열고 스킬 버튼을 초기화
    /// </summary>
    public void OpenPanel()
    {
        panel.SetActive(true);
        SetupSkillButtons(); // 새로운 스킬 선택 설정
    }
}
