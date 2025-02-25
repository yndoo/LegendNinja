using System.Collections.Generic;
using UnityEngine;
using static SkillData;

public class StageManager2 : MonoBehaviour
{
	private SkillManager skillManager;
	private WeaponHandler playerWeapon;

	private void Start()
	{
		skillManager = SkillManager.Instance;
		playerWeapon = FindObjectOfType<WeaponHandler>(); // 플레이어 무기 찾기
	}

	public void OnStageClear()
	{
		List<Skill> availableSkills = skillManager.GetSkills();
		if (availableSkills.Count > 0)
		{
			int randomIndex = Random.Range(0, availableSkills.Count);
			Skill selectedSkill = availableSkills[randomIndex];

			skillManager.ApplySkill(selectedSkill, playerWeapon);

			Debug.Log($"스테이지 클리어! {selectedSkill.name} 스킬 적용 완료!");
		}
	}
}
